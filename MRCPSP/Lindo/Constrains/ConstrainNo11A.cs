﻿using System;
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
    class ConstrainNo11A : IConstrain
    {
        public ConstrainNo11A()
            : base()
        {
        }

        public override void createConstrain(Solution sol, Problem prob)
        {
            foreach (Product f in prob.Products)
            {
                foreach (Step s1 in prob.StepsInProduct[f])
                {
                    //throw new ConstrainException("ConstrainNo11A", "Can't find parameter T" + cell.jobId + "" + cell.product.Id + "" + s1.Id + LindoContainer.TjfiType);
                    foreach (Step s2 in prob.StepsInProduct[f])
                    {
                        for (int j = 0; j < prob.JobsInProduct[f].Count; j++)
                        {
                            List<Job> jobs = prob.JobsInProduct[f];
                            Mode mode = sol.getSelectedModeByCell(f, s1, j);
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + jobs[j].Id + "," + f.Id + "," + s1.Id + LindoContainer.TjfiType))
                                continue;

                            if (s1.Equals(s2) || !prob.isStepSubsequentToStep(f, s1, s2))
                                continue;
                            Constraint cons = prob.getConatraintBySteps(s1, s2, f);
                            if (cons == null)
                                continue;
                            if (!LindoContainer.Instance.BooleanVariables.ContainsKey("Y" + jobs[j].Id + "," + f.Id + "," + s1.Id + "," + mode.IdPerStep + LindoContainer.YjfimType))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + jobs[j].Id + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType))
                                throw new ConstrainException("ConstrainNo11A", "Can't find parameter T" + jobs[j].Id + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType);

                            LindoContainer.Instance.Variables["T" + jobs[j].Id + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + jobs[j].Id + "," + f.Id + "," + s1.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);

                            Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + jobs[j].Id + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType + " - T" + jobs[j].Id + "," + f.Id + "," + s1.Id + LindoContainer.TjfiType + " >= " + mode.getTotalProcessTime() + " + " + cons.MinQueueTime);
                            LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime() + cons.MinQueueTime);
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }

                    }

                }
            }



        }
    }
}
