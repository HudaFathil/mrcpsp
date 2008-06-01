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

        public ResultSummary()
        {      
            m_best_solutions_in_generation = new List<Solution>();
        }

        public List<Solution> BestSolutions
        {
            get { return m_best_solutions_in_generation; }
            set
            {
                m_best_solutions_in_generation = value;
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
    }
}
