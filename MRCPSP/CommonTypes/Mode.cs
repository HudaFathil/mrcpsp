using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Mode
    {
        private System.Collections.ArrayList m_operations_list;

        public Mode()
        {
            m_operations_list = new System.Collections.ArrayList();
        }

        public System.Collections.ArrayList operations
        {
            get { return m_operations_list; }
            set
            {
                m_operations_list = value;
            }
        }
    }
}
