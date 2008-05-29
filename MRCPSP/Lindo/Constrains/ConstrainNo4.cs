using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo4 : IConstrain
    {
        public ConstrainNo4()
            : base()
        {
        }

        public override void createConstrain(Solution sol, Problem prob)
        {
                
            for (int r = 0; r < prob.Resources.Count; r++) 
            {
                List<Step> sList = sol.getStepsInResource(r, prob.Resources[r]);
                List<int> tList = sol.getTaskListForResource(r, prob.Resources[r]);
                foreach (Step s in sList)
                {
                    foreach (Mode m in prob.ModesInStep[s])
                    {
                        foreach (int t in tList)
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + t + LindoContainer.YimrlType))
                                continue;

                            LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + t+LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.Write ("Constrain No "+ LindoContainer.Instance.ConstrainsCounter +") Y"+ s.Id + "" + m.name + "" + r + "" + t+LindoContainer.YimrlType);
                            for (int f = 0; f < prob.Products.Count; f++)
                            {
                                for (int j = 0; j < prob.Products[f].Size; j++)
                                {
                                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t))
                                        continue;
                                    Console.Write(" + "+prob.Products[f].Size+"*X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t);
                                    LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, prob.Products[f].Size * 1 ); // instead of *1 should be added later * Krf
                                }
                            }
                            Console.WriteLine(" >= 0");
                            LindoContainer.Instance.RightHandSideValues.Add(0); 
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }

                        foreach (int t in tList)
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + t))
                                continue;

                            LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + s.Id + "" + m.name + "" + r + "" + t);
                            for (int f = 0; f < prob.Products.Count; f++)
                            {
                                for (int j = 0; j < prob.Products[f].Size; j++)
                                {
                                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t))
                                        continue;
                                    Console.Write(" + X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t);
                                    LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, prob.Products[f].Size * 1); // instead of *1 should be added later * Krf
                                }
                            }
                            Console.WriteLine(" >= 1");
                            LindoContainer.Instance.RightHandSideValues.Add(1); // should be replaced later with Kr
                            LindoContainer.Instance.ConstraintsSenses.Add("L");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }

                    }
                }
                
            }
                            
          
        }
    }
}
