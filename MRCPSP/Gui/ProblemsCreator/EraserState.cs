using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MRCPSP.Gui.ProblemCreator
{
    class EraserState : StateBase
    {

        public EraserState(int id) : base(id)
        {   
        }

        public override void onStepClicked(CanvasEditor canvas, StepItem s)
        {
            // erase step and all constraint items              
            System.Collections.ArrayList constraint_to_remove = new System.Collections.ArrayList();
            foreach (ProductItem p in ProblemCreatorState.Instance(monitor_id).getProducts())
            {
                foreach (ConstraintItem c in ProblemCreatorState.Instance(monitor_id).getConstraints(p))
                {
                    if ((c.getFromStep() == s) || (c.getToStep() == s))
                    {
                        constraint_to_remove.Add(c);
                    }
                }
                foreach (ConstraintItem c in constraint_to_remove)
                {
                    ProblemCreatorState.Instance(monitor_id).getConstraints(p).Remove(c);
                    c.Dispose();
                }
            }
            ProblemCreatorState.Instance(monitor_id).getSteps().Remove(s);
            s.Dispose();
            canvas.Refresh();
        }

        public override void onCanvasClicked(CanvasEditor canvas, MouseEventArgs e) {
            ConstraintItem c = findConstraintnearPos(e.X, e.Y);
            if (c == null)
                return;
            ProblemCreatorState.Instance(monitor_id).removeConstraint(c);
            c.Dispose();
            canvas.Refresh();
        }

        public override void onCanvasMoved(CanvasEditor canvas, MouseEventArgs e)
        {
            this.repaintConstraintsIfNeeded(canvas, e);
        }

        public override void onStepDoubleClicked(StepItem s) {
            // do nothing
        }
    }  
}
