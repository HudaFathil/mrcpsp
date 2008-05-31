using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Algorithm;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Exceptions;

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


            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                for (int t = 0; t < sol.DistributionMatrix.GetLength(1); t++)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, t];
                    int mode = sol.SelectedModeList[t];
                    if (!LindoContainer.Instance.Variables.ContainsKey("Y" + cell.step.Id + "" + mode + "" + r + "" + t + LindoContainer.YimrlType))
                        throw new ConstrainException("ConstrainNo1", "Can't find parameter " + "Y" + cell.step.Id + "" + mode + "" + r + "" + t + LindoContainer.YimrlType);

                    Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + cell.step.Id + "" + mode + "" + r + "" + t + LindoContainer.YimrlType + " = 1");
                    LindoContainer.Instance.Variables["Y" + cell.step.Id + "" + mode + "" + r + "" + t + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                    LindoContainer.Instance.RightHandSideValues.Add(1.0);
                    LindoContainer.Instance.ConstraintsSenses.Add("E");
                    LindoContainer.Instance.ConstrainsCounter++;
                    

                }
            }

            /*
            for (int r = 0; r < prob.Resources.Count; r++)
            {
                foreach (Step s in sol.getStepsInResource(r, prob.Resources[r]))
                {

                    foreach (Mode m in prob.ModesInStep[s])
                    {

                        int task = sol.getTaskNumber(r, prob.Resources[r], s);
                        if (task == -1)
                            throw new ConstrainException("ConstrainNo3", "Can't get task number for parameter Y" + s.Id + "" + m.name + "" + r + "" + task + LindoContainer.YimrlType);
                        LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + task + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                        LindoContainer.Instance.RightHandSideValues.Add(1.0);
                        LindoContainer.Instance.ConstraintsSenses.Add("E");
                        LindoContainer.Instance.ConstrainsCounter++;
                        Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + s.Id + "" + m.name + "" + r + "" + task + LindoContainer.YimrlType + " = 1");
                        
                        if (LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[0] + LindoContainer.YimrlType))
                        {
                            LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[0] + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                            LindoContainer.Instance.RightHandSideValues.Add(1.0);
                            LindoContainer.Instance.ConstraintsSenses.Add("L");
                            LindoContainer.Instance.ConstrainsCounter++;
                            Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + s.Id + "" + m.name + "" + r + "" + taskList[0] + LindoContainer.YimrlType + " <= 1");
                        }
                        for (int t = 1; t < taskList.Count; t++)
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[t] + LindoContainer.YimrlType))
                                continue;
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType))
                                continue;
                            LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[t] + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1);
                            LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1);
                            Console.WriteLine("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + s.Id + "" + m.name + "" + r + "" + taskList[t] + LindoContainer.YimrlType + "-Y" + s.Id + "" + m.name + "" + r + "" + taskList[t - 1] + LindoContainer.YimrlType + " <= 0");
                            LindoContainer.Instance.RightHandSideValues.Add(0.0);
                            LindoContainer.Instance.ConstraintsSenses.Add("L");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }
                        


                    }


                }
            }
             */
        }
    }
}
