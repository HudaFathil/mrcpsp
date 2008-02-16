using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MRCPSP.Controllers
{
    class ProblemSolverManager
    {
        private bool m_is_stop_requested;
        private int m_timeout;
        TimerCallback m_tc;

        public ProblemSolverManager()
        {
            m_is_stop_requested = false;
            m_tc = new TimerCallback(onTimeoutOver);
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
            m_is_stop_requested = true;
        }

        public void run()
        {
            Timer t = new Timer(m_tc, null, 0, m_timeout);
            // do the run
            t.Dispose();
        }
    }
}
