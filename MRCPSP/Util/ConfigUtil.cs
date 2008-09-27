using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MRCPSP.Util
{
    class ConfigUtil
    {

        private static ConfigUtil m_instance = null;
        private Dictionary<String,String> m_props;

        private ConfigUtil()
        {
            m_props = new Dictionary<string, string>();
            loadConfig();
        }


        

        private void loadConfig() 
        {
            StreamReader conf = new StreamReader("config.txt");
            String confElem; 
            while ((confElem = conf.ReadLine()) != null)
            {
                String[] confElemArray = confElem.Split(' ');
                m_props.Add(confElemArray[0], confElemArray[1]);
            }
        }


        public static ConfigUtil Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new ConfigUtil();
                return m_instance;
            }
        }


        public String getStringValue(String key)
        {
            return m_props[key];
        }


    }
}
