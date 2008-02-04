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

        private bool m_isDragging;
        private int m_offset_x;
        private int m_offset_y;
        public StepItem()
        {
            m_mode_monitor = new ModesMonitor();
            InitializeComponent();
            m_isDragging = false;
          
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
            this.MouseDown += new MouseEventHandler(StepItem_MouseDown);
            this.MouseMove += new MouseEventHandler(StepItem_MouseMove);
            this.MouseUp += new MouseEventHandler(StepItem_MouseUp);
        }

        void StepItem_MouseUp(object sender, MouseEventArgs e)
        {
            m_isDragging = false;
        }

        void StepItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_isDragging)
            {
                this.Left = e.X + this.Left - m_offset_x;
                this.Top = e.Y + this.Top - m_offset_y;
                this.Parent.Refresh();
            }
        }

        void StepItem_MouseDown(object sender, MouseEventArgs e)
        {
            m_offset_x = e.X;
            m_offset_y = e.Y;
            m_isDragging = true;
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
            ProblemCreatorState.Instance.state.onStepDoubleClicked(this);
        }

        private void onMouseClicked(object sender, MouseEventArgs e)
        {
            ProblemCreatorState.Instance.state.onStepClicked(
                    (CanvasEditor)this.Parent, this);
        }

        public void onShowModeMonitor()
        {          
            m_mode_monitor.ShowDialog(new Form());         
        }
    }
}
