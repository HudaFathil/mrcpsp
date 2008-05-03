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
            
            for (int i=0; i < distribution_count; i++) 
            {
                int available_modes = problem.getNumberOfModesById(i);                    
                solution.SelectedModeList[i] =  m_random.Next(available_modes)+1;
            }

           
                for (int i = 0; i < resource_count; i++)
                {
                    int[] permutation = CommonFunctions.Instance.createPermutation(distribution_count);
                    for (int j = 0; j < distribution_count; j++)
                    {
                        solution.DistributionMatrix[i, j] = permutation[j];
                    }
                }
           
        }   
      
    }
}
