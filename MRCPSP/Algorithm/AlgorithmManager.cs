using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;
using MRCPSP.CommonTypes;
using MRCPSP.Logger;
namespace MRCPSP.Algorithm
{
    class AlgorithmManager
    {  
        private GeneratePolicyBase m_generate_method_policy;
        private FitnessFunctionBase m_fitness_function;

        private Solution[] m_solutions_array;

        public AlgorithmManager()
        {
            m_generate_method_policy = new GenerateRandomPopulation();
            m_fitness_function = new MinimumMakeSpanPolicy();
        }

        public bool run(int population_size, int num_of_generation, double mutate_percent)
        {
            // maybe dont need
          
            LoggerFactory.getSimpleLogger().info("AlgorithmManager::run() activated");
            if (ApplicManager.Instance.CurrentProblem == null) {
                LoggerFactory.getSimpleLogger().error("AlgorithmManager::run() problem is not loaded to the system");
               throw (new NullReferenceException("Problem is not loaded to the system"));
            }

            // creating first population
            m_solutions_array = new Solution[population_size];
            for (int i = 0; i < population_size; i++)
            {
                m_solutions_array[i] = new Solution();
                m_generate_method_policy.GenerateSolution(m_solutions_array[i]);
            }

            performingGrowingLoop(population_size, num_of_generation, mutate_percent);

            return true;
        }

        private void performingGrowingLoop(int population_size, int num_of_generation, double mutate_percent)
        {
            for (int i = 0; i < num_of_generation; i++)
            {
                for (int j = 0; j < population_size; j++)
                {
                    m_fitness_function.evalFitness(m_solutions_array[j], ApplicManager.Instance.CurrentProblem);
                }
            }
        }
    }
}
