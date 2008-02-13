using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MRCPSP.Logger
{
        public abstract class ILogger
        {
            public  enum LOGGER_STATE
            {
                DEBUG = 0,
                INFO,
                WARN,
                ERROR
            };
       
            public abstract void debug(String message);
            public abstract void info(String message);
            public abstract void warn(String message);
            public abstract void error(String message);
            public abstract string LogFile { get; set; }
            public abstract LOGGER_STATE State { get; set; }
    }
}
