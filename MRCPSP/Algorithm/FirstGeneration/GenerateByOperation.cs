using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Controllers;
using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using MRCPSP.Util;

namespace MRCPSP.Algorithm.FirstGeneration
{
    class GenerateByOperation: GeneratePolicyBase
    {

        private Random m_random;
        public GenerateByOperation() : base()
        {
            m_random = new Random();
        }


        public override void  GenerateSolution(Solution solution)
        {
            Problem problem = ApplicManager.Instance.CurrentProblem;
            int distribution_count = problem.getTotalDistributionSize();
            int resource_count = problem.getNumberOfResources();

            for (int i = 0; i < distribution_count; i++)
            {
                int available_modes = problem.getNumberOfModesById(i);
                solution.SelectedModeList[i] = m_random.Next(available_modes) + 1;
            }
        
            MatrixCell[] cells = createAllPossibleMatrixCells();
            int[] permutation = CommonFunctions.Instance.createPermutation(distribution_count);
            List<MatrixCell> items_to_sort = new List<MatrixCell>();
            for (int j = 0; j < distribution_count; j++)
            {
                items_to_sort.Add(cells[permutation[j] - 1]);
            }
            MatrixCellComparer<MatrixCell> compare =  new MatrixCellComparer<MatrixCell>();
            for (int m = 0; m < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize() - 1; m++)
            {
                MatrixCell toCheckFirst = items_to_sort[m];
                for (int n = m + 1; n < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize(); n++)
                {
                    MatrixCell toCheckSecond = items_to_sort[n];
                    if (compare.Compare(toCheckFirst,toCheckSecond) > 0)
                    {
                        MatrixCell temp = toCheckFirst;
                        items_to_sort[m] = toCheckSecond;
                        items_to_sort[n] = temp;
                        m--;
                        break;
                    }
                }
            }
                
            /*
            items_to_sort.Sort(new MatrixCellComparer<MatrixCell>());
            */
            for (int j = 0; j < distribution_count; j++)
            {
                for (int i = 0; i < resource_count; i++)
                {           
                    solution.DistributionMatrix[i, j] = new MatrixCell(items_to_sort[j]);
                }
            }
     
        }   

        private MatrixCell[] createAllPossibleMatrixCells()
        {
            int counter =0;
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

        public override string ToString()
        {
            return "Generate Population by Operation";
        }
    }
}

  