using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;

namespace MRCPSP.Domain
{
    

    class Problem
    {
        private List<Resource> m_resource_list;
        private Dictionary< Step, List < Mode > > m_modes_in_step;     
        private List<Step> m_step_list;
        private List<Constraint> m_all_constraints;
        private List<Product> m_products_list;
        private Dictionary<Product, List<Step>> m_steps_in_product;
        private Dictionary<Product, List<Job>> m_jobs_in_product;
        private String m_title = "not implemented";

        public Problem( List<Resource> resource_list,
                        Dictionary< Step, List < Mode > > modes_in_step,
                        List<Step> step_list,       
                        List<Constraint> all_constraints,
                        List<Product> products_list,
                        Dictionary<Product, List<Job>> jobs_in_product)
        {
            m_resource_list = resource_list;
            m_modes_in_step = modes_in_step;
            foreach (Step s in modes_in_step.Keys)
            {
                foreach (Mode m in modes_in_step[s])
                {
                    m.BelongToStep = s;
                }
            }
            m_step_list = step_list;
            m_all_constraints = all_constraints;
            m_products_list = products_list;
            m_jobs_in_product = jobs_in_product;
            m_steps_in_product = new Dictionary<Product, List<Step>>();
            for (int i=0; i < m_products_list.Count(); i++)
                m_steps_in_product.Add(m_products_list[i], new List<Step>());

            foreach (Constraint c in m_all_constraints)
            {
                if (! m_steps_in_product.ContainsKey(c.Product))
                    throw new EntryPointNotFoundException("found constraint for non existing product"); 
               List<Step> product_steps = m_steps_in_product[c.Product];
               if (!product_steps.Contains(c.StepFrom))
                    product_steps.Add(c.StepFrom);
               if (!product_steps.Contains(c.StepTo))
                    product_steps.Add(c.StepTo);
            }       
        }

        public int getTotalDistributionSize()
        {
            int size = 0;
            for (int i = 0; i < m_products_list.Count(); i++)
            {
                size += (m_products_list[i].Size * getNumberOfStepsPerProduct(m_products_list[i])); 
            }
            return size;
        }

        public int getNumberOfResources()
        {
            return m_resource_list.Count();
        }

        public int getNumberOfModesById(int matrix_id)
        {
            for (int i = 0; i < m_products_list.Count(); i++)
            {
                Product p = m_products_list[i];
                int num_steps = m_steps_in_product[p].Count;
                if (matrix_id < 0)
                    throw new IndexOutOfRangeException();
                int size_of_current_family_block = (p.Size * num_steps);
                if (matrix_id < size_of_current_family_block)
                {
                    int pos_in_step_list = (matrix_id % num_steps);
                    Step s = m_steps_in_product[p][pos_in_step_list];                 
                    return m_modes_in_step[s].Count;
                }
                matrix_id-= (p.Size * m_steps_in_product[p].Count);
            }
            throw new IndexOutOfRangeException();
        }


        public Mode getSelectedModeByListIdAndTask(int matrix_id, int mode_id)
        {
            for (int i = 0; i < m_products_list.Count(); i++)
            {
                Product p = m_products_list[i];
                int num_steps = m_steps_in_product[p].Count;
                if (matrix_id < 0)
                    throw new IndexOutOfRangeException();
                int size_of_current_family_block = (p.Size * num_steps);
                if (matrix_id < size_of_current_family_block)
                {
                    int pos_in_step_list = (matrix_id % num_steps);
                    Step s = m_steps_in_product[p][pos_in_step_list];
                    return m_modes_in_step[s][mode_id - 1];
                }
                matrix_id -= (p.Size * m_steps_in_product[p].Count);
            }
            throw new IndexOutOfRangeException();
        }

        public Mode getSelectedModeByStepAndModeId(Step s, int mode)
        {  
            return m_modes_in_step[s][mode - 1];
        }


        public int getNumberOfStepsPerProduct(Product product)
        {
            if (! m_steps_in_product.ContainsKey(product))
                throw new EntryPointNotFoundException("request non existing product");
            return m_steps_in_product[product].Count;
        }

        public List<Resource> Resources
        {
            get { return m_resource_list; }
            set { m_resource_list = value; }
        }

        public Dictionary<Step, List<Mode>> ModesInStep
        {
            get { return m_modes_in_step; }
            set { m_modes_in_step = value; }
        }

        public Dictionary<Product, List<Job>> JobsInProduct
        {
            get { return m_jobs_in_product; }
            set { m_jobs_in_product = value; }
        }

        public List<Step> Steps
        {
            get { return m_step_list; }
            set { m_step_list = value; }
        }

        public List<Product> Products
        {
            get { return m_products_list; }
            set { m_products_list = value; }
        }

        public List<Constraint> Constraints
        {
            get { return m_all_constraints; }
            set { m_all_constraints = value; }
        }

        public Dictionary<Product, List<Step>> StepsInProduct
        {
            get { return m_steps_in_product; }
            set { m_steps_in_product = value; }
        }

        public List<Resource> getAllResourcesInStep(Step step)
        {
            List<Resource> resources = new List<Resource>();
            foreach (Mode m in m_modes_in_step[step])
            {
                foreach (Operation o in m.operations)
                {
                    if (!resources.Contains(o.Rseource))
                        resources.Add(o.Rseource);
                }
            }
            return resources;
        }

        public List<Resource> getAllResourcesInProduct(Product p)
        {
            List<Resource> resources = new List<Resource>();
            foreach (Step s in m_steps_in_product[p])
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
            for (int i = 0; i < m_products_list.Count; i++)
            {
                foreach (Step s in m_steps_in_product[m_products_list[i]])
                {
                    foreach (Mode m in m_modes_in_step[s])
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

        public List<Constraint> getAllConstraintForProduct(Product p)
        {
            List<Constraint> constr = new List<Constraint>();
            foreach (Constraint c in m_all_constraints)
            {
                if (c.Product == p)
                    constr.Add(c);
            }
            return constr;
        }
        
        public List<Step> getAllImmediatePrecedence(Product p, Step s)
        {
            List<Step> preced = new List<Step>();
            foreach (Constraint c in m_all_constraints)
            {
                if ((c.Product == p) && (c.StepTo == s))
                    if (!preced.Contains(c.StepFrom))
                        preced.Add(c.StepFrom);
            }
            return preced;
        }
        
        public List<Step> getAllImmediatePrecedence(Product p)
        {
            List<Step> preced = new List<Step>();
            foreach (Constraint c in m_all_constraints)
            {
                if (c.Product == p)
                    if (!preced.Contains(c.StepTo))
                        preced.Add(c.StepTo);
            }
            return preced;
        }

        public List<Step> getAllImmediateSubsequent(Product p, Step s)
        {
            List<Step> subs = new List<Step>();
            foreach (Constraint c in m_all_constraints)
            {
                if ((c.Product == p) && (c.StepFrom == s))
                    if (!subs.Contains(c.StepTo))
                        subs.Add(c.StepTo);
            }
            return subs;
        }

        public bool isStepSubsequentToStep(Product p, Step from, Step to)
        {
            if (from == to)
                return false;
            List<Step> steps = getAllImmediateSubsequent(p, from);
            for (int i = 0; i < steps.Count; i++)
            {
                if (steps[i] == to)
                    return true;
                if (isStepSubsequentToStep(p,steps[i], to))
                    return true;
            }
            return false;
        }

        public List<Step> getStepInResource(Resource r)
        {
            List<Step> sList = new List<Step>();
            foreach (Step s in m_step_list) {
                foreach (Mode m in ModesInStep[s])
                {
                    if (m.isResourceUsed(r) && !sList.Contains(s))
                        sList.Add(s);
                        
                }
            }
            return sList;
        }

        public String Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

         
    }
}
