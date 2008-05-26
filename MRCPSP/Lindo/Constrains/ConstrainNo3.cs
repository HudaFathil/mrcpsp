using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo3 : IConstrain
    {
        public ConstrainNo3()
            : base()
        {
        }

        public override void createConstrain(Solution sol, Problem prob)
        {
            for (int f = 0; f < prob.Products.Count; f++)
            {
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.Steps)
                    {

                        foreach (Mode m in prob.ModesInStep[s])
                        {

                            for (int r = 0; r < prob.Resources.Count; r++)
                            {
                                
                                List<int> taskList = LindoContainer.Instance.getTaskListForResource(prob.Resources[r]);
                                if (LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[0]))
                                {
                                    LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[0]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                                    LindoContainer.Instance.RightHandSideValues.Add(1.0);
                                    LindoContainer.Instance.ConstraintsSenses.Add("L");
                                    LindoContainer.Instance.ConstrainsCounter++;
                                    Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + s.Id + "" + m.name + "" + r + "" + taskList[0] + " <= 1");
                                }
                                for (int t = 1; t < taskList.Count; t++ ) 
                                {
                                        if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[t]))
                                            continue;
                                        if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[t-1]))
                                            continue;
                                        LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                                        LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[t-1]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1);
                                        Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + s.Id + "" + m.name + "" + r + "" + taskList[t] + "-Y" + s.Id + "" + m.name + "" + r + "" + taskList[t]+ " <= 0");
                                        LindoContainer.Instance.RightHandSideValues.Add(0.0);
                                        LindoContainer.Instance.ConstraintsSenses.Add("L");
                                        LindoContainer.Instance.ConstrainsCounter++;
                                }
                                
                                        
                                
                            }

                        }
                    }
                }
            }
        }
    }
}
