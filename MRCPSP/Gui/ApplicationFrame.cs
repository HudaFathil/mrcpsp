using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MRCPSP.Gui.ProblemCreator;
using MRCPSP.Gui.Logger;
using MRCPSP.Logger;
using MRCPSP.Gui.ProblemSolver;
using MRCPSP.Gui.StatisticsViewer;
using MRCPSP.Controllers;

namespace MRCPSP.Gui {
    public class ApplicationFrame : Form {
        private ToolStrip toolStrip1;
        private ToolStripButton m_create_new_problem_button;
        private ToolStripButton m_solve_problem_button;
        private ToolStripButton m_view_statistics_button;
        private StatusStrip statusStrip1;
        private MRCPSP.Logger.Logger m_logger;
        private static int m_problem_monitor_id;
        private ProblemSolverMonitor m_problem_solver_monitor;
        private StatisticsMonitor m_statistics_monitor;
        private ProgressBar m_progress_bar;
        private IContainer components;
        private Label label1;
        public BackgroundWorker backgroundWorker1;
        MainMenu MyMenu; 
 
        public ApplicationFrame() { 
            //initalizing the logger
            this.m_logger = LoggerFactory.getSimpleLogger();
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
            MenuItem subm4 = new MenuItem("Log Controller");
            m2.MenuItems.Add(subm4); 
            // Add event handlers for the menu items. 
            subm1.Click += new EventHandler(MMNewClick);        
            subm2.Click += new EventHandler(MMExitClick);
            subm3.Click += new EventHandler(MMSelectDBClick);
            subm4.Click += new EventHandler(MMLogController); 
            // Assign the menu to the form. 
            Menu = MyMenu;
            InitializeComponent();

            // update buttons action
            this.m_create_new_problem_button.Click += new System.EventHandler(this.onStartNewProblem);
            m_problem_monitor_id = 0;


            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            ApplicManager.Instance.signBackgroundWorker(backgroundWorker1);

        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show("error in execution", "notify");
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("execution was terminated by the user", "notify");
            }
            else
            {
                MessageBox.Show("execution done", "notify");
            }         
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            m_progress_bar.Value = e.ProgressPercentage;
        }

    // Handler for main menu Open selection. 
        protected void MMLogController(object who, EventArgs e)
        {
            LoggerFrame logC = new LoggerFrame();
            logC.MdiParent = this;
            logC.Show();
            m_logger.debug("Started log controller");
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.m_create_new_problem_button = new System.Windows.Forms.ToolStripButton();
            this.m_solve_problem_button = new System.Windows.Forms.ToolStripButton();
            this.m_view_statistics_button = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_progress_bar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
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
            this.toolStrip1.Size = new System.Drawing.Size(948, 52);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // m_create_new_problem_button
            // 
            this.m_create_new_problem_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_create_new_problem_button.Image = global::MRCPSP.Properties.Resources.new_problem;
            this.m_create_new_problem_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_create_new_problem_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_create_new_problem_button.Name = "m_create_new_problem_button";
            this.m_create_new_problem_button.Size = new System.Drawing.Size(52, 49);
            this.m_create_new_problem_button.Text = "toolStripButton1";
            this.m_create_new_problem_button.ToolTipText = "start new problem";
            // 
            // m_solve_problem_button
            // 
            this.m_solve_problem_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_solve_problem_button.Image = global::MRCPSP.Properties.Resources.bulb;
            this.m_solve_problem_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_solve_problem_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_solve_problem_button.Name = "m_solve_problem_button";
            this.m_solve_problem_button.Size = new System.Drawing.Size(52, 49);
            this.m_solve_problem_button.Text = "toolStripButton2";
            this.m_solve_problem_button.ToolTipText = "open problem solver monitor";
            this.m_solve_problem_button.Click += new System.EventHandler(this.m_solve_problem_button_Click);
            // 
            // m_view_statistics_button
            // 
            this.m_view_statistics_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_view_statistics_button.Image = global::MRCPSP.Properties.Resources.graphs;
            this.m_view_statistics_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_view_statistics_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_view_statistics_button.Name = "m_view_statistics_button";
            this.m_view_statistics_button.Size = new System.Drawing.Size(62, 49);
            this.m_view_statistics_button.Text = "toolStripButton3";
            this.m_view_statistics_button.ToolTipText = "open statistics viewer monitor";
            this.m_view_statistics_button.Click += new System.EventHandler(this.m_view_statistics_button_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 549);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(948, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_progress_bar
            // 
            this.m_progress_bar.Location = new System.Drawing.Point(664, 548);
            this.m_progress_bar.Name = "m_progress_bar";
            this.m_progress_bar.Size = new System.Drawing.Size(194, 23);
            this.m_progress_bar.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(582, 549);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Progress:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // ApplicationFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 571);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_progress_bar);
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
            ProblemCreatorMonitor pcm = new ProblemCreatorMonitor(m_problem_monitor_id);
            pcm.MdiParent = this;
            m_problem_monitor_id++;
            pcm.Show();
           
            Console.WriteLine("create new monitor");
        }

        private void m_solve_problem_button_Click(object sender, EventArgs e)
        {
            if (m_problem_solver_monitor != null)
                m_problem_solver_monitor.Dispose();
            m_problem_solver_monitor = new ProblemSolverMonitor();
            m_problem_solver_monitor.MdiParent = this;
            m_problem_solver_monitor.Show();
            m_problem_solver_monitor.signBackgroundWorker(backgroundWorker1);
        }

        private void m_view_statistics_button_Click(object sender, EventArgs e)
        {
            if (m_statistics_monitor != null)
                m_statistics_monitor.Dispose();
            m_statistics_monitor = new StatisticsMonitor();
            m_statistics_monitor.MdiParent = this;
            m_statistics_monitor.Show();
        }

    
    }


}
