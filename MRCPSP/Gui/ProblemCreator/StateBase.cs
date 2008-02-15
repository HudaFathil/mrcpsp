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
        abstract public void onStepDoubleClicked(StepItem s);
    }
}
