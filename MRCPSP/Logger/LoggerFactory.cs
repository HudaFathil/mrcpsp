using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Logger
{
    class LoggerFactory
    {

        public static Logger getSimpleLogger()
        {
            Logger logger = Logger.Instance;
            if (logger.LogFile == null)
                logger.LogFile = "C:/Users/Ofir/Desktop/Project/MRCPSP2/logs/TestLogger.log";
            return logger;
        }
    }
}
