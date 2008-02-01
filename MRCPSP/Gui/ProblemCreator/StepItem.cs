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
    public class StepItem : System.Windows.Forms.Label
    {
        ModesMonitor m_mode_monitor;

        public StepItem()
        {
            m_mode_monitor = new ModesMonitor();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StepItem
            // 
            this.ResumeLayout(false);
            this.MouseEnter += new EventHandler(this.onMouseEntered);
            this.MouseDoubleClick += new MouseEventHandler(this.onMouseDoubleClicked);
            this.MouseLeave += new EventHandler(this.onMouseLeave);
            this.MouseClick += new MouseEventHandler(this.onMouseClicked);
        }

        private void onMouseEntered(object sender, EventArgs e)
        {
            this.ForeColor = Color.CornflowerBlue;
        }

        private void onMouseLeave(object sender, EventArgs e)
        {
            this.ForeColor = Color.Black;    
        }

        private void onMouseDoubleClicked(object sender, MouseEventArgs e)
        {
            Console.WriteLine("im double clicked");

        }

        private void onMouseClicked(object sender, MouseEventArgs e)
        {
            ProblemCreatorState.Instance.state.onStepClicked(
                    (CanvasEditor)this.Parent, this);
        }
    }
}
