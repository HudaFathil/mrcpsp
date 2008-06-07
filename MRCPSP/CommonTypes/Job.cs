using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    class Job
    {
     
        private double m_arrive_time;
        private double m_latest_tern_time;
        private static int id_counter = 0;
        private int m_id;

        public Job(double arrive, double latest)
        {
            m_arrive_time = arrive;
            m_latest_tern_time = latest;
            m_id = id_counter;
            id_counter++;
        }
        public Job(int id , double arrive, double latest)
        {
            m_arrive_time = arrive;
            m_latest_tern_time = latest;
            m_id = id;
        }

        public double ArriveTime
        {
            get { return m_arrive_time; }
            set
            {
                m_arrive_time = value;
            }
        }

        public double LatestTermTime
        {
            get { return m_latest_tern_time; }
            set
            {
                m_latest_tern_time = value;
            }
        }

        public int Id
        {
            get { return m_id; }
            set
            {
                m_id = value;
            }
        }

    }
}
