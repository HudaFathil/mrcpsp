using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MRCPSP.Lindo;
using System.Runtime.InteropServices;

namespace MRCPSP.Lindo
{
    class LindoAPIHandler
    {
        
        private void APIErrorCheck(IntPtr pEnv, int nErr)
            {
                if (nErr > 0)
                {
                    StringBuilder cMessage = new StringBuilder(lindo.LS_MAX_ERROR_MESSAGE_LENGTH);
                    lindo.LSgetErrorMessage(pEnv, nErr, cMessage);
                }
            }

        
        /**
         * returns solution minimux max spam caculation
         */ 
        public double getResults (LindoSolution solution)  {

            int nErrorCode = lindo.LSERR_NO_ERROR;

            /* Number of constraints */
            int nCons = solution.getConstrainsValue().Length;

            /* Number of variables */
            int nVars = solution.getParamsNumber();

            /* declare an instance of the LINDO environment object */
            IntPtr pEnv = (IntPtr)0;

            /* declare an instance of the LINDO model object */
            IntPtr pModel = (IntPtr)0;


            int nSolStatus = lindo.LS_STATUS_UNKNOWN;

            StringBuilder LicenseKey = new StringBuilder(lindo.LS_MAX_ERROR_MESSAGE_LENGTH);

            /* >>> Step 1 <<< Create a LINDO environment. Note:
            MY_LICENSE_KEY must be defined to be the license key
            shipped with your software. */

            nErrorCode = lindo.LSloadLicenseString("C:\\Lindoapi\\license\\lndapi50.lic", LicenseKey);
            APIErrorCheck(pEnv, nErrorCode);


            pEnv = lindo.LScreateEnv(ref nErrorCode, LicenseKey.ToString());
            if (nErrorCode == lindo.LSERR_NO_VALID_LICENSE)
            {
                Console.WriteLine("Invalid License Key!\n");
               
            }
            APIErrorCheck(pEnv, nErrorCode);

            /* >>> Step 2 <<< Create a model in the environment. */
            pModel = lindo.LScreateModel(pEnv, ref nErrorCode);
            APIErrorCheck(pEnv, nErrorCode);

            /* >>> Step 3 <<< Specify the model.

            /* The direction of optimization */
            int nDir = lindo.LS_MIN;

            /* The objective's constant term */
            double dObjConst = 0.0;

            /* The coefficients of the objective function */
            double[] adC = new double[solution.getParamsNumber()]; //new double[] { 20.0, 30.0 , 10.0};

            /* The right-hand sides of the constraints */
            double[] adB = solution.getConstrainsValue(); //new double[] { 120.0, 60.0, 50.0 ,10.0};

            /* The constraint types */
            string acConTypes = solution.getConstrains(); //"LLLL";


            /* The number of nonzeros in the constraint matrix */
            int nNZ = solution.getValue().Length;

            /* The indices of the first nonzero in each column */
            int[] anBegCol = solution.getColumnStart();//new int[] { 0, 2, 4 , nNZ };

            /* The length of each column.  Since we aren't leaving
            any blanks in our matrix, we can set this to NULL */
            int[] pnLenCol = null;// new int[] { 2, 2 };

            /* The nonzero coefficients */
            double[] adA = solution.getValue(); //new double[] { 1.0, 1.0, 2.0, 1.0 , 1.0};

            /* The row indices of the nonzero coefficients */
            int[] anRowX = solution.getRowIndex();//new int[] { 0, 1, 0, 2 , 3};

            /* Simple upper and lower bounds on the variables.
            By default, all variables have a lower bound of zero
            and an upper bound of infinity.  Therefore pass NULL
            pointers in order to use these default values. */
            double[] pdLower = new double[solution.getParamsNumber()] ; // { 0.0, 0.0 ,0.0};
            double[] pdUpper = new double[solution.getParamsNumber()] ; // { lindo.LS_INFINITY, lindo.LS_INFINITY , lindo.LS_INFINITY};
            Console.WriteLine("Params number = " + solution.getParamsNumber());
            string[] varnames = new string[solution.getParamsNumber()] ; //{ "V1" , "V2" , "V3"};
           

            for (int i = 0 ; i < solution.getParamsNumber() ; i++) 
            {
                pdLower[i] = 0.0;
                pdUpper[i] = lindo.LS_INFINITY;
                varnames[i] = solution.getVarName(i);
                adC[i] = 1.0;
            }
            string[] connames = new string[solution.getConstrainsValue().Length]; //{ "C1", "C2", "C3" , "C4"};

            for (int i = 0; i < solution.getConstrainsValue().Length; i++)
            {
                connames[i] = "C" + i;
            }

            Console.Write("Cons = [");
            for (int i = 0; i < connames.Length; i++)
            {
                Console.Write(connames[i]);
            }
            Console.WriteLine("]");

            Console.Write("Vars = [");
            for (int i = 0; i < varnames.Length; i++)
            {
                Console.Write(varnames[i]);
            }
            Console.WriteLine("]");

                /* We have now assembled a full description of the model.
                We pass this information to LSloadLPData with the
                following call. */
                nErrorCode = lindo.LSloadLPData(pModel, nCons, nVars, nDir,
                    dObjConst, adC, adB, acConTypes, nNZ, anBegCol,
                    pnLenCol, adA, anRowX, pdLower, pdUpper);
            APIErrorCheck(pEnv, nErrorCode);


            nErrorCode = lindo.LSloadNameData(pModel, "MyTitle", "MyObj", null, null,
            null, connames, varnames, null);
            APIErrorCheck(pEnv, nErrorCode);

            /* >>> Step 4 <<< Perform the optimization */
            nErrorCode = lindo.LSoptimize(pModel, lindo.LS_METHOD_PSIMPLEX, ref nSolStatus);
            APIErrorCheck(pEnv, nErrorCode);

                /* >>> Step 5 <<< Retrieve the solution */
                double[] adX = new double[nVars];

                /* Get the variable values */
                nErrorCode = lindo.LSgetPrimalSolution(pModel, adX);
                APIErrorCheck(pEnv, nErrorCode);
                double maxSpan = 0;
                Hashtable varsValues = new Hashtable();
                for (int i = 0; i < nVars; i++)
                {
                    varsValues.Add(varnames[i], adX[i]);
                    Console.WriteLine(varnames[i] + " = " + adX[i]);
                    if (maxSpan < adX[i])
                        maxSpan = adX[i];
                }
                solution.VariablesResultValues = varsValues;
                Console.WriteLine("results: {0}", maxSpan);


            /* >>> Step 6 <<< Delete the LINDO environment */
            nErrorCode = lindo.LSdeleteModel(ref pModel);

            nErrorCode = lindo.LSdeleteEnv(ref pEnv);
                return maxSpan;
        }
       }
}
