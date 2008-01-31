﻿using System;
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
       
        public StepState() : base()
        {
                // do nothing
        }

        public override void onStepClicked() {
                // do nothing
        }

        public override void onCanvasClicked(CanvasEditor canvas, MouseEventArgs e)
        {              
            Image img = (Image)global::MRCPSP.Properties.Resources.new_step_pic;
            StepItem si = new StepItem();
            si.Image = img;
            si.Location = new System.Drawing.Point(e.X + canvas.Left - img.Width ,
                      e.Y + canvas.Top - img.Height / 2);
            si.Show();         
            si.Parent = canvas;                      
        }

            public override void onConstraintClicked() {
                // do nothing
            }

            public override void onStepDoubleClicked() {
                // open mode window
            }

        }

    
}
