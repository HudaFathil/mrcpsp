using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Algorithm;
using MRCPSP.Domain;

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
            for (int f = 0; f < prob.Products.Count; f++)
            {
                List<Job> jobs = prob.JobsInProduct[prob.Products[f]];
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s2 in prob.Steps)
                    {
                        foreach (Step s1 in prob.Steps)
                        {
                            if (s1.Equals(s2))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s1.Id))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s2.Id))
                                continue;
                            LindoContainer.Instance.Variables["T" + j + "" + f + "" + s1.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + j + "" + f + "" + s2.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + j + "" + f + "" + s1.Id + " - T" + j + "" + f + "" + s2.Id);
                            foreach (Mode m in prob.ModesInStep[s1])
                            {
                                if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s1.Id + "" + m.name))
                                    continue;
                                LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s1.Id + "" + m.name].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime());
                                Console.Write(" -Y " + j + "" + f + "" + s1.Id + "" + m.name);
                            }
                            Console.WriteLine(" <= Infinite");
                            LindoContainer.Instance.RightHandSideValues.Add(lindo.LS_INFINITY); // should be defined later as MAXLfi1i2
                            LindoContainer.Instance.ConstraintsSenses.Add("L");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }
                    }

                }
            }
        }

    }
}
