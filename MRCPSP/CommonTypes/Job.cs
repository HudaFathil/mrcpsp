using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    class Job
    {
     
        private int m_arrive_time;
        private int m_latest_tern_time;
        private static int id_counter = 0;
        private int m_id;
        private int m_units = 1;
        private int m_weight;

        public Job(int arrive, int latest, int weight)
        {
            m_arrive_time = arrive;
            m_latest_tern_time = latest;
            m_id = id_counter;
            m_weight = weight;
            id_counter++;
        }

        public Job(int id, int arrive, int latest, int weight, int units)
        {
            m_arrive_time = arrive;
            m_latest_tern_time = latest;
            m_weight = weight;
            m_id = id;
            m_units = units;
        }

        public Job(int id , int arrive, int latest , int weight)
        {
            m_arrive_time = arrive;
            m_latest_tern_time = latest;
            m_weight = weight;
            m_id = id;
        }

      

        public int ArriveTime
        {
            get { return m_arrive_time; }
            set
            {
                m_arrive_time = value;
            }
        }

        public int LatestTermTime
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

        public int Units
        {
            get { return m_units; }
            set { m_units = value; }
        }

        public int Weight
        {
            get { return m_weight; }
        }

        public override bool Equals(object obj)
        {
            return ((Job)obj).Id == m_id;
        }
    }
}
