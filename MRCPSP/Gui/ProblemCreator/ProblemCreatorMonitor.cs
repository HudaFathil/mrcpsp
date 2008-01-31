using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace MRCPSP.Gui.ProblemCreator
{
  

    class ProblemCreatorMonitor : Form
    {
        private Panel panel1;
        private Button m_add_machine_button;
        private Panel panel2;
        private Panel panel3;
        private Label label1;
        private Button m_add_worker_button;
        private Panel m_center_panel;
        private TextBox m_problem_title_le;
        private Panel panel5;
        private GroupBox groupBox1;
        private Label label2;
        private ListBox m_machine_list;
        private SplitContainer splitContainer1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ListBox m_worker_list;
        private ToolStrip m_controls_strip;
        private ToolStripButton m_new_step_button;
        private ToolStripButton m_new_constraint_button;
        private ToolStripButton m_eraser_button;
        private ToolStripButton m_pointer_button;
        private CanvasEditor m_canvas_pic;


        public ProblemCreatorMonitor()
        {
            InitializeComponent();

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProblemCreatorMonitor));
            this.m_machine_list = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_add_machine_button = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_worker_list = new System.Windows.Forms.ListBox();
            this.m_add_worker_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_problem_title_le = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_center_panel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_controls_strip = new System.Windows.Forms.ToolStrip();
            this.m_new_step_button = new System.Windows.Forms.ToolStripButton();
            this.m_new_constraint_button = new System.Windows.Forms.ToolStripButton();
            this.m_eraser_button = new System.Windows.Forms.ToolStripButton();
            this.m_pointer_button = new System.Windows.Forms.ToolStripButton();
            this.m_canvas_pic = new MRCPSP.Gui.ProblemCreator.CanvasEditor();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.m_center_panel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_controls_strip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_canvas_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // m_machine_list
            // 
            this.m_machine_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_machine_list.FormattingEnabled = true;
            this.m_machine_list.Location = new System.Drawing.Point(6, 42);
            this.m_machine_list.Name = "m_machine_list";
            this.m_machine_list.Size = new System.Drawing.Size(133, 69);
            this.m_machine_list.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(557, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(155, 247);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(145, 237);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_machine_list);
            this.groupBox2.Controls.Add(this.m_add_machine_button);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(145, 118);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Machines";
            // 
            // m_add_machine_button
            // 
            this.m_add_machine_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_add_machine_button.Location = new System.Drawing.Point(6, 19);
            this.m_add_machine_button.Name = "m_add_machine_button";
            this.m_add_machine_button.Size = new System.Drawing.Size(133, 23);
            this.m_add_machine_button.TabIndex = 1;
            this.m_add_machine_button.Text = "add new machine";
            this.m_add_machine_button.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_worker_list);
            this.groupBox3.Controls.Add(this.m_add_worker_button);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(145, 115);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Workers";
            // 
            // m_worker_list
            // 
            this.m_worker_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_worker_list.FormattingEnabled = true;
            this.m_worker_list.Location = new System.Drawing.Point(6, 42);
            this.m_worker_list.Name = "m_worker_list";
            this.m_worker_list.Size = new System.Drawing.Size(133, 56);
            this.m_worker_list.TabIndex = 4;
            // 
            // m_add_worker_button
            // 
            this.m_add_worker_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_add_worker_button.Location = new System.Drawing.Point(6, 19);
            this.m_add_worker_button.Name = "m_add_worker_button";
            this.m_add_worker_button.Size = new System.Drawing.Size(133, 23);
            this.m_add_worker_button.TabIndex = 3;
            this.m_add_worker_button.Text = "add new worker";
            this.m_add_worker_button.UseVisualStyleBackColor = true;
            this.m_add_worker_button.Click += new System.EventHandler(this.m_add_worker_button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_problem_title_le);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 292);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(712, 61);
            this.panel2.TabIndex = 2;
            // 
            // m_problem_title_le
            // 
            this.m_problem_title_le.Location = new System.Drawing.Point(578, 29);
            this.m_problem_title_le.Name = "m_problem_title_le";
            this.m_problem_title_le.Size = new System.Drawing.Size(100, 20);
            this.m_problem_title_le.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(575, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "problem title: ";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(712, 45);
            this.panel3.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(712, 45);
            this.label2.TabIndex = 4;
            this.label2.Text = "Problem Creator";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_center_panel
            // 
            this.m_center_panel.Controls.Add(this.panel5);
            this.m_center_panel.Controls.Add(this.panel1);
            this.m_center_panel.Controls.Add(this.m_canvas_pic);
            this.m_center_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_center_panel.Location = new System.Drawing.Point(0, 45);
            this.m_center_panel.Name = "m_center_panel";
            this.m_center_panel.Size = new System.Drawing.Size(712, 247);
            this.m_center_panel.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(69, 247);
            this.panel5.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_controls_strip);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(69, 241);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "controls";
            // 
            // m_controls_strip
            // 
            this.m_controls_strip.AllowMerge = false;
            this.m_controls_strip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_controls_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_new_step_button,
            this.m_new_constraint_button,
            this.m_eraser_button,
            this.m_pointer_button});
            this.m_controls_strip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.m_controls_strip.Location = new System.Drawing.Point(5, 18);
            this.m_controls_strip.Name = "m_controls_strip";
            this.m_controls_strip.Size = new System.Drawing.Size(59, 218);
            this.m_controls_strip.TabIndex = 5;
            this.m_controls_strip.Text = "toolStrip1";
            this.m_controls_strip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // m_new_step_button
            // 
            this.m_new_step_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_new_step_button.Image = global::MRCPSP.Properties.Resources.new_step_pic;
            this.m_new_step_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_new_step_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_new_step_button.Name = "m_new_step_button";
            this.m_new_step_button.Size = new System.Drawing.Size(57, 47);
            this.m_new_step_button.Text = "toolStripButton1";
            this.m_new_step_button.Click += new System.EventHandler(this.m_new_step_button_Click);
            // 
            // m_new_constraint_button
            // 
            this.m_new_constraint_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_new_constraint_button.Image = global::MRCPSP.Properties.Resources.new_arrow_pic;
            this.m_new_constraint_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_new_constraint_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_new_constraint_button.Name = "m_new_constraint_button";
            this.m_new_constraint_button.Size = new System.Drawing.Size(57, 47);
            this.m_new_constraint_button.Text = "toolStripButton2";
            this.m_new_constraint_button.Click += new System.EventHandler(this.m_new_constraint_button_Click);
            // 
            // m_eraser_button
            // 
            this.m_eraser_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_eraser_button.Image = global::MRCPSP.Properties.Resources.eraser_pic;
            this.m_eraser_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_eraser_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_eraser_button.Name = "m_eraser_button";
            this.m_eraser_button.Size = new System.Drawing.Size(57, 48);
            this.m_eraser_button.Text = "toolStripButton3";
            this.m_eraser_button.Click += new System.EventHandler(this.m_eraser_button_Click);
            // 
            // m_pointer_button
            // 
            this.m_pointer_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_pointer_button.Image = ((System.Drawing.Image)(resources.GetObject("m_pointer_button.Image")));
            this.m_pointer_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_pointer_button.Name = "m_pointer_button";
            this.m_pointer_button.Size = new System.Drawing.Size(57, 20);
            this.m_pointer_button.Text = "toolStripButton4";
            this.m_pointer_button.Click += new System.EventHandler(this.m_pointer_button_Click);
            // 
            // m_canvas_pic
            // 
            this.m_canvas_pic.BackColor = System.Drawing.Color.White;
            this.m_canvas_pic.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_canvas_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_canvas_pic.Location = new System.Drawing.Point(0, 0);
            this.m_canvas_pic.Name = "m_canvas_pic";
            this.m_canvas_pic.Size = new System.Drawing.Size(712, 247);
            this.m_canvas_pic.TabIndex = 5;
            this.m_canvas_pic.TabStop = false;
            // 
            // ProblemCreatorMonitor
            // 
            this.ClientSize = new System.Drawing.Size(712, 353);
            this.Controls.Add(this.m_center_panel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "ProblemCreatorMonitor";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.m_center_panel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_controls_strip.ResumeLayout(false);
            this.m_controls_strip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_canvas_pic)).EndInit();
            this.ResumeLayout(false);

        }

        private void m_add_worker_button_Click(object sender, EventArgs e)
        {
            Console.WriteLine("worker added");
          
        }

        private void m_new_step_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance.state = new StepState();
            m_new_step_button.CheckState = CheckState.Checked;
            uncheck_other((ToolStripButton) sender);
      
        }

        private void m_new_constraint_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance.state = new ConstraintFromState();
            m_new_constraint_button.CheckState = CheckState.Checked;
            uncheck_other((ToolStripButton)sender);
        }

        private void m_eraser_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance.state = new EraserState();
            m_eraser_button.CheckState = CheckState.Checked;
            uncheck_other((ToolStripButton)sender);
        }

        private void m_pointer_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance.state = new PointerState();
            m_pointer_button.CheckState = CheckState.Checked;
            uncheck_other((ToolStripButton)sender);
        }

        private void uncheck_other(ToolStripButton b)
        {
            ToolStripItemCollection collection = m_controls_strip.Items;
            
            foreach (ToolStripButton item in collection)
            {               
         
                  if ((!item.Equals(b)) && (item.CheckState == CheckState.Checked))
                  {
                     item.CheckState = CheckState.Unchecked;                    
                  }
                
            }
        }
    }

  
}
