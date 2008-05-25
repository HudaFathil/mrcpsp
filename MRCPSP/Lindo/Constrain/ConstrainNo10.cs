using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Algorithm;
using MRCPSP.Domain;

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
            for (int f = 0; f < prob.Products.Count; f++)
            {
                List<Job> jobs = prob.JobsInProduct[prob.Products[f]];
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.Steps)
                    {
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s.Id))
                            continue;
                        LindoContainer.Instance.Variables["T" + j + "" + f + "" + s.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + m.name))
                                continue;
                            LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + m.name].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, m.getTotalProcessTime());
                        }

                    }

                    LindoContainer.Instance.RightHandSideValues.Add(jobs[j].LatestTermTime);
                    LindoContainer.Instance.ConstraintsSenses.Add("L");
                    LindoContainer.Instance.ConstrainsCounter++;
                }
            }
        }
    }
}
