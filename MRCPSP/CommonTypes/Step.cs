using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    class StepComparer : IComparer
    {
        public StepComparer() : base() { }
        int IComparer.Compare(Object x, Object y)
        {
            Step x1 = (Step)x;
            Step y1 = (Step)y;
            if (x1.Id == y1.Id)
                return 0;
            if (x1.Id < y1.Id)
                return -1;
            return 1;
        }
    }

    public class Step
    {
        private int m_id;
        private string m_name;

        public Step(int id, string name)
        {
            m_id = id;
            m_name = name;
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public override bool Equals(object obj)
        {
            return m_id == ((Step)obj).Id;
        }
    }
}
