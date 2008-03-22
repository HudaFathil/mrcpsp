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

        public Operation(int start, int end, Resource resource)
        {
            m_start_time = start;
            m_end_time = end;
            m_resource = resource;
        }
    }
}
