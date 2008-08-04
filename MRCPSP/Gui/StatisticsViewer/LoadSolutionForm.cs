using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;

using MRCPSP.Controllers;
using MRCPSP.Domain;

namespace MRCPSP.Gui.StatisticsViewer
{
    class LoadSolutionForm : Form
    {
        private ListBox m_problems_list;
        private ListBox m_solutions_list;
        private Label label1;
        private Label label2;
        private Button m_cancel_button;
        private Button m_load_solution;

        public LoadSolutionForm()
        {
            InitializeComponent();
            this.GotFocus += new EventHandler(LoadSolutionForm_GotFocus);
            m_problems_list.Items.Clear();
            List<string> lst = ApplicManager.Instance.getProblemListFromDB();
            for (int i = 0; i < lst.Count; i++)
                m_problems_list.Items.Add(lst[i]);
        }

        void LoadSolutionForm_GotFocus(object sender, EventArgs e)
        {
            m_problems_list.Items.Clear();
            List<string> lst = ApplicManager.Instance.getProblemListFromDB();
            for (int i = 0; i < lst.Count; i++)
                m_problems_list.Items.Add(lst[i]);
        }

        private void InitializeComponent()
        {
            this.m_problems_list = new System.Windows.Forms.ListBox();
            this.m_solutions_list = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_load_solution = new System.Windows.Forms.Button();
            this.m_cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_problems_list
            // 
            this.m_problems_list.FormattingEnabled = true;
            this.m_problems_list.Location = new System.Drawing.Point(22, 41);
            this.m_problems_list.Name = "m_problems_list";
            this.m_problems_list.Size = new System.Drawing.Size(231, 199);
            this.m_problems_list.TabIndex = 0;
            this.m_problems_list.SelectedIndexChanged += new System.EventHandler(this.m_problems_list_SelectedIndexChanged);
            // 
            // m_solutions_list
            // 
            this.m_solutions_list.FormattingEnabled = true;
            this.m_solutions_list.Location = new System.Drawing.Point(259, 41);
            this.m_solutions_list.Name = "m_solutions_list";
            this.m_solutions_list.Size = new System.Drawing.Size(243, 199);
            this.m_solutions_list.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Problems";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(255, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Solutions";
            // 
            // m_load_solution
            // 
            this.m_load_solution.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_load_solution.Location = new System.Drawing.Point(436, 287);
            this.m_load_solution.Name = "m_load_solution";
            this.m_load_solution.Size = new System.Drawing.Size(75, 23);
            this.m_load_solution.TabIndex = 4;
            this.m_load_solution.Text = "Load";
            this.m_load_solution.UseVisualStyleBackColor = true;
            this.m_load_solution.Click += new System.EventHandler(this.m_load_solution_Click);
            // 
            // m_cancel_button
            // 
            this.m_cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancel_button.Location = new System.Drawing.Point(355, 287);
            this.m_cancel_button.Name = "m_cancel_button";
            this.m_cancel_button.Size = new System.Drawing.Size(75, 23);
            this.m_cancel_button.TabIndex = 5;
            this.m_cancel_button.Text = "Cancel";
            this.m_cancel_button.UseVisualStyleBackColor = true;
            // 
            // LoadSolutionForm
            // 
            this.ClientSize = new System.Drawing.Size(527, 331);
            this.Controls.Add(this.m_cancel_button);
            this.Controls.Add(this.m_load_solution);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_solutions_list);
            this.Controls.Add(this.m_problems_list);
            this.Name = "LoadSolutionForm";
            this.Text = "Solutions Loader";
            this.Load += new System.EventHandler(this.LoadSolutionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadSolutionForm_Load(object sender, EventArgs e)
        {

        }

        private void m_problems_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_solutions_list.Items.Clear();
            List<string> lst = ApplicManager.Instance.getSolutionsListFromDB((string)m_problems_list.SelectedItem);
            for (int i = 0; i < lst.Count; i++)
                m_solutions_list.Items.Add(lst[i]);
        }

        private void m_load_solution_Click(object sender, EventArgs e)
        {
            if (m_solutions_list.SelectedIndex < 0)
                return;

            ApplicManager.Instance.loadSolution(((String)m_solutions_list.SelectedItem).GetHashCode(), (string)m_problems_list.SelectedItem);
            
        }
    }
}
