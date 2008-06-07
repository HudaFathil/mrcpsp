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
        private Button m_job_properties_button;
        private GroupBox groupBox8;
        private NumericUpDown m_machine_batch_size_sb;
        private Label label6;
        private Button m_remove_selected_product_button;
        private Button m_remove_machine_btn;
        private Button m_remove_worker_btn;
        private NumericUpDown m_worker_available_at_sb;
        private Label label7;
        private NumericUpDown m_machine_available_at_sb;
        private Label label8;
        private Button m_resource_time_constraint_button;

        private Color[] colors_array = { Color.Red, Color.Blue, Color.Purple, Color.Pink, Color.Silver, Color.Yellow };
        private ResourceTimeConstraint m_resource_time_constraints;

        public ProblemCreatorMonitor(int monitor_id)
        {
            m_monitor_id = monitor_id;
            Text = "Problem Creator Monitor # " + m_monitor_id.ToString();
            InitializeComponent();
            m_canvas_pic.monitor_id = monitor_id;
            m_resource_time_constraints = new ResourceTimeConstraint(m_monitor_id);
            ProblemCreatorState.Instance(monitor_id).ResourceConstraints = m_resource_time_constraints.ResourceConstraints; ;
        }

        private void InitializeComponent()
        {
            this.m_machine_list = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_worker_list = new System.Windows.Forms.ListBox();
            this.m_resource_time_constraint_button = new System.Windows.Forms.Button();
            this.m_add_machine_button = new System.Windows.Forms.Button();
            this.m_add_worker_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.m_machine_available_at_sb = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.m_remove_machine_btn = new System.Windows.Forms.Button();
            this.m_machine_batch_size_sb = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_problem_title_le = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_load_problem_button = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.m_worker_available_at_sb = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.m_remove_worker_btn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.m_job_properties_button = new System.Windows.Forms.Button();
            this.m_product_cb = new System.Windows.Forms.ComboBox();
            this.m_product_color_lbl = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_remove_selected_product_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.m_new_product_name_le = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_new_product_size_sb = new System.Windows.Forms.NumericUpDown();
            this.m_add_product_button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_center_panel = new System.Windows.Forms.Panel();
            this.m_canvas_pic = new MRCPSP.Gui.ProblemCreator.CanvasEditor();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_controls_strip = new System.Windows.Forms.ToolStrip();
            this.m_new_step_button = new System.Windows.Forms.ToolStripButton();
            this.m_new_constraint_button = new System.Windows.Forms.ToolStripButton();
            this.m_eraser_button = new System.Windows.Forms.ToolStripButton();
            this.m_pointer_button = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_machine_available_at_sb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_machine_batch_size_sb)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_worker_available_at_sb)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_new_product_size_sb)).BeginInit();
            this.panel3.SuspendLayout();
            this.m_center_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_canvas_pic)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_controls_strip.SuspendLayout();
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
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.m_resource_time_constraint_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(696, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(155, 283);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(145, 250);
            this.splitContainer1.SplitterDistance = 119;
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
            this.groupBox2.Size = new System.Drawing.Size(145, 119);
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
            this.groupBox3.Size = new System.Drawing.Size(145, 127);
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
            this.m_worker_list.Size = new System.Drawing.Size(133, 95);
            this.m_worker_list.TabIndex = 4;
            // 
            // m_resource_time_constraint_button
            // 
            this.m_resource_time_constraint_button.BackColor = System.Drawing.Color.RoyalBlue;
            this.m_resource_time_constraint_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_resource_time_constraint_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_resource_time_constraint_button.Location = new System.Drawing.Point(5, 255);
            this.m_resource_time_constraint_button.Name = "m_resource_time_constraint_button";
            this.m_resource_time_constraint_button.Size = new System.Drawing.Size(145, 23);
            this.m_resource_time_constraint_button.TabIndex = 0;
            this.m_resource_time_constraint_button.Text = "Edit Resource Constraint";
            this.m_resource_time_constraint_button.UseVisualStyleBackColor = false;
            this.m_resource_time_constraint_button.Click += new System.EventHandler(this.m_resource_time_constraint_button_Click);
            // 
            // m_add_machine_button
            // 
            this.m_add_machine_button.Location = new System.Drawing.Point(6, 69);
            this.m_add_machine_button.Name = "m_add_machine_button";
            this.m_add_machine_button.Size = new System.Drawing.Size(104, 23);
            this.m_add_machine_button.TabIndex = 1;
            this.m_add_machine_button.Text = "Add New Machine";
            this.m_add_machine_button.UseVisualStyleBackColor = true;
            this.m_add_machine_button.Click += new System.EventHandler(this.m_add_machine_button_Click);
            // 
            // m_add_worker_button
            // 
            this.m_add_worker_button.Location = new System.Drawing.Point(6, 69);
            this.m_add_worker_button.Name = "m_add_worker_button";
            this.m_add_worker_button.Size = new System.Drawing.Size(103, 23);
            this.m_add_worker_button.TabIndex = 3;
            this.m_add_worker_button.Text = "Add New Worker";
            this.m_add_worker_button.UseVisualStyleBackColor = true;
            this.m_add_worker_button.Click += new System.EventHandler(this.m_add_worker_button_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.groupBox8);
            this.panel2.Controls.Add(this.groupBox7);
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 328);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(851, 143);
            this.panel2.TabIndex = 2;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.m_machine_available_at_sb);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.m_remove_machine_btn);
            this.groupBox8.Controls.Add(this.m_machine_batch_size_sb);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.m_add_machine_button);
            this.groupBox8.Location = new System.Drawing.Point(528, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(162, 125);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Add Machine";
            // 
            // m_machine_available_at_sb
            // 
            this.m_machine_available_at_sb.Location = new System.Drawing.Point(77, 42);
            this.m_machine_available_at_sb.Name = "m_machine_available_at_sb";
            this.m_machine_available_at_sb.Size = new System.Drawing.Size(69, 20);
            this.m_machine_available_at_sb.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Available at:";
            // 
            // m_remove_machine_btn
            // 
            this.m_remove_machine_btn.Location = new System.Drawing.Point(6, 96);
            this.m_remove_machine_btn.Name = "m_remove_machine_btn";
            this.m_remove_machine_btn.Size = new System.Drawing.Size(101, 23);
            this.m_remove_machine_btn.TabIndex = 4;
            this.m_remove_machine_btn.Text = "Remove Machine";
            this.m_remove_machine_btn.UseVisualStyleBackColor = true;
            this.m_remove_machine_btn.Click += new System.EventHandler(this.m_remove_machine_btn_Click);
            // 
            // m_machine_batch_size_sb
            // 
            this.m_machine_batch_size_sb.Location = new System.Drawing.Point(77, 20);
            this.m_machine_batch_size_sb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_machine_batch_size_sb.Name = "m_machine_batch_size_sb";
            this.m_machine_batch_size_sb.Size = new System.Drawing.Size(69, 20);
            this.m_machine_batch_size_sb.TabIndex = 3;
            this.m_machine_batch_size_sb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Batch: ";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.m_problem_title_le);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.m_load_problem_button);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox7.Location = new System.Drawing.Point(696, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(155, 143);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
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
            // m_problem_title_le
            // 
            this.m_problem_title_le.Location = new System.Drawing.Point(83, 16);
            this.m_problem_title_le.Name = "m_problem_title_le";
            this.m_problem_title_le.Size = new System.Drawing.Size(65, 20);
            this.m_problem_title_le.TabIndex = 1;
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
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.m_worker_available_at_sb);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.m_remove_worker_btn);
            this.groupBox6.Controls.Add(this.m_add_worker_button);
            this.groupBox6.Location = new System.Drawing.Point(366, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(156, 125);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Add Worker";
            // 
            // m_worker_available_at_sb
            // 
            this.m_worker_available_at_sb.Location = new System.Drawing.Point(77, 42);
            this.m_worker_available_at_sb.Name = "m_worker_available_at_sb";
            this.m_worker_available_at_sb.Size = new System.Drawing.Size(69, 20);
            this.m_worker_available_at_sb.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Available at:";
            // 
            // m_remove_worker_btn
            // 
            this.m_remove_worker_btn.Location = new System.Drawing.Point(6, 96);
            this.m_remove_worker_btn.Name = "m_remove_worker_btn";
            this.m_remove_worker_btn.Size = new System.Drawing.Size(103, 23);
            this.m_remove_worker_btn.TabIndex = 4;
            this.m_remove_worker_btn.Text = "Remove Worker";
            this.m_remove_worker_btn.UseVisualStyleBackColor = true;
            this.m_remove_worker_btn.Click += new System.EventHandler(this.m_remove_worker_btn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.m_job_properties_button);
            this.groupBox5.Controls.Add(this.m_product_cb);
            this.groupBox5.Controls.Add(this.m_product_color_lbl);
            this.groupBox5.Location = new System.Drawing.Point(12, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(138, 125);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Selected Product";
            // 
            // m_job_properties_button
            // 
            this.m_job_properties_button.Location = new System.Drawing.Point(6, 69);
            this.m_job_properties_button.Name = "m_job_properties_button";
            this.m_job_properties_button.Size = new System.Drawing.Size(121, 23);
            this.m_job_properties_button.TabIndex = 10;
            this.m_job_properties_button.Text = "Jobs Properties";
            this.m_job_properties_button.UseVisualStyleBackColor = true;
            this.m_job_properties_button.Click += new System.EventHandler(this.m_job_properties_button_Click);
            // 
            // m_product_cb
            // 
            this.m_product_cb.FormattingEnabled = true;
            this.m_product_cb.Location = new System.Drawing.Point(6, 19);
            this.m_product_cb.Name = "m_product_cb";
            this.m_product_cb.Size = new System.Drawing.Size(121, 21);
            this.m_product_cb.TabIndex = 7;
            this.m_product_cb.SelectedIndexChanged += new System.EventHandler(this.m_product_cb_SelectedIndexChanged);
            // 
            // m_product_color_lbl
            // 
            this.m_product_color_lbl.AutoSize = true;
            this.m_product_color_lbl.Location = new System.Drawing.Point(6, 45);
            this.m_product_color_lbl.Name = "m_product_color_lbl";
            this.m_product_color_lbl.Size = new System.Drawing.Size(108, 13);
            this.m_product_color_lbl.TabIndex = 9;
            this.m_product_color_lbl.Text = "Current Product Color";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.m_remove_selected_product_button);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.m_new_product_name_le);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.m_new_product_size_sb);
            this.groupBox4.Controls.Add(this.m_add_product_button);
            this.groupBox4.Location = new System.Drawing.Point(156, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(204, 125);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Products";
            // 
            // m_remove_selected_product_button
            // 
            this.m_remove_selected_product_button.Location = new System.Drawing.Point(101, 69);
            this.m_remove_selected_product_button.Name = "m_remove_selected_product_button";
            this.m_remove_selected_product_button.Size = new System.Drawing.Size(97, 23);
            this.m_remove_selected_product_button.TabIndex = 14;
            this.m_remove_selected_product_button.Text = "Remove Product";
            this.m_remove_selected_product_button.UseVisualStyleBackColor = true;
            this.m_remove_selected_product_button.Click += new System.EventHandler(this.m_remove_selected_product_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Product title:";
            // 
            // m_new_product_name_le
            // 
            this.m_new_product_name_le.Location = new System.Drawing.Point(101, 42);
            this.m_new_product_name_le.Name = "m_new_product_name_le";
            this.m_new_product_name_le.Size = new System.Drawing.Size(77, 20);
            this.m_new_product_name_le.TabIndex = 12;
            this.m_new_product_name_le.Text = "product 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Number of Jobs";
            // 
            // m_new_product_size_sb
            // 
            this.m_new_product_size_sb.Location = new System.Drawing.Point(101, 19);
            this.m_new_product_size_sb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_new_product_size_sb.Name = "m_new_product_size_sb";
            this.m_new_product_size_sb.Size = new System.Drawing.Size(77, 20);
            this.m_new_product_size_sb.TabIndex = 10;
            this.m_new_product_size_sb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_add_product_button
            // 
            this.m_add_product_button.Location = new System.Drawing.Point(9, 69);
            this.m_add_product_button.Name = "m_add_product_button";
            this.m_add_product_button.Size = new System.Drawing.Size(88, 23);
            this.m_add_product_button.TabIndex = 6;
            this.m_add_product_button.Text = "Add Product";
            this.m_add_product_button.UseVisualStyleBackColor = true;
            this.m_add_product_button.Click += new System.EventHandler(this.m_add_product_button_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(851, 45);
            this.panel3.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(851, 45);
            this.label2.TabIndex = 4;
            this.label2.Text = "Problem Creator";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_center_panel
            // 
            this.m_center_panel.BackColor = System.Drawing.Color.Transparent;
            this.m_center_panel.Controls.Add(this.m_canvas_pic);
            this.m_center_panel.Controls.Add(this.panel5);
            this.m_center_panel.Controls.Add(this.panel1);
            this.m_center_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_center_panel.Location = new System.Drawing.Point(0, 45);
            this.m_center_panel.Name = "m_center_panel";
            this.m_center_panel.Size = new System.Drawing.Size(851, 283);
            this.m_center_panel.TabIndex = 4;
            // 
            // m_canvas_pic
            // 
            this.m_canvas_pic.BackColor = System.Drawing.Color.White;
            this.m_canvas_pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_canvas_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_canvas_pic.Location = new System.Drawing.Point(85, 0);
            this.m_canvas_pic.monitor_id = 0;
            this.m_canvas_pic.Name = "m_canvas_pic";
            this.m_canvas_pic.Size = new System.Drawing.Size(611, 283);
            this.m_canvas_pic.TabIndex = 5;
            this.m_canvas_pic.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(85, 283);
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
            this.m_new_step_button.Image = global::MRCPSP.Properties.Resources.new_step_pic1;
            this.m_new_step_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_new_step_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_new_step_button.Name = "m_new_step_button";
            this.m_new_step_button.Size = new System.Drawing.Size(73, 76);
            this.m_new_step_button.Text = "toolStripButton1";
            this.m_new_step_button.ToolTipText = "Add Step to Canvas";
            this.m_new_step_button.MouseLeave += new System.EventHandler(this.new_step_mouseLeave);
            this.m_new_step_button.MouseEnter += new System.EventHandler(this.new_step_mouseEnter);
            this.m_new_step_button.Click += new System.EventHandler(this.m_new_step_button_Click);
            // 
            // m_new_constraint_button
            // 
            this.m_new_constraint_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_new_constraint_button.Image = global::MRCPSP.Properties.Resources.new_arrow1;
            this.m_new_constraint_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_new_constraint_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_new_constraint_button.Name = "m_new_constraint_button";
            this.m_new_constraint_button.Size = new System.Drawing.Size(73, 52);
            this.m_new_constraint_button.Text = "toolStripButton2";
            this.m_new_constraint_button.ToolTipText = "Add Scheduling Constraint";
            this.m_new_constraint_button.MouseLeave += new System.EventHandler(this.new_arrow_mouseLeave);
            this.m_new_constraint_button.MouseEnter += new System.EventHandler(this.new_arrow_mouseEnter);
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
            // ProblemCreatorMonitor
            // 
            this.ClientSize = new System.Drawing.Size(851, 471);
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
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_machine_available_at_sb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_machine_batch_size_sb)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_worker_available_at_sb)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_new_product_size_sb)).EndInit();
            this.panel3.ResumeLayout(false);
            this.m_center_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_canvas_pic)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_controls_strip.ResumeLayout(false);
            this.m_controls_strip.PerformLayout();
            this.ResumeLayout(false);

        }


        private void new_step_mouseEnter(object sender, EventArgs e)
        {
            this.m_new_step_button.Image.Dispose();
            this.m_new_step_button.Image = global::MRCPSP.Properties.Resources.new_step_pic2;
        }

        private void new_step_mouseLeave(object sender, EventArgs e)
        {
            this.m_new_step_button.Image.Dispose();
            this.m_new_step_button.Image = global::MRCPSP.Properties.Resources.new_step_pic1;
        }

        private void new_arrow_mouseEnter(object sender, EventArgs e)
        {
            this.m_new_constraint_button.Image.Dispose();
            this.m_new_constraint_button.Image = global::MRCPSP.Properties.Resources.new_arrow;
        }

        private void new_arrow_mouseLeave(object sender, EventArgs e)
        {
            this.m_new_constraint_button.Image.Dispose();
            this.m_new_constraint_button.Image = global::MRCPSP.Properties.Resources.new_arrow1;
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
            Machine m = new Machine((int)m_machine_available_at_sb.Value, (int)m_machine_batch_size_sb.Value);
            ProblemCreatorState.Instance(monitor_id).addMachine(m);                 
            m_machine_list.Items.Add(m);                             
        }

        private void m_add_worker_button_Click(object sender, EventArgs e)
        {
            Worker w = new Worker((int) m_worker_available_at_sb.Value);
                               
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

        public DataGridView ResourceTimeConstraints
        {
            get { return m_resource_time_constraints.ResourceConstraints; }
        }

        private void m_load_problem_button_Click(object sender, EventArgs e)
        {
            ProblemCreatorState.Instance(monitor_id).loadCurrentProblem(m_problem_title_le.Text);
        }

        private void m_add_product_button_Click(object sender, EventArgs e)
        {
            ProductItem p = new ProductItem(m_new_product_name_le.Text,(int)m_new_product_size_sb.Value);
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

        private void m_job_properties_button_Click(object sender, EventArgs e)
        {
            if (m_product_cb.SelectedIndex < 0)
                return;
            ProductItem p = ProblemCreatorState.Instance(monitor_id).CurrentProduct;
            Form f = new Form();
            f.Text = p.Name + ", Jobs data:";
            f.Controls.Add(p.JobsData);
            p.JobsData.Dock = DockStyle.Fill;
            f.ShowDialog(new Form());  
        }

        private void m_resource_time_constraint_button_Click(object sender, EventArgs e)
        {
            m_resource_time_constraints.updateResources();
            m_resource_time_constraints.ShowDialog(new Form());
        }

        private void m_remove_worker_btn_Click(object sender, EventArgs e)
        {
            if (m_worker_list.SelectedIndex < 0)
                return;
            DialogResult result = MessageBox.Show("This operation will remove all modes and constraints using this resource, continue?",
                               "Delete Resource",
                                MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            removeResource((Worker)m_worker_list.SelectedItem);
            ProblemCreatorState.Instance(monitor_id).removeWorker((Worker)m_worker_list.SelectedItem);
            m_worker_list.Items.Remove((Worker)m_worker_list.SelectedItem);
        }

        private void removeResource(ResourceBase r)
        {
            r.notifyOnDeleteMe();            
        }

        private void m_remove_machine_btn_Click(object sender, EventArgs e)
        {
            if (m_machine_list.SelectedIndex < 0)
                return;
            DialogResult result = MessageBox.Show("This operation will remove all modes and constraints using this resource, continue?",
                               "Delete Resource",
                                MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            removeResource((Machine)m_machine_list.SelectedItem);
            ProblemCreatorState.Instance(monitor_id).removeMachine((Machine)m_machine_list.SelectedItem);
            m_machine_list.Items.Remove((Machine)m_machine_list.SelectedItem);
        }

        private void m_remove_selected_product_button_Click(object sender, EventArgs e)
        {
            if (m_product_cb.Items.Count == 1)
            {
                MessageBox.Show("Product's list cann't be empty", "Error", MessageBoxButtons.OK);
                return;
            }
            if (m_product_cb.SelectedIndex < 0)
                return;
            ProductItem p = (ProductItem)m_product_cb.SelectedItem;
            ProblemCreatorState.Instance(monitor_id).removeProduct(p);

            m_product_cb.Items.Remove(p);
            if (m_product_cb.Items.Count >0 )
                m_product_cb.SelectedIndex = 0;
            else
                ProblemCreatorState.Instance(monitor_id).CurrentProduct = null;           
        }
      

    }

  
}
