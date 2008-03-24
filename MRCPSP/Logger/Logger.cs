using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MRCPSP.Logger
{
    class Logger : ILogger
    {

        private static Logger m_instance;
        private String m_logFileName;
        private static LOGGER_STATE m_state; 

        private Logger()
        {
            m_instance = null;
            m_state = LOGGER_STATE.DEBUG;
        }

       

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

        public override LOGGER_STATE State 
        { 
            get { return m_state;}
            set { 
                m_state = value;  
            } 
        }

        public override string LogFile 
        {
            get { return m_logFileName; }
            set { m_logFileName = value; }
        }

        private void writeToLog(String message)
        {
            StreamWriter outLog = null;
            if (File.Exists(m_logFileName))
            {
                outLog = new StreamWriter(m_logFileName, true);
            }
            DateTime time = DateTime.Now;
            if (outLog == null)
                Console.WriteLine("{0:G} " + message, time);
            else
            {
                outLog.WriteLine("{0:G} " + message, time);
                outLog.Flush();
                outLog.Close();
            }
        }

        public override void debug(String message)
        {
            if (m_state <= LOGGER_STATE.DEBUG)
                writeToLog("DEBUG: " + message);
        }

        public override void info(String message)
        {
            if (m_state <= LOGGER_STATE.INFO)
                writeToLog("INFO: " + message);
        }

        public override void warn(String message)
        {
            if (m_state <= LOGGER_STATE.WARN)
                writeToLog("WARNING: " + message);
        }

        public override void error(String message)
        {
            if (m_state <= LOGGER_STATE.ERROR)
                writeToLog("ERROR: " + message);
        }

    }
}
