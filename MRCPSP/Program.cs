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
            ILogger log = LoggerFactory.getSimpleLogger();

            ApplicationFrame skel = new ApplicationFrame();
            Application.Run(skel);
        }
    }
}
