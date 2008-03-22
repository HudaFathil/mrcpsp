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
using System.Collections;



namespace MRCPSP.Gui.ProblemCreator
{
    class ConstraintToState : StateBase
    {
        private StepItem m_from_step;

        public ConstraintToState(int id, StepItem from)
            : base(id)
        {
            m_from_step = from;
        }

        public override void onStepClicked(CanvasEditor c, StepItem s)
        {
            if (ProblemCreatorState.Instance(monitor_id).CurrentProduct == null)
                return;
            ConstraintItem item = new ConstraintItem(c, m_from_step, s);
            ProblemCreatorState.Instance(monitor_id).addConstraint(ProblemCreatorState.Instance(monitor_id).CurrentProduct, item);
            ProblemCreatorState.Instance(monitor_id).state = new ConstraintToState(monitor_id, s);        
        }

        public override void onCanvasClicked(CanvasEditor c, MouseEventArgs e)
        {
            // do nothing
        }

        public override void onCanvasMoved(CanvasEditor canvas, MouseEventArgs e)
        {
            // do nothing
        }

        public override void onStepDoubleClicked(StepItem s)
        {
            // do nothing
        }

    }

}
