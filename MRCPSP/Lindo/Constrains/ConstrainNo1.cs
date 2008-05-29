using System;
using System.Collections.Generic;
using System.Linq;
using MRCPSP.Algorithm;
using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using System.Text;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo1 : IConstrain
    {

        public ConstrainNo1()
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
                        int selecteMode = sol.getSelectedMode(prob.Products[f], s, j);
                        if (selecteMode == -1)
                        {
                            Console.WriteLine("ERROR - Probably bad solution");
                            continue;
                        }
                        Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") ");

                        if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + selecteMode + LindoContainer.YjfimType))
                                continue;
                        Console.Write("+ Y" + j + "" + f + "" + s.Id + "" + selecteMode + LindoContainer.YjfimType);
                        LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + selecteMode + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        
                        Console.WriteLine(" = 1");
                        LindoContainer.Instance.RightHandSideValues.Add(1.0);
                        LindoContainer.Instance.ConstraintsSenses.Add("E");
                        LindoContainer.Instance.ConstrainsCounter++;
                    }
                }
            }
        }
    }
}
