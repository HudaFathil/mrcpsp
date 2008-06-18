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
        private String m_name;
        private int m_idPerStep;
        private Step m_step;

        public Mode()
        {
            m_operations_list = new System.Collections.ArrayList();
            m_id = id_counter;
            m_name = "Mode " + m_id;
            id_counter++;
        }

        public Mode(int id , String name)
        {
            m_idPerStep = id;
            m_name = name;
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



        public int Id
        {
            get { return m_id; }
        }

        public Step BelongToStep
        {
            get { return m_step; }
            set { m_step = value; }
        }

        public int IdPerStep
        {
            get { return m_idPerStep; }
            set 
            {
                m_idPerStep = value;
            }
        }

        public String Name
        {
            get { return m_name; }
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

        public int getTotalProcessTime(Resource r)
        {
            if (m_operations_list == null)
            {
                return 0;
            }

            int min = -1;
            int max = -1;

            foreach (Operation op in m_operations_list)
            {
                if (op.Rseource.Equals(r))
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

            }
            return max - min;
        }

        public bool isResourceUsed(Resource r)
        {
            foreach (Operation op in m_operations_list)
            {
                if (op.Rseource.Equals(r))
                    return true;
            }
            return false;
        }


        public double startUsingResourceTime(Resource r)
        {
            int startTime = -1;
            foreach (Operation op in m_operations_list)
            {
                if (op.Rseource.Equals(r) && (startTime == -1 || startTime > op.StartTime)) 
                    startTime = op.StartTime;
            }
            return startTime;
        }

        public override bool Equals(object obj)
        {
            return m_id == ((Mode)obj).Id;
        }
    }
}
