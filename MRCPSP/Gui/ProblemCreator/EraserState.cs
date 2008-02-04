using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;

namespace MRCPSP.Gui.ProblemCreator
{
    class EraserState : StateBase
    {
            public EraserState() : base()
            {
                // do nothing
            }

            public override void onStepClicked(CanvasEditor canvas, StepItem s)
            {
                // erase step and all constraint items              
                System.Collections.ArrayList constraint_to_remove = new System.Collections.ArrayList();
                foreach (ConstraintItem c in ProblemCreatorState.Instance.getConstraints())
                {
                    if ((c.getFromStep() == s) || (c.getToStep() == s))
                    {
                        constraint_to_remove.Add(c);
                    }
                }
                foreach (ConstraintItem c in constraint_to_remove)
                {               
                        ProblemCreatorState.Instance.getConstraints().Remove(c);
                        c.Dispose();
               }
                ProblemCreatorState.Instance.getConstraints().Remove(s);
                s.Dispose();
                canvas.Refresh();
            }

            public override void onCanvasClicked(CanvasEditor canvas, MouseEventArgs e) {
                // do nothing
            }


            public override void onStepDoubleClicked(StepItem s) {
                // do nothing
            }

        }  
}
