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
        public ResourceBase(int available_time)
        {
            m_available_time = available_time;
            this.m_title = "Resource " + resource_counter.ToString() + " ("+ m_available_time.ToString()+")";
            resource_counter++;
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
    }
}
