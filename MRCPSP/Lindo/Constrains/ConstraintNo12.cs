using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using MRCPSP.Algorithm;
using MRCPSP.Log;

namespace MRCPSP.Lindo.Constrains
{
    class ConstraintNo12 : IConstrain
    {
        public ConstraintNo12()
            : base()
        {
        }

        public override void createConstrain(Solution sol, Problem prob)
        {

            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                for (int t = 1; t < taskList.Count; t++ )
                {
                    LindoContainer.Instance.Variables["Z" + r + "," + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    LindoContainer.Instance.ConstraintsSenses.Add("G");
                    Mode from = sol.getSelectedModeByCell(sol.DistributionMatrix[r,taskList[t-1]]);
                    Mode to = sol.getSelectedModeByCell(sol.DistributionMatrix[r,taskList[t]]);
                    ResourceConstraint rc = prob.getResourceConstraint(prob.Resources[r],from,to);
                    if (rc != null)
                    {
                        LindoContainer.Instance.RightHandSideValues.Add(rc.DelayTime);
                        Logger.Instance.debug("Constraint No " + LindoContainer.Instance.ConstrainsCounter + ") Z" + r + "," + taskList[t] + ">" + rc.DelayTime);
                    }
                    else
                    {
                        LindoContainer.Instance.RightHandSideValues.Add(0);
                        Logger.Instance.debug("Constraint No " + LindoContainer.Instance.ConstrainsCounter + ") Z" + r + "," + taskList[t] + ">" + 0);
                    }
                    
                    LindoContainer.Instance.ConstrainsCounter++;
                    
                }
            }

        }
    }
}
