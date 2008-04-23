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
                Product p = m_products_array[i];
                int num_steps = ((System.Collections.ArrayList)m_steps_in_product[m_products_array[i]]).Count;
                if (matrix_id < 0)
                    throw new IndexOutOfRangeException();
                int size_of_current_family_block = (m_products_array[i].Size * num_steps);
                if (matrix_id < size_of_current_family_block)
                {
                    int pos_in_step_list = (matrix_id % num_steps);
                    Step s = (Step)((System.Collections.ArrayList)m_steps_in_product[p])[pos_in_step_list];                 
                    return ((System.Collections.ArrayList)m_modes_in_step[s]).Count;
                }
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

        public Resource[] Resources
        {
            get { return m_resource_array; }
            set { m_resource_array = value; }
        }

        public System.Collections.Hashtable ModesInStep
        {
            get { return m_modes_in_step; }
            set { m_modes_in_step = value; }
        }

        public Step[] Steps
        {
            get { return m_step_array; }
            set { m_step_array = value; }
        }

        public Product[] Products
        {
            get { return m_products_array; }
            set { m_products_array = value; }
        }

        public System.Collections.ArrayList Constraints
        {
            get { return m_all_constraints; }
            set { m_all_constraints = value; }
        }

        public System.Collections.Hashtable StepsInProduct
        {
            get { return m_steps_in_product; }
            set { m_steps_in_product = value; }
        }

        public System.Collections.ArrayList getAllResourcesInStep(Step step)
        {
            System.Collections.ArrayList resources = new System.Collections.ArrayList();
            foreach (Mode m in (System.Collections.ArrayList)m_modes_in_step[step])
            {
                foreach (Operation o in m.operations)
                {
                    if (!resources.Contains(o.Rseource))
                        resources.Add(o.Rseource);
                }
            }
            return resources;
        }

        public System.Collections.ArrayList getAllResourcesInProduct(Product p)
        {
            System.Collections.ArrayList resources = new System.Collections.ArrayList();
            foreach (Step s in (System.Collections.ArrayList)m_steps_in_product[p])
            {
                foreach (Resource r in getAllResourcesInStep(s))
                {
                    if (!resources.Contains(r))
                        resources.Add(r);
                }
            }
            return resources;
        }

        public int getNumberOfResourceShowsInProduct(Resource r, Product p)
        {
            int count = 0;
            for (int i = 0; i < m_products_array.Length; i++)
            {
                foreach (Step s in (System.Collections.ArrayList)m_steps_in_product[m_products_array[i]])
                {
                    foreach (Mode m in (System.Collections.ArrayList)m_modes_in_step[s])
                    {
                        for (int j = 0; j < m.operations.Count; j++)
                        {
                            if (((Operation)m.operations[j]).Rseource == r)
                                count++;
                        }
                    }
                }
            }
            return count;
        }

        public System.Collections.ArrayList getAllConstraintForProduct(Product p)
        {
            System.Collections.ArrayList constr = new System.Collections.ArrayList();
            foreach (Constraint c in m_all_constraints)
            {
                if (c.Product == p)
                    constr.Add(c);
            }
            return constr;
        }
        
        public System.Collections.ArrayList getAllImmediatePrecedence(Product p, Step s)
        {
            System.Collections.ArrayList preced = new System.Collections.ArrayList();
            foreach (Constraint c in m_all_constraints)
            {
                if ((c.Product == p) && (c.StepTo == s))
                    if (!preced.Contains(c.StepTo))
                        preced.Add(c.StepTo);
            }
            return preced;
        }
        
        public System.Collections.ArrayList getAllImmediatePrecedence(Product p)
        {
            System.Collections.ArrayList preced = new System.Collections.ArrayList();
            foreach (Constraint c in m_all_constraints)
            {
                if (c.Product == p)
                    if (!preced.Contains(c.StepTo))
                        preced.Add(c.StepTo);
            }
            return preced;
        }

        public System.Collections.ArrayList getAllImmediateSubsequent(Product p, Step s)
        {
            System.Collections.ArrayList subs = new System.Collections.ArrayList();
            foreach (Constraint c in m_all_constraints)
            {
                if ((c.Product == p) && (c.StepFrom == s))
                    if (!subs.Contains(c.StepFrom))
                        subs.Add(c.StepFrom);
            }
            return subs;
        }
         
    }
}
