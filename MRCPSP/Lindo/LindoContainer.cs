﻿using System;
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
        private int constrainsCounter = 0;
        private Dictionary<int, List<Step>> m_finishSteps;
        public static String YjfimType = "A";
        public static String YimrlType = "B";
        public int N = 100000;

        private LindoContainer()
        {
        }

        public void init()
        {
            Problem prob = ApplicManager.Instance.CurrentProblem;
            Solution sol = ApplicManager.Instance.CurrentSolution;
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
                        if (!s1.Equals(s2) && prob.isStepSubsequentToStep(prob.Products[f], s1, s2))
                            finishFlag = false;
                    }
                    if (finishFlag)
                        m_finishSteps[f].Add(s1);
                }
            }

            constrainsCounter = 0;

            foreach (Step s in prob.Steps)
            {

                for (int f = 0; f < prob.Products.Count; f++)
                {
                    for (int j = 0; j < prob.Products[f].Size; j++)
                    {
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            MrcpspVariable Yjfim = new MrcpspVariable("Y" + j + "" + f + "" + s.Id + "" + m.name + YjfimType);
                            Yjfim.Type = "B";
                            m_variables.Add(Yjfim.Name, Yjfim);
                            for (int r = 0; r < prob.getNumberOfResources(); r++)
                            {
                                foreach (int task in sol.getTaskListForResource(r, prob.Resources[r]))
                                {
                                    MrcpspVariable Xjfimrl = new MrcpspVariable("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task);
                                    Xjfimrl.Type = "B";
                                    m_variables.Add(Xjfimrl.Name, Xjfimrl);

                                }
                            }
                        }
                        MrcpspVariable Tjfi = new MrcpspVariable("T" + j + "" + f + "" + s.Id);
                        Tjfi.Type = "C";
                        m_variables.Add(Tjfi.Name, Tjfi);
                    }
                }
                for (int r = 0; r < prob.getNumberOfResources(); r++)
                {
                    foreach (Mode m in prob.ModesInStep[s])
                    {
                        foreach (int task in sol.getTaskListForResource(r, prob.Resources[r]))
                        {
                            MrcpspVariable Yimrl = new MrcpspVariable("Y" + s.Id + "" + m.name + "" + r + "" + task + YimrlType);
                            Yimrl.Type = "B";
                            m_variables.Add(Yimrl.Name, Yimrl);
                        }
                    }
                }
            }
            for (int r = 0; r < prob.getNumberOfResources(); r++)
            {

                foreach (int task in sol.getTaskListForResource(r, prob.Resources[r]))
                {
                    MrcpspVariable Trl = new MrcpspVariable("T" + r + "" + task);
                    Trl.Type = "C";
                    m_variables.Add(Trl.Name, Trl);
                    /* MrcpspVariable Zrl = new MrcpspVariable("Z" + r + "" + counter);
                     m_variables.Add(Zrl.Name, Zrl);
                     MrcpspVariable Vrl = new MrcpspVariable("V" + r + "" + counter);
                     m_variables.Add(Vrl.Name, Vrl);
                     */
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

        public List<Resource> getResourceNeededInAllStepModes(Step s)
        {
            List<Resource> rList = new List<Resource>();
            List<Mode> mList = ApplicManager.Instance.CurrentProblem.ModesInStep[s];
            foreach (Resource r in ApplicManager.Instance.CurrentProblem.Resources)
            {
                bool toAdd = true;
                foreach (Mode m in mList)
                {
                    if (!m.isResourceUsed(r))
                        toAdd = false;
                }
                if (toAdd)
                    rList.Add(r);
            }
            return rList;
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
