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
            
          

            MatrixCell[] cells = createAllPossibleMatrixCells();
                for (int i = 0; i < resource_count; i++)
                {
                    int[] permutation = CommonFunctions.Instance.createPermutation(distribution_count);
                    for (int j = 0; j < distribution_count; j++)
                    {                       
                        solution.DistributionMatrix[i, j] = cells[permutation[j]];
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
                        int mode_id = m_random.Next(ApplicManager.Instance.CurrentProblem.ModesInStep[steps[s]].Count) + 1;
                        cells[counter] = new MatrixCell(ApplicManager.Instance.CurrentProblem.Products[p], j, steps[s], ApplicManager.Instance.CurrentProblem.ModesInStep[steps[s]][mode_id]);
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
