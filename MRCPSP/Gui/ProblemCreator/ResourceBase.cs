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

        public ResourceBase()
        {                    
            this.m_title = "Resource " + resource_counter.ToString();
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
