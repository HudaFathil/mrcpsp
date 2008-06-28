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

        public GenerateRandomPopulation() : base()
        {
        }


        public override void  GenerateSolution(Solution solution)
        {
            Problem problem = ApplicManager.Instance.CurrentProblem;
            int distribution_count = problem.getTotalDistributionSize();
            int resource_count = problem.getNumberOfResources();

            fillAllModeListRandomly(solution, distribution_count);
                      
            for (int i = 0; i < resource_count; i++)
            {           
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
                for (int j = 0; j < distribution_count; j++)
                {
                    solution.DistributionMatrix[i, j] = items_to_sort[j];
                }
         
            }

           
        }

      
        public override string ToString()
        {
            return "Generate Population by Resource";
        }
    }
}
