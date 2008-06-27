
using System;
using System.Collections.Generic;
using System.Text;
using MRCPSP.Gui;
using MRCPSP.Log;
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
           Logger.Instance.debug("#################### Start run ##########################");
            ApplicationFrame skel = new ApplicationFrame();
            Application.Run(skel);
        }
     
    }
 
}
