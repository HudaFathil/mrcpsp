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
    class ConstrainNo10 : IConstrain
    {

        public ConstrainNo10()
            : base()
        {
        }


        public override void createConstrain(Solution sol, Problem prob)
        {

            foreach (Product f in prob.Products)
            {
                foreach (Step s in prob.StepsInProduct[f])
                {
                    for (int j = 0; j < prob.JobsInProduct[f].Count; j++)
                    {
                        List<Job> jobs = prob.JobsInProduct[f];
                        Mode mode = sol.getSelectedModeByCell(f, s, j);

                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + j+ "," + f.Id + "," + s.Id + LindoContainer.TjfiType))
                            continue;
                        //throw new ConstrainException("ConstrainNo10", "Can't find parameter " + "T" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + LindoContainer.TjfiType);
                        LindoContainer.Instance.Variables["T" + j + "," + f.Id + "," + s.Id + LindoContainer.TjfiType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ")T" + j + "," + f.Id + "," + s.Id + LindoContainer.TjfiType + " <= " + jobs[j].LatestTermTime + " + " + mode.getTotalProcessTime());
                        LindoContainer.Instance.RightHandSideValues.Add(Convert.ToDouble(jobs[j].LatestTermTime) + mode.getTotalProcessTime());
                        LindoContainer.Instance.ConstraintsSenses.Add("L");
                        LindoContainer.Instance.ConstrainsCounter++;
                    }
                }
            }



        }
    }
}
