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

           static ProblemCreatorState()
           {
               instance = new ProblemCreatorState();
           }

           private ProblemCreatorState() {
               state = new PointerState();
               m_worker_list = new System.Collections.ArrayList();
               m_machine_list = new System.Collections.ArrayList();

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
       }
}
