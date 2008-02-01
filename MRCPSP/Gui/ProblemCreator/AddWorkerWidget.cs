using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace MRCPSP.Gui.ProblemCreator
{
    class AddWorkerWidget : Form
    {
        private Button m_save_button;
        private Button m_cancel_button;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private TextBox m_title_te;
        private Label label1;
        private Worker m_worker;

        public AddWorkerWidget(Worker w)
        {
            this.m_worker = w;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_save_button = new System.Windows.Forms.Button();
            this.m_cancel_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_title_te = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_save_button
            // 
            this.m_save_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_save_button.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_save_button.Location = new System.Drawing.Point(0, 0);
            this.m_save_button.Name = "m_save_button";
            this.m_save_button.Size = new System.Drawing.Size(75, 37);
            this.m_save_button.TabIndex = 0;
            this.m_save_button.Text = "Save";
            this.m_save_button.UseVisualStyleBackColor = true;
            this.m_save_button.Click += new System.EventHandler(this.m_save_button_Click);
            // 
            // m_cancel_button
            // 
            this.m_cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_cancel_button.Location = new System.Drawing.Point(189, 0);
            this.m_cancel_button.Name = "m_cancel_button";
            this.m_cancel_button.Size = new System.Drawing.Size(75, 37);
            this.m_cancel_button.TabIndex = 1;
            this.m_cancel_button.Text = "Cancel";
            this.m_cancel_button.UseVisualStyleBackColor = true;
            this.m_cancel_button.Click += new System.EventHandler(this.m_cancel_button_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 125);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_save_button);
            this.panel2.Controls.Add(this.m_cancel_button);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(264, 37);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_title_te);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(264, 23);
            this.panel3.TabIndex = 4;
            // 
            // m_title_te
            // 
            this.m_title_te.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_title_te.Location = new System.Drawing.Point(50, 0);
            this.m_title_te.Name = "m_title_te";
            this.m_title_te.Size = new System.Drawing.Size(100, 20);
            this.m_title_te.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddWorkerWidget
            // 
            this.ClientSize = new System.Drawing.Size(264, 125);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AddWorkerWidget";
            this.Text = "Add Worker";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void m_save_button_Click(object sender, EventArgs e)
        {
            m_worker.setTitle(m_title_te.Text);
        }

        private void m_cancel_button_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}
