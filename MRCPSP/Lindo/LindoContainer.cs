using System;
using System.Collections.Generic;
using System.Collections;
using MRCPSP.CommonTypes;
using MRCPSP.Controllers;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using System.Linq;
using System.Text;

namespace MRCPSP.Lindo
{
    class LindoContainer
    {

        private Dictionary<String,MrcpspVariable> m_variables;
        private static LindoContainer m_instance;
        List<String> m_constraintsSenses;
        private List<double> m_rightHandSideValues;
        private Dictionary<Resource,List<int>> m_taskList;
        private int constrainsCounter = 0;
        private Dictionary<int, List<Step>> m_finishSteps;

        private LindoContainer()
        {
        }

        public void init()
        {
            Problem prob = ApplicManager.Instance.CurrentProblem;
            Solution sol = ApplicManager.Instance.CurrentSolution;
            m_taskList = new Dictionary<Resource, List<int>>();
            m_variables = new Dictionary<String, MrcpspVariable>();
            m_rightHandSideValues = new List<double>();
            m_constraintsSenses = new List<String>();
            m_finishSteps = new Dictionary<int, List<Step>>();

            for (int f = 0; f < prob.Products.Count; f++)
            {
                m_finishSteps.Add(f, new List<Step>());
                foreach (Step s1 in prob.StepsInProduct[prob.Products[f]])
                {
                    bool finishFlag = true;
                    foreach (Step s2 in prob.StepsInProduct[prob.Products[f]])
                    {
                        if (!s1.Equals(s2) && prob.isStepSubsequentToStep(prob.Products[f],s1,s2))
                            finishFlag = false;
                    }
                    if (finishFlag)
                        m_finishSteps[f].Add(s1);
                }
            }

            constrainsCounter = 0;
            int counter = 0;
             for (int f = 0; f < prob.Products.Count; f++)
            {
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.StepsInProduct[prob.Products[f]]) 
                    {
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            if (sol.SelectedModeList[counter] == m.name)
                            {
                                MrcpspVariable Yjfim = new MrcpspVariable("Y" + j + "" + f + "" + s.Id + "" + m.name);
                                Yjfim.Type = "B";
                                m_variables.Add(Yjfim.Name, Yjfim);
                               
                                for (int r = 0 ; r<prob.getNumberOfResources(); r++) {
                                    if (!m_taskList.ContainsKey(prob.Resources[r]))
                                        m_taskList.Add(prob.Resources[r], new List<int>());
                                    m_taskList[prob.Resources[r]].Add(counter);
                                    MrcpspVariable Xjfimrl = new MrcpspVariable("X" + j + "" + f + "" + s.Id + "" + m.name+""+r+""+counter);
                                    Xjfimrl.Type = "B";
                                    m_variables.Add(Xjfimrl.Name, Xjfimrl);
                                    MrcpspVariable Yimrl = new MrcpspVariable("Y" +s.Id + "" + m.name+""+r+""+counter);
                                    Yimrl.Type = "B";
                                    m_variables.Add(Yimrl.Name, Yimrl);
                                    MrcpspVariable Trl = new MrcpspVariable("T" + r + "" + counter);
                                    Trl.Type = "C";
                                    m_variables.Add(Trl.Name, Trl);
                                   /* MrcpspVariable Zrl = new MrcpspVariable("Z" + r + "" + counter);
                                    m_variables.Add(Zrl.Name, Zrl);
                                    MrcpspVariable Vrl = new MrcpspVariable("V" + r + "" + counter);
                                    m_variables.Add(Vrl.Name, Vrl);
                                    */
                                }
                                MrcpspVariable Tjfi = new MrcpspVariable("T" + j+""+f + "" + s.Id);
                                Tjfi.Type = "C";
                                m_variables.Add(Tjfi.Name, Tjfi);
                                
                            }
                            
                        }
                        counter++;
                    }
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

        public int getPreviousTask(Resource resource , int task)
        {
            if (task ==0)
                return -1;
            return m_taskList[resource][task - 1];
        }

        public List<int> getTaskListForResource(Resource r)
        {
            return m_taskList[r];
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

        /**
         * returns a hashtable containing the results as follows <resource name , ArrayList of integers> 
         */
        public Dictionary<Resource, List<ResultParameter>> getResults()
        {
            Problem prob = ApplicManager.Instance.CurrentProblem;
            for (int f = 0; f < prob.Products.Count ; f++ )
            {
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.StepsInProduct[prob.Products[f]])
                    {
                        if (!m_variables.ContainsKey("T" + j + "" + f + "" + s.Id))
                            continue;
                        ResultParameter result = new ResultParameter();
                        MrcpspVariable mrcpsp = m_variables["T"+j+""+f+""+s.Id];
                        result.startTime = mrcpsp.FinalValue;
                        result.jobID = j;
                        result.product = prob.Products[f];
                        result.step = s;
                    }
                }
            }

            return null;
        }

    }
}
