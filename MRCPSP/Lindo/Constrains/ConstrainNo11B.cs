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
    class ConstrainNo11B : IConstrain
    {
         public ConstrainNo11B()
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

                    Step s1 = cell.step;
                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "" + cell.product.Id + "" + s1.Id + LindoContainer.TjfiType))
                        continue;
                        //throw new ConstrainException("ConstrainNo11A", "Can't find parameter T" + cell.jobId + "" + cell.product.Id + "" + s1.Id + LindoContainer.TjfiType);
                    foreach (Step s2 in prob.StepsInProduct[cell.product])
                    {
                        if (s1.Equals(s2) || !prob.isStepSubsequentToStep(cell.product, s1, s2))
                            continue;
                        Constraint cons = prob.getConatraintBySteps(s1, s2, cell.product);
                        if (cons == null)
                            continue;
                        if (!LindoContainer.Instance.BooleanVariables.ContainsKey("Y" + cell.jobId + "," + cell.product.Id + "," + s1.Id + "," + mode.IdPerStep + LindoContainer.YjfimType))
                            continue;
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "," + cell.product.Id + "," + s2.Id + LindoContainer.TjfiType))
                            throw new ConstrainException("ConstrainNo11A", "Can't find parameter T" + cell.jobId + "," + cell.product.Id + "," + s2.Id + LindoContainer.TjfiType);

                        LindoContainer.Instance.Variables["T" + cell.jobId + "," + cell.product.Id + "," + s2.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        LindoContainer.Instance.Variables["T" + cell.jobId + "," + cell.product.Id + "," + s1.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);

                        Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + cell.jobId + "" + cell.product.Id + "" + s2.Id + LindoContainer.TjfiType + " - T" + cell.jobId + "" + cell.product.Id + "" + s1.Id + LindoContainer.TjfiType);

                        Console.WriteLine(" <= " + mode.getTotalProcessTime() + " + 9999" + cons.MaxQueueTime);
                        LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime() + 9999);//+ cons.MaxQueueTime);
                        LindoContainer.Instance.ConstraintsSenses.Add("L");
                        LindoContainer.Instance.ConstrainsCounter++;
                    }

                }
            }
/*
            for (int f = 0; f < prob.Products.Count; f++)
            {
                List<Job> jobs = prob.JobsInProduct[prob.Products[f]];
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s1 in prob.Steps)
                    {
                        foreach (Step s2 in prob.Steps)
                        {
                            if (s1.Equals(s2) || !prob.isStepSubsequentToStep(prob.Products[f], s1, s2))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s1.Id))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s2.Id))
                                continue;

                            LindoContainer.Instance.Variables["T" + j + "" + f + "" + s2.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + j + "" + f + "" + s1.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + j + "" + f + "" + s2.Id + " - T" + j + "" + f + "" + s1.Id);
                            foreach (Mode m in prob.ModesInStep[s1])
                            {
                                if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s1.Id + "" + m.name + LindoContainer.YjfimType))
                                    continue;
                                LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s1.Id + "" + m.name + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime());
                                Console.Write(" " + -1 * m.getTotalProcessTime() + "Y" + j + "" + f + "" + s1.Id + "" + m.name + LindoContainer.YjfimType);
                            }
                            Console.WriteLine(" <= Infinite");
                            LindoContainer.Instance.RightHandSideValues.Add(LindoContainer.Instance.N); // should be defined later as MAXLfi1i2
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
