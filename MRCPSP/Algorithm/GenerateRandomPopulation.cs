using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.Controllers;
using MRCPSP.CommonTypes;

namespace MRCPSP.Algorithm
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
            for (int i = 0; i < resource_count; i++)
            {
                int[] permutation = createPermutation(distribution_count);
                for (int j=0; j < distribution_count; j++) 
                {
                    solution.DistributionMatrix[i,j] = permutation[j];
                }
            }
            Random rand = new Random();   
            for (int i=0; i < distribution_count; i++) 
            {
                int available_modes = problem.getNumberOfModesById(i);                    
                solution.SelectedModeList[i] =  rand.Next(available_modes);
            }
        }

        private int[] createPermutation(int n) // 1 .. n
        {
            Random rand = new Random();
            System.Collections.ArrayList numbers = new System.Collections.ArrayList();
            int[] permutation = new int[n];

            for (int i = 1; i <= permutation.Length; i++)
            {
                numbers.Add(i);
            }
            for (int i = 0; i < permutation.Length; i++)
            {
                int o = rand.Next(numbers.Count);
                permutation[i] = (int)numbers[o];
                numbers.RemoveAt(o);
            }
            return permutation;
        }
      
    }
}
