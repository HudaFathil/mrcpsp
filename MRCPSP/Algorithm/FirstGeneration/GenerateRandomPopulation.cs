using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.Controllers;
using MRCPSP.CommonTypes;
using MRCPSP.Util;

namespace MRCPSP.Algorithm.FirstGeneration
{

    class GenerateRandomPopulation : GeneratePolicyBase
    {

        private Random m_random;
        public GenerateRandomPopulation() : base()
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
                      
            for (int i = 0; i < resource_count; i++)
            {           
                MatrixCell[] cells = createAllPossibleMatrixCells();
                int[] permutation = CommonFunctions.Instance.createPermutation(distribution_count);
                List<MatrixCell> items_to_sort = new List<MatrixCell>();
                for (int j = 0; j < distribution_count; j++)
                {
                    items_to_sort.Add(cells[permutation[j] - 1]);
                }
                items_to_sort.Sort(new MatrixCellComparer<MatrixCell>());
                for (int j = 0; j < distribution_count; j++)
                {
                    solution.DistributionMatrix[i, j] = items_to_sort[j];
                }
            }

           
        }

        private void fixSolutionConstraints(Solution solution)
        {
            
            /*
            Problem problem = ApplicManager.Instance.CurrentProblem;
            int distribution_count = problem.getTotalDistributionSize();
            int resource_count = problem.getNumberOfResources();
            for (int i = 0; i < resource_count; i++)
            {
                bool not_valid = true;
                while (not_valid)
                {
                    for (int j = 0; j < distribution_count; j++)
                    {

                    }
                }
            }
              */
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
            return "Random Population";
        }
    }
}
