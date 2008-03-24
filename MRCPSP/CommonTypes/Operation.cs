using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Operation
    {
        private int m_start_time;
        private int m_end_time;
        private Resource m_resource;
        private int m_id;
        private static int id_counter = 1;

        public Operation(int start, int end, Resource resource)
        {
            m_start_time = start;
            m_end_time = end;
            m_resource = resource;
            m_id = id_counter;
            id_counter++;
        }

        public int StartTime
        {
            get { return m_start_time; }
            set { m_start_time = value; }
        }
        public int EndTime
        {
            get { return m_end_time; }
            set { m_end_time = value; }
        }
        public Resource Rseource
        {
            get { return m_resource; }
            set { m_resource = value; }
        }
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
    }
}
