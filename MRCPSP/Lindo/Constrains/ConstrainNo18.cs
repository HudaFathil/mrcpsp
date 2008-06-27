using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;
using MRCPSP.Log;

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
            /*
             for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                for (int t = 0; t < sol.DistributionMatrix.GetLength(1); t++)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, t];
                    Mode mode = sol.getSelectedModeByCell(cell);
                    if (LindoContainer.Instance.getFinishSteps(cell.product.Id).Contains(cell.step))
                    {
                        if (!LindoContainer.Instance.BooleanVariables.ContainsKey("Y" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id + "," + mode.IdPerStep + LindoContainer.YjfimType))
                            continue;
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id+LindoContainer.TjfiType))
                            continue;
                        LindoContainer.Instance.Variables["F"].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        LindoContainer.Instance.Variables["T" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                        Logger.Instance("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") F" +
                            " -T" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id+LindoContainer.TjfiType+
                            " >= " + mode.getTotalProcessTime());
                        LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime());
                        LindoContainer.Instance.ConstraintsSenses.Add("G");
                        LindoContainer.Instance.ConstrainsCounter++;

                    }
                    
                    
                }
             }
            */
           
            for (int f = 0; f < prob.Products.Count; f++)
            {
               // int j = prob.Products[f].Size - 1;
                for (int j = 0; j < prob.Products[f].Size ; j++) {
                    foreach (Step s in LindoContainer.Instance.getFinishSteps(prob.Products[f].Id))
                    {
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "," + prob.Products[f].Id + "," + s.Id+LindoContainer.TjfiType))
                                continue;
                            if (!LindoContainer.Instance.BooleanVariables.ContainsKey("Y" + j + "," + prob.Products[f].Id + "," + s.Id + "," + m.IdPerStep + LindoContainer.YjfimType))
                                continue;
                            LindoContainer.Instance.Variables["F"].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + j + "," + prob.Products[f].Id + "," + s.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            // LindoContainer.Instance.BooleanVariables["Y" + j + "" + f + "" + s.Id + "" + m.name + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime());
                            Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") F" +
                            " -T" + j + "," + prob.Products[f].Id + "," + s.Id + LindoContainer.TjfiType +
                            " >= " + m.getTotalProcessTime());
                            LindoContainer.Instance.RightHandSideValues.Add(m.getTotalProcessTime());
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }
                        
                    }
                }
            }
          
        }

    }
}

