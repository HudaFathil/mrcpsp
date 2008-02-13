using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MRCPSP.Logger
{
    class Logger : ILogger
    {

        private static ILogger instance;
        private String logFileName;
        private static LOGGER_STATE state; 

        static Logger()
        {
            instance = new Logger();
            state = LOGGER_STATE.DEBUG;
        }


        public static ILogger Instance
        {
            get { return instance; }
        }

        public override string LogFile 
        {
            get { return logFileName; }
            set { logFileName = value; }
        }

        private void writeToLog(String message)
        {
            StreamWriter outLog = new StreamWriter(this.logFileName, true);
            DateTime time = DateTime.Now;
            if (outLog == null)
                Console.WriteLine("{0:G} " + message, time);
            else
                outLog.WriteLine("{0:G} " + message, time);
            outLog.Flush();
            outLog.Close();
        }

        public override void debug(String message)
        {
            if (state <= LOGGER_STATE.DEBUG)
                writeToLog("DEBUG: " + message);
        }

        public override void info(String message)
        {
            if (state <= LOGGER_STATE.INFO)
                writeToLog("INFO: " + message);
        }

        public override void warn(String message)
        {
            if (state <= LOGGER_STATE.WARN)
                writeToLog("WARNING: " + message);
        }

        public override void error(String message)
        {
            if (state <= LOGGER_STATE.ERROR)
                writeToLog("ERROR: " + message);
        }

    }
}
