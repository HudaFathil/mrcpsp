using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Mode
    {
        private System.Collections.ArrayList m_operations_list;
        private static int id_counter = 1;
        private int m_id;

        public Mode()
        {
            m_operations_list = new System.Collections.ArrayList();
            m_id = id_counter;
            id_counter++;
        }

        public System.Collections.ArrayList operations
        {
            get { return m_operations_list; }
            set
            {
                m_operations_list = value;
            }
        }

        public int Id
        {
            get { return m_id; }
            set
            {
                m_id = value;
            }
        }
    }
}
