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

        private int m_population_size;
        private int m_num_of_generation;
        private double m_mutate_percent;
        private GeneratePolicyBase m_generate_method_policy;
        private Solution[] m_solutions_array;

        public AlgorithmManager()
        {
            m_generate_method_policy = new GenerateRandomPopulation();
        }

        public bool run(int population_size, int num_of_generation, double mutate_percent)
        {
            // maybe dont need
            m_num_of_generation = num_of_generation;
            m_population_size = population_size;
            m_mutate_percent = mutate_percent;

            LoggerFactory.getSimpleLogger().info("AlgorithmManager::run() activated");
            if (ApplicManager.Instance.CurrentProblem == null) {
                LoggerFactory.getSimpleLogger().error("AlgorithmManager::run() problem is not loaded to the system");
               throw (new NullReferenceException("Problem is not loaded to the system"));
            }

            // connect to lindo
            // need to implement!!!!!!

            // creating first population
            m_solutions_array = new Solution[population_size];
            for (int i = 0; i < population_size; i++)
            {
                m_solutions_array[i] = new Solution();
                m_generate_method_policy.GenerateSolution(m_solutions_array[i]);
            }

            return true;
        }

    }
}
