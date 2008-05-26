using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;

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
            LindoContainer.Instance.Variables["F"].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
            Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") F");
            for (int f = 0; f < prob.Products.Count; f++)
            {
                List<Job> jobs = prob.JobsInProduct[prob.Products[f]];
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    foreach (Step s in prob.Steps)
                    {
                        if (!LindoContainer.Instance.getFinishSteps(f).Contains(s))
                            continue;
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + j + "" + f + "" + s.Id))
                            continue;
                        Console.Write(" -T" + j + "" + f + "" + s.Id);
                        LindoContainer.Instance.Variables["T" + j + "" + f + "" + s.Id].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                        foreach (Mode m in prob.ModesInStep[s])
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + j + "" + f + "" + s.Id + "" + m.name))
                                continue;
                            Console.Write(" " + -1 * m.getTotalProcessTime() + "Y" + j + "" + f + "" + s.Id + "" + m.name);
                            LindoContainer.Instance.Variables["Y" + j + "" + f + "" + s.Id + "" + m.name].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime());
                        }

                    }

                   
                }
            }
            Console.WriteLine(" >= 0");
            LindoContainer.Instance.RightHandSideValues.Add(0.0);
            LindoContainer.Instance.ConstraintsSenses.Add("G");
            LindoContainer.Instance.ConstrainsCounter++;
        }

    }
}
