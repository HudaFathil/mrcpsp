using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.Exceptions;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo17 : IConstrain
    {
        public ConstrainNo17()
            : base()
        {
        }

        public override void createConstrain(Solution sol, Problem prob)
        {

            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                foreach (int t in taskList)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, t];
                    Mode mode = sol.getSelectedModeByCell(cell);

                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType))
                        throw new ConstrainException("ConstrainNo17", "Can't find parameter" + "T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType);
                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + t + LindoContainer.TrlType))
                        throw new ConstrainException("ConstrainNo17", "Can't find parameter" + "T" + r + "" + t + LindoContainer.TrlType);
                    if (!LindoContainer.Instance.BooleanVariables.ContainsKey("X" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t))
                        throw new ConstrainException("ConstrainNo17", "Can't find parameter" + "X" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t);
                    LindoContainer.Instance.Variables["T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    LindoContainer.Instance.Variables["T" + r + "" + t + LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);

                    String toPrint = "Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType + " -T" + r + "" + t + LindoContainer.TrlType;
                    double startUsingResource = mode.startUsingResourceTime(prob.Resources[r]);

                    Console.WriteLine(toPrint + " = " + -1 * startUsingResource);
                    LindoContainer.Instance.RightHandSideValues.Add(-1 * startUsingResource);
                    LindoContainer.Instance.ConstraintsSenses.Add("E");
                    LindoContainer.Instance.ConstrainsCounter++;
                }
            }
        }
    }
}
