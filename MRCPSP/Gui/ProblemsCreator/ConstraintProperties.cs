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
    class ConstraintProperties : Form
    {
        private Panel panel3;
        private Label label1;
        private Panel panel1;
        private Button m_cancel_button;
        private Panel panel2;
        private TextBox m_maximal_time_le;
        private TextBox m_minimal_time_le;
        private Label label3;
        private Label label2;
        private Button m_save_button;

        public ConstraintProperties()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_maximal_time_le = new System.Windows.Forms.TextBox();
            this.m_minimal_time_le = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cancel_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_save_button = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 23);
            this.panel3.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Constraint Properties";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_maximal_time_le);
            this.panel1.Controls.Add(this.m_minimal_time_le);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 109);
            this.panel1.TabIndex = 8;
            // 
            // m_maximal_time_le
            // 
            this.m_maximal_time_le.Location = new System.Drawing.Point(144, 69);
            this.m_maximal_time_le.Name = "m_maximal_time_le";
            this.m_maximal_time_le.Size = new System.Drawing.Size(103, 20);
            this.m_maximal_time_le.TabIndex = 3;
            // 
            // m_minimal_time_le
            // 
            this.m_minimal_time_le.Location = new System.Drawing.Point(144, 42);
            this.m_minimal_time_le.Name = "m_minimal_time_le";
            this.m_minimal_time_le.Size = new System.Drawing.Size(103, 20);
            this.m_minimal_time_le.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Maximal Queue Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Minimal Queue Time:";
            // 
            // m_cancel_button
            // 
            this.m_cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_cancel_button.Location = new System.Drawing.Point(194, 0);
            this.m_cancel_button.Name = "m_cancel_button";
            this.m_cancel_button.Size = new System.Drawing.Size(75, 37);
            this.m_cancel_button.TabIndex = 1;
            this.m_cancel_button.Text = "Cancel";
            this.m_cancel_button.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_save_button);
            this.panel2.Controls.Add(this.m_cancel_button);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 37);
            this.panel2.TabIndex = 9;
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
            // 
            // ConstraintProperties
            // 
            this.ClientSize = new System.Drawing.Size(269, 146);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ConstraintProperties";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public TextBox MaxQueueTime       
        {
            get { return m_maximal_time_le; }
            set { m_maximal_time_le = value; }
        }

        public TextBox MinQueueTime
        {
            get { return m_minimal_time_le; }
            set { m_minimal_time_le = value; }
        }
    }
}
