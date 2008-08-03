using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Gui.ProblemCreator
{
    public class Machine : ResourceBase
    {
        private int m_batch_size;

        public Machine(int available_time, int batch_size)
            : base(available_time)
        {
            m_batch_size = batch_size;
        }

        public override string ToString()
        {
            return base.ToString() + " (" + m_batch_size.ToString()+ ")";
        }

        public int BatchSize
        {
            get { return m_batch_size; }
            set { m_batch_size = value; }
        }
    }
}
