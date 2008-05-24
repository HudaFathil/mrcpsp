using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo3 : IConstrain
    {
        public ConstrainNo3()
            : base()
        {
        }

        public override void createConstrain(Solution sol, Problem prob)
        {
            int task = 0;
            for (int f = 0; f < prob.Products.Count; f++)
            {
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.Steps)
                    {

                        foreach (Mode m in prob.ModesInStep[s])
                        {

                            for (int r = 0; r < prob.Resources.Count; r++)
                            {
                                if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + task))
                                    continue;
                                LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + task].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                                LindoContainer.Instance.RightHandSideValues.Add(1.0);
                                LindoContainer.Instance.ConstraintsSenses.Add("L");
                                LindoContainer.Instance.ConstrainsCounter++;
                            }

                        }
                        task++;
                    }
                }
            }
        }
    }
}
