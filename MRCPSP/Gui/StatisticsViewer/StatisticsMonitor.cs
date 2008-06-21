using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;

using MRCPSP.CommonTypes;
using MRCPSP.Controllers;
using MRCPSP.Domain;
using MRCPSP.Logger;

using ZedGraph;

using MRCPSP.Gui.StatisticsViewer.Graph;

namespace MRCPSP.Gui.StatisticsViewer
{
    class StatisticsMonitor : Form
    {
        private BindingNavigator m_bind_navigator;
        private IContainer components;
        private ToolStripLabel bindingNavigatorCountItem;
        private ToolStripButton bindingNavigatorDeleteItem;
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox bindingNavigatorPositionItem;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox1;
        private System.Windows.Forms.Label m_problem_title_lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_first_gen_lbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label m_selection_lbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label m_crossover_lbl;
        private System.Windows.Forms.Label label7;
        private Panel m_gantt_panel;
        private TabControl m_main_tab;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private SaveFileDialog saveFileDialog1;

        private ResultSummary m_current_summary;

        private MRCPSP.Gui.StatisticsViewer.Graph.GanttChart m_resources_gantt;
        private MRCPSP.Gui.StatisticsViewer.Graph.PieGraph m_resources_pie_chart;
        private Panel panel2;
        private Button m_export_to_excel_button;
        private Panel panel1;
        private TabPage tabPage5;
        private MRCPSP.Gui.StatisticsViewer.Graph.MinMaxGraph m_min_max_graph;
        private System.Windows.Forms.Label m_finish_time_lbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label m_start_time_lbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label m_iteration_lbl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label m_result_lbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label m_mutation_lbl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label m_population_size_lbl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label m_generation_size_lbl;
        private System.Windows.Forms.Label label15;
        private MRCPSP.Gui.StatisticsViewer.Graph.XYGraph m_generation_over_time_graph;

        public StatisticsMonitor()
        {
            InitializeComponent();
            bindingNavigatorPositionItem.TextChanged += new EventHandler(bindingNavigatorPositionItem_TextChanged);
            this.GotFocus += new EventHandler(StatisticsMonitor_GotFocus);
            m_main_tab.BackColor = Color.FromArgb(0, 0, 0, 0);
        }
   

        void StatisticsMonitor_GotFocus(object sender, EventArgs e)
        {
            
            refreshMonitor();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsMonitor));
            this.m_bind_navigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_population_size_lbl = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.m_generation_size_lbl = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.m_mutation_lbl = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.m_result_lbl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_iteration_lbl = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.m_finish_time_lbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_start_time_lbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_selection_lbl = new System.Windows.Forms.Label();
            this.m_first_gen_lbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_crossover_lbl = new System.Windows.Forms.Label();
            this.m_problem_title_lbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_main_tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_gantt_panel = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_generation_over_time_graph = new MRCPSP.Gui.StatisticsViewer.Graph.XYGraph();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.m_resources_pie_chart = new MRCPSP.Gui.StatisticsViewer.Graph.PieGraph();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.m_min_max_graph = new MRCPSP.Gui.StatisticsViewer.Graph.MinMaxGraph();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_export_to_excel_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_bind_navigator)).BeginInit();
            this.m_bind_navigator.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_main_tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_bind_navigator
            // 
            this.m_bind_navigator.AddNewItem = null;
            this.m_bind_navigator.CountItem = this.bindingNavigatorCountItem;
            this.m_bind_navigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.m_bind_navigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorDeleteItem});
            this.m_bind_navigator.Location = new System.Drawing.Point(0, 0);
            this.m_bind_navigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.m_bind_navigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.m_bind_navigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.m_bind_navigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.m_bind_navigator.Name = "m_bind_navigator";
            this.m_bind_navigator.PositionItem = this.bindingNavigatorPositionItem;
            this.m_bind_navigator.Size = new System.Drawing.Size(959, 25);
            this.m_bind_navigator.TabIndex = 2;
            this.m_bind_navigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            this.bindingNavigatorMovePreviousItem.Click += new System.EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            this.bindingNavigatorMoveLastItem.Click += new System.EventHandler(this.bindingNavigatorMoveLastItem_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(31, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Problem Results";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.m_population_size_lbl);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.m_generation_size_lbl);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.m_mutation_lbl);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.m_result_lbl);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.m_iteration_lbl);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.m_finish_time_lbl);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.m_start_time_lbl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.m_selection_lbl);
            this.groupBox1.Controls.Add(this.m_first_gen_lbl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_crossover_lbl);
            this.groupBox1.Controls.Add(this.m_problem_title_lbl);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(34, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 345);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // m_population_size_lbl
            // 
            this.m_population_size_lbl.AutoSize = true;
            this.m_population_size_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_population_size_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_population_size_lbl.Location = new System.Drawing.Point(185, 39);
            this.m_population_size_lbl.Name = "m_population_size_lbl";
            this.m_population_size_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_population_size_lbl.TabIndex = 21;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label13.Location = new System.Drawing.Point(7, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 20);
            this.label13.TabIndex = 20;
            this.label13.Text = "Population Size:";
            // 
            // m_generation_size_lbl
            // 
            this.m_generation_size_lbl.AutoSize = true;
            this.m_generation_size_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_generation_size_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_generation_size_lbl.Location = new System.Drawing.Point(185, 62);
            this.m_generation_size_lbl.Name = "m_generation_size_lbl";
            this.m_generation_size_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_generation_size_lbl.TabIndex = 23;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label15.Location = new System.Drawing.Point(7, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(156, 20);
            this.label15.TabIndex = 22;
            this.label15.Text = "Num of Generations:";
            // 
            // m_mutation_lbl
            // 
            this.m_mutation_lbl.AutoSize = true;
            this.m_mutation_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_mutation_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_mutation_lbl.Location = new System.Drawing.Point(185, 153);
            this.m_mutation_lbl.Name = "m_mutation_lbl";
            this.m_mutation_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_mutation_lbl.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label12.Location = new System.Drawing.Point(8, 153);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(134, 20);
            this.label12.TabIndex = 18;
            this.label12.Text = "Mutation Percent:";
            // 
            // m_result_lbl
            // 
            this.m_result_lbl.AutoSize = true;
            this.m_result_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_result_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_result_lbl.Location = new System.Drawing.Point(184, 258);
            this.m_result_lbl.Name = "m_result_lbl";
            this.m_result_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_result_lbl.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(7, 258);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Result:";
            // 
            // m_iteration_lbl
            // 
            this.m_iteration_lbl.AutoSize = true;
            this.m_iteration_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_iteration_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_iteration_lbl.Location = new System.Drawing.Point(184, 177);
            this.m_iteration_lbl.Name = "m_iteration_lbl";
            this.m_iteration_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_iteration_lbl.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label11.Location = new System.Drawing.Point(7, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 20);
            this.label11.TabIndex = 14;
            this.label11.Text = "Iterations:";
            // 
            // m_finish_time_lbl
            // 
            this.m_finish_time_lbl.AutoSize = true;
            this.m_finish_time_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_finish_time_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_finish_time_lbl.Location = new System.Drawing.Point(184, 223);
            this.m_finish_time_lbl.Name = "m_finish_time_lbl";
            this.m_finish_time_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_finish_time_lbl.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(7, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Finish Time: ";
            // 
            // m_start_time_lbl
            // 
            this.m_start_time_lbl.AutoSize = true;
            this.m_start_time_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_start_time_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_start_time_lbl.Location = new System.Drawing.Point(184, 200);
            this.m_start_time_lbl.Name = "m_start_time_lbl";
            this.m_start_time_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_start_time_lbl.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(7, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Start Time: ";
            // 
            // m_selection_lbl
            // 
            this.m_selection_lbl.AutoSize = true;
            this.m_selection_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_selection_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_selection_lbl.Location = new System.Drawing.Point(184, 129);
            this.m_selection_lbl.Name = "m_selection_lbl";
            this.m_selection_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_selection_lbl.TabIndex = 9;
            // 
            // m_first_gen_lbl
            // 
            this.m_first_gen_lbl.AutoSize = true;
            this.m_first_gen_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_first_gen_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_first_gen_lbl.Location = new System.Drawing.Point(184, 82);
            this.m_first_gen_lbl.Name = "m_first_gen_lbl";
            this.m_first_gen_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_first_gen_lbl.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(7, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Selection Method:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(8, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "First Population:";
            // 
            // m_crossover_lbl
            // 
            this.m_crossover_lbl.AutoSize = true;
            this.m_crossover_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_crossover_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_crossover_lbl.Location = new System.Drawing.Point(184, 105);
            this.m_crossover_lbl.Name = "m_crossover_lbl";
            this.m_crossover_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_crossover_lbl.TabIndex = 7;
            // 
            // m_problem_title_lbl
            // 
            this.m_problem_title_lbl.AutoSize = true;
            this.m_problem_title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_problem_title_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_problem_title_lbl.Location = new System.Drawing.Point(184, 16);
            this.m_problem_title_lbl.Name = "m_problem_title_lbl";
            this.m_problem_title_lbl.Size = new System.Drawing.Size(0, 20);
            this.m_problem_title_lbl.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(8, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Crossover Method:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Problem Title:";
            // 
            // m_main_tab
            // 
            this.m_main_tab.Controls.Add(this.tabPage1);
            this.m_main_tab.Controls.Add(this.tabPage2);
            this.m_main_tab.Controls.Add(this.tabPage3);
            this.m_main_tab.Controls.Add(this.tabPage4);
            this.m_main_tab.Controls.Add(this.tabPage5);
            this.m_main_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_main_tab.Location = new System.Drawing.Point(0, 0);
            this.m_main_tab.Name = "m_main_tab";
            this.m_main_tab.SelectedIndex = 0;
            this.m_main_tab.Size = new System.Drawing.Size(959, 454);
            this.m_main_tab.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(951, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Overview";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Image = global::MRCPSP.Properties.Resources.circle_cromosom;
            this.label4.Location = new System.Drawing.Point(553, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(327, 321);
            this.label4.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_gantt_panel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(951, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Resource to Operation Usage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_gantt_panel
            // 
            this.m_gantt_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gantt_panel.Location = new System.Drawing.Point(3, 3);
            this.m_gantt_panel.Name = "m_gantt_panel";
            this.m_gantt_panel.Size = new System.Drawing.Size(945, 422);
            this.m_gantt_panel.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_generation_over_time_graph);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(951, 428);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Results Over Generation";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // m_generation_over_time_graph
            // 
            this.m_generation_over_time_graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_generation_over_time_graph.Location = new System.Drawing.Point(3, 3);
            this.m_generation_over_time_graph.Name = "m_generation_over_time_graph";
            this.m_generation_over_time_graph.Size = new System.Drawing.Size(945, 422);
            this.m_generation_over_time_graph.TabIndex = 3;
            this.m_generation_over_time_graph.Title = "";
            this.m_generation_over_time_graph.XAxis = "";
            this.m_generation_over_time_graph.YAxis = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.m_resources_pie_chart);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(951, 428);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Resources Workload";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // m_resources_pie_chart
            // 
            this.m_resources_pie_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_resources_pie_chart.Location = new System.Drawing.Point(3, 3);
            this.m_resources_pie_chart.Name = "m_resources_pie_chart";
            this.m_resources_pie_chart.Size = new System.Drawing.Size(945, 422);
            this.m_resources_pie_chart.TabIndex = 7;
            this.m_resources_pie_chart.Title = "";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.m_min_max_graph);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(951, 428);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Solutions Range in Generations";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // m_min_max_graph
            // 
            this.m_min_max_graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_min_max_graph.Location = new System.Drawing.Point(3, 3);
            this.m_min_max_graph.Name = "m_min_max_graph";
            this.m_min_max_graph.Size = new System.Drawing.Size(945, 422);
            this.m_min_max_graph.TabIndex = 0;
            this.m_min_max_graph.Title = "";
            this.m_min_max_graph.XAxis = "";
            this.m_min_max_graph.YAxis = "";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.m_export_to_excel_button);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 479);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(959, 94);
            this.panel2.TabIndex = 10;
            // 
            // m_export_to_excel_button
            // 
            this.m_export_to_excel_button.FlatAppearance.BorderSize = 3;
            this.m_export_to_excel_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_export_to_excel_button.Image = global::MRCPSP.Properties.Resources.excel_icon;
            this.m_export_to_excel_button.Location = new System.Drawing.Point(22, 14);
            this.m_export_to_excel_button.Name = "m_export_to_excel_button";
            this.m_export_to_excel_button.Size = new System.Drawing.Size(75, 56);
            this.m_export_to_excel_button.TabIndex = 0;
            this.m_export_to_excel_button.UseVisualStyleBackColor = true;
            this.m_export_to_excel_button.Click += new System.EventHandler(this.m_expor_to_excel_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_main_tab);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 454);
            this.panel1.TabIndex = 11;
            // 
            // StatisticsMonitor
            // 
            this.ClientSize = new System.Drawing.Size(959, 573);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_bind_navigator);
            this.Name = "StatisticsMonitor";
            this.Text = "Statistics Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.m_bind_navigator)).EndInit();
            this.m_bind_navigator.ResumeLayout(false);
            this.m_bind_navigator.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_main_tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void refreshMonitor()
        {
            bindingNavigatorCountItem.Text = "of {" + ApplicManager.Instance.SavedResults.Count.ToString() + "}";
            bool val = (ApplicManager.Instance.SavedResults.Count > 0);
            bindingNavigatorMoveFirstItem.Enabled = val;
            bindingNavigatorMoveLastItem.Enabled = val;
            bindingNavigatorMoveNextItem.Enabled = val;
            bindingNavigatorMovePreviousItem.Enabled = val;
            bindingNavigatorPositionItem.Enabled = val;
            bindingNavigatorCountItem.Enabled = val;
        }


        void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int report_index = Convert.ToInt32(bindingNavigatorPositionItem.Text);
                m_current_summary = ApplicManager.Instance.SavedResults[report_index - 1];
                updateReport();
            }
            catch (Exception)
            {
                LoggerFactory.getSimpleLogger().error("StatisticsMonitor:: failed to generate report");
            }
        }

        private void updateGeneralParams()
        {
            m_problem_title_lbl.Text = m_current_summary.Title;
            m_generation_size_lbl.Text = m_current_summary.NumOfGeneration;
            m_population_size_lbl.Text = m_current_summary.SizeOfPopulation;
            m_first_gen_lbl.Text = m_current_summary.GeneratePopulationType;
            m_selection_lbl.Text = m_current_summary.SelectionType;
            m_crossover_lbl.Text = m_current_summary.CrossoverType;
            m_iteration_lbl.Text = m_current_summary.NumberOfIterations.ToString();
            m_start_time_lbl.Text = m_current_summary.StartTime;
            m_finish_time_lbl.Text = m_current_summary.FinishTime;
            m_mutation_lbl.Text = m_current_summary.MutationPercent;
            m_result_lbl.Text = m_current_summary.BestResult.ToString();
        }

        private void updateGenerationOverTime()
        {
            PointPairList list = new PointPairList();
            for (int i = 0; i < m_current_summary.BestSolutions.Count; i++)
            {
                list.Add(i, m_current_summary.BestSolutions[i].scoreFromLindo);
            }
            m_generation_over_time_graph.setGraphData(list, "solution");
            m_generation_over_time_graph.XAxis = "Generation Number";
            m_generation_over_time_graph.YAxis = "Best Result";
            m_generation_over_time_graph.Title = "Best Result in Generation";
            m_generation_over_time_graph.Refresh();
        }

        private void updateResourcesInPie()
        {
         
            m_resources_pie_chart.setPieData(m_current_summary);
            m_resources_pie_chart.Refresh();
        }

        private void updateMinMaxGraph()
        {
            m_min_max_graph.setMinMaxData(m_current_summary.MinMaxPerGeneration, "solutions range");
            m_min_max_graph.XAxis = "Generation Number";
            m_min_max_graph.YAxis = "Result";
            m_min_max_graph.Title = "Solutions Range in Generations";
            m_min_max_graph.Refresh();
        }

        private void updateResourcesInGantt()
        {
            if (m_resources_gantt != null)
                m_resources_gantt.Dispose();
            m_resources_gantt = new MRCPSP.Gui.StatisticsViewer.Graph.GanttChart();
            this.m_resources_gantt.Location = new System.Drawing.Point(0, 0);
            this.m_resources_gantt.Name = "m_resources_gantt";
            this.m_resources_gantt.Size = new System.Drawing.Size(m_gantt_panel.Width, m_gantt_panel.Height);
            this.m_resources_gantt.TabIndex = 7;
            this.m_resources_gantt.Title = "";
            this.m_gantt_panel.Controls.Add(this.m_resources_gantt);
            m_resources_gantt.Dock = DockStyle.Fill;

            m_resources_gantt.setGanttData(m_current_summary);            
            m_resources_gantt.Refresh();
            m_resources_gantt.setGanttData2(m_current_summary);          
         //   m_resources_gantt.setGanttDataByResource(m_current_summary);
            m_resources_gantt.Title = "Best Result in Generation";
            m_resources_gantt.Refresh();
        }

        private void updateReport()
        {
            updateGeneralParams();
            updateGenerationOverTime();
            updateResourcesInPie();
            updateResourcesInGantt();
            updateMinMaxGraph(); 
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            try
            {
                int report_index = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;
                if (report_index < 1)
                    return;
                bindingNavigatorPositionItem.Text = report_index.ToString();
            }
            catch (Exception)
            {
                LoggerFactory.getSimpleLogger().error("StatisticsMonitor:: failed to generate report");
            }
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            try
            {
                int report_index = Convert.ToInt32(bindingNavigatorPositionItem.Text) + 1;
                if (report_index > ApplicManager.Instance.SavedResults.Count)
                    return;
                bindingNavigatorPositionItem.Text = report_index.ToString();
            }
            catch (Exception)
            {
                LoggerFactory.getSimpleLogger().error("StatisticsMonitor:: failed to generate report");
            }
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ApplicManager.Instance.SavedResults.Count <= 0)
                    return;
                bindingNavigatorPositionItem.Text = "1";
            }
            catch (Exception)
            {
                LoggerFactory.getSimpleLogger().error("StatisticsMonitor:: failed to generate report");
            }
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ApplicManager.Instance.SavedResults.Count <= 0)
                    return;
                bindingNavigatorPositionItem.Text = ApplicManager.Instance.SavedResults.Count.ToString();
            }
            catch (Exception)
            {
                LoggerFactory.getSimpleLogger().error("StatisticsMonitor:: failed to generate report");
            }
        }

   
        private void m_expor_to_excel_button_Click(object sender, EventArgs e)
        {
            
            saveFileDialog1.Filter = "Excel Files (*.xlsx) |*.xlsx";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.InitialDirectory = Application.StartupPath;

            if (!(saveFileDialog1.ShowDialog() == DialogResult.OK))
            {
                return;

            }
            /*
            if (! saveFileDialog1.FileName.EndsWith(".xlsx"))
                saveFileDialog1.FileName = saveFileDialog1.FileName + ".xlsx";
              */
            ExcelParser excel = new ExcelParser(saveFileDialog1.FileName, m_current_summary);
           // ExcelParser excel = new ExcelParser( m_current_summary);
            //m_resources_gantt.saveImage(Application.StartupPath + "gantt.png");   

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            int report_index = Convert.ToInt32(bindingNavigatorPositionItem.Text);
            if (report_index < 1 || report_index >= ApplicManager.Instance.SavedResults.Count)
                return;
            ApplicManager.Instance.SavedResults.RemoveAt(report_index - 1);
            refreshMonitor();
          
        }

       
    }
}
