using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MRCPSP.Gui.ProblemCreator;

namespace MRCPSP.Gui {
    public class ApplicationFrame : Form {
        private ToolStrip toolStrip1;
        private ToolStripButton m_create_new_problem_button;
        private ToolStripButton m_solve_problem_button;
        private ToolStripButton m_view_statistics_button;
        private StatusStrip statusStrip1; 
        MainMenu MyMenu; 
 
        public ApplicationFrame() { 
            // Create a main menu object. 
            MyMenu  = new MainMenu(); 
            // Add top-level menu items to the menu. 
            MenuItem m1 = new MenuItem("File"); 
            MyMenu.MenuItems.Add(m1); 
            MenuItem m2 = new MenuItem("Tools"); 
            MyMenu.MenuItems.Add(m2); 
            // Create File submenu 
            MenuItem subm1 = new MenuItem("New"); 
            m1.MenuItems.Add(subm1); 
            MenuItem subm2 = new MenuItem("Exit"); 
            m1.MenuItems.Add(subm2); 
            // Create Tools submenu
            MenuItem subm3 = new MenuItem("select DB");
            m2.MenuItems.Add(subm3); 
            // Add event handlers for the menu items. 
            subm1.Click += new EventHandler(MMNewClick);        
            subm2.Click += new EventHandler(MMExitClick);
            subm3.Click += new EventHandler(MMSelectDBClick); 
            // Assign the menu to the form. 
            Menu = MyMenu;
            InitializeComponent();

            // update buttons action
            this.m_create_new_problem_button.Click += new System.EventHandler(this.onStartNewProblem);

        }   
  
  // Handler for main menu Open selection. 
        protected void MMNewClick(object who, EventArgs e) {
            MessageBox.Show("Inactive", "Inactive",
                            MessageBoxButtons.OK);
        }
 
  // Handler for main menu Exit selection. 
        protected void MMExitClick(object who, EventArgs e) {
            DialogResult result = MessageBox.Show("Stop Program?",
                                    "Terminate",
                                     MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) Application.Exit();
        }

  // Handler for main menu Exit selection. 
        protected void MMSelectDBClick(object who, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Stop Program?",
                                    "Terminate",
                                     MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) Application.Exit();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationFrame));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_create_new_problem_button = new System.Windows.Forms.ToolStripButton();
            this.m_solve_problem_button = new System.Windows.Forms.ToolStripButton();
            this.m_view_statistics_button = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_create_new_problem_button,
            this.m_solve_problem_button,
            this.m_view_statistics_button});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(707, 50);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 377);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(707, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_create_new_problem_button
            // 
            this.m_create_new_problem_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_create_new_problem_button.Image = global::MRCPSP.Properties.Resources.new_problem;
            this.m_create_new_problem_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_create_new_problem_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_create_new_problem_button.Name = "m_create_new_problem_button";
            this.m_create_new_problem_button.Size = new System.Drawing.Size(52, 47);
            this.m_create_new_problem_button.Text = "toolStripButton1";
            this.m_create_new_problem_button.ToolTipText = "start new problem";
            // 
            // m_solve_problem_button
            // 
            this.m_solve_problem_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_solve_problem_button.Image = ((System.Drawing.Image)(resources.GetObject("m_solve_problem_button.Image")));
            this.m_solve_problem_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_solve_problem_button.Name = "m_solve_problem_button";
            this.m_solve_problem_button.Size = new System.Drawing.Size(23, 47);
            this.m_solve_problem_button.Text = "toolStripButton2";
            this.m_solve_problem_button.ToolTipText = "open problem solver monitor";
            // 
            // m_view_statistics_button
            // 
            this.m_view_statistics_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_view_statistics_button.Image = ((System.Drawing.Image)(resources.GetObject("m_view_statistics_button.Image")));
            this.m_view_statistics_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_view_statistics_button.Name = "m_view_statistics_button";
            this.m_view_statistics_button.Size = new System.Drawing.Size(23, 22);
            this.m_view_statistics_button.Text = "toolStripButton3";
            this.m_view_statistics_button.ToolTipText = "open statistics viewer monitor";
            // 
            // ApplicationFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 399);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "ApplicationFrame";
            this.Text = "MRCPSP";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void onStartNewProblem(object who, EventArgs e) {
            ProblemCreatorMonitor pcm = new ProblemCreatorMonitor();
            pcm.MdiParent = this;
            pcm.Show();
            Console.WriteLine("create new monitor");
        }
    }


}
