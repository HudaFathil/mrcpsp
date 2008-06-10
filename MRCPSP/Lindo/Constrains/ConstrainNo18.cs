using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo18 : IConstrain
    {
          public ConstrainNo18()
            : base()
        {
        } 

        public override void createConstrain(Solution sol, Problem prob)
        {
             for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                for (int t = 0; t < sol.DistributionMatrix.GetLength(1); t++)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, t];
                    Mode mode = sol.getSelectedModeByCell(cell);
                    if (LindoContainer.Instance.getFinishSteps(cell.product.Id).Contains(cell.step))
                    {
                        if (!LindoContainer.Instance.BooleanVariables.ContainsKey("Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + LindoContainer.YjfimType))
                            continue;
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id+LindoContainer.TjfiType))
                            continue;
                        LindoContainer.Instance.Variables["F"].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") F");
                        
                        Console.Write(" -T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id+LindoContainer.TjfiType);
                        LindoContainer.Instance.Variables["T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id+LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                        Console.WriteLine(" >= " + mode.getTotalProcessTime());
                        LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime());
                        LindoContainer.Instance.ConstraintsSenses.Add("G");
                        LindoContainer.Instance.ConstrainsCounter++;

                    }
                    
                    
                }
             }

           
           /*
            for (int f = 0; f < prob.Products.Count; f++)
            {
               // int j = prob.Products[f].Size - 1;
                for (int j = 0; j < prob.Products[f].Size ; j++) {
                    foreach (Step s in LindoContainer.Instance.getFinishSteps(f))
                    {
                        LindoContainer.Instance.Variables["F"].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") F");
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s.Id))
                            continue;
                        Console.Write(" -T" + j + "" + f + "" + s.Id);
                        LindoContainer.Instance.Variables["T" + j + "" + f + "" + s.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + m.name + LindoContainer.YjfimType))
                                continue;
                            Console.Write(" " + -1 * m.getTotalProcessTime() + "Y" + j + "" + f + "" + s.Id + "" + m.name + LindoContainer.YjfimType);
                            LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + m.name + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime());
                        }
                        Console.WriteLine(" >= 0");
                        LindoContainer.Instance.RightHandSideValues.Add(0.0);
                        LindoContainer.Instance.ConstraintsSenses.Add("G");
                        LindoContainer.Instance.ConstrainsCounter++;
                    }
                }
            }
          */
        }

    }
}

