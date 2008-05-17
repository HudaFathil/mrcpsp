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
    class PointerState : StateBase
    {
       
            public PointerState(int id) : base(id)
            {
                // do nothing
            }

            public override void onStepClicked(CanvasEditor c, StepItem s)
            {
                // do nothing
            }

            public override void onCanvasClicked(CanvasEditor canvas, MouseEventArgs e) {
                ConstraintItem c = findConstraintnearPos(e.X, e.Y);
                if (c == null)
                    return;
                c.openPropertiesWindow();
                canvas.Refresh();
            }

            public override void onCanvasMoved(CanvasEditor canvas, MouseEventArgs e)
            {
                // do nothing
            }

            public override void onStepDoubleClicked(StepItem s) {
                s.onShowModeMonitor();
                // open mode window
            }

    }
}
