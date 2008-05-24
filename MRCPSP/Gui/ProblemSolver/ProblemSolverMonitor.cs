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
        private NumericUpDown m_loops_sb;
        private Label label6;
        private RadioButton m_none_rb;
        private RadioButton m_increase_pop_rb;
        private RadioButton m_increase_mutate_rb;
        private GroupBox groupBox2;
        private Button m_stop_btn;
        private Panel panel2;
        private Panel panel3;
        private Button m_remove_problem_button;
        private Button m_add_problem_button;

        private BackgroundWorker m_background_worker;

        public ProblemSolverMonitor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.m_all_problems_lst = new System.Windows.Forms.ListBox();
            this.m_refresh_list_button = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_selected_problems_lst = new System.Windows.Forms.ListBox();
            this.m_clear_selected_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_remove_problem_button = new System.Windows.Forms.Button();
            this.m_add_problem_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_loops_sb = new System.Windows.Forms.NumericUpDown();
            this.m_mutation_percent_le = new System.Windows.Forms.TextBox();
            this.m_num_of_gen_le = new System.Windows.Forms.TextBox();
            this.m_population_size_le = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_increase_pop_rb = new System.Windows.Forms.RadioButton();
            this.m_none_rb = new System.Windows.Forms.RadioButton();
            this.m_increase_mutate_rb = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_stop_btn = new System.Windows.Forms.Button();
            this.m_start_test_button = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_loops_sb)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            // m_all_problems_lst
            // 
            this.m_all_problems_lst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_all_problems_lst.FormattingEnabled = true;
            this.m_all_problems_lst.Location = new System.Drawing.Point(0, 13);
            this.m_all_problems_lst.Name = "m_all_problems_lst";
            this.m_all_problems_lst.Size = new System.Drawing.Size(119, 186);
            this.m_all_problems_lst.TabIndex = 4;
            // 
            // m_refresh_list_button
            // 
            this.m_refresh_list_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_refresh_list_button.Location = new System.Drawing.Point(0, 208);
            this.m_refresh_list_button.Name = "m_refresh_list_button";
            this.m_refresh_list_button.Size = new System.Drawing.Size(119, 23);
            this.m_refresh_list_button.TabIndex = 7;
            this.m_refresh_list_button.Text = "refresh";
            this.m_refresh_list_button.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(27, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 237);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_selected_problems_lst);
            this.panel2.Controls.Add(this.m_clear_selected_button);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(198, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(119, 231);
            this.panel2.TabIndex = 9;
            // 
            // m_selected_problems_lst
            // 
            this.m_selected_problems_lst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_selected_problems_lst.FormattingEnabled = true;
            this.m_selected_problems_lst.Location = new System.Drawing.Point(0, 13);
            this.m_selected_problems_lst.Name = "m_selected_problems_lst";
            this.m_selected_problems_lst.Size = new System.Drawing.Size(119, 186);
            this.m_selected_problems_lst.TabIndex = 10;
            // 
            // m_clear_selected_button
            // 
            this.m_clear_selected_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_clear_selected_button.Location = new System.Drawing.Point(0, 208);
            this.m_clear_selected_button.Name = "m_clear_selected_button";
            this.m_clear_selected_button.Size = new System.Drawing.Size(119, 23);
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
            this.panel1.Size = new System.Drawing.Size(119, 231);
            this.panel1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_remove_problem_button);
            this.panel3.Controls.Add(this.m_add_problem_button);
            this.panel3.Location = new System.Drawing.Point(128, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(64, 231);
            this.panel3.TabIndex = 10;
            // 
            // m_remove_problem_button
            // 
            this.m_remove_problem_button.Image = global::MRCPSP.Properties.Resources.arrow_left;
            this.m_remove_problem_button.Location = new System.Drawing.Point(3, 98);
            this.m_remove_problem_button.Name = "m_remove_problem_button";
            this.m_remove_problem_button.Size = new System.Drawing.Size(58, 41);
            this.m_remove_problem_button.TabIndex = 13;
            this.m_remove_problem_button.UseVisualStyleBackColor = true;
            // 
            // m_add_problem_button
            // 
            this.m_add_problem_button.Image = global::MRCPSP.Properties.Resources.arrow_right;
            this.m_add_problem_button.Location = new System.Drawing.Point(3, 53);
            this.m_add_problem_button.Name = "m_add_problem_button";
            this.m_add_problem_button.Size = new System.Drawing.Size(58, 39);
            this.m_add_problem_button.TabIndex = 12;
            this.m_add_problem_button.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.m_loops_sb);
            this.groupBox1.Controls.Add(this.m_mutation_percent_le);
            this.groupBox1.Controls.Add(this.m_num_of_gen_le);
            this.groupBox1.Controls.Add(this.m_population_size_le);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(378, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 140);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Loops:";
            // 
            // m_loops_sb
            // 
            this.m_loops_sb.Location = new System.Drawing.Point(146, 110);
            this.m_loops_sb.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_loops_sb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_loops_sb.Name = "m_loops_sb";
            this.m_loops_sb.Size = new System.Drawing.Size(100, 20);
            this.m_loops_sb.TabIndex = 7;
            this.m_loops_sb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_mutation_percent_le
            // 
            this.m_mutation_percent_le.Location = new System.Drawing.Point(146, 84);
            this.m_mutation_percent_le.Name = "m_mutation_percent_le";
            this.m_mutation_percent_le.Size = new System.Drawing.Size(100, 20);
            this.m_mutation_percent_le.TabIndex = 5;
            this.m_mutation_percent_le.Text = "0";
            // 
            // m_num_of_gen_le
            // 
            this.m_num_of_gen_le.Location = new System.Drawing.Point(146, 58);
            this.m_num_of_gen_le.Name = "m_num_of_gen_le";
            this.m_num_of_gen_le.Size = new System.Drawing.Size(100, 20);
            this.m_num_of_gen_le.TabIndex = 4;
            this.m_num_of_gen_le.Text = "10";
            // 
            // m_population_size_le
            // 
            this.m_population_size_le.Location = new System.Drawing.Point(146, 32);
            this.m_population_size_le.Name = "m_population_size_le";
            this.m_population_size_le.Size = new System.Drawing.Size(100, 20);
            this.m_population_size_le.TabIndex = 3;
            this.m_population_size_le.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Mutation Percentage";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Population Size";
            // 
            // m_increase_pop_rb
            // 
            this.m_increase_pop_rb.AutoSize = true;
            this.m_increase_pop_rb.Location = new System.Drawing.Point(18, 42);
            this.m_increase_pop_rb.Name = "m_increase_pop_rb";
            this.m_increase_pop_rb.Size = new System.Drawing.Size(142, 17);
            this.m_increase_pop_rb.TabIndex = 9;
            this.m_increase_pop_rb.Text = "Increase Population Size";
            this.m_increase_pop_rb.UseVisualStyleBackColor = true;
            // 
            // m_none_rb
            // 
            this.m_none_rb.AutoSize = true;
            this.m_none_rb.Checked = true;
            this.m_none_rb.Location = new System.Drawing.Point(18, 19);
            this.m_none_rb.Name = "m_none_rb";
            this.m_none_rb.Size = new System.Drawing.Size(51, 17);
            this.m_none_rb.TabIndex = 10;
            this.m_none_rb.TabStop = true;
            this.m_none_rb.Text = "None";
            this.m_none_rb.UseVisualStyleBackColor = true;
            // 
            // m_increase_mutate_rb
            // 
            this.m_increase_mutate_rb.AutoSize = true;
            this.m_increase_mutate_rb.Location = new System.Drawing.Point(18, 65);
            this.m_increase_mutate_rb.Name = "m_increase_mutate_rb";
            this.m_increase_mutate_rb.Size = new System.Drawing.Size(136, 17);
            this.m_increase_mutate_rb.TabIndex = 11;
            this.m_increase_mutate_rb.Text = "Increase Mutation Rate";
            this.m_increase_mutate_rb.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_increase_mutate_rb);
            this.groupBox2.Controls.Add(this.m_none_rb);
            this.groupBox2.Controls.Add(this.m_increase_pop_rb);
            this.groupBox2.Location = new System.Drawing.Point(378, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Advanced Parameters";
            // 
            // m_stop_btn
            // 
            this.m_stop_btn.Image = global::MRCPSP.Properties.Resources.stop;
            this.m_stop_btn.Location = new System.Drawing.Point(444, 274);
            this.m_stop_btn.Name = "m_stop_btn";
            this.m_stop_btn.Size = new System.Drawing.Size(87, 84);
            this.m_stop_btn.TabIndex = 11;
            this.m_stop_btn.UseVisualStyleBackColor = true;
            this.m_stop_btn.Click += new System.EventHandler(this.m_stop_btn_Click);
            // 
            // m_start_test_button
            // 
            this.m_start_test_button.Image = global::MRCPSP.Properties.Resources.play;
            this.m_start_test_button.Location = new System.Drawing.Point(537, 274);
            this.m_start_test_button.Name = "m_start_test_button";
            this.m_start_test_button.Size = new System.Drawing.Size(87, 84);
            this.m_start_test_button.TabIndex = 3;
            this.m_start_test_button.UseVisualStyleBackColor = true;
            this.m_start_test_button.Click += new System.EventHandler(this.m_start_test_button_Click);
            // 
            // ProblemSolverMonitor
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(645, 387);
            this.Controls.Add(this.m_stop_btn);
            this.Controls.Add(this.groupBox2);
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
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_loops_sb)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void m_start_test_button_Click(object sender, EventArgs e)
        {
            ApplicManager.Instance.loadAdvancedParams((int)m_loops_sb.Value);
            int[] alg_params = new int[3];
            alg_params[0] = Convert.ToInt32(m_population_size_le.Text);
            alg_params[1] = Convert.ToInt32(m_num_of_gen_le.Text);
            alg_params[2] = Convert.ToInt32(m_mutation_percent_le.Text);
            m_background_worker.RunWorkerAsync(alg_params);
                                        
        }

        private void m_stop_btn_Click(object sender, EventArgs e)
        {
            m_background_worker.CancelAsync();
        }

        public void signBackgroundWorker(BackgroundWorker worker)
        {
            m_background_worker = worker;

        }

    }
}
