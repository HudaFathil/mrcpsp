using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo2 : IConstrain
    {
          public ConstrainNo2()
            : base()
        {
        }


        public override void createConstrain(Solution sol , Problem prob)
        {
            for (int r = 0; r < prob.Resources.Count; r++)
            {
           
                    foreach (Step s in sol.getStepsInResource(r,prob.Resources[r]))
                    {

                        foreach (Mode m in prob.ModesInStep[s])
                        {
                             for (int f = 0; f < prob.Products.Count; f++)
                            {
                                for (int j = 0; j < prob.Products[f].Size; j++)
                                {
                                    int task = sol.getTaskNumber(r, prob.Resources[r], s, prob.Products[f], j);
                                    if (task == -1)
                                        continue;
                                    Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task + " = 1");
                                    LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                                    LindoContainer.Instance.RightHandSideValues.Add(1.0);
                                    LindoContainer.Instance.ConstraintsSenses.Add("E");
                                    LindoContainer.Instance.ConstrainsCounter++;
                                    /*
                                    if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + m.name+LindoContainer.YjfimType))
                                        continue;
                                    int task = sol.getTaskNumber(r,prob.Resources[r],s,prob.Products[f],j);
                                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task))
                                        continue;
                                    LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + m.name+LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                                    Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + j + "" + f + "" + s.Id + "" + m.name+LindoContainer.YjfimType);
                                    Console.Write(" - X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task);
                                    LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                                    Console.WriteLine(" = 0");
                                    LindoContainer.Instance.RightHandSideValues.Add(0.0);
                                    LindoContainer.Instance.ConstraintsSenses.Add("E");
                                    LindoContainer.Instance.ConstrainsCounter++;
                                     */
                            
                            }
                            

                        }
                    }
                }
            }					
        }
    }
}
