using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
namespace MRCPSP.Controllers
{
    class ApplicManager
    {
        private static ApplicManager instance = null;
        Problem m_current_problem;

        private ApplicManager()
        {
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
            Logger.Logger.Instance.info("ApplicManager::lodProblemFromDataBase, title: " + title);
        }
    }
}
