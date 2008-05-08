﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.CommonTypes;

namespace MRCPSP.Domain
{
    class ResultSummary
    {
        private Dictionary<Resource, List<int>> m_resources_gantt_data;
        private List<double> m_best_solutions_in_generation;
        private String m_problem_title;
        private String m_selection_type;
        private String m_crossover_type;
        private String m_generate_population_type;

        public ResultSummary()
        {
            m_resources_gantt_data = new Dictionary<Resource, List<int>>();
            m_best_solutions_in_generation = new List<double>();
        }
        public Dictionary<Resource, List<int>> ResourcesGanttData
        {
            get { return m_resources_gantt_data; }
            set
            {
                m_resources_gantt_data = value;
            }
        }

        public List<double> BestSolutions
        {
            get { return m_best_solutions_in_generation; }
            set
            {
                m_best_solutions_in_generation = value;
            }
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
