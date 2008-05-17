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
    public abstract class StateBase
    {
        private int m_monitor_id;

        public StateBase(int id)
        {
            m_monitor_id = id;
        }
        public int monitor_id
        {
            get { return m_monitor_id; }
            set
            {
                m_monitor_id = value;            
            }
        }

        abstract public void onStepClicked(CanvasEditor c, StepItem s);
        abstract public void onCanvasClicked(CanvasEditor c, MouseEventArgs e);
        abstract public void onCanvasMoved(CanvasEditor c, MouseEventArgs e);
        abstract public void onStepDoubleClicked(StepItem s);

        public ConstraintItem findConstraintnearPos(int x, int y)
        {
            foreach (ProductItem p in ProblemCreatorState.Instance(monitor_id).getProducts())
            {
                foreach (ConstraintItem c in ProblemCreatorState.Instance(monitor_id).getConstraints(p))
                {
                    Point p1 = c.getFromPoint();
                    Point p2 = c.getToPoint();
                    if (p1.X > x && p2.X > x)
                        return null;
                    if (p1.X < x && p2.X < x)
                        return null;
                    double a = (double)(p1.Y - p2.Y) / (double)(p1.X - p2.X);
                    double b = (double)p1.Y - (a * p1.X);
                    double res = Math.Abs((double)y - a * x - b);
                    if (res < 10) // tolerance for being close to the arrow
                        return c;
                }
            }
            return null;
        }
    }
}
