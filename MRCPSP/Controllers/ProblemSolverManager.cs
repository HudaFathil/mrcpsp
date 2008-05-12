using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading;
using MRCPSP.Algorithm;
using MRCPSP.Logger;

namespace MRCPSP.Controllers
{
    class ProblemSolverManager
    { 
        private int m_num_of_loops;  
        private AlgorithmManager m_algorithm_manager;
        private BackgroundWorker m_background_worker;


        public ProblemSolverManager()
        {
            m_algorithm_manager = new AlgorithmManager();
            m_num_of_loops = 1;
        }

        public long run(int population_size, int num_of_generation, double mutation_percentage, DoWorkEventArgs e)
        {
            LoggerFactory.getSimpleLogger().info("ProblemSolverManager::run() activated");
            m_background_worker.ReportProgress(0);
            for (int i = 0; i < m_num_of_loops; i++)
            {
                
                if (m_background_worker.CancellationPending)
                {
                    e.Cancel = true;
                    return -1;
                }
                m_algorithm_manager.run(population_size, num_of_generation, mutation_percentage);
                double percent =  (i+1) * 100.0 / (double)m_num_of_loops;
                m_background_worker.ReportProgress( (int)percent);
            }
  
            LoggerFactory.getSimpleLogger().info("ProblemSolverManager::run() finished");
            return 1;
        }

        public void loadAdvancedParams(int loops)
        {
            m_num_of_loops = loops;
        }

        public void signBackgroundWorker(BackgroundWorker backgroundWorker1)
        {
            m_background_worker = backgroundWorker1;
            m_background_worker.DoWork += new DoWorkEventHandler(m_background_worker_DoWork);
    //        m_background_worker.ProgressChanged += new ProgressChangedEventHandler(m_background_worker_ProgressChanged);
        }
        /*
        void m_background_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            m_background_worker.ReportProgress((int)sender);
        }
        */
        void m_background_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int[] alg_params = (int[])e.Argument;
           
            e.Result = run(alg_params[0], alg_params[1], alg_params[2],e);   
        } 
    }
}
