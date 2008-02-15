

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


    public class CanvasEditor : PictureBox
    {

        private int m_monitor_id;
        public CanvasEditor() {
            InitializeComponent();
        }
        public CanvasEditor(int monitor_id)
        {
            m_monitor_id = monitor_id;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.BackColor = System.Drawing.Color.White;
            this.ResumeLayout(false);

            this.MouseClick += new MouseEventHandler(this.onMouseClicked);
            this.ClientSize = new System.Drawing.Size(628, 426);
            this.Resize += new System.EventHandler(this.OnResize2);
            this.Paint += new PaintEventHandler(CanvasEditor_Paint);     
        }

        void CanvasEditor_Paint(object sender, PaintEventArgs e)
        {
            foreach (ConstraintItem c in ProblemCreatorState.Instance(monitor_id).getConstraints())
            {
                c.ConstraintItem_Paint(sender, e);
            }
        }

        private void onMouseClicked(object sender, MouseEventArgs e)
        {
            ProblemCreatorState.Instance(monitor_id).state.onCanvasClicked((CanvasEditor)sender, e);
        }
       
        protected void OnResize2(object sender, System.EventArgs e)
        {
            Invalidate();
            foreach (ConstraintItem c in ProblemCreatorState.Instance(monitor_id).getConstraints())
            {
                c.ConstraintItem_Resize(sender, e);
            }
            
        }

        public int monitor_id
        {
            get { return m_monitor_id; }
            set
            {
                m_monitor_id = value;
            }
        }

    }
}
