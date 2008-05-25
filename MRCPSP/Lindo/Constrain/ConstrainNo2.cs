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
            for (int f = 0; f < prob.Products.Count; f++)
            {
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.Steps)
                    {

                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + m.name))
                                continue;
                            LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + m.name].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                            Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + j + "" + f + "" + s.Id + "" + m.name);
                            

                            for (int r = 0; r <prob.Resources.Count; r++)
                            {
                                List<int> taskList = LindoContainer.Instance.getTaskListForResource(prob.Resources[r]);
                                foreach (int task in taskList)
                                {
                                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task))
                                        continue;
                                    Console.Write(" - X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task);
                                    LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + task].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                                }
                            
                            }
                            Console.WriteLine(" = 0");
                            LindoContainer.Instance.RightHandSideValues.Add(0.0);
                            LindoContainer.Instance.ConstraintsSenses.Add("E");
                            LindoContainer.Instance.ConstrainsCounter++;

                        }
                    }
                }
            }					
        }
    }
}
