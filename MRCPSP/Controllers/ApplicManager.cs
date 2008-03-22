using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using MRCPSP.Logger;

namespace MRCPSP.Controllers
{
    class ApplicManager
    {
        private static ApplicManager instance = null;
        Problem m_current_problem;
        ProblemSolverManager m_problem_solver_manager;


        private ApplicManager()
        {
            m_problem_solver_manager = new ProblemSolverManager();
        }

        public static ApplicManager Instance
        {
            get {
                if (instance == null)
                    instance = new ApplicManager();
                return instance; 
            }
        }

        public void loadProblem(Resource[] resource_array,
                                System.Collections.Hashtable modes_in_step,
                                Step[] step_array, 
                                System.Collections.ArrayList all_constraints,
                                Product[] products_array)
        {
            m_current_problem = new Problem(resource_array, modes_in_step, step_array, all_constraints, products_array);
            
        }


        public  void loadProblemFromDataBase(string title)
        {
            LoggerFactory.getSimpleLogger().info("ApplicManager::lodProblemFromDataBase, title: " + title);
        }

        public void sendStopRequest()
        {
            m_problem_solver_manager.stopRequested = true;
        }

        public Problem CurrentProblem
        {
            get { return m_current_problem;  }
            set { m_current_problem = value; }
        }
    }
}
