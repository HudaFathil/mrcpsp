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

        public void loadProblem(List<Resource> resource_list,
                        Dictionary< Step, List < Mode > > modes_in_step,
                        List<Step> step_list,       
                        List<Constraint> all_constraints,
                        List<Product> products_list)                                                         
        {
            m_current_problem = new Problem(resource_list, modes_in_step, step_list, all_constraints, products_list);
            
        }


        public  void loadProblemFromDataBase(string title)
        {
            LoggerFactory.getSimpleLogger().info("ApplicManager::lodProblemFromDataBase, title: " + title);
        }

        public void sendStopRequest()
        {
            m_problem_solver_manager.stopRequested = true;
        }
        public void run(int pop_size, int num_of_gen, double mutation_percentage)
        {
            m_problem_solver_manager.run(pop_size, num_of_gen, mutation_percentage);
        }

        public Problem CurrentProblem
        {
            get { return m_current_problem;  }
            set { m_current_problem = value; }
        }
    }
}
