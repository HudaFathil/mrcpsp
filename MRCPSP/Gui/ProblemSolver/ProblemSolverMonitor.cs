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
        private Label label1;
        private Button m_start_test_button;
        private ListBox m_all_problems_lst;
        private Button m_refresh_list_button;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private ListBox m_selected_problems_lst;
        private Button m_clear_selected_button;
        private Label label2;
        private GroupBox groupBox1;
        private TextBox m_mutation_percent_le;
        private TextBox m_num_of_gen_le;
        private TextBox m_population_size_le;
        private Label label5;
        private Label label4;
        private Label label3;
        private Panel panel2;

        public ProblemSolverMonitor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_population_size_le = new System.Windows.Forms.TextBox();
            this.m_num_of_gen_le = new System.Windows.Forms.TextBox();
            this.m_mutation_percent_le = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(27, 26);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_mutation_percent_le);
            this.groupBox1.Controls.Add(this.m_num_of_gen_le);
            this.groupBox1.Controls.Add(this.m_population_size_le);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(303, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 142);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Population Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Number Of Generation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Mutation Percentage";
            // 
            // m_population_size_le
            // 
            this.m_population_size_le.Location = new System.Drawing.Point(146, 32);
            this.m_population_size_le.Name = "m_population_size_le";
            this.m_population_size_le.Size = new System.Drawing.Size(100, 20);
            this.m_population_size_le.TabIndex = 3;
            this.m_population_size_le.Text = "10";
            // 
            // m_num_of_gen_le
            // 
            this.m_num_of_gen_le.Location = new System.Drawing.Point(146, 58);
            this.m_num_of_gen_le.Name = "m_num_of_gen_le";
            this.m_num_of_gen_le.Size = new System.Drawing.Size(100, 20);
            this.m_num_of_gen_le.TabIndex = 4;
            this.m_num_of_gen_le.Text = "10";
            // 
            // m_mutation_percent_le
            // 
            this.m_mutation_percent_le.Location = new System.Drawing.Point(146, 98);
            this.m_mutation_percent_le.Name = "m_mutation_percent_le";
            this.m_mutation_percent_le.Size = new System.Drawing.Size(100, 20);
            this.m_mutation_percent_le.TabIndex = 5;
            this.m_mutation_percent_le.Text = "0";
            // 
            // ProblemSolverMonitor
            // 
            this.ClientSize = new System.Drawing.Size(592, 321);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.m_start_test_button);
            this.Name = "ProblemSolverMonitor";
            this.Text = "Problem Solver Monitor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void m_start_test_button_Click(object sender, EventArgs e)
        {
            // need to load pselected arams

            ApplicManager.Instance.run(Convert.ToInt32(m_population_size_le.Text),
                                        Convert.ToInt32(m_num_of_gen_le.Text),
                                        Convert.ToInt32(m_mutation_percent_le.Text));
        
        }

    }
}
