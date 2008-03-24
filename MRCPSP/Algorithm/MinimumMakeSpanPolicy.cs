using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Logger;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using System.Runtime.InteropServices;


namespace MRCPSP.Algorithm
{
    /*
    [StructLayout(LayoutKind.Sequential)]
    public class CallbackData
    {
        public int count;

        // Constructor:    
        public CallbackData()
        {
            count = 0;
        }
    }
    */
    class MinimumMakeSpanPolicy : FitnessFunctionBase
    {

        public MinimumMakeSpanPolicy () : base()
        {
        }
        /*
        private System.Collections.Hashtable trimMatrix(Solution sol , Problem prob)
        {
            System.Collections.Hashtable new_matrix = new System.Collections.Hashtable();
            for (int i = 0; i < prob.getNumberOfResources(); i++)
            {
                new_matrix[i] = new System.Collections.ArrayList();
                for (int j = 0; j < prob.getTotalDistributionSize(); j++)
                {

                }
            }
            //sol.DistributionMatrix
            return new_matrix;
        }
        */
        public override void evalFitness(Solution solution, MRCPSP.Domain.Problem problem)
        {
            LoggerFactory.getSimpleLogger().info("MinimumMakeSpanPolicy::evalFitness");
         //   connectToLindo();

            LindoHandler l1 = new LindoHandler();
            l1.run();

        }

        private void connectToLindo()
        {
            LoggerFactory.getSimpleLogger().info("MinimumMakeSpanPolicy::connectToLindo() start");
       
            LoggerFactory.getSimpleLogger().info("MinimumMakeSpanPolicy::connectToLindo() done");
        }

        /*
        public static int MyCallback(IntPtr pMod, int nLoc, IntPtr myData)
        {
            int iter = 0;
            double pinf = 0, pobj = 0;

            CallbackData cb = new CallbackData();

            Marshal.PtrToStructure(myData, cb);

            lindo.LSgetCallbackInfo(pMod, 0, lindo.LS_DINFO_PINFEAS, ref pinf);
            lindo.LSgetCallbackInfo(pMod, 0, lindo.LS_DINFO_POBJ, ref pobj);
            lindo.LSgetCallbackInfo(pMod, 0, lindo.LS_IINFO_SIM_ITER, ref iter);
            Console.WriteLine("callback @iter={0}, obj={1}, pinf={2}, mydata={3}", iter, pobj, pinf, cb.count);
            cb.count++;

            Marshal.StructureToPtr(cb, myData, true);

            return 0;
        }

        public static void APIErrorCheck(IntPtr pEnv, int nErr)
        {
            if (nErr > 0)
            {
                StringBuilder cMessage = new StringBuilder(lindo.LS_MAX_ERROR_MESSAGE_LENGTH);
                lindo.LSgetErrorMessage(pEnv, nErr, cMessage);
                LoggerFactory.getSimpleLogger().info("LINDO ERROR CHECK:: "+ cMessage.ToString());
            }
        }
        */
    }
}



