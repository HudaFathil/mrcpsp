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
        private int m_last_index_found_child1;
        private int m_last_index_found_child2;

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
            m_last_index_found_child1 = 0;
            m_last_index_found_child2 = 0;
            for (int i = 0; i < crossPoint; i++)
            {
                child1.SelectedModeList[i] = sol1.SelectedModeList[i];
                child2.SelectedModeList[i] = sol2.SelectedModeList[i];
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.getNumberOfResources(); j++)
                {
                    child1.DistributionMatrix[j, i] = new MatrixCell(sol1.DistributionMatrix[j, i]);
                    child2.DistributionMatrix[j, i] = new MatrixCell(sol2.DistributionMatrix[j, i]);
                }
            }
            for (int i = crossPoint; i < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize(); i++)
            {
                child2.SelectedModeList[i] = sol1.SelectedModeList[i];
                child1.SelectedModeList[i] = sol2.SelectedModeList[i];
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.getNumberOfResources(); j++)
                {
                    getNextValueForChild(child2, j,i, sol1, ref m_last_index_found_child2);
                    getNextValueForChild(child1, j,i, sol2, ref m_last_index_found_child1);
                }
            }

            return new KeyValuePair<Solution, Solution>(child1, child2);
        }

        /**
         * return the valueInParentMatrix if it doesn't exists in currentArray
         * else returns the minimum available value
         */
        private MatrixCell getNextValueForChild(Solution child, int row, int col, Solution parent, ref int last_index)
        {
            for (last_index=0; last_index < parent.DistributionMatrix.GetLength(1); last_index++) 
            {
                MatrixCell parent_val = parent.DistributionMatrix[row,last_index];
                bool exists = false;
                for (int j=0; j < col; j++) {
                    if (child.DistributionMatrix[row, j] == parent_val)
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                    continue;
                child.DistributionMatrix[row, col] = new MatrixCell(parent_val);
            }
            
            return null;
        }
        /*
        private int getNextValueForChild(MatrixCell[] currentArray, int valueInParentMatrix)
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
        */
        public override string ToString()
        {
            return "One Point Crossover";
        }
    }
}
