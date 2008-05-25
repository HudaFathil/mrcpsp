using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace MRCPSP.Lindo
{
    class MrcpspVariable
    {
        private String m_name;
        private String m_type;
        private List<double> m_coefficient;
        private List<int> m_equationLevel;
        private double m_finalValue;

        public MrcpspVariable(String name)
        {
            m_name = name;
            m_type = "";
            m_equationLevel = new List<int>();
            m_coefficient = new List<double>();
        }

        public double FinalValue
        {
            get { return m_finalValue; }
            set { m_finalValue = value; }
        }

        public String Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public String Name
        {
            get { return m_name; }
        }

        public List<double> Coefficient
        {
            get { return m_coefficient; }
        }

        public void AddCoefficient(int equationLevel, double coefficient)
        {
            m_coefficient.Add(coefficient);
            m_equationLevel.Add(equationLevel);
        }



        public void TransferListsToVectors(ref List<double> lindoadA, ref List<int> lindoanRowsList, ref List<int> lindopnLenColList)
        {
            lindopnLenColList.Add(m_coefficient.Count);
            for (int i = 0 ; i < m_coefficient.Count; i++)
            {
                lindoadA.Add(m_coefficient[i]);
                lindoanRowsList.Add(m_equationLevel[i]);    
            }
            
        }		
    }
}
