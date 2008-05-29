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
        private int m_crossPoint;

        public OnePointCrossOver()
        {
            m_random = new Random();

        }

        public override List<Solution> doCrossOver(List<Solution> solutions)
        {
            List<KeyValuePair<Solution, Solution>> listOfPairs = createPairsForCrossOver(solutions);
            List<Solution> childrensAfterCrossover = new List<Solution>();
            m_crossPoint = m_random.Next(ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()-1) + 1;
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
            for (int i = 0; i < m_crossPoint; i++)
            {
                child1.SelectedModeList[i] = sol1.SelectedModeList[i];
                child2.SelectedModeList[i] = sol2.SelectedModeList[i];
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.getNumberOfResources(); j++)
                {
                    child1.DistributionMatrix[j, i] = new MatrixCell(sol1.DistributionMatrix[j, i]);
                    child2.DistributionMatrix[j, i] = new MatrixCell(sol2.DistributionMatrix[j, i]);
                }
            }
            for (int i = m_crossPoint; i < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize(); i++)
            {
                child2.SelectedModeList[i] = sol1.SelectedModeList[i];
                child1.SelectedModeList[i] = sol2.SelectedModeList[i];
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.getNumberOfResources(); j++)
                {
                    getNextValueForChild(child2, j,i, sol1);
                    getNextValueForChild(child1, j,i, sol2);
                }
            }

            return new KeyValuePair<Solution, Solution>(child1, child2);
        }

        /**
         * return the valueInParentMatrix if it doesn't exists in currentArray
         * else returns the minimum available value
         */
        private void getNextValueForChild(Solution child, int row, int col, Solution parent)
        {
            for (int i =0; i < parent.DistributionMatrix.GetLength(1); i++) 
            {
                MatrixCell parent_val = parent.DistributionMatrix[row,i];
                bool exists = false;
                for (int j=0; j < col; j++) {
                    if (child.DistributionMatrix[row, j].Equals(parent_val))
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                    continue;
                child.DistributionMatrix[row, col] = new MatrixCell(parent_val);
                return;
            }
        }

        public int CrossPoint
        {
            get { return m_crossPoint; }
            set { m_crossPoint = value; }
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
