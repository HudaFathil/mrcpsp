using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Gui.ProblemCreator
{
  
       public class ProblemCreatorState {
           private static ProblemCreatorState instance;

           public StateBase state;

           // problem data
           private System.Collections.ArrayList m_machine_list;
           private System.Collections.ArrayList m_worker_list;
           private System.Collections.ArrayList m_constraint_list;
           private System.Collections.ArrayList m_step_list;

           static ProblemCreatorState()
           {
               instance = new ProblemCreatorState();
           }

           private ProblemCreatorState() {
               state = new PointerState();
               m_worker_list = new System.Collections.ArrayList();
               m_machine_list = new System.Collections.ArrayList();
               m_constraint_list = new System.Collections.ArrayList();
               m_step_list = new System.Collections.ArrayList();
           }

           public static ProblemCreatorState Instance
           {
               get { return instance; }
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

       }
}
