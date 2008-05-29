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
        private Label label2;
        private Button m_add_delay_button;
        private NumericUpDown m_delay_time_sb;
        private Label label3;
        private DataGridView m_data_grid_table;
        private Button m_done_button;
        private Label label4;
        private Label label5;
        private DataGridViewTextBoxColumn Resource;
        private DataGridViewTextBoxColumn DelayFrom;
        private DataGridViewTextBoxColumn DelayTo;
        private DataGridViewTextBoxColumn DelayTime;

        private int m_monitor_id;
        public ResourceTimeConstraint(int monitor_id)
        {
            m_monitor_id = monitor_id;
            InitializeComponent();
            m_data_grid_table.RowCount = 1;
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.m_resource_cb = new System.Windows.Forms.ComboBox();
            this.m_operation_from_list = new System.Windows.Forms.ListBox();
            this.m_operation_to_list = new System.Windows.Forms.ListBox();
            this.m_data_grid_table = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.m_add_delay_button = new System.Windows.Forms.Button();
            this.m_delay_time_sb = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.m_done_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelayFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelayTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelayTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.m_data_grid_table)).BeginInit();
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
            this.m_resource_cb.SelectedIndexChanged += new System.EventHandler(this.m_resource_cb_SelectedIndexChanged);
            // 
            // m_operation_from_list
            // 
            this.m_operation_from_list.FormattingEnabled = true;
            this.m_operation_from_list.Location = new System.Drawing.Point(62, 69);
            this.m_operation_from_list.Name = "m_operation_from_list";
            this.m_operation_from_list.Size = new System.Drawing.Size(128, 108);
            this.m_operation_from_list.TabIndex = 2;
            // 
            // m_operation_to_list
            // 
            this.m_operation_to_list.FormattingEnabled = true;
            this.m_operation_to_list.Location = new System.Drawing.Point(302, 69);
            this.m_operation_to_list.Name = "m_operation_to_list";
            this.m_operation_to_list.Size = new System.Drawing.Size(129, 108);
            this.m_operation_to_list.TabIndex = 3;
            // 
            // m_data_grid_table
            // 
            this.m_data_grid_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_data_grid_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource,
            this.DelayFrom,
            this.DelayTo,
            this.DelayTime});
            this.m_data_grid_table.Location = new System.Drawing.Point(16, 247);
            this.m_data_grid_table.Name = "m_data_grid_table";
            this.m_data_grid_table.Size = new System.Drawing.Size(443, 129);
            this.m_data_grid_table.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Image = global::MRCPSP.Properties.Resources.new_arrow;
            this.label2.Location = new System.Drawing.Point(196, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 5;
            // 
            // m_add_delay_button
            // 
            this.m_add_delay_button.Location = new System.Drawing.Point(199, 217);
            this.m_add_delay_button.Name = "m_add_delay_button";
            this.m_add_delay_button.Size = new System.Drawing.Size(75, 23);
            this.m_add_delay_button.TabIndex = 6;
            this.m_add_delay_button.Text = "Add Delay";
            this.m_add_delay_button.UseVisualStyleBackColor = true;
            this.m_add_delay_button.Click += new System.EventHandler(this.m_add_delay_button_Click);
            // 
            // m_delay_time_sb
            // 
            this.m_delay_time_sb.Location = new System.Drawing.Point(244, 183);
            this.m_delay_time_sb.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_delay_time_sb.Name = "m_delay_time_sb";
            this.m_delay_time_sb.Size = new System.Drawing.Size(78, 20);
            this.m_delay_time_sb.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Minimum Delay";
            // 
            // m_done_button
            // 
            this.m_done_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_done_button.Location = new System.Drawing.Point(199, 382);
            this.m_done_button.Name = "m_done_button";
            this.m_done_button.Size = new System.Drawing.Size(75, 23);
            this.m_done_button.TabIndex = 9;
            this.m_done_button.Text = "Done";
            this.m_done_button.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(59, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Operation: Delay From";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(299, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Operation: Delay To";
            // 
            // Resource
            // 
            this.Resource.HeaderText = "Resource";
            this.Resource.Name = "Resource";
            this.Resource.ReadOnly = true;
            // 
            // DelayFrom
            // 
            this.DelayFrom.HeaderText = "Delay From";
            this.DelayFrom.Name = "DelayFrom";
            this.DelayFrom.ReadOnly = true;
            // 
            // DelayTo
            // 
            this.DelayTo.HeaderText = "Delay To";
            this.DelayTo.Name = "DelayTo";
            this.DelayTo.ReadOnly = true;
            // 
            // DelayTime
            // 
            this.DelayTime.HeaderText = "Delay Time";
            this.DelayTime.Name = "DelayTime";
            // 
            // ResourceTimeConstraint
            // 
            this.ClientSize = new System.Drawing.Size(482, 424);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_done_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_delay_time_sb);
            this.Controls.Add(this.m_add_delay_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_data_grid_table);
            this.Controls.Add(this.m_operation_to_list);
            this.Controls.Add(this.m_operation_from_list);
            this.Controls.Add(this.m_resource_cb);
            this.Controls.Add(this.label1);
            this.Name = "ResourceTimeConstraint";
            this.Text = "Resources Time Constraints";
            ((System.ComponentModel.ISupportInitialize)(this.m_data_grid_table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_delay_time_sb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void m_add_delay_button_Click(object sender, EventArgs e)
        {
            if (m_resource_cb.SelectedIndex < 0)
                return;
            if (m_operation_from_list.SelectedIndex < 0)
                return;
            if (m_operation_to_list.SelectedIndex < 0)
                return;
            Object[] val = {m_resource_cb.SelectedItem, m_operation_from_list.SelectedItem, m_operation_to_list.SelectedItem, m_delay_time_sb.Value};
            m_data_grid_table.Rows.Add(val);    
        }

        public void updateResources()
        {
            m_resource_cb.Items.Clear();
            foreach (Worker w in ProblemCreatorState.Instance(m_monitor_id).getWorkers())
                m_resource_cb.Items.Add(w);
            foreach (Machine w in ProblemCreatorState.Instance(m_monitor_id).getMachines())
                m_resource_cb.Items.Add(w);
        }

        private void m_resource_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_operation_from_list.Items.Clear();
            m_operation_to_list.Items.Clear();
            ResourceBase r = (ResourceBase)m_resource_cb.SelectedItem;
            foreach (StepItem s in ProblemCreatorState.Instance(m_monitor_id).getSteps())
            {
                Dictionary<int, DataGridView> modes_dic = s.getModesDictionary();
                foreach (int mode_index in modes_dic.Keys)
                {
                    for (int i = 0; i < modes_dic[mode_index].RowCount; i++)
                    {
                        if (modes_dic[mode_index]["Resource", 0].Value == r)
                        {
                            StepModeItem sm = new StepModeItem(s, mode_index);
                            m_operation_from_list.Items.Add(sm);
                            m_operation_to_list.Items.Add(sm);
                            break;
                        }
                    }
                }
            }
        }
    }

    public class StepModeItem
    {
        private StepItem m_step;
        private int m_mode_index;

        public StepModeItem(StepItem s, int m)
        {
            m_step = s;
            m_mode_index = m;
        }

        public StepItem myStep
        {
            get { return m_step; }
            set { m_step = value; }
        }

        public int myModeIndex
        {
            get { return m_mode_index; }
            set { m_mode_index = value; }
        }

        public override string ToString()
        {
            return  m_step.ToString() + ", mode: " + m_mode_index.ToString();
        }
    }
}
