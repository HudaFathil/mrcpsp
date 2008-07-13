using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Algorithm;
using MRCPSP.Domain;
using MRCPSP.Exceptions;
using MRCPSP.Log;

namespace MRCPSP.Lindo.Constrains
{
    class ConstraintNo13 : IConstrain
    {
        public ConstraintNo13()
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
                    MatrixCell cell = sol.DistributionMatrix[r, t];
                    Mode mode = sol.getSelectedModeByCell(cell);
                    // getting  DLimr
                    int dlimr = 0;
                    foreach (SetupTime st in prob.SetupTimeList)
                    {
                        if (st.Mode.Equals(mode) && st.Resource.Equals(prob.Resources[r]))
                            dlimr = st.ResourceSetupTime;
                    }
                    List<Job> jobs = prob.JobsInProduct[cell.product];
                    // geting Ujf (number of units per job and family)
                    int ujf = 1;
                    foreach (Job j in jobs)
                    {
                        if (j.Id == cell.jobId)
                            ujf = j.Units;

                    }
                    if (dlimr * ujf > 0)
                    {
                        Logger.Instance.debug("Constraint No " + LindoContainer.Instance.ConstrainsCounter + ") V" + r + "," + taskList[t] + " > " + dlimr * ujf);
                        LindoContainer.Instance.Variables["V" + r + "," + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                        LindoContainer.Instance.RightHandSideValues.Add(dlimr * ujf);
                        LindoContainer.Instance.ConstraintsSenses.Add("G");
                        LindoContainer.Instance.ConstrainsCounter++;
                    }
                }
            }
        }
    }
}
