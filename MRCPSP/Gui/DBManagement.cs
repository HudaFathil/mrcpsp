using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Data.Odbc;
using MRCPSP.Database;

namespace MRCPSP.Gui
{
    class DBManagement : Form
    {
        private ListBox m_available_db_list;
        private Button m_db_switch_button;
        private Label label1;

        private Button m_apply_button;

        public DBManagement()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_apply_button = new System.Windows.Forms.Button();
            this.m_available_db_list = new System.Windows.Forms.ListBox();
            this.m_db_switch_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_apply_button
            // 
            this.m_apply_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_apply_button.Location = new System.Drawing.Point(114, 210);
            this.m_apply_button.Name = "m_apply_button";
            this.m_apply_button.Size = new System.Drawing.Size(75, 23);
            this.m_apply_button.TabIndex = 0;
            this.m_apply_button.Text = "Apply";
            this.m_apply_button.UseVisualStyleBackColor = true;
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
            // DBManagement
            // 
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_db_switch_button);
            this.Controls.Add(this.m_available_db_list);
            this.Controls.Add(this.m_apply_button);
            this.Name = "DBManagement";
            this.Load += new System.EventHandler(this.DBManagement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void DBManagement_Load(object sender, EventArgs e)
        {
            OdbcDataReader data =  DBHandler.Instance.queryForElement("show databases");
            while (data.Read())
            {
                m_available_db_list.Items.Add(data.GetString(0));
            }
        }

        private void m_db_switch_button_Click(object sender, EventArgs e)
        {
            DBHandler.Instance.reconnectToDB((String)m_available_db_list.SelectedItem);
        }
    }
}
