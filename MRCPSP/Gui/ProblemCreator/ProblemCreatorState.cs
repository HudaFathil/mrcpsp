using System;
using System.Collections;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;
using MRCPSP.CommonTypes;

namespace MRCPSP.Gui.ProblemCreator
{
  
       public class ProblemCreatorState {
           private static ProblemCreatorState instance;

           public static System.Collections.Hashtable m_state_for_monitor;
           
           static ProblemCreatorState()
           {
               instance = new ProblemCreatorState();
               m_state_for_monitor = new System.Collections.Hashtable();
               
           }

           public static MonitorState Instance(int id)
           {
               if (! m_state_for_monitor.ContainsKey(id))
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
           private ProductItem m_current_product;

           private int m_next_step_id;
  
           public MonitorState(int id) {
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

           internal void addWorker(Worker w)
           {
               m_worker_list.Add(w);
           }

           internal void addMachine(Machine m)
           {
             
              m_machine_list.Add(m);
           }

           internal void addStep(StepItem s)
           {
               m_step_list.Add(s);
           }

           internal void addConstraint(ProductItem p, ConstraintItem c)
           {
               if (!m_constraint_map.ContainsKey(p))
                   m_constraint_map.Add(p, new System.Collections.ArrayList());
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
               if (!m_constraint_map.ContainsKey(p))
                   return;
               System.Collections.ArrayList array = (System.Collections.ArrayList)m_constraint_map[p];
               if (array.Contains(c))
                    array.Remove(c);
           }

           internal void addProduct(ProductItem p)
           {
               m_product_list.Add(p);
           }

           internal System.Collections.ArrayList getConstraints(ProductItem p)
           {
               if (!m_constraint_map.ContainsKey(p))
                   m_constraint_map.Add(p, new System.Collections.ArrayList());
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

           internal void loadCurrentProblem()
           {
               System.Collections.Hashtable modes_in_step = new System.Collections.Hashtable();
               System.Collections.Hashtable all_resources = new System.Collections.Hashtable();
               System.Collections.Hashtable all_steps = new System.Collections.Hashtable();
               System.Collections.Hashtable all_products = new System.Collections.Hashtable();
               System.Collections.ArrayList all_constraints = new System.Collections.ArrayList();

               foreach (ProductItem p in m_product_list)
               {
                   all_products[p] = new Product(p.Id, p.Name, p.Size);
               }

               foreach (StepItem s in m_step_list)
               {
                   Step new_step = new Step(s.getID(), s.Text);
                   modes_in_step.Add(new_step, new System.Collections.ArrayList());
                   all_steps[s] = new_step;

                   foreach (ModeItem m in s.getAllModes())
                   {
                       Mode new_mode = new Mode();
                       for (int i = 0; i < m.m_resource_list.Items.Count; i++)
                       {
                           string name = m.m_resource_list.Items[i].ToString();
                           if (!all_resources.Contains(name))
                               all_resources[name] = new Resource(name);
                           new_mode.operations.Add(new Operation(Convert.ToInt32(m.m_start_time_list.Items[i]),
                                                                 Convert.ToInt32(m.m_end_time_list.Items[i]),
                                                                 (Resource)all_resources[name]));
                           new_mode.name =Convert.ToInt32(m.m_id.Text) + 1;
                       }
                       ((System.Collections.ArrayList)modes_in_step[new_step]).Add(new_mode);
                   }
               }
              
               foreach (ProductItem p in m_constraint_map.Keys)
               {
                   foreach (ConstraintItem c in (System.Collections.ArrayList)m_constraint_map[p])
                   {
                       Step from = (Step)all_steps[c.getFromStep()];
                       Step to = (Step)all_steps[c.getToStep()];
                       all_constraints.Add(new Constraint((Product)all_products[p],from ,to));
                   }
               }
       
               Product[] products_array = new Product[all_products.Count];
               all_products.Values.CopyTo(products_array, 0);
               Resource[] resource_array = new Resource[all_resources.Count];
               all_resources.Values.CopyTo(resource_array, 0);

               Step[] step_array = new Step[all_steps.Count];
               ArrayList steps = new ArrayList();
               
               foreach (Step s in all_steps.Values)
               {
                   steps.Add(s);
               }
               steps.Sort(new StepComparer());
               steps.CopyTo(step_array, 0);

               all_constraints.Cast<Constraint>();
               ApplicManager.Instance.loadProblem(resource_array, modes_in_step,step_array, all_constraints, products_array);
           }
       }
}
