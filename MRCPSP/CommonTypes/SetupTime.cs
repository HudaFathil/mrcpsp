using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    class SetupTime
    {
        private Mode m_mode;
        private Resource m_resource;
        private int m_setupTime;

        public SetupTime(Mode mode, Resource resource, int setupTime)
        {
            m_mode = mode;
            m_resource = resource;
            m_setupTime = setupTime;
        }

        public Mode Mode
        {
            get { return m_mode; }
        }

        public Resource Resource
        {
            get { return m_resource; }
        }

        public int ResourceSetupTime
        {
            get { return m_setupTime; }
        }

    }
}
