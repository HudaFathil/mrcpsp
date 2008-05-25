using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.Controllers;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo14 : IConstrain
    {
        public ConstrainNo14()
            : base()
        {
        } 

        public override void createConstrain(Solution sol, Problem prob)
        {
           /// int task = 0;
            for (int f = 0; f < prob.Products.Count; f++)
            {
                List<Job> jobs = prob.JobsInProduct[prob.Products[f]];
                for (int j = 0; j < prob.Products[f].Size; j++)
                {
                    for (int r = 0; r < prob.getNumberOfResources(); r++) 
                    {
                        List<int> taskList = LindoContainer.Instance.getTaskListForResource(prob.Resources[r]);
                        if (LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[0]))
                        {
                            LindoContainer.Instance.Variables["T" + r + "" + taskList[0]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[0]+" >= 0");
                            // should add here Zr0 , Vr0
                            LindoContainer.Instance.RightHandSideValues.Add(0.0);
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }
                        for (int t = 1 ; t < taskList.Count ; t++) 
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[t]))
                                    continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[t-1]))
                                continue;
                                
                            LindoContainer.Instance.Variables["T" + r + "" + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                            LindoContainer.Instance.Variables["T" + r + "" + taskList[t-1]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[t] + " -T" + r + "" + taskList[t - 1]);
                            foreach (Step s in prob.Steps)
                            {
                                foreach (Mode m in prob.ModesInStep[s])
                                {
                                    if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[t-1]))
                                        continue;
                                    Console.Write(-1 * m.getTotalProcessTime(prob.Resources[r])+"*Y" + s.Id + "" + m.name + "" + r + "" + taskList[t - 1]);
                                    LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[t-1]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime(prob.Resources[r]));
                                }

                                
                               
                            }
                            Console.WriteLine(" >= 0");
                            // should add here Zrl , Vrl
                            LindoContainer.Instance.RightHandSideValues.Add(0.0);
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }
                      }
                      
                  }
            }
        }

    }
}
