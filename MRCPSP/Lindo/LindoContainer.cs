using System;
using System.Collections.Generic;
using System.Collections;
using MRCPSP.CommonTypes;
using MRCPSP.Controllers;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.Exceptions;
using System.Linq;
using System.Text;

namespace MRCPSP.Lindo
{
    class LindoContainer
    {

        private Dictionary<String,MrcpspVariable> m_variables;
        private Dictionary<String, MrcpspVariable> m_boolVariables;
        private static LindoContainer m_instance;
        List<String> m_constraintsSenses;
        private List<double> m_rightHandSideValues;
        private int constrainsCounter = 0;
        private Dictionary<int, List<Step>> m_finishSteps;
        public static String YjfimType = "A";
        public static String YimrlType = "B";

        public static String TrlType = "A";
        public static String TjfiType = "B";

        public int N = 100000;

        private LindoContainer()
        {
        }

        public void init()
        {
            Problem prob = ApplicManager.Instance.CurrentProblem;
            Solution sol = ApplicManager.Instance.CurrentSolution;
            m_variables = new Dictionary<String, MrcpspVariable>();
            m_boolVariables = new Dictionary<String, MrcpspVariable>();
            m_rightHandSideValues = new List<double>();
            m_constraintsSenses = new List<String>();
            m_finishSteps = new Dictionary<int, List<Step>>();

            for (int f = 0; f < prob.Products.Count; f++)
            {
                m_finishSteps.Add(prob.Products[f].Id, new List<Step>());
                foreach (Step s1 in prob.StepsInProduct[prob.Products[f]])
                {
                    bool finishFlag = true;
                    foreach (Step s2 in prob.StepsInProduct[prob.Products[f]])
                    {
                        if (!s1.Equals(s2) && prob.isStepSubsequentToStep(prob.Products[f], s1, s2))
                            finishFlag = false;
                    }
                    if (finishFlag)
                        m_finishSteps[prob.Products[f].Id].Add(s1);
                }
            }

            constrainsCounter = 0;

            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                foreach (int t in taskList)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, t];
                    // creating Trl
                    MrcpspVariable Trl = new MrcpspVariable("T" + r + "," + t + TrlType);
                    Trl.Type = "C";
                    m_variables.Add(Trl.Name, Trl);
                    // creating Zrl
                    MrcpspVariable Zrl = new MrcpspVariable("Z" + r + "," + t);
                    m_variables.Add(Zrl.Name, Zrl);
                    // creating Vrl
                    MrcpspVariable Vrl = new MrcpspVariable("V" + r + "," + t);
                    m_variables.Add(Vrl.Name, Vrl);
                    Mode mode = sol.getSelectedModeByCell(cell);// sol.SelectedModeList[t];
                    // creating Yjfim
                    MrcpspVariable Yjfim = new MrcpspVariable("Y" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id + "," + mode.IdPerStep + YjfimType);
                    Yjfim.Type = "B";
                    if (!m_boolVariables.ContainsKey(Yjfim.Name))
                        m_boolVariables.Add(Yjfim.Name, Yjfim);
                    // creating Xjfimrl
                    MrcpspVariable Xjfimrl = new MrcpspVariable("X" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id + "," + mode.IdPerStep + "," + r + "," + t);
                    Xjfimrl.Type = "B";
                    if (!m_boolVariables.ContainsKey(Xjfimrl.Name))
                        m_boolVariables.Add(Xjfimrl.Name, Xjfimrl);
                    // creating Tjfi
                    MrcpspVariable Tjfi = new MrcpspVariable("T" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id+TjfiType);
                    Tjfi.Type = "C";
                    if (!m_variables.ContainsKey(Tjfi.Name))
                        m_variables.Add(Tjfi.Name, Tjfi);
                    // creating Yimrl
                    MrcpspVariable Yimrl = new MrcpspVariable("Y" + cell.step.Id + "," + mode.IdPerStep + "," + r + "," + t + YimrlType);
                    Yimrl.Type = "B";
                    if (!m_boolVariables.ContainsKey(Yimrl.Name))
                        m_boolVariables.Add(Yimrl.Name, Yimrl);
                }

            }
            
            MrcpspVariable F = new MrcpspVariable("F");
            F.Type = "C";
            m_variables.Add(F.Name, F);


        }

        public List<Step> getFinishSteps(int familiy)
        {
            return m_finishSteps[familiy];
        }

      

        public List<double> RightHandSideValues
        {
            get { return m_rightHandSideValues; }
        }
       
        public List<String> ConstraintsSenses
        {
            get { return m_constraintsSenses; }
        }


        public int ConstrainsCounter
        {
            get { return constrainsCounter; }
            set { constrainsCounter = value; }
        }

        public static LindoContainer Instance
        {
            get {
                if (m_instance == null)
                {
                    m_instance = new LindoContainer();
                }
                return m_instance;
                }
        }

        public Dictionary<String,MrcpspVariable> Variables
        {
            get { return m_variables; }
        }

        public Dictionary<String, MrcpspVariable> BooleanVariables
        {
            get { return m_boolVariables; }
        }

        /**
         * returns a hashtable containing the results as follows <resource name , List of ResultParameter> 
         */
        public Dictionary<Resource, List<ResultParameter>> getResults()
        {
            Problem prob = ApplicManager.Instance.CurrentProblem;
            Solution sol = ApplicManager.Instance.CurrentSolution;

            Dictionary<Resource, List<ResultParameter>> toReturn = new Dictionary<Resource, List<ResultParameter>>();

            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                if (!toReturn.ContainsKey(prob.Resources[r]))
                    toReturn.Add(prob.Resources[r], new List<ResultParameter>());
                if (taskList.Count == 0)
                    continue;
                for (int t = 0; t < taskList.Count ; t++)
                {
                    if (!m_variables.ContainsKey("T" + r + "," + taskList[t]+LindoContainer.TrlType))
                        throw new ConstrainException("LindoContainer", "Can't find parameter T" + r + "," + taskList[t] + LindoContainer.TrlType);
                    
                    ResultParameter result = new ResultParameter();
                    result.startTime = m_variables["T" + r + "," + taskList[t] + LindoContainer.TrlType].FinalValue;
                    MatrixCell cell = sol.DistributionMatrix[r,taskList[t]]; 
                    result.jobID = cell.jobId;
                    result.product = cell.product;
                    result.step = cell.step;
                    Mode mode = sol.getSelectedModeByCell(cell);
                    result.finishTime = result.startTime + mode.getTotalProcessTime(prob.Resources[r]);
                    toReturn[prob.Resources[r]].Add(result);
                }
            }
            return toReturn;
        }

    }
}
