using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MRCPSP.Controllers;

namespace MRCPSP.Gui.ProblemSolver
{
    class ProblemSolverMonitor : Form
    {
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Label label1;
        private Button m_start_test_button;
        private ListBox m_all_problems_lst;
        private Button m_refresh_list_button;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private ListBox m_selected_problems_lst;
        private Button m_clear_selected_button;
        private Label label2;
        private Panel panel2;

        public ProblemSolverMonitor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.m_start_test_button = new System.Windows.Forms.Button();
            this.m_all_problems_lst = new System.Windows.Forms.ListBox();
            this.m_refresh_list_button = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_selected_problems_lst = new System.Windows.Forms.ListBox();
            this.m_clear_selected_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(33, 24);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(33, 47);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "available problems";
            // 
            // m_start_test_button
            // 
            this.m_start_test_button.Location = new System.Drawing.Point(474, 286);
            this.m_start_test_button.Name = "m_start_test_button";
            this.m_start_test_button.Size = new System.Drawing.Size(75, 23);
            this.m_start_test_button.TabIndex = 3;
            this.m_start_test_button.Text = "start";
            this.m_start_test_button.UseVisualStyleBackColor = true;
            this.m_start_test_button.Click += new System.EventHandler(this.m_start_test_button_Click);
            // 
            // m_all_problems_lst
            // 
            this.m_all_problems_lst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_all_problems_lst.FormattingEnabled = true;
            this.m_all_problems_lst.Location = new System.Drawing.Point(0, 13);
            this.m_all_problems_lst.Name = "m_all_problems_lst";
            this.m_all_problems_lst.Size = new System.Drawing.Size(129, 95);
            this.m_all_problems_lst.TabIndex = 4;
            // 
            // m_refresh_list_button
            // 
            this.m_refresh_list_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_refresh_list_button.Location = new System.Drawing.Point(0, 113);
            this.m_refresh_list_button.Name = "m_refresh_list_button";
            this.m_refresh_list_button.Size = new System.Drawing.Size(129, 23);
            this.m_refresh_list_button.TabIndex = 7;
            this.m_refresh_list_button.Text = "refresh";
            this.m_refresh_list_button.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(177, 75);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(270, 142);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_selected_problems_lst);
            this.panel2.Controls.Add(this.m_clear_selected_button);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(138, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(129, 136);
            this.panel2.TabIndex = 9;
            // 
            // m_selected_problems_lst
            // 
            this.m_selected_problems_lst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_selected_problems_lst.FormattingEnabled = true;
            this.m_selected_problems_lst.Location = new System.Drawing.Point(0, 13);
            this.m_selected_problems_lst.Name = "m_selected_problems_lst";
            this.m_selected_problems_lst.Size = new System.Drawing.Size(129, 95);
            this.m_selected_problems_lst.TabIndex = 10;
            // 
            // m_clear_selected_button
            // 
            this.m_clear_selected_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_clear_selected_button.Location = new System.Drawing.Point(0, 113);
            this.m_clear_selected_button.Name = "m_clear_selected_button";
            this.m_clear_selected_button.Size = new System.Drawing.Size(129, 23);
            this.m_clear_selected_button.TabIndex = 9;
            this.m_clear_selected_button.Text = "clear";
            this.m_clear_selected_button.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "selected problems";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_all_problems_lst);
            this.panel1.Controls.Add(this.m_refresh_list_button);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(129, 136);
            this.panel1.TabIndex = 9;
            // 
            // ProblemSolverMonitor
            // 
            this.ClientSize = new System.Drawing.Size(592, 321);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.m_start_test_button);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Name = "ProblemSolverMonitor";
            this.Text = "Problem Solver Monitor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void m_start_test_button_Click(object sender, EventArgs e)
        {
            // need to load pselected arams

            ApplicManager.Instance.run();
        
        }

    }
}
