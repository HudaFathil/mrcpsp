using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Resource
    {
        private static int m_id_counter = 1;
        private int m_id;
        private int m_arrive_time;
        private string m_resource_name;
        private int m_capacity;
  
        public Resource(string name , int arriveTime)
        {
            m_resource_name = name;
            m_arrive_time = arriveTime;
            m_capacity = 1;
            m_id = m_id_counter;
            m_id_counter++;
        }

        public Resource(int id , string name, int capacity ,int arriveTime)
        {
            m_resource_name = name;
            m_arrive_time = arriveTime;
            m_capacity = capacity;
            m_id = id;
        }


        public Resource(string name, int capacity , int arriveTime)
        {
            m_resource_name = name;
            m_arrive_time = arriveTime;
            m_capacity = capacity;
            m_id = m_id_counter;
            m_id_counter++;
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public int Capacity
        {
            get { return m_capacity; }
        }

        public bool isBatch()
        {
            return  m_capacity > 1;       
        }

        public int ArriveTime
        {
            get { return m_arrive_time; } 
        }

        public string Name
        {
            get { return m_resource_name; }
            set { m_resource_name = value; }
        }

        public override bool Equals(object obj)
        {
            return ((Resource)obj).Id == m_id;
        }

        public override string ToString()
        {
            return m_resource_name;
        }
    }
}
