﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Algorithm;
using MRCPSP.Domain;
using MRCPSP.Exceptions;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo11A : IConstrain
    {
        public ConstrainNo11A()
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
                    Step s1 = cell.step;
                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "" + cell.product.Id + "" + s1.Id))
                        throw new ConstrainException("ConstrainNo11A", "Can't find parameter T" + cell.jobId + "" + cell.product.Id + "" + s1.Id);
                    foreach (Step s2 in prob.StepsInProduct[cell.product])
                    {
                        if (s1.Equals(s2) || !prob.isStepSubsequentToStep(cell.product, s1, s2))
                            continue;
                        if (!LindoContainer.Instance.Variables.ContainsKey("Y" + cell.jobId + "" + cell.product.Id + "" + s1.Id + "" + mode + LindoContainer.YjfimType))
                            continue;
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + cell.jobId + "" + cell.product.Id + "" + s2.Id))
                            throw new ConstrainException("ConstrainNo11A", "Can't find parameter T" + cell.jobId + "" + cell.product.Id + "" + s2.Id);
                        
                        LindoContainer.Instance.Variables["T" + cell.jobId + "" + cell.product.Id + "" + s2.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        LindoContainer.Instance.Variables["T" + cell.jobId + "" + cell.product.Id + "" + s1.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);

                        Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + cell.jobId + "" + cell.product.Id + "" + s2.Id + " - T" + cell.jobId + "" + cell.product.Id + "" + s1.Id);
                        LindoContainer.Instance.Variables["Y" + cell.jobId + "" + cell.product.Id + "" + s1.Id + "" + mode + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime());
                        Console.Write(" " + -1 * m.getTotalProcessTime() + "Y" + cell.jobId + "" + cell.product.Id + "" + s1.Id + "" + mode + LindoContainer.YjfimType);

                        //List<Job> jobs = prob.JobsInProduct[cell.product];
                        Console.WriteLine(" >= 0");
                        LindoContainer.Instance.RightHandSideValues.Add(0); // should be defined later as MINLfi1i2
                        LindoContainer.Instance.ConstraintsSenses.Add("G");
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
                            if (s1.Equals(s2) || ! prob.isStepSubsequentToStep(prob.Products[f],s1,s2))
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
                                if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s1.Id + "" + m.name+LindoContainer.YjfimType))
                                    continue;
                                LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s1.Id + "" + m.name + LindoContainer.YjfimType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime());
                                Console.Write(" " + -1 * m.getTotalProcessTime() + "Y" + j + "" + f + "" + s1.Id + "" + m.name + LindoContainer.YjfimType);
                            }
                            Console.WriteLine(" >= 0");
                            LindoContainer.Instance.RightHandSideValues.Add(0); // should be defined later as MINLfi1i2
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }
                    }

                }
            }
            */
        }
    }
}
