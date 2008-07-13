using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MRCPSP.Log
{
    class Logger 
    {

        private static Logger m_instance;
        private static String LOG_FILE_NAME  = "../../../mrcpsp.log";
        private StreamWriter m_outLog;
        private static LOGGER_STATE m_state; 

        private Logger()
        {
            m_instance = null;
            m_state = LOGGER_STATE.ERROR;
            m_outLog = new StreamWriter(LOG_FILE_NAME, false);
        }

        public enum LOGGER_STATE
        {
            DEBUG = 0,
            INFO,
            WARN,
            ERROR
        };
       

        public static Logger Instance
        {
            get
            {
                if (null == m_instance)
                {
                    m_instance = new Logger();
                }
                return m_instance;
            }
        }

        public  LOGGER_STATE State 
        { 
            get { return m_state;}
            set { 
                m_state = value;  
            } 
        }

       

        private void writeToLog(String message)
        {
            
            DateTime time = DateTime.Now;
            if (m_outLog == null)
                Console.WriteLine("{0:G} " + message, time);
            else
            {
                m_outLog.WriteLine("{0:G} " + message, time);
            }
        }

        public  void debug(String message)
        {
            if (m_state <= LOGGER_STATE.DEBUG)
                writeToLog("DEBUG: " + message);
        }

        public  void info(String message)
        {
            if (m_state <= LOGGER_STATE.INFO)
                writeToLog("INFO: " + message);
        }

        public  void warn(String message)
        {
            if (m_state <= LOGGER_STATE.WARN)
                writeToLog("WARNING: " + message);
        }

        public  void error(String message)
        {
            if (m_state <= LOGGER_STATE.ERROR)
                writeToLog("ERROR: " + message);
        }

    }
}
