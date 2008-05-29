using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.CommonTypes
{
    public class Product
    {
        private int m_product_id;
        private string m_product_name;
        private int m_product_size; // number of jobs
    

        public Product(int id, string name, int size) {    
            m_product_id = id;
            m_product_name = name;
            m_product_size = size;
        }

        public int Size
        {
            get { return m_product_size; }
            set
            {
               m_product_size = value;
            }
        }
        public string Name
        {
            get { return m_product_name; }
            set
            {
                m_product_name = value;
            }
        }

        public int Id
        {
            get { return m_product_id; }
            set { m_product_id = value; }
        }

        public override bool Equals(object obj)
        {
            return ((Product)obj).Id == m_product_id;
        }
      
    }
}
