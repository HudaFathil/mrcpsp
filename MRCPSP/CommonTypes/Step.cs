using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Step
    {
        private int m_id;
        private string m_name;

        public Step(int id, string name)
        {
            m_id = id;
            m_name = name;
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
    }
}
