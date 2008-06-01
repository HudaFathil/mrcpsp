﻿using System;
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

//using ExpertChart;

namespace MRCPSP.Gui.StatisticsViewer
{
    class StatisticsMonitor : Form
    {
        private BindingNavigator m_bind_navigator;
        private IContainer components;
        private ToolStripButton bindingNavigatorAddNewItem;
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
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private SaveFileDialog saveFileDialog1;

        private ResultSummary m_current_summary;

        private MRCPSP.Gui.StatisticsViewer.Graph.GanttChart m_resources_gantt;
        private MRCPSP.Gui.StatisticsViewer.Graph.PieGraph m_resources_pie_chart;
        private Panel panel1;
        private Panel panel2;
        private Button m_export_to_excel_button;
        private MRCPSP.Gui.StatisticsViewer.Graph.XYGraph m_generation_over_time_graph;

        public StatisticsMonitor()
        {
            InitializeComponent();
            bindingNavigatorPositionItem.TextChanged += new EventHandler(bindingNavigatorPositionItem_TextChanged);
            this.GotFocus += new EventHandler(StatisticsMonitor_GotFocus);
            tabControl1.BackColor = Color.FromArgb(0, 0, 0, 0);

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
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
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
            this.m_selection_lbl = new System.Windows.Forms.Label();
            this.m_first_gen_lbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_crossover_lbl = new System.Windows.Forms.Label();
            this.m_problem_title_lbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_gantt_panel = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_export_to_excel_button = new System.Windows.Forms.Button();
            this.m_generation_over_time_graph = new MRCPSP.Gui.StatisticsViewer.Graph.XYGraph();
            this.m_resources_pie_chart = new MRCPSP.Gui.StatisticsViewer.Graph.PieGraph();
            ((System.ComponentModel.ISupportInitialize)(this.m_bind_navigator)).BeginInit();
            this.m_bind_navigator.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_bind_navigator
            // 
            this.m_bind_navigator.AddNewItem = this.bindingNavigatorAddNewItem;
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
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.m_bind_navigator.Location = new System.Drawing.Point(0, 0);
            this.m_bind_navigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.m_bind_navigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.m_bind_navigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.m_bind_navigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.m_bind_navigator.Name = "m_bind_navigator";
            this.m_bind_navigator.PositionItem = this.bindingNavigatorPositionItem;
            this.m_bind_navigator.Size = new System.Drawing.Size(923, 25);
            this.m_bind_navigator.TabIndex = 2;
            this.m_bind_navigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
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
            this.label1.Location = new System.Drawing.Point(54, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Problem  Input Parameters";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_selection_lbl);
            this.groupBox1.Controls.Add(this.m_first_gen_lbl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_crossover_lbl);
            this.groupBox1.Controls.Add(this.m_problem_title_lbl);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(95, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 186);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // m_selection_lbl
            // 
            this.m_selection_lbl.AutoSize = true;
            this.m_selection_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_selection_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_selection_lbl.Location = new System.Drawing.Point(149, 87);
            this.m_selection_lbl.Name = "m_selection_lbl";
            this.m_selection_lbl.Size = new System.Drawing.Size(51, 20);
            this.m_selection_lbl.TabIndex = 9;
            this.m_selection_lbl.Text = "label4";
            // 
            // m_first_gen_lbl
            // 
            this.m_first_gen_lbl.AutoSize = true;
            this.m_first_gen_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_first_gen_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_first_gen_lbl.Location = new System.Drawing.Point(149, 40);
            this.m_first_gen_lbl.Name = "m_first_gen_lbl";
            this.m_first_gen_lbl.Size = new System.Drawing.Size(51, 20);
            this.m_first_gen_lbl.TabIndex = 3;
            this.m_first_gen_lbl.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(6, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Selection Method:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(7, 40);
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
            this.m_crossover_lbl.Location = new System.Drawing.Point(149, 63);
            this.m_crossover_lbl.Name = "m_crossover_lbl";
            this.m_crossover_lbl.Size = new System.Drawing.Size(51, 20);
            this.m_crossover_lbl.TabIndex = 7;
            this.m_crossover_lbl.Text = "label3";
            // 
            // m_problem_title_lbl
            // 
            this.m_problem_title_lbl.AutoSize = true;
            this.m_problem_title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_problem_title_lbl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.m_problem_title_lbl.Location = new System.Drawing.Point(149, 16);
            this.m_problem_title_lbl.Name = "m_problem_title_lbl";
            this.m_problem_title_lbl.Size = new System.Drawing.Size(51, 20);
            this.m_problem_title_lbl.TabIndex = 1;
            this.m_problem_title_lbl.Text = "label3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(7, 63);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(923, 426);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(915, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_gantt_panel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(915, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_gantt_panel
            // 
            this.m_gantt_panel.Location = new System.Drawing.Point(16, 6);
            this.m_gantt_panel.Name = "m_gantt_panel";
            this.m_gantt_panel.Size = new System.Drawing.Size(820, 279);
            this.m_gantt_panel.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_generation_over_time_graph);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(915, 400);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.m_resources_pie_chart);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(915, 400);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(923, 426);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.m_export_to_excel_button);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 368);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(923, 83);
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
            // m_generation_over_time_graph
            // 
            this.m_generation_over_time_graph.Location = new System.Drawing.Point(52, 25);
            this.m_generation_over_time_graph.Name = "m_generation_over_time_graph";
            this.m_generation_over_time_graph.Size = new System.Drawing.Size(524, 277);
            this.m_generation_over_time_graph.TabIndex = 3;
            this.m_generation_over_time_graph.Title = "";
            this.m_generation_over_time_graph.XAxis = "";
            this.m_generation_over_time_graph.YAxis = "";
            // 
            // m_resources_pie_chart
            // 
            this.m_resources_pie_chart.Location = new System.Drawing.Point(51, 19);
            this.m_resources_pie_chart.Name = "m_resources_pie_chart";
            this.m_resources_pie_chart.Size = new System.Drawing.Size(524, 228);
            this.m_resources_pie_chart.TabIndex = 7;
            this.m_resources_pie_chart.Title = "";
            // 
            // StatisticsMonitor
            // 
            this.ClientSize = new System.Drawing.Size(923, 451);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_bind_navigator);
            this.Name = "StatisticsMonitor";
            this.Text = "Statistics Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.m_bind_navigator)).EndInit();
            this.m_bind_navigator.ResumeLayout(false);
            this.m_bind_navigator.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
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
            /*
            WebChart WebChart1 = new WebChart();
            WebChart1.DataSource =GetTestData();           
            // top title            
            WebChart1.Titles.Top.Visible = true;            
            WebChart1.Titles.Top.Text = "Chart title";            
            WebChart1.Titles.Top.Color = Color.Blue;            
            WebChart1.Titles.Top.Font = new Font("Verdana", 12, FontStyle.Bold);            
            // bottom title            
            WebChart1.Titles.Bottom.Visible = true;            
            WebChart1.Titles.Bottom.Text = "NOTE: This is the bottom title - red, smaller, italic, right aligned"; 
            WebChart1.Titles.Bottom.Color = Color.Red;            
            WebChart1.Titles.Bottom.Font = new Font("Verdana", 8, FontStyle.Italic);            
            WebChart1.Titles.Bottom.Align = ExpertChart.Align.Right;             
            // left title            
            WebChart1.Titles.Left.Visible = true;            
            WebChart1.Titles.Left.Text = "Left title can be used as Y axis label";            
            WebChart1.Titles.Left.Color = Color.Green;            
            WebChart1.Titles.Left.Font = new Font("Verdana", 10);
            this.components.Add(WebChart1);
            WebChart1.Visible = true;
           */
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
            m_first_gen_lbl.Text = m_current_summary.GeneratePopulationType;
            m_selection_lbl.Text = m_current_summary.SelectionType;
            m_crossover_lbl.Text = m_current_summary.CrossoverType;
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
          
            m_resources_gantt.setGanttData(m_current_summary);            
            m_resources_gantt.Refresh();
            m_resources_gantt.setGanttData2(m_current_summary);
            m_resources_gantt.Title = "Best Result in Generation";
            m_resources_gantt.Refresh();
        }

        private void updateReport()
        {
            updateGeneralParams();
            updateGenerationOverTime();
            updateResourcesInPie();
            updateResourcesInGantt();
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
            saveFileDialog1.Filter = "Excel Files (*.xls) |*.xls";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.InitialDirectory = Application.StartupPath;

            if (!(saveFileDialog1.ShowDialog() == DialogResult.OK))
            {
                return;

            }
            /*
            if (! saveFileDialog1.FileName.EndsWith(".xlsx"))
                saveFileDialog1.FileName = saveFileDialog1.FileName + ".xlsx";
             * */
            ExcelParser excel = new ExcelParser(saveFileDialog1.FileName, m_current_summary);
            //m_resources_gantt.saveImage(Application.StartupPath + "gantt.png");   

        }

        



    }
}
