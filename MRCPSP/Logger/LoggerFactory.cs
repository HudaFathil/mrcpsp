using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Logger
{
    class LoggerFactory
    {

        public static SimpleLogger getSimpleLogger()
        {
            SimpleLogger logger = SimpleLogger.Instance;
            if (logger.LogFile == null)
                logger.LogFile = "../../../logs/TestLogger.log";
            return logger;
        }
    }
}
