using System;
using System.Collections.Generic;
using System.Text;
using MRCPSP.Gui;
using MRCPSP.Logger;
using System.Windows.Forms;

namespace MRCPSP
{
    class Program
    {

        static void Main(string[] args)
        {
            ILogger log = LoggerFactory.getSimpleLogger("C:/Users/Ofir/Desktop/Project/MRCPSP2/logs/TestLogger.log");
            log.debug("Teating the logger1");
            log.info("Teating the logger2");
            log.warn("Teating the logger3");
            log.error("Teating the logger4");

            ApplicationFrame skel = new ApplicationFrame();
            Application.Run(skel);
        }
    }
}

