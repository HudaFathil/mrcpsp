using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Constraint
    {
        private Product m_product;
        private Step m_from_step;
        private Step m_to_step;

        public Constraint(Product p, Step from, Step to)
        {
            m_product = p;
            m_from_step = from;
            m_to_step = to;
        }

        public Product Product
        {
            get { return m_product; }
            set
            {
                m_product = value;
            }
        }

        public Step StepFrom
        {
            get { return m_from_step; }
            set
            {
                m_from_step = value;
            }
        }

        public Step StepTo
        {
            get { return m_to_step; }
            set
            {
                m_to_step = value;
            }
        }
    }
}
