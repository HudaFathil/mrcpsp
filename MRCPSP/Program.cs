
using System;
using System.Collections.Generic;
using System.Text;
using MRCPSP.Gui;
using MRCPSP.Logger;
using MRCPSP.Database;
using System.Windows.Forms;

namespace MRCPSP
{
 


    class Program
    {
        [STAThread]


    
        
        static void Main(string[] args)
        {

            ILogger log = LoggerFactory.getSimpleLogger();
            //SchemaCreator.createSchema();
            ApplicationFrame skel = new ApplicationFrame();
            Application.Run(skel);
        }
     
    }
 
}
