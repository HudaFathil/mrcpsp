using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;
using MRCPSP.CommonTypes;
using System.Windows.Forms;

namespace MRCPSP.Gui.ProblemCreator
{

    public class ProblemCreatorState
    {
        private static ProblemCreatorState instance;

        public static System.Collections.Hashtable m_state_for_monitor;

        static ProblemCreatorState()
        {
            instance = new ProblemCreatorState();
            m_state_for_monitor = new System.Collections.Hashtable();

        }

        public static MonitorState Instance(int id)
        {
            if (!m_state_for_monitor.ContainsKey(id))
                m_state_for_monitor.Add(id, new MonitorState(id));
            return (MonitorState)m_state_for_monitor[id];
        }

    }

    public class MonitorState
    {
        public StateBase state;
        private int m_monitor_id;

        // problem data
        private System.Collections.ArrayList m_machine_list;
        private System.Collections.ArrayList m_worker_list;
        private System.Collections.Hashtable m_constraint_map;
        private System.Collections.ArrayList m_step_list;
        private System.Collections.ArrayList m_product_list;
        private DataGridView m_resource_constraints;
        private ProductItem m_current_product;

        private int m_next_step_id;

        public MonitorState(int id)
        {
            m_monitor_id = id;
            state = new PointerState(id);
            m_worker_list = new System.Collections.ArrayList();
            m_machine_list = new System.Collections.ArrayList();

            m_constraint_map = new System.Collections.Hashtable();
            m_step_list = new System.Collections.ArrayList();
            m_product_list = new System.Collections.ArrayList();
            m_next_step_id = 0;
            m_current_product = null;

        }

        public DataGridView ResourceConstraints
        {
            get { return m_resource_constraints; }
            set { m_resource_constraints = value; }
        }

        internal void addWorker(Worker w)
        {
            m_worker_list.Add(w);
        }

        internal void addMachine(Machine m)
        {

            m_machine_list.Add(m);
        }

        internal void removeWorker(Worker w)
        {
            m_worker_list.Remove(w);
        }

        internal void removeMachine(Machine m)
        {

            m_machine_list.Remove(m);
        }

        internal void addStep(StepItem s)
        {
            m_step_list.Add(s);
        }

        internal void addConstraint(ProductItem p, ConstraintItem c)
        {
            System.Collections.ArrayList array = (System.Collections.ArrayList)m_constraint_map[p];
            array.Add(c);
        }

        internal void removeConstraint(ConstraintItem c)
        {
            foreach (ProductItem p in m_constraint_map.Keys)
                removeConstraint(p, c);
        }

        internal void removeConstraint(ProductItem p, ConstraintItem c)
        {
            System.Collections.ArrayList array = (System.Collections.ArrayList)m_constraint_map[p];
            if (array.Contains(c))
                array.Remove(c);
        }

        internal void addProduct(ProductItem p)
        {
            m_product_list.Add(p);
            m_constraint_map.Add(p, new System.Collections.ArrayList());
        }

        internal void removeProduct(ProductItem p)
        {
            m_product_list.Remove(p);
            m_constraint_map.Remove(p);
        }

        internal System.Collections.ArrayList getConstraints(ProductItem p)
        {
            return (System.Collections.ArrayList)m_constraint_map[p];
        }

        internal System.Collections.ArrayList getSteps()
        {
            return m_step_list;
        }
        internal System.Collections.ArrayList getMachines()
        {
            return m_machine_list;
        }

        internal System.Collections.ArrayList getWorkers()
        {
            return m_worker_list;
        }

        internal System.Collections.ArrayList getProducts()
        {
            return m_product_list;
        }

        public int monitor_id
        {
            get { return m_monitor_id; }
            set
            {
                m_monitor_id = value;
            }
        }

        public ProductItem CurrentProduct
        {
            get { return m_current_product; }
            set
            {
                m_current_product = value;
            }
        }

        internal int getNextStepId()
        {
            m_next_step_id++;
            return m_next_step_id;
        }

        internal void loadCurrentProblem(String title)
        {
            Dictionary<Step, List<Mode>> modes_in_step = new Dictionary<Step, List<Mode>>();
            System.Collections.Hashtable all_resources = new System.Collections.Hashtable();
            System.Collections.Hashtable all_steps = new System.Collections.Hashtable();
            System.Collections.Hashtable all_products = new System.Collections.Hashtable();
            List<Constraint> all_constraints = new List<Constraint>();

            Dictionary<Product, List<Job>> jobs_in_product = new Dictionary<Product, List<Job>>();
            foreach (ProductItem p in m_product_list)
            {
                all_products[p] = new Product(p.Id, p.Name, p.Size);
                jobs_in_product.Add((Product)all_products[p], new List<Job>());
                for (int i = 0; i < p.Size; i++)
                {
                    jobs_in_product[(Product)all_products[p]].Add(new Job(Convert.ToInt32(p.JobsData[1, i].Value), Convert.ToInt32(p.JobsData[2, i].Value)));
                }
            }

            Dictionary<KeyValuePair<StepItem, int>, Mode> map_resource_constraints = new Dictionary<KeyValuePair<StepItem, int>, Mode>();
            foreach (StepItem s in m_step_list)
            {
                Step new_step = new Step(s.getID(), s.Text);
                modes_in_step.Add(new_step, new List<Mode>());
                all_steps[s] = new_step;
                int modes_id = 1;
                foreach (int key in s.getModesDictionary().Keys)
                {
                    DataGridView data = s.getModesDictionary()[key];
                    if (data.RowCount == 1)
                        continue;

                    Mode new_mode = new Mode();
                    for (int i = 0; i < data.RowCount; i++)
                    {
                        if (data["Resource", i] == null)
                            continue;
                        if (data["Resource", i].Value == null)
                            continue;
                        string name = data["Resource", i].Value.ToString();
                        if (!all_resources.Contains(name))
                            all_resources[name] = new Resource(name);
                        new_mode.operations.Add(new Operation(Convert.ToInt32(data["StartTime", i].Value),
                                                              Convert.ToInt32(data["EndTime", i].Value),
                                                              (Resource)all_resources[name]));
                    }
                    if (new_mode.operations.Count > 0)
                    {
                        new_mode.IdPerStep = modes_id ;
                        modes_id++;
                        modes_in_step[new_step].Add(new_mode);
                    }
                    map_resource_constraints.Add(new KeyValuePair<StepItem, int>(s, key), new_mode);
                }
            }

            List<ResourceConstraint> resource_constraints = new List<ResourceConstraint>();
            for (int i = 0; i < m_resource_constraints.RowCount; i++)
            {
                if (m_resource_constraints[0, i] == null || m_resource_constraints[0, i].Value == null)
                    break;
                Resource r = (Resource)all_resources[m_resource_constraints[0, i].Value.ToString()];
                StepModeItem from = (StepModeItem)m_resource_constraints[1, i].Value;
                StepModeItem to = (StepModeItem)m_resource_constraints[2, i].Value;
                int delay = (int)m_resource_constraints[3, i].Value;
                ResourceConstraint rc = new ResourceConstraint(r, map_resource_constraints[new KeyValuePair<StepItem, int>(from.myStep, from.myModeIndex)],
                                                                 map_resource_constraints[new KeyValuePair<StepItem, int>(to.myStep, to.myModeIndex)], delay);
                resource_constraints.Add(rc);
            }

            foreach (ProductItem p in m_constraint_map.Keys)
            {
                foreach (ConstraintItem c in (System.Collections.ArrayList)m_constraint_map[p])
                {
                    Step from = (Step)all_steps[c.getFromStep()];
                    Step to = (Step)all_steps[c.getToStep()];
                    all_constraints.Add(new Constraint((Product)all_products[p], from, to, c.MinQueueTime, c.MaxQueueTime));
                }
            }

            Product[] products_array = new Product[all_products.Count];
            all_products.Values.CopyTo(products_array, 0);
            List<Product> products_list = products_array.ToList<Product>();

            Resource[] resource_array = new Resource[all_resources.Count];
            all_resources.Values.CopyTo(resource_array, 0);
            List<Resource> resource_list = resource_array.ToList<Resource>();


            List<Step> step_list = new List<Step>();
            foreach (Step s in all_steps.Values)
            {
                step_list.Add(s);
            }

            Dictionary<Product, List<Step>> steps_in_product = new Dictionary<Product, List<Step>>();
            for (int i = 0; i < products_list.Count(); i++)
                steps_in_product.Add(products_list[i], new List<Step>());

            foreach (Constraint c in all_constraints)
            {
                if (!steps_in_product.ContainsKey(c.Product))
                    throw new EntryPointNotFoundException("found constraint for non existing product");
                List<Step> product_steps = steps_in_product[c.Product];
                if (!product_steps.Contains(c.StepFrom))
                    product_steps.Add(c.StepFrom);
                if (!product_steps.Contains(c.StepTo))
                    product_steps.Add(c.StepTo);
            } 
            ApplicManager.Instance.loadProblem(resource_list, modes_in_step, step_list, all_constraints, products_list, jobs_in_product, steps_in_product, resource_constraints, title);
        }

        internal bool isStepPrecedenceToNewStep(StepItem from_step, StepItem s)
        {
            ArrayList array = (ArrayList)m_constraint_map[m_current_product];
            foreach (ConstraintItem c in array)
            {
                if (from_step.Equals(c.getFromStep()))
                {
                    if (s == c.getToStep())
                        return true;
                    if (isStepPrecedenceToNewStep(c.getToStep(), s))
                        return true;
                }
            }
            return false;
        }

    }
}
