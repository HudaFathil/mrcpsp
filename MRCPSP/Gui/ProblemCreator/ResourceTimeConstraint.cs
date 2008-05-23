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
    class ResourceTimeConstraint : Form
    {
        private Label label1;
        private ComboBox m_resource_cb;
        private ListBox m_operation_from_list;
        private ListBox m_operation_to_list;
        private DataGridViewTextBoxColumn Resource;
        private DataGridViewTextBoxColumn DelayFrom;
        private DataGridViewTextBoxColumn DelayTo;
        private DataGridViewTextBoxColumn DelayTime;
        private Label label2;
        private Button m_add_delay_button;
        private NumericUpDown m_delay_time_sb;
        private Label label3;
        private DataGridView dataGridView1;

        private int m_monitor_id;
        public ResourceTimeConstraint(int monitor_id)
        {
            m_monitor_id = monitor_id;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.m_resource_cb = new System.Windows.Forms.ComboBox();
            this.m_operation_from_list = new System.Windows.Forms.ListBox();
            this.m_operation_to_list = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelayFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelayTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelayTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.m_add_delay_button = new System.Windows.Forms.Button();
            this.m_delay_time_sb = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_delay_time_sb)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reource:";
            // 
            // m_resource_cb
            // 
            this.m_resource_cb.FormattingEnabled = true;
            this.m_resource_cb.Location = new System.Drawing.Point(190, 9);
            this.m_resource_cb.Name = "m_resource_cb";
            this.m_resource_cb.Size = new System.Drawing.Size(121, 21);
            this.m_resource_cb.TabIndex = 1;
            // 
            // m_operation_from_list
            // 
            this.m_operation_from_list.FormattingEnabled = true;
            this.m_operation_from_list.Location = new System.Drawing.Point(66, 43);
            this.m_operation_from_list.Name = "m_operation_from_list";
            this.m_operation_from_list.Size = new System.Drawing.Size(120, 95);
            this.m_operation_from_list.TabIndex = 2;
            // 
            // m_operation_to_list
            // 
            this.m_operation_to_list.FormattingEnabled = true;
            this.m_operation_to_list.Location = new System.Drawing.Point(298, 43);
            this.m_operation_to_list.Name = "m_operation_to_list";
            this.m_operation_to_list.Size = new System.Drawing.Size(120, 95);
            this.m_operation_to_list.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource,
            this.DelayFrom,
            this.DelayTo,
            this.DelayTime});
            this.dataGridView1.Location = new System.Drawing.Point(12, 220);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(443, 129);
            this.dataGridView1.TabIndex = 4;
            // 
            // Resource
            // 
            this.Resource.HeaderText = "Resource";
            this.Resource.Name = "Resource";
            // 
            // DelayFrom
            // 
            this.DelayFrom.HeaderText = "Delay From";
            this.DelayFrom.Name = "DelayFrom";
            // 
            // DelayTo
            // 
            this.DelayTo.HeaderText = "Delay To";
            this.DelayTo.Name = "DelayTo";
            // 
            // DelayTime
            // 
            this.DelayTime.HeaderText = "Delay Time";
            this.DelayTime.Name = "DelayTime";
            // 
            // label2
            // 
            this.label2.Image = global::MRCPSP.Properties.Resources.new_arrow;
            this.label2.Location = new System.Drawing.Point(192, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 5;
            // 
            // m_add_delay_button
            // 
            this.m_add_delay_button.Location = new System.Drawing.Point(195, 190);
            this.m_add_delay_button.Name = "m_add_delay_button";
            this.m_add_delay_button.Size = new System.Drawing.Size(75, 23);
            this.m_add_delay_button.TabIndex = 6;
            this.m_add_delay_button.Text = "Add Delay";
            this.m_add_delay_button.UseVisualStyleBackColor = true;
            this.m_add_delay_button.Click += new System.EventHandler(this.m_add_delay_button_Click);
            // 
            // m_delay_time_sb
            // 
            this.m_delay_time_sb.Location = new System.Drawing.Point(240, 156);
            this.m_delay_time_sb.Name = "m_delay_time_sb";
            this.m_delay_time_sb.Size = new System.Drawing.Size(78, 20);
            this.m_delay_time_sb.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Minimum Delay";
            // 
            // ResourceTimeConstraint
            // 
            this.ClientSize = new System.Drawing.Size(482, 361);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_delay_time_sb);
            this.Controls.Add(this.m_add_delay_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.m_operation_to_list);
            this.Controls.Add(this.m_operation_from_list);
            this.Controls.Add(this.m_resource_cb);
            this.Controls.Add(this.label1);
            this.Name = "ResourceTimeConstraint";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_delay_time_sb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void m_add_delay_button_Click(object sender, EventArgs e)
        {
            //m_operation_from_list.SelectedItem();
        }

    }

    public class StepModeItem
    {
        private StepItem m_step;

        public StepModeItem(StepItem s)
        {
            m_step = s;
        }

        public StepItem myStep
        {
            get { return m_step; }
            set { m_step = value; }
        }

        public override string ToString()
        {
            return  m_step.ToString();
        }
    }
}
