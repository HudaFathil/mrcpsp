using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Resource
    {
        private string m_resource_name;
        private bool m_is_batch;

        public Resource()
        {
            m_resource_name = "";
            m_is_batch = false;
        }

        public Resource(string name)
        {
            m_resource_name = name;
            m_is_batch = false;
        }

        public Resource(string name, bool batch)
        {
            m_resource_name = name;
            m_is_batch = batch;
        }
    }
}
