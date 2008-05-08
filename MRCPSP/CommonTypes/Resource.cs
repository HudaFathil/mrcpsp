using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Resource
    {
        private static int m_id_counter = 1;
        private int m_id;
        private string m_resource_name;
        private bool m_is_batch;

        public Resource()
        {
            m_resource_name = "";
            m_is_batch = false;
            m_id = m_id_counter;
            m_id_counter++;
        }

        public Resource(string name)
        {
            m_resource_name = name;
            m_is_batch = false;
            m_id = m_id_counter;
            m_id_counter++;
        }

        public Resource(string name, bool batch)
        {
            m_resource_name = name;
            m_is_batch = batch;
            m_id = m_id_counter;
            m_id_counter++;
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public string Name
        {
            get { return m_resource_name; }
            set { m_resource_name = value; }
        }

        public override string ToString()
        {
            return m_resource_name;
        }
    }
}
