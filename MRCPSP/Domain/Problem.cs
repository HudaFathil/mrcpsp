using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;

namespace MRCPSP.Domain
{
    class Problem
    {
        private Resource[] m_resource_array;
        private System.Collections.Hashtable m_modes_in_step;
        private Step[] m_step_array;
        private System.Collections.ArrayList m_all_constraints;
        private Product[] m_products_array;
        private System.Collections.Hashtable m_steps_in_product;

        public Problem(Resource[] resource_array,
                                System.Collections.Hashtable modes_in_step,
                                Step[] step_array,
                                System.Collections.ArrayList all_constraints,
                                Product[] products_array)
        {
            m_resource_array = resource_array;
            m_modes_in_step = modes_in_step;
            m_step_array = step_array;
            m_all_constraints = all_constraints;
            m_products_array = products_array;
            m_steps_in_product = new System.Collections.Hashtable();
            for (int i=0; i < m_products_array.Count(); i++)
                m_steps_in_product.Add(m_products_array[i], new System.Collections.ArrayList());

            foreach (Constraint c in m_all_constraints)
            {
                if (! m_steps_in_product.ContainsKey(c.Product))
                    throw new EntryPointNotFoundException("found constraint for non existing product"); 
               System.Collections.ArrayList product_steps = (System.Collections.ArrayList)m_steps_in_product[c.Product];
               if (!product_steps.Contains(c.StepFrom))
                    product_steps.Add(c.StepFrom);
               if (!product_steps.Contains(c.StepTo))
                    product_steps.Add(c.StepTo);
            }       
        }

        public int getTotalDistributionSize()
        {
            int size = 0;
            for (int i = 0; i < m_products_array.Count(); i++)
            {
                size += (m_products_array[i].Size * getNumberOfStepsPerProduct(m_products_array[i])); 
            }
            return size;
        }

        public int getNumberOfResources()
        {
            return m_resource_array.Count();
        }

        public int getNumberOfModesById(int matrix_id)
        {
            for (int i = 0; i < m_products_array.Count(); i++)
            {
                if (matrix_id < 0)
                    throw new IndexOutOfRangeException();
                if (matrix_id < (m_products_array[i].Size * ((System.Collections.ArrayList)m_steps_in_product[m_products_array[i]]).Count))
                    return matrix_id % ((System.Collections.ArrayList)m_steps_in_product[m_products_array[i]]).Count;
                matrix_id-= (m_products_array[i].Size * ((System.Collections.ArrayList)m_steps_in_product[m_products_array[i]]).Count);
            }
            throw new IndexOutOfRangeException();
        }

        public int getNumberOfStepsPerProduct(Product product)
        {
            if (! m_steps_in_product.ContainsKey(product))
                throw new EntryPointNotFoundException("request non existing product");
            return ((System.Collections.ArrayList)m_steps_in_product[product]).Count;
        }
    }
}
