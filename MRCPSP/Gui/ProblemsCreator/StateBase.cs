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
        private ConstraintItem m_last_entered_item;

        public StateBase(int id)
        {
            m_last_entered_item = null;
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

        public void repaintConstraintsIfNeeded(CanvasEditor canvas, MouseEventArgs e)
        {
            ConstraintItem c = findConstraintnearPos(e.X, e.Y);
            if (c == null)
            {
                if (m_last_entered_item != null)
                {
                    m_last_entered_item.mouseExit();
                    m_last_entered_item = null;
                    canvas.Refresh();
                }
                return;
            }
            c.mouseEnter();
            m_last_entered_item = c;
            canvas.Refresh();
        }
    }
}
