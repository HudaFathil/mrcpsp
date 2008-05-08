using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;
using System.Collections;

namespace MRCPSP.Algorithm.CrossOver
{
    class OnePointCrossOver : CorssOverBase
    {

        private Random m_random;

        public OnePointCrossOver()
        {
            m_random = new Random();
        }

        public override List<Solution> doCrossOver(List<Solution> solutions)
        {
            List<KeyValuePair<Solution, Solution>> listOfPairs = createPairsForCrossOver(solutions);
            List<Solution> childrensAfterCrossover = new List<Solution>();
            foreach (KeyValuePair<Solution, Solution> p in listOfPairs)
            {
                KeyValuePair<Solution, Solution> a = performBiCrossing(p.Key, p.Value);
                childrensAfterCrossover.Add(a.Key);
                childrensAfterCrossover.Add(a.Value);
            }
            return childrensAfterCrossover;
        }

        private KeyValuePair<Solution, Solution> performBiCrossing(Solution sol1, Solution sol2) {
            Solution child1 = new Solution();
            Solution child2 = new Solution();
            ApplicManager.Instance.CurrentProblem.getTotalDistributionSize();           
            int crossPoint = m_random.Next(ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()-1) + 1;

            for (int i = 0; i < crossPoint; i++)
            {
                child1.SelectedModeList[i] = sol1.SelectedModeList[i];
                child2.SelectedModeList[i] = sol2.SelectedModeList[i];
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.getNumberOfResources(); j++)
                {
                    child1.DistributionMatrix[j, i] = sol1.DistributionMatrix[j, i];
                    child2.DistributionMatrix[j, i] = sol2.DistributionMatrix[j, i];
                }
            }
            for (int i = crossPoint; i < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize(); i++)
            {
                child2.SelectedModeList[i] = sol1.SelectedModeList[i];
                child1.SelectedModeList[i] = sol2.SelectedModeList[i];
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.getNumberOfResources(); j++)
                {

                    child2.DistributionMatrix[j, i] = getNextValueForChild(child1.getRowArray(j, 0, i), sol1.DistributionMatrix[j, i]);
                    child1.DistributionMatrix[j, i] = getNextValueForChild(child2.getRowArray(j, 0, i), sol2.DistributionMatrix[j, i]);

                }
            }

            return new KeyValuePair<Solution, Solution>(child1, child2);
        }

        /**
         * return the valueInParentMatrix if it doesn't exists in currentArray
         * else returns the minimum available value
         */
        private int getNextValueForChild(int[] currentArray, int valueInParentMatrix)
        {
            bool isAvailable = true;
            int minAvailable = 1;
            List<int> sortedArray = new List<int>();
            for (int i = 0; i < currentArray.Length; i++)
                sortedArray.Add(currentArray[i]);
             
            sortedArray.Sort();

            foreach (int el in sortedArray)
            {
                if (el == minAvailable)
                    minAvailable++;

                if (el == valueInParentMatrix)
                    isAvailable = false;
            }
            
            if (isAvailable)
                return valueInParentMatrix;
            return minAvailable;
        }

        public override string ToString()
        {
            return "One Point Crossover";
        }
    }
}
