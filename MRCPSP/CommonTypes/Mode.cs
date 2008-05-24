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
        private int m_name;

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
        }

        public int name
        {
            get { return m_name; }
            set 
            {
                m_name = value;
            }
        }

        /* calculates the total process time of the mode
         * <total process time> = MAX(operations finish time) MIN (operations start time)
         * return <total process time>
         */
        public int getTotalProcessTime()
        {
            if (m_operations_list == null) 
            {
                return 0;
            }

            int min = -1;
            int max = -1;

            foreach (Operation op in m_operations_list) 
            {
                if (op.StartTime < min || min == -1)
                {
                    min = op.StartTime;
                }
                if (op.EndTime > max)
                {
                    max = op.EndTime;
                }

            }
            return max - min;
        }


        public override bool Equals(object obj)
        {
            return m_id == ((Mode)obj).Id;
        }
    }
}
