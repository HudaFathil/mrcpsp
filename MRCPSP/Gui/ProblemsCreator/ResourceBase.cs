using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Gui.ProblemCreator
{
    public class ResourceBase 
    {
        private static int resource_counter= 0;
        private string m_title;
        private int m_available_time;
        private List<ResourceObserver> m_objects_using_me;

        public ResourceBase(int available_time)
        {
            m_available_time = available_time;
            this.m_title = "Resource " + resource_counter.ToString() + " ("+ m_available_time.ToString()+")";
            resource_counter++;
            m_objects_using_me = new List<ResourceObserver>();            
        }

        public override string ToString()
        {
            return m_title;
        }

        public void setTitle(string s)
        {
            this.m_title = s;
        }

        public string getTitle()
        {
            return m_title;
        }

        public void sign(ResourceObserver ro)
        {
            if (! m_objects_using_me.Contains(ro))
                m_objects_using_me.Add(ro);
        }

        public void notifyOnDeleteMe()
        {
            for (int i = 0; i < m_objects_using_me.Count; i++)
            {
                m_objects_using_me[i].resourceDeleteNotify(this);
            }
        }

        public int ArriveTime
        {
            get { return m_available_time; }
            set { m_available_time = value; }
        }
    }
}
