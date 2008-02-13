using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Logger
{
    class LoggerFactory
    {
        public static ILogger getSimpleLogger(String outFile)
        {
            ILogger logger = Logger.Instance;
            logger.LogFile = outFile;
            return logger;
        }
    }
}
