using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Lindo
{
    class ResourceSelectedOperationComparer : IComparer  
    {
        public ResourceSelectedOperationComparer() : base() { }
        int IComparer.Compare (Object x , Object y) 
        {
            ResourceSelectedOperation x1 = (ResourceSelectedOperation)x;
            ResourceSelectedOperation y1 = (ResourceSelectedOperation)y;
            if (x1.index == y1.index) 
                return 0;
            if (x1.index < y1.index) 
                return -1;
            return 1;
        }
    }
    
    struct SolutionColTitle
    {
        public Product product;
        public Step step;
        public int jobNum;
    }

    class ResourceSelectedOperation
    {
        public Mode mode;
        public Step step;
        public int jobNum;
        public Resource resource;
        public int index;
    }



    class LindoSolution
    {
        private ArrayList m_values;
        private ArrayList m_colStart;
        private ArrayList m_rowIndex;
        private String m_constrains;
        private ArrayList m_constrainsValues;
        private int m_paramsNum;
        private Problem m_problem;
        private ArrayList m_lindoParams;
        private Hashtable m_finalResults;

        /*

        // For debugging only 
        
        private Problem createProblemForDebugging()
        {
            

            //resources
            MRCPSP.CommonTypes.Resource[] ra = new MRCPSP.CommonTypes.Resource[3];
            ra[0] = new MRCPSP.CommonTypes.Resource("R1");
            ra[1] = new MRCPSP.CommonTypes.Resource("R2");
            ra[2] = new MRCPSP.CommonTypes.Resource("R3");
            // steps
            MRCPSP.CommonTypes.Step[] steps = new MRCPSP.CommonTypes.Step[2];
            steps[0] = new MRCPSP.CommonTypes.Step(1, "S1");
            steps[1] = new MRCPSP.CommonTypes.Step(2, "S2");

            //opreations
            //System.Collections.ArrayList op1;
            MRCPSP.CommonTypes.Operation op1 = new MRCPSP.CommonTypes.Operation(0, 20, ra[0]);
            MRCPSP.CommonTypes.Operation op2a = new MRCPSP.CommonTypes.Operation(0, 20, ra[1]);
            MRCPSP.CommonTypes.Operation op2b = new MRCPSP.CommonTypes.Operation(10, 30, ra[2]);
            MRCPSP.CommonTypes.Operation op3 = new MRCPSP.CommonTypes.Operation(0, 10, ra[1]);
            MRCPSP.CommonTypes.Operation op4a = new MRCPSP.CommonTypes.Operation(0, 15, ra[0]);
            MRCPSP.CommonTypes.Operation op4b = new MRCPSP.CommonTypes.Operation(15, 30, ra[1]);
            MRCPSP.CommonTypes.Operation op4c = new MRCPSP.CommonTypes.Operation(30, 45, ra[2]);

            System.Collections.ArrayList oplist1 = new ArrayList();
            oplist1.Add(op1);
            System.Collections.ArrayList oplist2 = new ArrayList();
            oplist2.Add(op2a);
            oplist2.Add(op2b);
            System.Collections.ArrayList oplist3 = new ArrayList();
            oplist3.Add(op3);
            System.Collections.ArrayList oplist4 = new ArrayList();
            oplist4.Add(op4a);
            oplist4.Add(op4b);
            oplist4.Add(op4c);


            //modes
            Hashtable modesInStep = new Hashtable();
            MRCPSP.CommonTypes.Mode m1s1 = new MRCPSP.CommonTypes.Mode();
            m1s1.name = 1;
            m1s1.operations = oplist1;
            MRCPSP.CommonTypes.Mode m2s1 = new MRCPSP.CommonTypes.Mode();
            m2s1.name = 2;
            m2s1.operations = oplist2;
            MRCPSP.CommonTypes.Mode m1s2 = new MRCPSP.CommonTypes.Mode();
            m1s2.name = 1;
            m1s2.operations = oplist3;
            MRCPSP.CommonTypes.Mode m2s2 = new MRCPSP.CommonTypes.Mode();
            m2s2.name = 2;
            m2s2.operations = oplist4;

            System.Collections.ArrayList modesS1 = new ArrayList();
            modesS1.Add(m1s1);
            modesS1.Add(m2s1);

            System.Collections.ArrayList modesS2 = new ArrayList();
            modesS2.Add(m1s2);
            modesS2.Add(m2s2);

            modesInStep.Add(steps[0], modesS1);
            modesInStep.Add(steps[1], modesS2);

            // Product
            MRCPSP.CommonTypes.Product[] pa = new MRCPSP.CommonTypes.Product[1];
            pa[0] = new MRCPSP.CommonTypes.Product(1, "P1", 2);

            //Constrains
            MRCPSP.CommonTypes.Constraint c = new MRCPSP.CommonTypes.Constraint(pa[0], steps[0], steps[1]);
            System.Collections.ArrayList ca = new ArrayList();
            ca.Add(c);

            Problem pr = new Problem(ra, modesInStep, steps, ca, pa);
            return pr;
        }

        */
        /*

        // For debugging only
        private Solution createSolutionForDebugging()
        {
            Solution s = new Solution(4, 3);

            s.DistributionMatrix[0, 0] = 1;
            s.DistributionMatrix[0, 1] = 2;
            s.DistributionMatrix[0, 2] = 3;
            s.DistributionMatrix[0, 3] = 4;

            s.DistributionMatrix[1, 0] = 3;
            s.DistributionMatrix[1, 1] = 2;
            s.DistributionMatrix[1, 2] = 1;
            s.DistributionMatrix[1, 3] = 4;

            s.DistributionMatrix[2, 0] = 2;
            s.DistributionMatrix[2, 1] = 3;
            s.DistributionMatrix[2, 2] = 1;
            s.DistributionMatrix[2, 3] = 4;

            s.SelectedModeList[0] = 1;
            s.SelectedModeList[1] = 1;
            s.SelectedModeList[2] = 2;
            s.SelectedModeList[3] = 1;
            return s;
        }

        */
        /**
         * Constructor
         */
        
        public LindoSolution(Solution sol, Problem prob)
        {
            m_values = new ArrayList();
            m_colStart = new ArrayList();
            m_rowIndex = new ArrayList();
            m_constrainsValues = new ArrayList();
            m_lindoParams = new ArrayList();
            m_constrains = "";
            m_problem = prob;
            //convertValues(createSolutionForDebugging() , createProblemForDebugging());
            convertValues(sol, prob);
        }

        /**
         *returns true if the resource r needed at one of the Mode operations
         */ 
        private bool resourceExistsInMode(Mode mode , Resource r) 
        {
            foreach (MRCPSP.CommonTypes.Operation op in mode.operations)
            {
                if (r.Id == op.Rseource.Id)
                {
                    return true;
                }
            }
            return false;
        }

        /*
         * This is the main function , converts from regular Solution to LindoSolution
         */  
        private void convertValues(Solution sol, Problem problem)
        {
            int[,] matrix = sol.DistributionMatrix;
            int[] modes = sol.SelectedModeList;

            ArrayList[] resources = new ArrayList[matrix.GetLength(0)];
            SolutionColTitle[] title = new SolutionColTitle[matrix.GetLength(1)];
            int counter = 0;
            
            for (int p = 0; p < problem.Products.Count; p++)
            {
                for (int j = 0; j < problem.Products[p].Size; j++)
                    {
                        for (int s = 0; s < problem.Steps.Count; s++)
                        {
                            title[counter].product = problem.Products[p];
                            title[counter].step = problem.Steps[s];
                            title[counter].jobNum = j;
                            counter++;
                        }
                    }
            }


                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        List<Mode> modesInStep = problem.ModesInStep[title[c].step];
                        MRCPSP.CommonTypes.Mode mode = null;
                        foreach (MRCPSP.CommonTypes.Mode m in modesInStep)
                        {
                            if (m.name == sol.SelectedModeList[c])
                            {
                                mode = m;
                                break;
                            }
                        }
                        if (mode != null)
                        {
                            if (resourceExistsInMode(mode,problem.Resources[r])) 
                            {
                                ResourceSelectedOperation sel = new ResourceSelectedOperation();
                                sel.index = matrix[r, c];
                                sel.mode = mode;
                                sel.step = title[c].step;
                                sel.resource  = problem.Resources[r];

                                if (resources[r] == null)
                                {
                                    resources[r] = new ArrayList();
                                }
                                
                                resources[r].Add(sel);
                            }
                        }

                    }
                }
                setLindoParametersList(resources);
            updateLindoSolutionVariables();
           // printResultsToLog();            
        }

        private void printResultsToLog()
        {
            String toPrint = "\n";
            if (m_values != null)
            {
                toPrint+="m_values = ";
                foreach (double el in m_values)
                {
                    toPrint+=el + ",";
                }
                toPrint+="\n";
            }

            if (m_colStart != null)
            {
                toPrint += "m_colStart = ";
                foreach (int el in m_colStart)
                {
                    toPrint += el + ",";
                }
                toPrint += "\n";
            }

            if (m_constrainsValues != null)
            {
                toPrint += "m_constrainsValues = ";
                foreach (double el in m_constrainsValues)
                {
                    toPrint += el + ",";
                }
                toPrint += "\n";
            }

            if (m_rowIndex != null)
            {
                toPrint += "m_rowIndex = ";
                foreach (int el in m_rowIndex)
                {
                    toPrint += el + ",";
                }
                toPrint += "\n";
            }

            toPrint+="m_constrains = " + m_constrains+"\n";
            Logger.LoggerFactory.getSimpleLogger().debug(toPrint);
        }

        private void updateLindoSolutionVariables()
        {
            
            int rowIndex = 0;
            // adding all solution dependancies 
            foreach (LindoParameter lp1 in m_lindoParams)
            {
                if (lp1.Predecessor == null)
                {
                    lp1.Rows.Add(rowIndex);
                    lp1.Columns.Add(1.0);
                    m_constrains += "G";
                    m_constrainsValues.Add(0.0);
                    rowIndex++;
                    Console.WriteLine(lp1.ToString() + " >= 0");
                    continue;
                }

                foreach (LindoParameter lp2 in m_lindoParams)
                {

                    if (lp2.Equals(lp1.Predecessor))
                    {



                        if (lp1.type == LINDO_PARAMETER_TYPE.FINISH)
                        {
                            addColAndRow(lp1, lp2, rowIndex);
                            m_constrainsValues.Add(lp1.getProcessTime());
                            m_constrains += "E";
                            rowIndex++;
                            Console.WriteLine(lp1.ToString() + " - " + lp2.ToString() + " = " + lp2.getProcessTime());
                        }
                        else
                        {
                            if (lp2.type == LINDO_PARAMETER_TYPE.START)
                            {
                                addColAndRow(lp1, lp2, rowIndex);
                                m_constrainsValues.Add(lp1.getOperationStartTime());
                                m_constrains += "G";
                                rowIndex++;
                                Console.WriteLine(lp1.ToString() + " - " + lp2.ToString() + " >= " + lp2.getOperationStartTime() + ".0");
                            }
                            if (lp2.type == LINDO_PARAMETER_TYPE.FINISH)
                            {
                                addColAndRow(lp1, lp2, rowIndex);
                                m_constrainsValues.Add(0.0);
                                m_constrains += "G";
                                rowIndex++;
                                Console.WriteLine(lp1.ToString() + " - " + lp2.ToString() + " >= " + "0.0");
                            }

                        }
                    }
                }

            }
            // adding step dependancy parameters
            foreach (LindoParameter lp in m_lindoParams)
            {
                if (lp.Step.Id == 1)
                    continue;
                if (lp.type == LINDO_PARAMETER_TYPE.FINISH)
                    continue;
                LindoParameter previousLP = getPreviousLP(m_lindoParams, lp.Step);
                if (previousLP == null)
                    continue;
                addColAndRow(lp, previousLP, rowIndex);
                m_constrainsValues.Add(0.0);
                m_constrains += "G";
                rowIndex++;
                Console.WriteLine(lp.ToString() + " - " + previousLP.ToString() + " >= " + "0.00");

            }

            int col = 0;
            m_colStart.Add(col);
            foreach (LindoParameter lp in m_lindoParams)
            {
                m_values.AddRange(lp.Columns);
                col += lp.Columns.Count;
                m_colStart.Add(col);
                m_rowIndex.AddRange(lp.Rows);
            }

            Console.WriteLine("");


            if ((int)m_colStart[m_colStart.Count - 1] < m_values.Count)
            {
                m_colStart.Add(m_values.Count);
            }

            m_paramsNum = m_lindoParams.Count;
        }

        /**
         *  creating Lindo Parameters 
         */ 
        private void  setLindoParametersList(ArrayList[] resources)
        {
            System.Collections.ArrayList LindoParams = new ArrayList();
           
            Hashtable modeStartParams = new Hashtable();

            

            for (int i = 0 ; i < resources.Length ; i++) 
            {
                if (resources[i] == null)
                {
                    continue;
                }
                resources[i].Sort(new ResourceSelectedOperationComparer());
                ResourceSelectedOperation[] selections = (ResourceSelectedOperation[])resources[i].ToArray(typeof(ResourceSelectedOperation));
                
                if (! modeStartParams.ContainsKey(selections[0].mode)) 
                {
                    LindoParameter lpSM;
                    lpSM = new LindoParameter(LINDO_PARAMETER_TYPE.START, null, selections[0].mode, selections[0].resource, selections[0].step , selections[0].jobNum);
                    LindoParams.Add(lpSM);
                    modeStartParams.Add(selections[0].mode,lpSM);
                }
                LindoParameter lpS0 = new LindoParameter(LINDO_PARAMETER_TYPE.START, (LindoParameter)modeStartParams[selections[0].mode], selections[0].mode, selections[0].resource, selections[0].step, selections[0].jobNum);
                LindoParameter lpF0 = new LindoParameter(LINDO_PARAMETER_TYPE.FINISH, lpS0, selections[0].mode, selections[0].resource, selections[0].step, selections[0].jobNum);

                LindoParams.Add(lpS0);
                LindoParams.Add(lpF0);
                
                for (int j = 1 ; j < selections.Length ; j++) 
                {
                    LindoParameter lpS = new LindoParameter(LINDO_PARAMETER_TYPE.START, (LindoParameter)LindoParams[LindoParams.Count - 1], selections[j].mode, selections[j].resource, selections[j].step, selections[j].jobNum );
                    LindoParameter lpF = new LindoParameter(LINDO_PARAMETER_TYPE.FINISH, lpS, selections[j].mode, selections[j].resource, selections[j].step , selections[j].jobNum);

                    LindoParams.Add(lpS);
                    LindoParams.Add(lpF);
                }
            }

            m_lindoParams = LindoParams;
            //return LindoParams;
        }

        private void addColAndRow(LindoParameter lp1, LindoParameter lp2, int row)
        {
            lp1.Columns.Add(1.0);
            lp2.Columns.Add(-1.0);
            lp1.Rows.Add(row);
            lp2.Rows.Add(row);
        }

        private List<Step> getPredecessorSteps(Step step)
        {
            List<Step> toReturn = new List<Step>();
            foreach (Constraint c in m_problem.Constraints)
            {
                if (c.StepTo.Equals(step))
                    toReturn.Add(c.StepFrom);
            }
            return toReturn;
        }

        private LindoParameter getPreviousLP(ArrayList LindoParams, Step step)
        {
            List<Step> constrainStep = getPredecessorSteps(step);
            List<LindoParameter> toReturn = new List<LindoParameter>();
            foreach (LindoParameter lp in LindoParams)
            {
                if (lp.type == LINDO_PARAMETER_TYPE.FINISH) {
                    if (constrainStep.Contains(lp.Step) && ! lp.NextStepWaiting)
                    {
                        lp.NextStepWaiting = true;
                        return lp;
                    }

                }
                
            }
            return null;
        }


        public double[] getValue()
        {
            return (double[])m_values.ToArray(typeof(double));
        }

        public int[] getColumnStart()
        {
            return (int[])m_colStart.ToArray(typeof(int));
        }

        public int[] getRowIndex()
        {
            return (int [])m_rowIndex.ToArray(typeof(int));
        }

        public String getConstrains()
        {
            return m_constrains;
        }

        public double[] getConstrainsValue()
        {
            return (double []) m_constrainsValues.ToArray(typeof(double));
        }

        public int getParamsNumber()
        {
            return m_paramsNum;
        }

        public String getVarName(int index) 
        {
            return ((LindoParameter)m_lindoParams[index]).ToString();
        }

        public Hashtable VariablesResultValues
        {
            get { return m_finalResults;  }
            set
            {
                m_finalResults = value;
            }
        }

        /**
         * returns a hashtable containing the results as follows <resource name , ArrayList of integers> 
         */ 
        public void getResults()
        {
            Hashtable results = new Hashtable();
            
            foreach (LindoParameter lp in m_lindoParams)
            {
                if (lp.resource != null)
                {
                    if (results[lp.resource.Id] == null)
                        results.Add(lp.resource.Id, new ArrayList());

                    ArrayList resourceTiming = (ArrayList)results[lp.resource.Id];
                    lp.Value = (double)m_finalResults[lp.ToString()];
                    resourceTiming.Add(lp);
                    //Console.WriteLine()
                }
            }
            
            foreach (Resource r in m_problem.Resources)
            {
                ArrayList a = (ArrayList)results[r.Id];
                if (a == null)
                    continue;
                Console.WriteLine("Resource "+ r.Id+" Results : ");
                foreach (LindoParameter lp in a) 
                {
                    if (lp.type == LINDO_PARAMETER_TYPE.START)
                    {
                        Console.WriteLine("Starting S"+lp.Step.Id+"J"+lp.JobNum+" in mode "+lp.mode.name+" At "+lp.Value);
                    }
                    if (lp.type == LINDO_PARAMETER_TYPE.FINISH)
                    {
                        Console.WriteLine("Finishing S" + lp.Step.Id + "J" + lp.JobNum + " in mode " + lp.mode.name + " At " + lp.Value);
                    }
                }
                //Console.WriteLine("");
            }
        }

    }
}
