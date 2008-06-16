using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Data.Odbc;
using MRCPSP.Database.MsSqlServer;

namespace MRCPSP.Gui
{
    class DBManagement : Form
    {
        private ListBox m_available_db_list;
        private Button m_db_switch_button;
        private Label label1;
        private Button m_create_new_db_schema_button;
        private ListBox m_available_problems_in_db_list;
        private Label label2;
        private Button m_remove_problem_from_database;

        private Button m_done_button;

        public DBManagement()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_done_button = new System.Windows.Forms.Button();
            this.m_available_db_list = new System.Windows.Forms.ListBox();
            this.m_db_switch_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_create_new_db_schema_button = new System.Windows.Forms.Button();
            this.m_available_problems_in_db_list = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_remove_problem_from_database = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_done_button
            // 
            this.m_done_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_done_button.Location = new System.Drawing.Point(124, 208);
            this.m_done_button.Name = "m_done_button";
            this.m_done_button.Size = new System.Drawing.Size(75, 23);
            this.m_done_button.TabIndex = 0;
            this.m_done_button.Text = "Done";
            this.m_done_button.UseVisualStyleBackColor = true;
            // 
            // m_available_db_list
            // 
            this.m_available_db_list.FormattingEnabled = true;
            this.m_available_db_list.Location = new System.Drawing.Point(12, 26);
            this.m_available_db_list.Name = "m_available_db_list";
            this.m_available_db_list.Size = new System.Drawing.Size(120, 95);
            this.m_available_db_list.TabIndex = 1;
            // 
            // m_db_switch_button
            // 
            this.m_db_switch_button.Location = new System.Drawing.Point(27, 118);
            this.m_db_switch_button.Name = "m_db_switch_button";
            this.m_db_switch_button.Size = new System.Drawing.Size(92, 23);
            this.m_db_switch_button.TabIndex = 2;
            this.m_db_switch_button.Text = "Switch to DB";
            this.m_db_switch_button.UseVisualStyleBackColor = true;
            this.m_db_switch_button.Click += new System.EventHandler(this.m_db_switch_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Available Databases:";
            // 
            // m_create_new_db_schema_button
            // 
            this.m_create_new_db_schema_button.Location = new System.Drawing.Point(86, 159);
            this.m_create_new_db_schema_button.Name = "m_create_new_db_schema_button";
            this.m_create_new_db_schema_button.Size = new System.Drawing.Size(161, 23);
            this.m_create_new_db_schema_button.TabIndex = 4;
            this.m_create_new_db_schema_button.Text = "Recreate Database Schema";
            this.m_create_new_db_schema_button.UseVisualStyleBackColor = true;
            this.m_create_new_db_schema_button.Click += new System.EventHandler(this.m_create_new_db_schema_button_Click);
            // 
            // m_available_problems_in_db_list
            // 
            this.m_available_problems_in_db_list.FormattingEnabled = true;
            this.m_available_problems_in_db_list.Location = new System.Drawing.Point(179, 26);
            this.m_available_problems_in_db_list.Name = "m_available_problems_in_db_list";
            this.m_available_problems_in_db_list.Size = new System.Drawing.Size(148, 95);
            this.m_available_problems_in_db_list.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Available Problem in Databse:";
            // 
            // m_remove_problem_from_database
            // 
            this.m_remove_problem_from_database.Location = new System.Drawing.Point(182, 117);
            this.m_remove_problem_from_database.Name = "m_remove_problem_from_database";
            this.m_remove_problem_from_database.Size = new System.Drawing.Size(145, 23);
            this.m_remove_problem_from_database.TabIndex = 7;
            this.m_remove_problem_from_database.Text = "Remove Problem";
            this.m_remove_problem_from_database.UseVisualStyleBackColor = true;
            this.m_remove_problem_from_database.Click += new System.EventHandler(this.m_remove_problem_from_database_Click);
            // 
            // DBManagement
            // 
            this.ClientSize = new System.Drawing.Size(354, 264);
            this.Controls.Add(this.m_remove_problem_from_database);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_available_problems_in_db_list);
            this.Controls.Add(this.m_create_new_db_schema_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_db_switch_button);
            this.Controls.Add(this.m_available_db_list);
            this.Controls.Add(this.m_done_button);
            this.Name = "DBManagement";
            this.Load += new System.EventHandler(this.DBManagement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void DBManagement_Load(object sender, EventArgs e)
        {
           // OdbcDataReader data =  DBHandler.Instance.queryForElement("show databases");
            //while (data.Read())
            //{
             //   m_available_db_list.Items.Add(data.GetString(0));
            //}
            //updateProblemList();
        }

        private void m_db_switch_button_Click(object sender, EventArgs e)
        {
         //   DBHandler.Instance.reconnectToDB((String)m_available_db_list.SelectedItem);
          //  updateProblemList();

        }

        private void m_create_new_db_schema_button_Click(object sender, EventArgs e)
        {
           // SchemaCreator.createSchema();
            MessageBox.Show("New Schema was created", "notify");

        }

        private void updateProblemList()
        {
            m_available_problems_in_db_list.Items.Clear();
            foreach (String problemName in DBHandler.Instance.getProblemNameList())
                m_available_problems_in_db_list.Items.Add(problemName);
        }

        private void m_remove_problem_from_database_Click(object sender, EventArgs e)
        {

        }

    }
}
