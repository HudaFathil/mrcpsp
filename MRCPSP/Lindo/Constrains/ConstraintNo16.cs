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
    class ConstraintNo16 : IConstrain
    {
        public ConstraintNo16()
            : base()
        {
        }

         public override void createConstrain(Solution sol, Problem prob)
         {

             for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
             {
                 List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                 for (int t = 0; t < taskList.Count; t++)
                 {
                     MatrixCell cell = sol.DistributionMatrix[r, taskList[t]];
                     Mode mode = sol.getSelectedModeByCell(cell);
                     List<Job> jobs = prob.JobsInProduct[cell.product];
                     // geting Ujf (number of units per job and family)
                     int ajf = 0;
                     foreach (Job j in jobs)
                     {
                         if (j.Id == cell.jobId)
                             ajf = j.ArriveTime;

                     }
                     LindoContainer.Instance.Variables["T" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                     Console.WriteLine("Constraint No " + LindoContainer.Instance.ConstrainsCounter + ") T" + cell.jobId + "," + cell.product.Id + "," + cell.step.Id + LindoContainer.TjfiType + " > " + ajf);
                     LindoContainer.Instance.RightHandSideValues.Add(ajf);
                     LindoContainer.Instance.ConstraintsSenses.Add("G");
                     LindoContainer.Instance.ConstrainsCounter++;
                 }
             }
         }
    }
}
