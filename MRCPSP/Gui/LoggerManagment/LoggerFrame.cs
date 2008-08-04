using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using MRCPSP.Log;

namespace MRCPSP.Gui.LoggerManagment
{
   
    class LoggerFrame : Form
    {
        private Label m_title;
        private Panel m_panel1;
        private ComboBox m_level;
        private Button m_save;

        public LoggerFrame()
        {
            InitializeComponent();
        }

        private void InitializeComponent() 
        {
            this.m_title = new System.Windows.Forms.Label();
            this.m_panel1 = new System.Windows.Forms.Panel();
            this.m_level = new System.Windows.Forms.ComboBox();
            this.m_save = new System.Windows.Forms.Button();
            this.m_panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_title
            // 
            this.m_title.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.m_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_title.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_title.Location = new System.Drawing.Point(0, 13);
            this.m_title.Name = "m_title";
            this.m_title.Size = new System.Drawing.Size(204, 21);
            this.m_title.TabIndex = 0;
            this.m_title.Text = "Please select the logging level";
            this.m_title.TextChanged += new System.EventHandler(this.m_title_TextChanged);
            // 
            // m_panel1
            // 
            this.m_panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_panel1.Controls.Add(this.m_title);
            this.m_panel1.Controls.Add(this.m_level);
            this.m_panel1.Controls.Add(this.m_save);
            this.m_panel1.Location = new System.Drawing.Point(1, -1);
            this.m_panel1.Name = "m_panel1";
            this.m_panel1.Size = new System.Drawing.Size(337, 101);
            this.m_panel1.TabIndex = 1;
            // 
            // m_level
            // 
            this.m_level.DisplayMember = "DEBUG";
            this.m_level.Items.AddRange(new object[] {
            "DEBUG",
            "INFO",
            "WARN",
            "ERROR"});
            this.m_level.Location = new System.Drawing.Point(202, 15);
            this.m_level.Name = "m_level";
            this.m_level.Size = new System.Drawing.Size(132, 21);
            this.m_level.TabIndex = 1;
            this.m_level.Text = "<Select>";
            this.m_level.SelectedIndexChanged += new System.EventHandler(this.m_level_SelectedIndexChanged);
            // 
            // m_save
            // 
            this.m_save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_save.Location = new System.Drawing.Point(132, 75);
            this.m_save.Name = "m_save";
            this.m_save.Size = new System.Drawing.Size(75, 23);
            this.m_save.TabIndex = 2;
            this.m_save.Text = "Save";
            this.m_save.Click += new System.EventHandler(this.m_save_Click);
            // 
            // LoggerFrame
            // 
            this.ClientSize = new System.Drawing.Size(336, 158);
            this.Controls.Add(this.m_panel1);
            this.Location = new System.Drawing.Point(20, 20);
            this.Name = "LoggerFrame";
            this.Text = "Add Machine";
            this.Load += new System.EventHandler(this.LoggerFrame_Load_1);
            this.m_panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void LoggerFrame_Load(object sender, EventArgs e)
        {

        }

        private void LoggerFrame_Load_1(object sender, EventArgs e)
        {

        }

        private void m_title_TextChanged(object sender, EventArgs e)
        {

        }

        private void m_level_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_save_Click(object sender, EventArgs e)
        {
            String st = (String)this.m_level.SelectedItem;
            if (st.Equals("DEBUG"))
            {
                Logger.Instance.State = Logger.LOGGER_STATE.DEBUG;
            }
            if (st.Equals("ERROR"))
            {
                Logger.Instance.State = Logger.LOGGER_STATE.ERROR;
            }
            if (st.Equals("INFO"))
            {
                Logger.Instance.State = Logger.LOGGER_STATE.INFO;
            }
            if (st.Equals("WARN"))
            {
                Logger.Instance.State = Logger.LOGGER_STATE.WARN;
            }

            MessageBox.Show("Logger state changed successfully", "notify");
        }
    }
}
