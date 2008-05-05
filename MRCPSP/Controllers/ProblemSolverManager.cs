﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MRCPSP.Algorithm;
using MRCPSP.Logger;

namespace MRCPSP.Controllers
{
    class ProblemSolverManager
    {
        private bool m_is_stop_requested;
        private int m_timeout;
        TimerCallback m_tc;
        private AlgorithmManager m_algorithm_manager;

        public ProblemSolverManager()
        {
            m_is_stop_requested = false;
            m_tc = new TimerCallback(onTimeoutOver);
            m_algorithm_manager = new AlgorithmManager();
        }
       
        public bool stopRequested {
            get
            {
                return m_is_stop_requested;
            }
            set
            {
                m_is_stop_requested = value;
            }
        }

        public int timeout
        {
            get
            {
                return m_timeout;
            }
            set
            {
                m_timeout = value;
            }
        }

        public  void onTimeoutOver(Object state)
        {
            LoggerFactory.getSimpleLogger().info("ProblemSolverManager::exiting on timeout");
            m_is_stop_requested = true;
        }

        public void run(int population_size, int num_of_generation, double mutation_percentage)
        {
            LoggerFactory.getSimpleLogger().info("ProblemSolverManager::run() activated");
   //       Timer t = new Timer(m_tc, null, 0, m_timeout);
            // do the run

            m_algorithm_manager.run(population_size, num_of_generation, mutation_percentage);

     //       t.Dispose();
            LoggerFactory.getSimpleLogger().info("ProblemSolverManager::run() finished");
        }
    }
}
