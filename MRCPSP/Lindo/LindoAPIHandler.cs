using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MRCPSP.Lindo;
using System.Runtime.InteropServices;
using MRCPSP.Lindo.Constrains;

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
        public double getResults ()  {

            LindoContainer.Instance.init();
            IConstrain.createAllConstrains();
			List<int> anRoxList = new List<int>();
			List<double> adAList = new List<double>();
			List<int> pnLenColList = new List<int>();
			List<int> anBegCol_1 = new List<int>();
            String varType = "";
			foreach (MrcpspVariable mrcpsp in LindoContainer.Instance.Variables.Values)
			{
                varType += mrcpsp.Type;
                mrcpsp.TransferListsToVectors(ref adAList, ref anRoxList, ref pnLenColList);
			}			

			// Creating the anBegCol Vector
			IEnumerator myEnumeratorPn = pnLenColList.GetEnumerator();
			int accumulator = 0;//(int)myEnumeratorAnRoxList.Current;
			anBegCol_1.Add(accumulator);
			
			for (int i=0;i<(pnLenColList.Count-1);i++)
			{
				myEnumeratorPn.MoveNext();
				accumulator=accumulator + (int)myEnumeratorPn.Current;
			
				anBegCol_1.Add(accumulator);
			}
			anBegCol_1.Add(adAList.Count);


            int nErrorCode = lindo.LSERR_NO_ERROR;

            /* Number of constraints */
            int nCons = LindoContainer.Instance.ConstrainsCounter;

            /* Number of variables */
            int nVars = LindoContainer.Instance.Variables.Count;

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

            double[] adC = new double[LindoContainer.Instance.Variables.Count]; //new double[] { 20.0, 30.0 , 10.0};
            for (int i = 0; i < LindoContainer.Instance.Variables.Count - 1; i++)
                adC[i] = 1.0;
            adC[LindoContainer.Instance.Variables.Count - 1] = 1.0;
            /* The right-hand sides of the constraints */
            double[] adB = LindoContainer.Instance.RightHandSideValues.ToArray<double>();//new double[] { 120.0, 60.0, 50.0 ,10.0};

            /* The constraint types */
            String constrainSensesString = "";
            foreach (String sense in LindoContainer.Instance.ConstraintsSenses) 
            {
                constrainSensesString +=sense;
            }

            string acConTypes = constrainSensesString; //"LLLL";


            /* The number of nonzeros in the constraint matrix */
            int nNZ = adAList.Count;

            /* The indices of the first nonzero in each column */
            int[] anBegCol = anBegCol_1.ToArray<int>();// = solution.getColumnStart();//new int[] { 0, 2, 4 , nNZ };

            /* The length of each column.  Since we aren't leaving
            any blanks in our matrix, we can set this to NULL */
            int[] pnLenCol = pnLenColList.ToArray<int>();// new int[] { 2, 2 };

            /* The nonzero coefficients */
            double[] adA = adAList.ToArray<double>();//new double[] { 1.0, 1.0, 2.0, 1.0 , 1.0};

            /* The row indices of the nonzero coefficients */
            int[] anRowX = anRoxList.ToArray<int>();//new int[] { 0, 1, 0, 2 , 3};

            /* Simple upper and lower bounds on the variables.
            By default, all variables have a lower bound of zero
            and an upper bound of infinity.  Therefore pass NULL
            pointers in order to use these default values. */
            double[] pdLower = new double[LindoContainer.Instance.Variables.Count] ; // { 0.0, 0.0 ,0.0};
            double[] pdUpper = new double[LindoContainer.Instance.Variables.Count]; // { lindo.LS_INFINITY, lindo.LS_INFINITY , lindo.LS_INFINITY};
        //    Console.WriteLine("Params number = " + solution.getParamsNumber());
            string[] varnames = LindoContainer.Instance.Variables.Keys.ToArray<String>();//{ "V1" , "V2" , "V3"};
            string[] connames = new string[LindoContainer.Instance.ConstrainsCounter]; //{ "C1", "C2", "C3" , "C4"};
            for (int c = 0; c < LindoContainer.Instance.ConstrainsCounter; c++)
                connames[c] = "C" + c;
            int counter = 0;
            foreach (MrcpspVariable mrcpsp in LindoContainer.Instance.Variables.Values)
            {
                pdLower[counter] = 0.0;
                if (mrcpsp.Type.Equals("B"))
                    pdUpper[counter] = 1;
                else
                    pdUpper[counter] = lindo.LS_INFINITY;
                counter++;
            }
           

                /* We have now assembled a full description of the model.
                We pass this information to LSloadLPData with the
                following call. */
                nErrorCode = lindo.LSloadLPData(pModel, nCons, nVars, nDir,
                    dObjConst, adC, adB, acConTypes, nNZ, anBegCol,
                    pnLenCol, adA, anRowX, pdLower, pdUpper);
            APIErrorCheck(pEnv, nErrorCode);
            // Mark all Variables as being Binary Integer	
            //Console.WriteLine("LindoVarType.ToString()={0}\t\nVarType={1}",LindoVarType,VarType);
         //   nErrorCode = lindo.LSloadVarType(pModel, varType);
         //   APIErrorCheck(pEnv, nErrorCode);
            /*
            nErrorCode = lindo.LSloadNameData(pModel, "MyTitle", "MyObj", null, null,
            null, connames, varnames, null);
            APIErrorCheck(pEnv, nErrorCode);
            */
            /* >>> Step 4 <<< Perform the optimization */
            nErrorCode = lindo.LSoptimize(pModel, lindo.LS_METHOD_PSIMPLEX, ref nSolStatus);
            APIErrorCheck(pEnv, nErrorCode);

                /* >>> Step 5 <<< Retrieve the solution */
                double[] adX = new double[nVars];
                double[] adY = new double[nCons];
                double[] dSlack = new double[nVars];
                /* Get the variable values */
                nErrorCode = lindo.LSgetPrimalSolution(pModel, adX);
                APIErrorCheck(pEnv, nErrorCode);

                for (int v = 0; v < nVars; v++ )
                {
                    LindoContainer.Instance.Variables[varnames[v]].FinalValue = adX[v];
                }
                
                Console.WriteLine("results: {0}", adX[nVars -1]);


            /* >>> Step 6 <<< Delete the LINDO environment */
            nErrorCode = lindo.LSdeleteModel(ref pModel);

            nErrorCode = lindo.LSdeleteEnv(ref pEnv);
            return adX[nVars -1];
        }
       }
}
