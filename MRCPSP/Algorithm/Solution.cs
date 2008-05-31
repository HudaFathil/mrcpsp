using System;
using System.Collections;
using System.Collections.Generic;
using MRCPSP.CommonTypes;
using MRCPSP.Lindo;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.Controllers;

namespace MRCPSP.Algorithm
{
    class Solution
    {
        private int[] m_selected_mode_list;
        private MatrixCell[,] m_distribution_matrix;
        private double m_scoreFromLindo;
        private Dictionary<Resource, List<KeyValuePair<LindoParameter, LindoParameter>>> m_results;

        public Solution()
        {
            m_selected_mode_list = new int[ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()];
            m_distribution_matrix = new MatrixCell [ApplicManager.Instance.CurrentProblem.getNumberOfResources(), ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()];
                                            
        }

        // for debugging only for Ofir!!!!!
        public Solution(int distSize, int resourceNum)
        {
            m_selected_mode_list = new int[distSize];
            m_distribution_matrix = new MatrixCell[resourceNum, distSize];
        }
        // 


        public MatrixCell[,] DistributionMatrix
        {
            get { return m_distribution_matrix;}
            set
            {
                m_distribution_matrix = value;
            }
        }
        
        public int[] SelectedModeList
        {
            get { return m_selected_mode_list; }
            set
            {
                m_selected_mode_list = value;
            }
        }
        
        public double scoreFromLindo
        {
            get { return m_scoreFromLindo ; }
            set
            {
                m_scoreFromLindo = value;
            }
        }

        public Mode getSelectedMode(MatrixCell cell, int t)
        {
            Problem prob = ApplicManager.Instance.CurrentProblem;
            return prob.ModesInStep[cell.step][m_selected_mode_list[t] - 1];
        }

        public int getSelectedMode(Product f, Step s, int j)
        {
            for (int r = 0; r < m_distribution_matrix.GetLength(0); r++)
            {
                for (int t = 0; t < m_distribution_matrix.GetLength(1); t++)
                {
                    if (m_distribution_matrix[r, t].jobId == j && m_distribution_matrix[r, t].product.Equals(f) &&
                        m_distribution_matrix[r, t].step.Equals(s))
                        return m_selected_mode_list[t];
                }
            } 
            return -1;
        }

        public List<int> getTaskListForResource(int rIndex, Resource r) 
        {
            List<int> taskList = new List<int>();

            for (int c = 0; c < m_distribution_matrix.GetLength(1); c++ )
            {
                MatrixCell cell = m_distribution_matrix[rIndex ,  c];
                bool addTask = false;
                foreach (Mode m in ApplicManager.Instance.CurrentProblem.ModesInStep[cell.step])
                {
                    if (m.isResourceUsed(r))
                        addTask = true;
                }
                if (addTask)
                    taskList.Add(c);
            }
            return taskList;

        }

        public int getTaskNumber(int rIndex , Resource r , Step s, Product p, int j)
        {
            for (int c = 0; c < m_distribution_matrix.GetLength(1); c++)
            {
                MatrixCell cell = m_distribution_matrix[rIndex, c];
                bool addTask = false;
                foreach (Mode m in ApplicManager.Instance.CurrentProblem.ModesInStep[cell.step])
                {
                    if (m.isResourceUsed(r) && cell.step.Equals(s) && cell.product.Equals(p) && cell.jobId == j)
                        return c;
                }
            }
            return -1;
        }

        public int getTaskNumber(int rIndex, Resource r, Step s)
        {
            for (int c = 0; c < m_distribution_matrix.GetLength(1); c++)
            {
                MatrixCell cell = m_distribution_matrix[rIndex, c];
                bool addTask = false;
                foreach (Mode m in ApplicManager.Instance.CurrentProblem.ModesInStep[cell.step])
                {
                    if (m.isResourceUsed(r) && cell.step.Equals(s))
                        return c;
                }
            }
            return -1;
        }


        public List<Step> getStepsInResource(int rIndex, Resource r)
        {
            List<Step> sList = new List<Step>();
            for (int c = 0; c < m_distribution_matrix.GetLength(1); c++)
            {
                MatrixCell cell = m_distribution_matrix[rIndex, c];
                foreach (Mode m in ApplicManager.Instance.CurrentProblem.ModesInStep[cell.step]) 
                {
                    if (m.isResourceUsed(r) && !sList.Contains(cell.step))
                        sList.Add(cell.step);
                }
            }
            return sList;
        }



        public Dictionary<Resource, List<KeyValuePair<LindoParameter, LindoParameter>>> resultFromLindo
        {
            get { return m_results; }
            set
            {
                m_results = value;
            }
        }
        /*
        public MatrixCell[] getRowArray(int row , int fromCol, int toCol)
        {
            MatrixCell [] toReturn;
            if (toCol< fromCol || toCol> m_distribution_matrix.GetLength(1) || fromCol < 0) 
                return null;
            toReturn = new MatrixCell[toCol - fromCol];
            for (int i = fromCol; i < toCol; i++ )
            {
                toReturn[i] = m_distribution_matrix[row,i];
            }
            return toReturn;
        }
        */
      
    }
}
