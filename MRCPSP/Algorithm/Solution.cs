using System;
using System.Collections;
using System.Linq;
using System.Text;
using MRCPSP.Domain;
using MRCPSP.Controllers;

namespace MRCPSP.Algorithm
{
    class Solution
    {
        private int[] m_selected_mode_list;
        private int[,] m_distribution_matrix;
        private double m_resultFromLindo;

        public Solution()
        {
            m_selected_mode_list = new int[ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()];
            m_distribution_matrix = new int [ApplicManager.Instance.CurrentProblem.getNumberOfResources(), ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()];
                                            
        }

        // for debugging only for Ofir!!!!!
        public Solution(int distSize, int resourceNum)
        {
            m_selected_mode_list = new int[distSize];
            m_distribution_matrix = new int[resourceNum, distSize];
        }
        // 


        public int[,] DistributionMatrix
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
        
        public double resultFromLindo
        {
            get { return m_resultFromLindo ; }
            set
            {
                m_resultFromLindo = value;
            }
        }
        
        public int[] getRowArray(int row , int fromCol, int toCol)
        {
            int [] toReturn;
            if (toCol< fromCol || toCol> m_distribution_matrix.GetLength(1) || fromCol < 0) 
                return null;
            toReturn = new int[toCol - fromCol];
            for (int i = fromCol; i < toCol; i++ )
            {
                toReturn[i] = m_distribution_matrix[row,i];
            }
            return toReturn;
        }

       

    }
}
