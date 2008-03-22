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
                // do nothing
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
             
            }

            public override void onStepDoubleClicked(StepItem s) {
                // do nothing
            }

            private ConstraintItem findConstraintnearPos(int x, int y)
            {
                foreach (ProductItem p in ProblemCreatorState.Instance(monitor_id).getProducts())
                {
                    foreach (ConstraintItem c in ProblemCreatorState.Instance(monitor_id).getConstraints(p))
                    {
                        Point p1 = c.getFromPoint();
                        Point p2 = c.getToPoint();
                        double a = (double)(p1.Y - p2.Y) / (double)(p1.X - p2.X);
                        double b = (double)p1.Y - (a * p1.X);
                        double res = (double)y - a * x - b;
                        if (res < 10) // tolerance for being close to the arrow
                            return c;
                    }
                }
                return null;
            }
        }  
}
