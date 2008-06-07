using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading;

using MRCPSP.Algorithm;
using MRCPSP.Algorithm.FirstGeneration;
using MRCPSP.Algorithm.CrossOver;
using MRCPSP.Algorithm.SelectionPolicy;
using MRCPSP.Domain;
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
            List<ResultSummary> results_for_problem = new List<ResultSummary>();
            string start_time = DateTime.Now.ToString();
            for (int i = 0; i < m_num_of_loops; i++)
            {             
                if (m_background_worker.CancellationPending)
                {
                    e.Cancel = true;
                    return -1;
                }               
                results_for_problem.Add(m_algorithm_manager.run(population_size, num_of_generation, mutation_percentage));
                double percent =  (i+1) * 100.0 / (double)m_num_of_loops;
                m_background_worker.ReportProgress( (int)percent);
            }
            ResultSummary best_result = getBestResult(results_for_problem);
            ApplicManager.Instance.SavedResults.Add(best_result);
            best_result.StartTime = start_time;
            string finish_time = DateTime.Now.ToString();
            best_result.FinishTime = finish_time;
            best_result.NumberOfIterations = m_num_of_loops;
            LoggerFactory.getSimpleLogger().info("ProblemSolverManager::run() finished");
            return 1;
        }

        private ResultSummary getBestResult(List<ResultSummary> results)
        {
            double val = double.MaxValue;
            int best_idx =0;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].getBestSolution().scoreFromLindo != 0 && results[i].getBestSolution().scoreFromLindo < val)
                {
                    val = results[i].getBestSolution().scoreFromLindo;
                    best_idx = i;
                }
            }
            return results[best_idx];
        }

        public void loadParams(int loops, GeneratePolicyBase gen_policy, CorssOverBase crossover, SelectionPolicyBase selection)
        {
            m_num_of_loops = loops;
            m_algorithm_manager.CurrentGeneratePolicy = gen_policy;
            m_algorithm_manager.CurrentCrossOverPolicy = crossover;
            m_algorithm_manager.CurrentSelectionPolicy = selection;
        }

        public void signBackgroundWorker(BackgroundWorker backgroundWorker1)
        {
            m_background_worker = backgroundWorker1;
            m_background_worker.DoWork += new DoWorkEventHandler(m_background_worker_DoWork);
        }
    
        void m_background_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int[] alg_params = (int[])e.Argument;          
            e.Result = run(alg_params[0], alg_params[1], alg_params[2],e);   
        }

    }
}
