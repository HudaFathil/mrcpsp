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

        public ConstraintToState(StepItem from)
            : base()
        {
            m_from_step = from;
        }

        public override void onStepClicked(CanvasEditor c, StepItem s)
        {
            ConstraintItem item = new ConstraintItem(c, m_from_step, s);
            ProblemCreatorState.Instance.addConstraint(item);       
        }

        public override void onCanvasClicked(CanvasEditor c, MouseEventArgs e)
        {
            // do nothing
        }

        public override void onStepDoubleClicked(StepItem s)
        {
            // do nothing
        }

    }

}
