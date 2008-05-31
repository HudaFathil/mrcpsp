using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.Controllers;
using MRCPSP.Exceptions;

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

            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                if (taskList.Count == 0)
                    continue;

                LindoContainer.Instance.Variables["T" + r + "" + taskList[0]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[0] + " >= 0");
                // should add here Zr0 , Vr0
                LindoContainer.Instance.RightHandSideValues.Add(0.0);
                LindoContainer.Instance.ConstraintsSenses.Add("G");
                LindoContainer.Instance.ConstrainsCounter++;

                for (int t = 1; t < taskList.Count; t++)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, taskList[t - 1]];
                    Mode m = sol.getSelectedMode(cell, taskList[t - 1]);
                    int mode = sol.SelectedModeList[taskList[t - 1]];

                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[t]))
                        continue;
                    if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[t - 1]))
                        continue;
                    if (!LindoContainer.Instance.Variables.ContainsKey("Y" + cell.step.Id + "" + mode + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType))
                        continue;
                    LindoContainer.Instance.Variables["T" + r + "" + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    LindoContainer.Instance.Variables["T" + r + "" + taskList[t - 1]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[t] + " -T" + r + "" + taskList[t - 1]);


                    //throw new ConstrainException("ConstrainNo14", "Can't find parameter" + "Y" + cell.step.Id + "" + mode + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType);
                    Console.Write(-1 * m.getTotalProcessTime(prob.Resources[r]) + "*Y" + cell.step.Id + "" + mode + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType);
                    LindoContainer.Instance.Variables["Y" + cell.step.Id + "" + mode + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime(prob.Resources[r]));
                    Console.WriteLine(" >= 0");
                    // should add here Zrl , Vrl
                    LindoContainer.Instance.RightHandSideValues.Add(0.0);
                    LindoContainer.Instance.ConstraintsSenses.Add("G");
                    LindoContainer.Instance.ConstrainsCounter++;
                }
            }

            /*
           /// int task = 0;
            for (int f = 0; f < prob.Products.Count; f++)
            {
                for (int r = 0; r < prob.getNumberOfResources(); r++)
                {
                    List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                    if (taskList.Count == 0)
                        continue;
                    if (LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[0]))
                    {
                        LindoContainer.Instance.Variables["T" + r + "" + taskList[0]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[0] + " >= 0");
                        // should add here Zr0 , Vr0
                        LindoContainer.Instance.RightHandSideValues.Add(0.0);
                        LindoContainer.Instance.ConstraintsSenses.Add("G");
                        LindoContainer.Instance.ConstrainsCounter++;
                    }
                    for (int t = 1; t < taskList.Count; t++)
                    {
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[t]))
                            continue;
                        if (!LindoContainer.Instance.Variables.ContainsKey("T" + r + "" + taskList[t - 1]))
                            continue;

                        LindoContainer.Instance.Variables["T" + r + "" + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                        LindoContainer.Instance.Variables["T" + r + "" + taskList[t - 1]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                        Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[t] + " -T" + r + "" + taskList[t - 1]);
                        foreach (Step s in sol.getStepsInResource(r, prob.Resources[r]))
                        {
                            foreach (Mode m in prob.ModesInStep[s])
                            {
                                if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType))
                                    continue;
                                Console.Write(-1 * m.getTotalProcessTime(prob.Resources[r]) + "*Y" + s.Id + "" + m.name + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType);
                                LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1 * m.getTotalProcessTime(prob.Resources[r]));
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
            */
        }

    }
}
