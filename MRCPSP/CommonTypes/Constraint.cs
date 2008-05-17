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
        private double m_min_queue_time;
        private double m_max_queue_time;

        public Constraint(Product p, Step from, Step to, double min_queue, double max_queue)
        {
            m_product = p;
            m_from_step = from;
            m_to_step = to;
            m_min_queue_time = min_queue;
            m_max_queue_time = max_queue;
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

        public double MinQueueTime
        {
            get { return m_min_queue_time; }
        }

        public double MaxQueueTime
        {
            get { return m_min_queue_time; }
        }
    }
}
