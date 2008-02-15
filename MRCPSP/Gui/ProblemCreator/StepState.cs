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
    class StepState : StateBase
    {
       
        public StepState(int id) : base(id)
        {
                // do nothing
        }

        public override void onStepClicked(CanvasEditor c, StepItem s) {
                // do nothing
        }

        public override void onCanvasClicked(CanvasEditor canvas, MouseEventArgs e)
        {
            StepItem si = new StepItem(monitor_id);
            Image img = (Image)global::MRCPSP.Properties.Resources.new_step_pic;
            si.Image = img;
            si.Location = new System.Drawing.Point(e.X + canvas.Left - img.Width ,
                      e.Y + canvas.Top - img.Height / 2);
            si.Parent = canvas;
            ProblemCreatorState.Instance(monitor_id).addStep(si);
        }

      
        public override void onStepDoubleClicked(StepItem s) {
                // do nothing
            }

        }

    
}
