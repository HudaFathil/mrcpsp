using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    class ResourceConstraint
    {

        private Mode m_mode_from;
        private Mode m_mode_to;
        private Resource m_resource;
        private int m_delay;

        public ResourceConstraint(Resource r, Mode from, Mode to, int delay)
        {
            m_resource = r;
            m_mode_from = from;
            m_mode_to = to;
            m_delay = delay;
        }

        public Mode FromMode
        {
            get { return m_mode_from; }
            set { m_mode_from = value; }
        }

        public Mode ToMode
        {
            get { return m_mode_to; }
            set { m_mode_to = value; }
        }

        public Resource CurrentResource
        {
            get { return m_resource; }
            set { m_resource = value; }
        }

        public int DelayTime
        {
            get { return m_delay; }
            set { m_delay = value; }
        }
    }
}
