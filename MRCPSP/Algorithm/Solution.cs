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
