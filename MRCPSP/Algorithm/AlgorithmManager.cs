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

        private List<Solution> m_solutions;

        private ResultSummary m_result_summary;

        public AlgorithmManager()
        {
            m_generate_method_policy = new GenerateRandomPopulation();
            m_fitness_function = new MinimumMakeSpanPolicy();
            m_crossOverFunction = new OnePointCrossOver();
            m_selection_policy = new RankSelectionPolicy();
        }

        public bool run(int population_size, int num_of_generation, double mutate_percent)
        {
            LindoParameter.init();
            createNewResultSummary(population_size, num_of_generation, mutate_percent);

            LoggerFactory.getSimpleLogger().info("AlgorithmManager::run() activated");
            if (ApplicManager.Instance.CurrentProblem == null) {
                LoggerFactory.getSimpleLogger().error("AlgorithmManager::run() problem is not loaded to the system");
               throw (new NullReferenceException("Problem is not loaded to the system"));
            }

            // creating first population
            m_solutions = new List<Solution>();
            for (int i = 0; i < population_size; i++)
            {
                Solution sol = new Solution();
                m_generate_method_policy.GenerateSolution(sol);
                m_solutions.Add(sol);
            }

            performingGrowingLoop(population_size, num_of_generation, mutate_percent);
            
            return true;
        }

        private void performingGrowingLoop(int population_size, int num_of_generation, double mutate_percent)
        {
            String forDebugging = "Fitness summery:\n";
            for (int i = 0; i < num_of_generation; i++)
            {
                forDebugging += "Generation Number "+i+ " ";
                for (int j = 0; j < population_size; j++)
                {
                    m_fitness_function.evalFitness(m_solutions[j], ApplicManager.Instance.CurrentProblem);
                    forDebugging += m_solutions[j].resultFromLindo+" , ";
                }

                List<Solution> childSolutions = m_crossOverFunction.doCrossOver(m_solutions);
                
                

                for (int j = 0; j < population_size; j++)
                {
                    m_fitness_function.evalFitness(childSolutions[j], ApplicManager.Instance.CurrentProblem);
                    
                }

                foreach (Solution s in childSolutions)
                {
                    forDebugging += s.resultFromLindo + " , ";
                }

                forDebugging += "\n";
                m_solutions =  m_selection_policy.keepOnlySuitedSolutions(m_solutions, childSolutions, population_size);
                m_result_summary.BestSolutions.Add(m_solutions[0]);
            }
            Logger.LoggerFactory.getSimpleLogger().debug(forDebugging);

            ApplicManager.Instance.SavedResults.Add(m_result_summary);
        }

        private void createNewResultSummary(int pop_size, int num_of_gen, double mutate_percent)
        {
            m_result_summary = new ResultSummary();

            m_result_summary.Title = ApplicManager.Instance.CurrentProblem.Title;
            m_result_summary.CrossoverType = m_crossOverFunction.ToString();
            m_result_summary.SelectionType = m_selection_policy.ToString();
            m_result_summary.GeneratePopulationType = m_generate_method_policy.ToString();
        }
    }
}
