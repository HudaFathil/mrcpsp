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
        public GenerateByOperation() : base()
        {
        }


        public override void  GenerateSolution(Solution solution)
        {
            Problem problem = ApplicManager.Instance.CurrentProblem;
            int distribution_count = problem.getTotalDistributionSize();
            int resource_count = problem.getNumberOfResources();

            fillAllModeListRandomly(solution, distribution_count);
        
            MatrixCell[] cells = createAllPossibleMatrixCells();
            int[] permutation = CommonFunctions.Instance.createPermutation(distribution_count);
            List<MatrixCell> items_to_sort = new List<MatrixCell>();
            for (int j = 0; j < distribution_count; j++)
            {
                items_to_sort.Add(cells[permutation[j] - 1]);
            }
            sortMatrixRow(items_to_sort);
                
            /*
            items_to_sort.Sort(new MatrixCellComparer<MatrixCell>());
            */
            for (int i = 0; i < resource_count; i++)
            {
                bool batch_resource = ApplicManager.Instance.CurrentProblem.isResourceByIndexIsBatch(i);
                for (int j = 0; j < distribution_count; j++)
                {                         
                    solution.DistributionMatrix[i, j] = new MatrixCell(items_to_sort[j]);
                    if (batch_resource)
                        updateBatchParam(solution.DistributionMatrix[i, j]);
                }
            }
     
        }   

        public override string ToString()
        {
            return "Generate Population by Operation";
        }
    }
}

  