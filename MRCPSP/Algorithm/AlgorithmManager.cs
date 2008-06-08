using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Controllers;
using MRCPSP.CommonTypes;
using MRCPSP.Logger;
using MRCPSP.Lindo;
using MRCPSP.Algorithm.Fitness;
using MRCPSP.Algorithm.CrossOver;
using MRCPSP.Algorithm.FirstGeneration;
using MRCPSP.Algorithm.SelectionPolicy;
using MRCPSP.Domain;

namespace MRCPSP.Algorithm
{
    class AlgorithmManager
    {  
        private GeneratePolicyBase m_generate_method_policy;
        private FitnessFunctionBase m_fitness_function;
        private CorssOverBase m_crossOverFunction;
        private SelectionPolicyBase m_selection_policy;

        private Random m_random;

        private List<Solution> m_solutions;

        private ResultSummary m_result_summary;

        public AlgorithmManager()
        {
            m_random = new Random();
            m_generate_method_policy = new GenerateRandomPopulation();
            m_fitness_function = new MinimumMakeSpanPolicy();
            m_crossOverFunction = new OnePointCrossOver();
            m_selection_policy = new RankSelectionPolicy();
        }

        public ResultSummary run(int population_size, int num_of_generation, double mutate_percent)
        {
            createNewResultSummary(population_size, num_of_generation, mutate_percent);

            LoggerFactory.getSimpleLogger().info("AlgorithmManager::run() activated");
            if (ApplicManager.Instance.CurrentProblem == null) {
                LoggerFactory.getSimpleLogger().error("AlgorithmManager::run() problem is not loaded to the system");
               throw (new NullReferenceException("Problem is not loaded to the system"));
            }

            createFirstPopulation(population_size);

            performingGrowingLoop(population_size, num_of_generation, mutate_percent);

            return m_result_summary;
        }

        private void createFirstPopulation(int population_size)
        {           
            m_solutions = new List<Solution>();
            for (int i = 0; i < population_size; i++)
            {
                Solution sol = new Solution();
                m_generate_method_policy.GenerateSolution(sol);
                m_solutions.Add(sol);
            }
            for (int j = 0; j < population_size; j++)
            {
                m_fitness_function.evalFitness(m_solutions[j], ApplicManager.Instance.CurrentProblem);
            }
            //m_result_summary.MinMaxPerGeneration.Add(getMinMaxForGeneration(m_solutions));
        }

        private void performingGrowingLoop(int population_size, int num_of_generation, double mutate_percent)
        {
            String forDebugging = "Fitness summery:\n";
            for (int i = 0; i < num_of_generation; i++)
            {
                forDebugging += "Generation Number "+i+ " ";    
                List<Solution> childSolutions = m_crossOverFunction.doCrossOver(m_solutions);                             
                for (int j = 0; j < population_size; j++)
                {
                    m_fitness_function.evalFitness(childSolutions[j], ApplicManager.Instance.CurrentProblem);
                    forDebugging += childSolutions[j].scoreFromLindo + " , ";
                }
                
                forDebugging += "\n";
                m_solutions =  m_selection_policy.keepOnlySuitedSolutions(m_solutions, childSolutions, population_size);
                m_solutions.Sort(new SolutionComparer<Solution>());
                // mutation
                performMutation(mutate_percent);
                m_result_summary.MinMaxPerGeneration.Add(getMinMaxForGeneration(m_solutions));
                m_result_summary.BestSolutions.Add(m_solutions[0]);
            }
            Logger.LoggerFactory.getSimpleLogger().debug(forDebugging);
        }

        private KeyValuePair<double, double> getMinMaxForGeneration(List<Solution> sol_list)
        {
            double min = double.MaxValue; 
            double max = double.MinValue;
            for (int i = 0; i < sol_list.Count; i++)
            {
                if (sol_list[i].scoreFromLindo < min && sol_list[i].scoreFromLindo != 0)
                    min = sol_list[i].scoreFromLindo;
                if (sol_list[i].scoreFromLindo > max && sol_list[i].scoreFromLindo != 0)
                    max = sol_list[i].scoreFromLindo;
            }
            if (min ==double.MaxValue && max == double.MinValue)
                return new KeyValuePair<double, double>(0, 0);
            else
                return new KeyValuePair<double, double>(min, max);
        }

        private void createNewResultSummary(int pop_size, int num_of_gen, double mutate_percent)
        {
            m_result_summary = new ResultSummary();

            m_result_summary.Title = ApplicManager.Instance.CurrentProblem.Title;
            m_result_summary.CrossoverType = m_crossOverFunction.ToString();
            m_result_summary.SelectionType = m_selection_policy.ToString();
            m_result_summary.GeneratePopulationType = m_generate_method_policy.ToString();
        }

        private void performMutation(double mutate_percent) 
        {
            if (m_random.Next(100) > mutate_percent)
            {
                return;
            }

            if (m_random.Next(100) > 50)
            {
                performMutateInModeList(m_solutions[m_random.Next(m_solutions.Count)]);
            }
            else
            {
                performMutateInMatrix(m_solutions[m_random.Next(m_solutions.Count)]);
            }
        }

        private void performMutateInMatrix(Solution sol) 
        {
            int resource_id = m_random.Next(ApplicManager.Instance.CurrentProblem.getNumberOfResources());
            MatrixCellComparer<MatrixCell> compare =  new MatrixCellComparer<MatrixCell>();
             
            for (int i=0; i < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize() - 1; i++) 
            {
                if (compare.Compare(sol.DistributionMatrix[resource_id,i], sol.DistributionMatrix[resource_id,i+1]) > 0)
                    return;
                else {
                    // do swap
                    MatrixCell temp = sol.DistributionMatrix[resource_id,i];
                    sol.DistributionMatrix[resource_id, i] = sol.DistributionMatrix[resource_id, i + 1];
                    sol.DistributionMatrix[resource_id, i + 1] = temp;
                }
            }
        }


        private void performMutateInModeList(Solution sol)
        {
            int mutate_idx = m_random.Next(ApplicManager.Instance.CurrentProblem.getTotalDistributionSize());
            int modes_count = ApplicManager.Instance.CurrentProblem.getNumberOfModesById(mutate_idx);
            if (modes_count < 2)
                return;
            sol.SelectedModeList[mutate_idx] = ((sol.SelectedModeList[mutate_idx] + 1) % modes_count) + 1;
        }

        public GeneratePolicyBase CurrentGeneratePolicy
        {
            get { return m_generate_method_policy; }
            set { m_generate_method_policy = value; }
        }

        public CorssOverBase CurrentCrossOverPolicy
        {
            get { return m_crossOverFunction; }
            set { m_crossOverFunction = value; }
        }

        public SelectionPolicyBase CurrentSelectionPolicy
        {
            get { return m_selection_policy; }
            set { m_selection_policy = value; }
        }
    }
}
