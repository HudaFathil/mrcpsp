using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Exceptions
{
    class ConstrainException : Exception
    {
        private String m_message;

        public ConstrainException(String constrainClass, String message)
        {
            m_message = "Constrain Exceprion at " + constrainClass + " - " + message;
        }

        public override string Message
        {
            get
            {
                return m_message;
            }
        }


    }
}
