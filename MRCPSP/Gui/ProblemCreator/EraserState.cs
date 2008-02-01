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

            public override void onStepClicked(CanvasEditor c, StepItem s)
            {
                // do nothing
            }

            public override void onCanvasClicked(CanvasEditor canvas, MouseEventArgs e) {
                // do nothing
            }

            public override void onConstraintClicked() {
                // do nothing
            }

            public override void onStepDoubleClicked() {
                // open mode window
            }

        }  
}
