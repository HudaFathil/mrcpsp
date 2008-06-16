
using System;
using System.Collections.Generic;
using System.Text;
using MRCPSP.Gui;
using MRCPSP.Logger;
using MRCPSP.Database.MsSqlServer;
using MRCPSP.Domain;
using System.Windows.Forms;

namespace MRCPSP
{
 


    class Program
    {
        [STAThread]


    
        
        static void Main(string[] args)
        {
            
            //DBHandler.Instance.loadProblem(21);
           // Console.WriteLine(ProblemLoader.getProblemList().Count);
            //Problem pr = ProblemLoader.queryProblem(22);
            ILogger log = LoggerFactory.getSimpleLogger();
            //SchemaCreator.createSchema();
            ApplicationFrame skel = new ApplicationFrame();
            Application.Run(skel);
        }
     
    }
 
}
