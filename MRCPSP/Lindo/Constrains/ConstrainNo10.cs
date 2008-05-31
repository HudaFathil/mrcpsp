using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Algorithm;
using MRCPSP.Domain;
using MRCPSP.Exceptions;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo10 : IConstrain

    {

        public ConstrainNo10()
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
                    Mode m = sol.getSelectedMode(cell, t);
                    int mode = sol.SelectedModeList[t];
                    List<Job> jobs = prob.JobsInProduct[cell.product];   
                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id))
                        throw new ConstrainException("ConstrainNo10", "Can't find parameter " + "T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id);
                    LindoContainer.Instance.Variables["T" + cell.jobId+ "" + cell.product.Id + "" + cell.step.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    
                    

                    if (!LindoContainer.Instance.Variables.ContainsKey("Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode + LindoContainer.YjfimType))
                        throw new ConstrainException("ConstrainNo10", "Can't find parameter " + "Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode + LindoContainer.YjfimType);
                    LindoContainer.Instance.Variables["Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, m.getTotalProcessTime());
                    Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ")T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + " - " + m.getTotalProcessTime() + "*Y" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode + LindoContainer.YjfimType + " <= " + jobs[cell.jobId].LatestTermTime);
                    LindoContainer.Instance.RightHandSideValues.Add(jobs[cell.jobId].LatestTermTime);
                    LindoContainer.Instance.ConstraintsSenses.Add("L");
                    LindoContainer.Instance.ConstrainsCounter++;
                    

                }
            }
            
            /*
            for (int f = 0; f < prob.Products.Count; f++)
            {
                List<Job> jobs = prob.JobsInProduct[prob.Products[f]];
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.Steps)
                    {
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s.Id))
                            continue;
                        LindoContainer.Instance.Variables["T" + j + "" + f + "" + s.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + m.name+LindoContainer.YjfimType))
                                continue;
                            LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + m.name+LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, m.getTotalProcessTime());
                        }

                    }

                    LindoContainer.Instance.RightHandSideValues.Add(jobs[j].LatestTermTime);
                    LindoContainer.Instance.ConstraintsSenses.Add("L");
                    LindoContainer.Instance.ConstrainsCounter++;
                }
            }
             */
        }
    }
}
