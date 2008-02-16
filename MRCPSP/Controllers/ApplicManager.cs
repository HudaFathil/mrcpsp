using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
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

        public void loadProblem(System.Collections.ArrayList steps_name_list,
                                         System.Collections.ArrayList constraints_list,
                                         System.Collections.ArrayList operation_list)
        {
        }


        public  void loadProblemFromDataBase(string title)
        {
            LoggerFactory.getSimpleLogger().info("ApplicManager::lodProblemFromDataBase, title: " + title);
        }

        public void sendStopRequest()
        {
            m_problem_solver_manager.stopRequested = true;
        }
    }
}
