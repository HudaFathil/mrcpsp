﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;

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
        }
    }
}
