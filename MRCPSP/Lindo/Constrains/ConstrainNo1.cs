using System;
using System.Collections.Generic;
using System.Linq;
using MRCPSP.Algorithm;
using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using System.Text;
using MRCPSP.Exceptions;

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
            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                for (int t = 0; t < sol.DistributionMatrix.GetLength(1); t++)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, t];
                    Mode mode = sol.getSelectedModeByCell(cell);
                    if (!LindoContainer.Instance.Variables.ContainsKey("Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.IdPerStep + LindoContainer.YjfimType))
                        throw new ConstrainException("ConstrainNo1", "Can't find parameter Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.IdPerStep + LindoContainer.YjfimType);
                    LindoContainer.Instance.Variables["Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.IdPerStep + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    Console.WriteLine("Constrain No "+LindoContainer.Instance.ConstrainsCounter+") Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.IdPerStep + LindoContainer.YjfimType + " = 1");
                    LindoContainer.Instance.RightHandSideValues.Add(1.0);
                    LindoContainer.Instance.ConstraintsSenses.Add("E");
                    LindoContainer.Instance.ConstrainsCounter++;


                }
            }
                    /*
            
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
                            throw new ConstrainException("ConstrainNo1", "Can't get task number for Product " + prob.Products[f].Name + " Step " + s.Id + " job " + j);
                        }
                        Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") ");

                        if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + selecteMode + LindoContainer.YjfimType))
                        {
                            throw new ConstrainException("ConstrainNo1", "Can't find parameter Y" + j + "" + f + "" + s.Id + "" + selecteMode + LindoContainer.YjfimType);
                        }
                        Console.Write("+ Y" + j + "" + f + "" + s.Id + "" + selecteMode + LindoContainer.YjfimType);
                        LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + selecteMode + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        
                        Console.WriteLine(" = 1");
                        LindoContainer.Instance.RightHandSideValues.Add(1.0);
                        LindoContainer.Instance.ConstraintsSenses.Add("E");
                        LindoContainer.Instance.ConstrainsCounter++;
                    }
                }
            }
                     */
}
    }
}
