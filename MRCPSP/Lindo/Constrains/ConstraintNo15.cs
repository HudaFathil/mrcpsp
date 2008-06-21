using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using MRCPSP.Algorithm;

namespace MRCPSP.Lindo.Constrains
{
    class ConstraintNo15 : IConstrain
    {
        public ConstraintNo15()
            : base()
        {
        }

        public override void createConstrain(Solution sol, Problem prob)
        {
            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                if (taskList.Count == 0)
                    continue;
                LindoContainer.Instance.Variables["T" + r + "," + taskList[0]+LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                LindoContainer.Instance.Variables["V" + r + "," + taskList[0]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1);
                LindoContainer.Instance.RightHandSideValues.Add(prob.Resources[r].ArriveTime);
                LindoContainer.Instance.ConstraintsSenses.Add("G");
                LindoContainer.Instance.ConstrainsCounter++;
            }
        }
 

    }
}
