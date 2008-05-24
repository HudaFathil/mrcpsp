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
        private float[,] m_constraintsMatrix;
        private List<double> m_rightHandSideValues;
        private List<String> m_constraintsSenses;
        private int constrainsCounter = 0;
        private Solution m_sol;

        private LindoContainer(Solution sol)
        {
            m_sol = sol;
            m_constraintsMatrix = new float[9500, 9500];
            m_variables = new Dictionary<String, MrcpspVariable>();
            m_rightHandSideValues = new List<double>();
            m_constraintsSenses = new List<String>();
            init();
        }

        private void init()
        {
            Problem prob = ApplicManager.Instance.CurrentProblem;
            int counter = 0;
             for (int f = 0; f < prob.Products.Count; f++)
            {
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.Steps) 
                    {
                        
                        
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            if (m_sol.SelectedModeList[counter] == m.name)
                            {
                                MrcpspVariable Yjfim = new MrcpspVariable("Y" + j + "" + f + "" + s.Id + "" + m.name);
                                Yjfim.Type = "B";
                                m_variables.Add(Yjfim.Name, Yjfim);
                                for (int r = 0 ; r<prob.getNumberOfResources(); r++) {
                                    MrcpspVariable Xjfimrl = new MrcpspVariable("X" + j + "" + f + "" + s.Id + "" + m.name+""+r+""+counter);
                                    m_variables.Add(Xjfimrl.Name, Xjfimrl);
                                    MrcpspVariable Yimrl = new MrcpspVariable("Y" +s.Id + "" + m.name+""+r+""+counter);
                                    m_variables.Add(Yimrl.Name, Yimrl);
                                    MrcpspVariable Trl = new MrcpspVariable("T" + r + "" + counter);
                                    m_variables.Add(Trl.Name, Trl);
                                    MrcpspVariable Zrl = new MrcpspVariable("Z" + r + "" + counter);
                                    m_variables.Add(Zrl.Name, Zrl);
                                    MrcpspVariable Vrl = new MrcpspVariable("V" + r + "" + counter);
                                    m_variables.Add(Vrl.Name, Vrl);
                                }
                                MrcpspVariable Tjfi = new MrcpspVariable("T" + j+""+f + "" + s.Id);
                                m_variables.Add(Tjfi.Name, Tjfi);
                                
                            }
                            
                        }
                        counter++;
                    }
                }
             }
          
            
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
                    m_instance = new LindoContainer(ApplicManager.Instance.CurrentSolution);
                }
                return m_instance;
                }
        }

        public Dictionary<String,MrcpspVariable> Variables
        {
            get { return m_variables; }
        }

    }
}
