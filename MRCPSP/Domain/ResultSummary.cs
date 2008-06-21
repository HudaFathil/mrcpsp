using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Algorithm;


using MRCPSP.CommonTypes;

namespace MRCPSP.Domain
{
    class ResultSummary
    {
        private List<Solution> m_best_solutions_in_generation;
        private String m_problem_title;
        private String m_selection_type;
        private String m_crossover_type;
        private String m_generate_population_type;
        private String m_size_of_population;
        private String m_num_of_generation;
        private String m_start_time;
        private String m_finish_time;
        private String m_mutation_percent;
        private int m_number_of_iteration;
        private List<KeyValuePair<double, double>> m_min_max_per_generation;

        public ResultSummary()
        {      
            m_best_solutions_in_generation = new List<Solution>();
            m_min_max_per_generation = new List<KeyValuePair<double, double>>();
        }

        public List<Solution> BestSolutions
        {
            get { return m_best_solutions_in_generation; }
            set
            {
                m_best_solutions_in_generation = value;
            }
        }

        public List<KeyValuePair<double,double>> MinMaxPerGeneration
        {
            get { return m_min_max_per_generation; }
            set
            {
                m_min_max_per_generation = value;
            }
        }

        public Solution getBestSolution()
        {
            Solution best = m_best_solutions_in_generation[0];
            foreach (Solution s in m_best_solutions_in_generation) 
            {
                if (s.scoreFromLindo < best.scoreFromLindo && s.scoreFromLindo != 0 || best.scoreFromLindo == 0) 
                {
                    best = s;
                }
            }
            return best;
        }

        public String Title
        {
            get { return m_problem_title; }
            set
            {
                m_problem_title = value;
            }
        }

        public String SelectionType
        {
            get { return m_selection_type; }
            set
            {
                m_selection_type = value;
            }
        }

        public String CrossoverType
        {
            get { return m_crossover_type; }
            set
            {
                m_crossover_type = value;
            }
        }

        public String GeneratePopulationType
        {
            get { return m_generate_population_type; }
            set
            {
                m_generate_population_type = value;
            }
        }
       
        public String SizeOfPopulation
        {
            get { return m_size_of_population; }
            set
            {
                m_size_of_population = value;
            }
        }

        public String NumOfGeneration
        {
            get { return m_num_of_generation; }
            set
            {
                m_num_of_generation = value;
            }
        }
        public String StartTime          
        {
            get { return m_start_time; }
            set
            {
                m_start_time = value;
            }
        }

        public String FinishTime
        {
            get { return m_finish_time; }
            set
            {
                m_finish_time = value;
            }
        }

        public String MutationPercent
        {
            get { return m_mutation_percent; }
            set
            {
                m_mutation_percent = value;
            }
        }

        public int NumberOfIterations
        {
            get { return m_number_of_iteration; }
            set
            {
                m_number_of_iteration = value;
            }
        }

        public double BestResult
        {
            get { return getBestSolution().scoreFromLindo; }
        }
    }
}
