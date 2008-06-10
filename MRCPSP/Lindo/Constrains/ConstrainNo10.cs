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
                    Mode mode = sol.getSelectedModeByCell(cell);
                    
                    List<Job> jobs = prob.JobsInProduct[cell.product];   
                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id+LindoContainer.TjfiType))
                        throw new ConstrainException("ConstrainNo10", "Can't find parameter " + "T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType);
                    LindoContainer.Instance.Variables["T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ")T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + " <= " + jobs[cell.jobId].LatestTermTime+" + "+mode.getTotalProcessTime());
                    LindoContainer.Instance.RightHandSideValues.Add(jobs[cell.jobId].LatestTermTime+mode.getTotalProcessTime());
                    LindoContainer.Instance.ConstraintsSenses.Add("L");
                    LindoContainer.Instance.ConstrainsCounter++;
                    

                }
            }
            
   
        }
    }
}
