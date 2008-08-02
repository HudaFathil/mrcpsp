using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.Controllers;
using MRCPSP.Exceptions;
using MRCPSP.Log;

namespace MRCPSP.Lindo.Constrains
{
    class ConstrainNo14 : IConstrain
    {
        private Dictionary<Mode,int> m_modeBatchHash;
        public ConstrainNo14()
            : base()
        {
            m_modeBatchHash = new Dictionary<Mode, int>();
        } 

        public override void createConstrain(Solution sol, Problem prob)
        {

            for (int r = 0; r < sol.DistributionMatrix.GetLength(0); r++)
            {
                List<int> taskList = sol.getTaskListForResource(r, prob.Resources[r]);
                if (taskList.Count == 0)
                    continue;

                LindoContainer.Instance.Variables["T" + r + "," + taskList[0]+LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "," + taskList[0] +LindoContainer.TrlType+ " >= 0");
                // should add here Zr0 , Vr0
                LindoContainer.Instance.RightHandSideValues.Add(0.0);
                LindoContainer.Instance.ConstraintsSenses.Add("G");
                LindoContainer.Instance.ConstrainsCounter++;

                for (int t = 1; t < taskList.Count; t++)
                {
                    MatrixCell cell = sol.DistributionMatrix[r, taskList[t - 1]];
                    MatrixCell nextCell = sol.DistributionMatrix[r, taskList[t]];
                    Mode mode = sol.getSelectedModeByCell(cell);
                    Mode nextMode = sol.getSelectedModeByCell(nextCell);
                    LindoContainer.Instance.Variables["T" + r + "," + taskList[t] + LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, 1.0);
                    LindoContainer.Instance.Variables["T" + r + "," + taskList[t - 1] + LindoContainer.TrlType].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    LindoContainer.Instance.Variables["Z" + r + "," + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    LindoContainer.Instance.Variables["V" + r + "," + taskList[t]].AddCoefficient(LindoContainer.Instance.ConstrainsCounter, -1.0);
                    if (cell.doBatch && nextCell.doBatch &&
                        nextCell.step.Equals(cell.step) &&
                        nextMode.Equals(mode))
                    {
                        if (!m_modeBatchHash.ContainsKey(mode))
                            m_modeBatchHash.Add(mode, 1);
                        if (m_modeBatchHash[mode] < mode.getMaximumBatchSize())
                        {
                            m_modeBatchHash[mode]++;
                            Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[t] + LindoContainer.TrlType + " -T" + r + "," + taskList[t - 1] + LindoContainer.TrlType + " - Z" + r + "," + taskList[t] + " >= " + 0);
                            LindoContainer.Instance.RightHandSideValues.Add(0);
                        }
                        else
                        {
                            Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[t] + LindoContainer.TrlType + " -T" + r + "," + taskList[t - 1] + LindoContainer.TrlType + " - Z" + r + "," + taskList[t] + " >= " + mode.getTotalProcessTime(prob.Resources[r]));
                            LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime(prob.Resources[r]));
                        }

                    }
                    else
                    {
                        Logger.Instance.debug("Constrain No " + LindoContainer.Instance.ConstrainsCounter + ") T" + r + "" + taskList[t] + LindoContainer.TrlType + " -T" + r + "," + taskList[t - 1] + LindoContainer.TrlType + " - Z" + r + "," + taskList[t] + " >= " + mode.getTotalProcessTime(prob.Resources[r]));
                        LindoContainer.Instance.RightHandSideValues.Add(mode.getTotalProcessTime(prob.Resources[r]));
                    }
                    LindoContainer.Instance.ConstraintsSenses.Add("G");
                    LindoContainer.Instance.ConstrainsCounter++;
                }
            }

           
        }

    }
}
