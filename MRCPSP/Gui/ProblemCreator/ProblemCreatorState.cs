using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Gui.ProblemCreator
{
  
       public class ProblemCreatorState {

           private static ProblemCreatorState instance;

           public StateBase state;

           static ProblemCreatorState()
           {
               instance = new ProblemCreatorState();
           }

           private ProblemCreatorState() {
               state = new PointerState();
           }

           public static ProblemCreatorState Instance
           {
               get { return instance; }
           }
    }
}
