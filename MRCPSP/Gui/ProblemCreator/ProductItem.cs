using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace MRCPSP.Gui.ProblemCreator
{
    public class ProductItem
    {
        private static int m_product_id_counter = 0;
        private int m_product_id;
        private string m_product_name;
        private int m_product_size;
        private Color m_constraints_color;

        public ProductItem() {
            this.m_product_id = m_product_id_counter;
            m_product_id_counter++;
            m_product_name = "product " + m_product_id.ToString();
            m_product_size = 0;
        }

        public void deleteMe() {
            m_product_id_counter--;
        }

        public override string ToString()
        {
            return m_product_name + " (" + m_product_size.ToString() + ")";
        }

        public int Size
        {
            get { return m_product_size; }
            set
            {
               m_product_size = value;
            }
        }

        public int Id
        {
            get { return m_product_id; }
            set
            {
                m_product_id = value;
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
        public Color ConstraintsColor
        {
            get { return m_constraints_color; }
            set
            {
                m_constraints_color = value;
            }
        }
    }
}
