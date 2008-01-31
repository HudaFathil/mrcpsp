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


    public class CanvasEditor : System.Windows.Forms.PictureBox
    {
           
        public CanvasEditor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CanvasEditor
            // 
            this.BackColor = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

            this.MouseClick += new MouseEventHandler(this.onMouseClicked);
    
        }

        private void onMouseClicked(object sender, MouseEventArgs e)
        {
            ProblemCreatorState.Instance.state.onCanvasClicked((CanvasEditor)sender, e);
        }

    }
}
