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
    class ConstraintFromState: StateBase
    {
       
            public ConstraintFromState(int id) : base(id)
            {
                // do nothing
            }

            public override void onStepClicked(CanvasEditor c, StepItem s) {
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

            public override void onStepDoubleClicked(StepItem s) {
                // do nothing
            }

        }

}
