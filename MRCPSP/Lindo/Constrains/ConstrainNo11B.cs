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
    class ConstrainNo11B : IConstrain
    {
        public ConstrainNo11B()
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
                            Mode mode = sol.getSelectedModeByCell(f, s1, j);
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "," + f.Id + "," + s1.Id + LindoContainer.TjfiType))
                                continue;

                            if (s1.Equals(s2) || !prob.isStepSubsequentToStep(f, s1, s2))
                                continue;
                            Constraint cons = prob.getConatraintBySteps(s1, s2, f);
                            if (cons == null)
                                continue;
                            if (!LindoContainer.Instance.BooleanVariables.ContainsKey("Y" + j + "," + f.Id + "," + s1.Id + "," + mode.IdPerStep + LindoContainer.YjfimType))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType))
                                throw new ConstrainException("ConstrainNo11A", "Can't find parameter T" + j + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType);

                            LindoContainer.Instance.Variables["T" + j + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + j + "," + f.Id + "," + s1.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);

                            Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + j + "," + f.Id + "," + s2.Id + LindoContainer.TjfiType + " - T" + j + "," + f.Id + "," + s1.Id + LindoContainer.TjfiType + " <= " + mode.getTotalProcessTime() + " + 9999" + cons.MaxQueueTime);
                            LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime() + 9999);//+ cons.MaxQueueTime);
                            LindoContainer.Instance.ConstraintsSenses.Add("L");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }

                    }

                }
            }


        }

    }
}
