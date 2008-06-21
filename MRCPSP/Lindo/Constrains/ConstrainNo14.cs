using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.Controllers;
using MRCPSP.Exceptions;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo14 : IConstrain
    {
        public ConstrainNo14()
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

                LindoContainer.Instance.Variables["T" + r + "," + taskList[0]+LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "," + taskList[0] +LindoContainer.TrlType+ " >= 0");
                // should add here Zr0 , Vr0
                LindoContainer.Instance.RightHandSideValues.Add(0.0);
                LindoContainer.Instance.ConstraintsSenses.Add("G");
                LindoContainer.Instance.ConstrainsCounter++;

                for (int t = 1; t < taskList.Count; t++)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, taskList[t - 1]];
                    Mode mode = sol.getSelectedModeByCell(cell);
                    LindoContainer.Instance.Variables["T" + r + "," + taskList[t] + LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    LindoContainer.Instance.Variables["T" + r + "," + taskList[t - 1] + LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    LindoContainer.Instance.Variables["Z" + r + "," + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    LindoContainer.Instance.Variables["V" + r + "," + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[t] + LindoContainer.TrlType + " -T" + r + "," + taskList[t - 1] + LindoContainer.TrlType+" - Z"+r+","+taskList[t]);

                    Console.WriteLine(" >= " + mode.getTotalProcessTime(prob.Resources[r]));
                    LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime(prob.Resources[r]));
                    LindoContainer.Instance.ConstraintsSenses.Add("G");
                    LindoContainer.Instance.ConstrainsCounter++;
                }
            }

           
        }

    }
}
