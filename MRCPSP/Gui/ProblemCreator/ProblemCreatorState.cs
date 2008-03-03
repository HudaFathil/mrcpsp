using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;

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
           private System.Collections.ArrayList m_constraint_list;
           private System.Collections.ArrayList m_step_list;
           private int m_next_step_id;
  
           public MonitorState(int id) {
               m_monitor_id = id;
               state = new PointerState(id);
               m_worker_list = new System.Collections.ArrayList();
               m_machine_list = new System.Collections.ArrayList();
               m_constraint_list = new System.Collections.ArrayList();
               m_step_list = new System.Collections.ArrayList();
               m_next_step_id = 0;
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

           internal void addConstraint(ConstraintItem c)
           {

               m_constraint_list.Add(c);
           }
           internal void removeConstraint(ConstraintItem c)
           {

               m_constraint_list.Remove(c);
           }

           internal System.Collections.ArrayList getConstraints()
           {
               return m_constraint_list;
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
           public int monitor_id
           {
               get { return m_monitor_id; }
               set
               {
                   m_monitor_id = value;
               }
           }

           internal int getNextStepId()
           {
               m_next_step_id++;
               return m_next_step_id;
           }

           internal void loadCurrentProblem()
           {
               ApplicManager.Instance.loadProblemFromDataBase("None");
           }
       }
}
