﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using MRCPSP.Log;
using MRCPSP.Algorithm;
using MRCPSP.Algorithm.FirstGeneration;
using MRCPSP.Algorithm.CrossOver;
using MRCPSP.Algorithm.SelectionPolicy;
using MRCPSP.Database.MsSqlServer;

namespace MRCPSP.Controllers
{
    class ApplicManager
    {
        private static ApplicManager instance = null;
        private Problem m_current_problem;
        private ProblemSolverManager m_problem_solver_manager;
        private List<ResultSummary> m_saved_results;
        private Solution m_currentSolution;

        private ApplicManager()
        {
            m_problem_solver_manager = new ProblemSolverManager();
            m_saved_results = new List<ResultSummary>();
        }

        public static ApplicManager Instance
        {
            get {
                if (instance == null)
                    instance = new ApplicManager();
                return instance; 
            }
        }

        public void loadProblem(List<Resource> resource_list,
                        Dictionary< Step, List < Mode > > modes_in_step,
                        List<Step> step_list,       
                        List<Constraint> all_constraints,
                        List<Product> products_list,
                        Dictionary<Product, List<Job>> jobs_in_product,
                        Dictionary<Product, List<Step>> steps_in_product,
                        List<ResourceConstraint> resource_time_constraints,
                        List<SetupTime> setup_time,
                        String title)                                                         
        {
            m_current_problem = new Problem(resource_list, modes_in_step, step_list, all_constraints, products_list, jobs_in_product,steps_in_product, resource_time_constraints,setup_time, title);
           
        }

        public void saveProblemToDB()
        {
            ProblemSaver.saveProblem(m_current_problem);
        }

        public void loadProblemFromDataBase(string title)
        {
            int problemID = DBHandler.Instance.queryProblemForProblemID(title);
            m_current_problem = ProblemLoader.queryProblem(problemID);
            Logger.Instance.info("ApplicManager::lodProblemFromDataBase, title: " + title);
        }

        /*
        public void run(int pop_size, int num_of_gen, double mutation_percentage)
        {
            m_problem_solver_manager.run(pop_size, num_of_gen, mutation_percentage);
        }
        */

        public List<String> getProblemListFromDB()
        {
            return DBHandler.Instance.getProblemNameList();
        }

        public void saveSolution(ResultSummary summary)
        {
           SolutionSaver.saveSolution(summary);
        }


        /*
        public void run(int pop_size, int num_of_gen, double mutation_percentage)
        {
            m_problem_solver_manager.run(pop_size, num_of_gen, mutation_percentage);
        }
        */

        public Problem CurrentProblem
        {
            get { return m_current_problem;  }
            set { m_current_problem = value; }
        }

        public Solution CurrentSolution
        {
            get { return m_currentSolution; }
            set { m_currentSolution = value; }
        }

        public List<ResultSummary> SavedResults
        {
            get { return m_saved_results; }
            set { m_saved_results = value; }
        }

        public void loadParams(int loops,GeneratePolicyBase gpb, CorssOverBase cob, SelectionPolicyBase spb )
        {
            m_problem_solver_manager.loadParams(loops, gpb, cob, spb);
        }

        public void signBackgroundWorker(System.ComponentModel.BackgroundWorker backgroundWorker1)
        {
            m_problem_solver_manager.signBackgroundWorker(backgroundWorker1);
        }

        public bool SolvingFromList
        {
            get { return m_problem_solver_manager.SolvingFromList; }
            set { m_problem_solver_manager.SolvingFromList = value; }
        }

        public void loadProblemNames(List<String> problem_names)
        {
            m_problem_solver_manager.loadProblemNames(problem_names);
        }

         public void loadSolution(int solutionID)
        {
            SolutionLoader.loadSolution(solutionID);
        }
    }
}
