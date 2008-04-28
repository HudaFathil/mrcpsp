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

        public Solution()
        {
            m_selected_mode_list = new int[ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()];
            m_distribution_matrix = new int [ApplicManager.Instance.CurrentProblem.getNumberOfResources(), ApplicManager.Instance.CurrentProblem.getTotalDistributionSize()];
                                            
        }

        public Solution(int distSize, int resourceNum)
        {
            m_selected_mode_list = new int[distSize];
            m_distribution_matrix = new int[resourceNum, distSize];
        }

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

    }
}
