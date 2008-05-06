using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;

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
        private Button m_load_problem_button;
        private CanvasEditor m_canvas_pic;
        private ComboBox m_product_cb;
        private Button m_add_product_button;
        private Label label3;
        private GroupBox groupBox4;
        private Label m_product_color_lbl;
        private NumericUpDown m_new_product_size_sb;
        private Label label5;
        private TextBox m_new_product_name_le;
        private Label label4;
        private GroupBox groupBox5;
        private int m_monitor_id;
        private GroupBox groupBox6;
        private GroupBox groupBox7;

        private Color[] colors_array = { Color.Red, Color.Blue, Color.Purple, Color.Pink, Color.Silver, Color.Yellow };
        public ProblemCreatorMonitor(int monitor_id)
        {
            m_monitor_id = monitor_id;
            Text = "Problem Creator Monitor # " + m_monitor_id.ToString();
            InitializeComponent();
            m_canvas_pic.monitor_id = monitor_id;
        }

        private void InitializeComponent()
        {
            this.m_machine_list = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_worker_list = new System.Windows.Forms.ListBox();
            this.m_add_machine_button = new System.Windows.Forms.Button();
            this.m_add_worker_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.m_product_cb = new System.Windows.Forms.ComboBox();
            this.m_product_color_lbl = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_new_product_name_le = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_new_product_size_sb = new System.Windows.Forms.NumericUpDown();
            this.m_add_product_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_load_problem_button = new System.Windows.Forms.Button();
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.m_canvas_pic = new MRCPSP.Gui.ProblemCreator.CanvasEditor();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_new_product_size_sb)).BeginInit();
            this.panel3.SuspendLayout();
            this.m_center_panel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_controls_strip.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_canvas_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // m_machine_list
            // 
            this.m_machine_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_machine_list.FormattingEnabled = true;
            this.m_machine_list.Location = new System.Drawing.Point(6, 19);
            this.m_machine_list.Name = "m_machine_list";
            this.m_machine_list.Size = new System.Drawing.Size(133, 82);
            this.m_machine_list.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(679, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(155, 242);
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
            this.splitContainer1.Size = new System.Drawing.Size(145, 232);
            this.splitContainer1.SplitterDistance = 113;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_machine_list);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(145, 113);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Machines";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_worker_list);
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
            this.m_worker_list.Location = new System.Drawing.Point(6, 19);
            this.m_worker_list.Name = "m_worker_list";
            this.m_worker_list.Size = new System.Drawing.Size(133, 82);
            this.m_worker_list.TabIndex = 4;
            // 
            // m_add_machine_button
            // 
            this.m_add_machine_button.Location = new System.Drawing.Point(5, 69);
            this.m_add_machine_button.Name = "m_add_machine_button";
            this.m_add_machine_button.Size = new System.Drawing.Size(104, 23);
            this.m_add_machine_button.TabIndex = 1;
            this.m_add_machine_button.Text = "add new machine";
            this.m_add_machine_button.UseVisualStyleBackColor = true;
            this.m_add_machine_button.Click += new System.EventHandler(this.m_add_machine_button_Click);
            // 
            // m_add_worker_button
            // 
            this.m_add_worker_button.Location = new System.Drawing.Point(5, 38);
            this.m_add_worker_button.Name = "m_add_worker_button";
            this.m_add_worker_button.Size = new System.Drawing.Size(103, 23);
            this.m_add_worker_button.TabIndex = 3;
            this.m_add_worker_button.Text = "add new worker";
            this.m_add_worker_button.UseVisualStyleBackColor = true;
            this.m_add_worker_button.Click += new System.EventHandler(this.m_add_worker_button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox7);
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 287);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(834, 126);
            this.panel2.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.m_add_machine_button);
            this.groupBox6.Controls.Add(this.m_add_worker_button);
            this.groupBox6.Location = new System.Drawing.Point(366, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(116, 98);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Add Resources";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.m_product_cb);
            this.groupBox5.Controls.Add(this.m_product_color_lbl);
            this.groupBox5.Location = new System.Drawing.Point(12, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(152, 98);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Selected Product";
            // 
            // m_product_cb
            // 
            this.m_product_cb.FormattingEnabled = true;
            this.m_product_cb.Location = new System.Drawing.Point(16, 19);
            this.m_product_cb.Name = "m_product_cb";
            this.m_product_cb.Size = new System.Drawing.Size(121, 21);
            this.m_product_cb.TabIndex = 7;
            this.m_product_cb.SelectedIndexChanged += new System.EventHandler(this.m_product_cb_SelectedIndexChanged);
            // 
            // m_product_color_lbl
            // 
            this.m_product_color_lbl.AutoSize = true;
            this.m_product_color_lbl.Location = new System.Drawing.Point(13, 47);
            this.m_product_color_lbl.Name = "m_product_color_lbl";
            this.m_product_color_lbl.Size = new System.Drawing.Size(108, 13);
            this.m_product_color_lbl.TabIndex = 9;
            this.m_product_color_lbl.Text = "Current Product Color";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.m_new_product_name_le);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.m_new_product_size_sb);
            this.groupBox4.Controls.Add(this.m_add_product_button);
            this.groupBox4.Location = new System.Drawing.Point(170, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(190, 98);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Products";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Product title:";
            // 
            // m_new_product_name_le
            // 
            this.m_new_product_name_le.Location = new System.Drawing.Point(99, 38);
            this.m_new_product_name_le.Name = "m_new_product_name_le";
            this.m_new_product_name_le.Size = new System.Drawing.Size(77, 20);
            this.m_new_product_name_le.TabIndex = 12;
            this.m_new_product_name_le.Text = "product 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "number of jobs";
            // 
            // m_new_product_size_sb
            // 
            this.m_new_product_size_sb.Location = new System.Drawing.Point(99, 12);
            this.m_new_product_size_sb.Name = "m_new_product_size_sb";
            this.m_new_product_size_sb.Size = new System.Drawing.Size(77, 20);
            this.m_new_product_size_sb.TabIndex = 10;
            // 
            // m_add_product_button
            // 
            this.m_add_product_button.Location = new System.Drawing.Point(75, 69);
            this.m_add_product_button.Name = "m_add_product_button";
            this.m_add_product_button.Size = new System.Drawing.Size(101, 23);
            this.m_add_product_button.TabIndex = 6;
            this.m_add_product_button.Text = "Add Product";
            this.m_add_product_button.UseVisualStyleBackColor = true;
            this.m_add_product_button.Click += new System.EventHandler(this.m_add_product_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "select";
            // 
            // m_load_problem_button
            // 
            this.m_load_problem_button.Location = new System.Drawing.Point(69, 61);
            this.m_load_problem_button.Name = "m_load_problem_button";
            this.m_load_problem_button.Size = new System.Drawing.Size(79, 23);
            this.m_load_problem_button.TabIndex = 3;
            this.m_load_problem_button.Text = "load problem";
            this.m_load_problem_button.UseVisualStyleBackColor = true;
            this.m_load_problem_button.Click += new System.EventHandler(this.m_load_problem_button_Click);
            // 
            // m_problem_title_le
            // 
            this.m_problem_title_le.Location = new System.Drawing.Point(83, 16);
            this.m_problem_title_le.Name = "m_problem_title_le";
            this.m_problem_title_le.Size = new System.Drawing.Size(65, 20);
            this.m_problem_title_le.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
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
            this.panel3.Size = new System.Drawing.Size(834, 45);
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
            this.label2.Size = new System.Drawing.Size(834, 45);
            this.label2.TabIndex = 4;
            this.label2.Text = "Problem Creator";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_center_panel
            // 
            this.m_center_panel.Controls.Add(this.m_canvas_pic);
            this.m_center_panel.Controls.Add(this.panel5);
            this.m_center_panel.Controls.Add(this.panel1);
            this.m_center_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_center_panel.Location = new System.Drawing.Point(0, 45);
            this.m_center_panel.Name = "m_center_panel";
            this.m_center_panel.Size = new System.Drawing.Size(834, 242);
            this.m_center_panel.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(85, 242);
            this.panel5.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_controls_strip);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(85, 285);
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
            this.m_controls_strip.Size = new System.Drawing.Size(75, 262);
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
            this.m_new_step_button.Size = new System.Drawing.Size(73, 47);
            this.m_new_step_button.Text = "toolStripButton1";
            this.m_new_step_button.ToolTipText = "Add Step to Canvas";
            this.m_new_step_button.Click += new System.EventHandler(this.m_new_step_button_Click);
            // 
            // m_new_constraint_button
            // 
            this.m_new_constraint_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_new_constraint_button.Image = global::MRCPSP.Properties.Resources.new_arrow_pic;
            this.m_new_constraint_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_new_constraint_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_new_constraint_button.Name = "m_new_constraint_button";
            this.m_new_constraint_button.Size = new System.Drawing.Size(73, 47);
            this.m_new_constraint_button.Text = "toolStripButton2";
            this.m_new_constraint_button.ToolTipText = "Add Scheduling Constraint";
            this.m_new_constraint_button.Click += new System.EventHandler(this.m_new_constraint_button_Click);
            // 
            // m_eraser_button
            // 
            this.m_eraser_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_eraser_button.Image = global::MRCPSP.Properties.Resources.eraser_pic;
            this.m_eraser_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_eraser_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_eraser_button.Name = "m_eraser_button";
            this.m_eraser_button.Size = new System.Drawing.Size(73, 48);
            this.m_eraser_button.Text = "toolStripButton3";
            this.m_eraser_button.ToolTipText = "Eraser";
            this.m_eraser_button.Click += new System.EventHandler(this.m_eraser_button_Click);
            // 
            // m_pointer_button
            // 
            this.m_pointer_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_pointer_button.Image = global::MRCPSP.Properties.Resources.selectarrow;
            this.m_pointer_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_pointer_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_pointer_button.Name = "m_pointer_button";
            this.m_pointer_button.Size = new System.Drawing.Size(73, 46);
            this.m_pointer_button.Text = "toolStripButton4";
            this.m_pointer_button.ToolTipText = "Select";
            this.m_pointer_button.Click += new System.EventHandler(this.m_pointer_button_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.m_problem_title_le);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.m_load_problem_button);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox7.Location = new System.Drawing.Point(679, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(155, 126);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            // 
            // m_canvas_pic
            // 
            this.m_canvas_pic.BackColor = System.Drawing.Color.White;
            this.m_canvas_pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_canvas_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_canvas_pic.Location = new System.Drawing.Point(85, 0);
            this.m_canvas_pic.monitor_id = 0;
            this.m_canvas_pic.Name = "m_canvas_pic";
            this.m_canvas_pic.Size = new System.Drawing.Size(594, 242);
            this.m_canvas_pic.TabIndex = 5;
            this.m_canvas_pic.TabStop = false;
            // 
            // ProblemCreatorMonitor
            // 
            this.ClientSize = new System.Drawing.Size(834, 413);
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
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_new_product_size_sb)).EndInit();
            this.panel3.ResumeLayout(false);
            this.m_center_panel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_controls_strip.ResumeLayout(false);
            this.m_controls_strip.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_canvas_pic)).EndInit();
            this.ResumeLayout(false);

        }

        private void m_new_step_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance(monitor_id).state = new StepState(monitor_id);
            m_new_step_button.CheckState = CheckState.Checked;
            uncheck_other((ToolStripButton) sender);
      
        }

        private void m_new_constraint_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance(monitor_id).state = new ConstraintFromState(monitor_id);
            m_new_constraint_button.CheckState = CheckState.Checked;
            uncheck_other((ToolStripButton)sender);
        }

        private void m_eraser_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance(monitor_id).state = new EraserState(monitor_id);
            m_eraser_button.CheckState = CheckState.Checked;
            uncheck_other((ToolStripButton)sender);
        }

        private void m_pointer_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance(monitor_id).state = new PointerState(monitor_id);
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

        private void m_add_machine_button_Click(object sender, EventArgs e)
        {
            Machine m = new Machine();
            ProblemCreatorState.Instance(monitor_id).addMachine(m);                 
            m_machine_list.Items.Add(m);                             
        }

        private void m_add_worker_button_Click(object sender, EventArgs e)
        {
            Worker w = new Worker();
                               
            ProblemCreatorState.Instance(monitor_id).addWorker(w);                 
            m_worker_list.Items.Add(w);              
        }

        private void m_machine_list_Selected_doubleClicked(object sender, EventArgs e)
        {      
        }

        public int monitor_id
        {
            get { return m_monitor_id; }
            set
            {
                m_monitor_id = value;
            }
        }

        private void m_load_problem_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance(monitor_id).loadCurrentProblem();
        }

        private void m_add_product_button_Click(object sender, EventArgs e)
        {
            ProductItem p = new ProductItem();
            p.Name = m_new_product_name_le.Text;
            p.Size = (int)m_new_product_size_sb.Value;
            p.ConstraintsColor = colors_array[p.Id % colors_array.Length];
            ProblemCreatorState.Instance(monitor_id).addProduct(p);
            m_product_cb.Items.Add(p);
            m_product_cb.SelectedItem = p;
            //set next product default name
            m_new_product_name_le.Text = "product " + (p.Id + 1).ToString();
            
        }

        private void m_product_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductItem p = (ProductItem)(m_product_cb.SelectedItem);
            m_product_color_lbl.BackColor = p.ConstraintsColor;
            ProblemCreatorState.Instance(monitor_id).CurrentProduct = p;
    }
    }

  
}
