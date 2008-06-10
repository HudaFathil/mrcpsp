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

                    String toPrint = "Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType+" -T" + r + "" + t + LindoContainer.TrlType;
                    double startUsingResource = mode.startUsingResourceTime(prob.Resources[r]);

                    Console.WriteLine(toPrint + " = " + -1*startUsingResource);
                    LindoContainer.Instance.RightHandSideValues.Add(-1 * startUsingResource);
                    LindoContainer.Instance.ConstraintsSenses.Add("E");
                    LindoContainer.Instance.ConstrainsCounter++;
                    /*
                    LindoContainer.Instance.Variables["T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    LindoContainer.Instance.Variables["T" + r + "" + t + LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);

                    toPrint = "Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + " -T" + r + "" + t;
                    startUsingResource = mode.startUsingResourceTime(prob.Resources[r]);

                    Console.WriteLine(toPrint + " <= " + LindoContainer.Instance.N + "* ( 1 - " + startUsingResource+ " )");
                    LindoContainer.Instance.RightHandSideValues.Add(LindoContainer.Instance.N - startUsingResource);
                    LindoContainer.Instance.ConstraintsSenses.Add("L");
                    LindoContainer.Instance.ConstrainsCounter++;
                    */


                }
            }

            /*
            for (int f = 0; f < prob.Products.Count; f++)
            {
                for (int j = 0; j < prob.Products[f].Size; j++)
                {

                    for (int r = 0; r < prob.Resources.Count; r++)
                    {
                        foreach (Step s in sol.getStepsInResource(r, prob.Resources[r]))
                        {

                            int t = sol.getTaskNumber(r, prob.Resources[r], s, prob.Products[f], j);
                            bool addedXjfimrl = false;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s.Id))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + t))
                                continue;
                            String toPrint = "";
                            toPrint += "Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + j + "" + f + "" + s.Id + " -T" + r + "" + t;
       
                            foreach (Mode m in prob.ModesInStep[s])
                            {

                                if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t))
                                    continue;
                                addedXjfimrl = true;
                                double startUsingResource = m.startUsingResourceTime(prob.Resources[r]);
                                toPrint += " +" + startUsingResource + "*X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t;
                                LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, startUsingResource - LindoContainer.Instance.N);
                                toPrint += -1 * LindoContainer.Instance.N + "*X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t;
                            }
                            if (!addedXjfimrl)
                                continue;
                            LindoContainer.Instance.Variables["T" + j + "" + f + "" + s.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.WriteLine(toPrint + " >= " + -1 * LindoContainer.Instance.N + "");
                            LindoContainer.Instance.RightHandSideValues.Add(-1 * LindoContainer.Instance.N);
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;


                            addedXjfimrl = false;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s.Id))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + t))
                                continue;
                            toPrint = "";
                            toPrint += "Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + j + "" + f + "" + s.Id + " -T" + r + "" + t;
                            foreach (Mode m in prob.ModesInStep[s])
                            {
                                if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t))
                                    continue;
                                addedXjfimrl = true;
                                double startUsingResource = m.startUsingResourceTime(prob.Resources[r]);
                                toPrint += " +" + startUsingResource + "X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t;
                                LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, startUsingResource + LindoContainer.Instance.N);
                                toPrint += " +" + LindoContainer.Instance.N + "X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t;
                            }
                            if (!addedXjfimrl)
                                continue;
                            LindoContainer.Instance.Variables["T" + j + "" + f + "" + s.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.WriteLine(toPrint + " <= " + LindoContainer.Instance.N + "");
                            LindoContainer.Instance.RightHandSideValues.Add(LindoContainer.Instance.N);
                            LindoContainer.Instance.ConstraintsSenses.Add("L");
                            LindoContainer.Instance.ConstrainsCounter++;




                        }

                    }
                }
            }
            */
        }
    }
}
