using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Controllers;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Algorithm.FirstGeneration
{
    abstract class GeneratePolicyBase
    {
        private Random m_random;

        public GeneratePolicyBase()
        {
            m_random = new Random();
        }

        abstract public void GenerateSolution(Solution solution);

        protected MatrixCell[] createAllPossibleMatrixCells()
        {
            int counter = 0;
            MatrixCell[] cells = new MatrixCell[ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()];
            for (int p = 0; p < ApplicManager.Instance.CurrentProblem.Products.Count; p++)
            {
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.Products[p].Size; j++)
                {
                    List<Step> steps = ApplicManager.Instance.CurrentProblem.StepsInProduct[ApplicManager.Instance.CurrentProblem.Products[p]];
                    for (int s = 0; s < steps.Count; s++)
                    {
                        cells[counter] = new MatrixCell(ApplicManager.Instance.CurrentProblem.Products[p], j, steps[s]);
                        counter++;
                    }
                }
            }
            return cells;
        }
                   
        protected void fillAllModeListRandomly(Solution solution, int distribution_count) 
        {
            Problem problem = ApplicManager.Instance.CurrentProblem;
            for (int i = 0; i < distribution_count; i++)
            {
                int available_modes = problem.getNumberOfModesById(i);
                solution.SelectedModeList[i] = m_random.Next(available_modes) + 1;
            }
        }

        protected void sortMatrixRow(List<MatrixCell> items_to_sort)
        {
            MatrixCellComparer<MatrixCell> compare = new MatrixCellComparer<MatrixCell>();
            for (int m = 0; m < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize() - 1; m++)
            {
                MatrixCell toCheckFirst = items_to_sort[m];
                for (int n = m + 1; n < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize(); n++)
                {
                    MatrixCell toCheckSecond = items_to_sort[n];
                    if (compare.Compare(toCheckFirst, toCheckSecond) > 0)
                    {
                        MatrixCell temp = toCheckFirst;
                        items_to_sort[m] = toCheckSecond;
                        items_to_sort[n] = temp;
                        m--;
                        break;
                    }
                }
            }
        }

        protected void updateBatchParam(MatrixCell cell) 
        {
            cell.doBatch = (m_random.Next(100) > 50);            
        }
    }
}
