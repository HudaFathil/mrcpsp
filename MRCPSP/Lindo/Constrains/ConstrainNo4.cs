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
    class ConstrainNo4 : IConstrain
    {
        public ConstrainNo4()
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
                    Mode mode = sol.getSelectedModeByCell(cell);
                    if (!LindoContainer.Instance.Variables.ContainsKey("Y" + cell.step.Id + "" + mode.name + "" + r + "" + t + LindoContainer.YimrlType))
                        throw new ConstrainException("ConstrainNo4", "Can't find parameter " + "Y" + cell.step.Id + "" + mode.name + "" + r + "" + t + LindoContainer.YimrlType);

                    LindoContainer.Instance.Variables["Y" + cell.step.Id + "" + mode.name + "" + r + "" + t + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + cell.step.Id + "" + mode.name + "" + r + "" + t + LindoContainer.YimrlType);
                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + cell.step.Id + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t))
                        throw new ConstrainException("ConstrainNo4", "Can't find parameter " + "X" + cell.step.Id + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t);
                    Console.Write(" + " + cell.product.Size + "*X" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t);
                    LindoContainer.Instance.Variables["X" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, cell.product.Size * 1); // instead of *1 should be added later * Krf
                    
                    Console.WriteLine(" >= 0");
                    LindoContainer.Instance.RightHandSideValues.Add(0);
                    LindoContainer.Instance.ConstraintsSenses.Add("G");
                    LindoContainer.Instance.ConstrainsCounter++;


                    LindoContainer.Instance.Variables["Y" + cell.step.Id + "" + mode.name + "" + r + "" + t + LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + cell.step.Id + "" + mode.name + "" + r + "" + t + LindoContainer.YimrlType);
                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + cell.step.Id + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t))
                        throw new ConstrainException("ConstrainNo4", "Can't find parameter " + "X" + cell.step.Id + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t);
                    Console.Write(" + " + cell.product.Size + "*X" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t);
                    LindoContainer.Instance.Variables["X" + cell.jobId + "" + cell.product.Id + "" + cell.step.Id + "" + mode.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, cell.product.Size * 1); // instead of *1 should be added later * Krf

                    Console.WriteLine(" <= 1");
                    LindoContainer.Instance.RightHandSideValues.Add(1);
                    LindoContainer.Instance.ConstraintsSenses.Add("L");
                    LindoContainer.Instance.ConstrainsCounter++;

                }
            }

            /*
                
            for (int r = 0; r < prob.Resources.Count; r++) 
            {
                List<Step> sList = sol.getStepsInResource(r, prob.Resources[r]);
                List<int> tList = sol.getTaskListForResource(r, prob.Resources[r]);
                foreach (Step s in sList)
                {
                    foreach (Mode m in prob.ModesInStep[s])
                    {
                        foreach (int t in tList)
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + t + LindoContainer.YimrlType))
                                continue;

                            LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + t+LindoContainer.YimrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.Write ("Constrain No "+ LindoContainer.Instance.ConstrainsCounter +") Y"+ s.Id + "" + m.name + "" + r + "" + t+LindoContainer.YimrlType);
                            for (int f = 0; f < prob.Products.Count; f++)
                            {
                                for (int j = 0; j < prob.Products[f].Size; j++)
                                {
                                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t))
                                        continue;
                                    Console.Write(" + "+prob.Products[f].Size+"*X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t);
                                    LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, prob.Products[f].Size * 1 ); // instead of *1 should be added later * Krf
                                }
                            }
                            Console.WriteLine(" >= 0");
                            LindoContainer.Instance.RightHandSideValues.Add(0); 
                            LindoContainer.Instance.ConstraintsSenses.Add("G");
                            LindoContainer.Instance.ConstrainsCounter++;
                        }

                        foreach (int t in tList)
                        {
                            if (!LindoContainer.Instance.Variables.ContainsKey("Y" + s.Id + "" + m.name + "" + r + "" + t))
                                continue;

                            LindoContainer.Instance.Variables["Y" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                            Console.Write("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") Y" + s.Id + "" + m.name + "" + r + "" + t);
                            for (int f = 0; f < prob.Products.Count; f++)
                            {
                                for (int j = 0; j < prob.Products[f].Size; j++)
                                {
                                    if (!LindoContainer.Instance.Variables.ContainsKey("X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t))
                                        continue;
                                    Console.Write(" + X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t);
                                    LindoContainer.Instance.Variables["X" + j + "" + f + "" + s.Id + "" + m.name + "" + r + "" + t].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, prob.Products[f].Size * 1); // instead of *1 should be added later * Krf
                                }
                            }
                            Console.WriteLine(" >= 1");
                            LindoContainer.Instance.RightHandSideValues.Add(1); // should be replaced later with Kr
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
