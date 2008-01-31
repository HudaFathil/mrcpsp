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
    class StepItem : System.Windows.Forms.Label
    {
        public StepItem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StepItem
            // 
            this.ResumeLayout(false);
            this.MouseMove += new MouseEventHandler(this.onMouseEntered);
            this.MouseDoubleClick += new MouseEventHandler(this.onMouseDoubleClicked);
        }


        private void onMouseEntered(object sender, MouseEventArgs e)
        {
            Console.WriteLine("im over");
        }

        private void onMouseDoubleClicked(object sender, MouseEventArgs e)
        {
            Console.WriteLine("im double clicked");
        }
    }
}
