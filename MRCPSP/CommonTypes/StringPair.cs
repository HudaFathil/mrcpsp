using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    class StringPair
    {
        private string m_first;
        private string m_second;

        public string First
        {
            get { return m_first; }
            set
            {
                m_first = value;
            }
        }

        public string Second
        {
            get { return m_second; }
            set
            {
                m_second = value;
            }
        }
    }
}
