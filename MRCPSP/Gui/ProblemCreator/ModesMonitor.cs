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
    class ModesMonitor : Form
    {
        private GroupBox groupBox1;
        private NumericUpDown m_end_time_sb;
        private NumericUpDown m_start_time_sb;
        private Button m_add_operation_button;
        private ComboBox m_resource_name_cb;
        private NumericUpDown m_mode_id_sb;
        private Button m_done_button;
        private Panel m_center_panel;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private int m_monitor_id;
        private Button m_add_new_mode_button;
        private int m_box_counter;
        private Panel panel1;
        private GroupBox groupBox2;
        private NumericUpDown numericUpDown1;
        private Button m_remove_mode_button;
        private Dictionary<int, DataGridView> m_modes_index_to_table;
        public ModesMonitor(int monitor_id)
        {
            InitializeComponent();
            m_modes_index_to_table = new Dictionary<int, DataGridView>();
            m_monitor_id = monitor_id;
            m_box_counter = 1;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_done_button = new System.Windows.Forms.Button();
            this.m_add_operation_button = new System.Windows.Forms.Button();
            this.m_resource_name_cb = new System.Windows.Forms.ComboBox();
            this.m_end_time_sb = new System.Windows.Forms.NumericUpDown();
            this.m_start_time_sb = new System.Windows.Forms.NumericUpDown();
            this.m_mode_id_sb = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_center_panel = new System.Windows.Forms.Panel();
            this.m_add_new_mode_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_remove_mode_button = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_end_time_sb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_start_time_sb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mode_id_sb)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_add_operation_button);
            this.groupBox1.Controls.Add(this.m_resource_name_cb);
            this.groupBox1.Controls.Add(this.m_end_time_sb);
            this.groupBox1.Controls.Add(this.m_start_time_sb);
            this.groupBox1.Controls.Add(this.m_mode_id_sb);
            this.groupBox1.Location = new System.Drawing.Point(205, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 140);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "add operation";
            // 
            // m_done_button
            // 
            this.m_done_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_done_button.Location = new System.Drawing.Point(191, 159);
            this.m_done_button.Name = "m_done_button";
            this.m_done_button.Size = new System.Drawing.Size(72, 26);
            this.m_done_button.TabIndex = 5;
            this.m_done_button.Text = "done";
            this.m_done_button.UseVisualStyleBackColor = true;
            // 
            // m_add_operation_button
            // 
            this.m_add_operation_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_add_operation_button.Location = new System.Drawing.Point(9, 111);
            this.m_add_operation_button.Name = "m_add_operation_button";
            this.m_add_operation_button.Size = new System.Drawing.Size(75, 23);
            this.m_add_operation_button.TabIndex = 4;
            this.m_add_operation_button.Text = "add";
            this.m_add_operation_button.UseVisualStyleBackColor = true;
            this.m_add_operation_button.Click += new System.EventHandler(this.m_add_operation_button_Click);
            // 
            // m_resource_name_cb
            // 
            this.m_resource_name_cb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_resource_name_cb.FormattingEnabled = true;
            this.m_resource_name_cb.Location = new System.Drawing.Point(129, 39);
            this.m_resource_name_cb.Name = "m_resource_name_cb";
            this.m_resource_name_cb.Size = new System.Drawing.Size(100, 21);
            this.m_resource_name_cb.TabIndex = 3;
            // 
            // m_end_time_sb
            // 
            this.m_end_time_sb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_end_time_sb.Location = new System.Drawing.Point(129, 92);
            this.m_end_time_sb.Name = "m_end_time_sb";
            this.m_end_time_sb.Size = new System.Drawing.Size(100, 20);
            this.m_end_time_sb.TabIndex = 2;
            // 
            // m_start_time_sb
            // 
            this.m_start_time_sb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_start_time_sb.Location = new System.Drawing.Point(129, 66);
            this.m_start_time_sb.Name = "m_start_time_sb";
            this.m_start_time_sb.Size = new System.Drawing.Size(100, 20);
            this.m_start_time_sb.TabIndex = 1;
            // 
            // m_mode_id_sb
            // 
            this.m_mode_id_sb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mode_id_sb.Location = new System.Drawing.Point(129, 13);
            this.m_mode_id_sb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_mode_id_sb.Name = "m_mode_id_sb";
            this.m_mode_id_sb.Size = new System.Drawing.Size(100, 20);
            this.m_mode_id_sb.TabIndex = 0;
            this.m_mode_id_sb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(6, 13);
            this.label4.MinimumSize = new System.Drawing.Size(120, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "mode id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.MinimumSize = new System.Drawing.Size(120, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "resource";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.MinimumSize = new System.Drawing.Size(120, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "start time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(6, 92);
            this.label1.MinimumSize = new System.Drawing.Size(120, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "end time";
            // 
            // m_center_panel
            // 
            this.m_center_panel.AutoScroll = true;
            this.m_center_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_center_panel.Location = new System.Drawing.Point(0, 0);
            this.m_center_panel.Name = "m_center_panel";
            this.m_center_panel.Size = new System.Drawing.Size(464, 386);
            this.m_center_panel.TabIndex = 4;
            // 
            // m_add_new_mode_button
            // 
            this.m_add_new_mode_button.Location = new System.Drawing.Point(6, 19);
            this.m_add_new_mode_button.Name = "m_add_new_mode_button";
            this.m_add_new_mode_button.Size = new System.Drawing.Size(98, 23);
            this.m_add_new_mode_button.TabIndex = 5;
            this.m_add_new_mode_button.Text = "Add New Mode";
            this.m_add_new_mode_button.UseVisualStyleBackColor = true;
            this.m_add_new_mode_button.Click += new System.EventHandler(this.m_add_new_mode_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.m_done_button);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 194);
            this.panel1.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.m_remove_mode_button);
            this.groupBox2.Controls.Add(this.m_add_new_mode_button);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 140);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Mode";
            // 
            // m_remove_mode_button
            // 
            this.m_remove_mode_button.Location = new System.Drawing.Point(6, 53);
            this.m_remove_mode_button.Name = "m_remove_mode_button";
            this.m_remove_mode_button.Size = new System.Drawing.Size(98, 23);
            this.m_remove_mode_button.TabIndex = 6;
            this.m_remove_mode_button.Text = "Remove Mode";
            this.m_remove_mode_button.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(122, 56);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown1.TabIndex = 7;
            // 
            // ModesMonitor
            // 
            this.ClientSize = new System.Drawing.Size(464, 386);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_center_panel);
            this.Name = "ModesMonitor";
            this.Text = "Mode Monitor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_end_time_sb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_start_time_sb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mode_id_sb)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        private bool isOperationParamsLegal()
        {
            if (m_resource_name_cb.SelectedItem == null)
                return false;
            if (m_start_time_sb.Value < 0)
                return false;
            if (m_end_time_sb.Value <= m_start_time_sb.Value)
                return false;
            
            int id_of_operation = Convert.ToInt32(m_mode_id_sb.Value);
            if (!m_modes_index_to_table.ContainsKey(id_of_operation))
                return false;
            DataGridView data = m_modes_index_to_table[id_of_operation];
            for (int i=0; i < data.RowCount; i++) {
                if (data["Resource", 0].Value == m_resource_name_cb.SelectedItem)
                    return false;
            }
            return true;
        }

        private void m_add_operation_button_Click(object sender, EventArgs e)
        {
            if (!isOperationParamsLegal())
            {
                MessageBox.Show("Illegal Parameters of Operation", "Error");
                return;
            }
            int id_of_operation = Convert.ToInt32(m_mode_id_sb.Value);
            DataGridView data = m_modes_index_to_table[id_of_operation];
            Object[] val = {m_resource_name_cb.SelectedItem,m_start_time_sb.Value,m_end_time_sb.Value};
            data.Rows.Add(val);          
        }
    
        public void updateResources()
        {
            m_resource_name_cb.Items.Clear();
            foreach (Worker w in ProblemCreatorState.Instance(monitor_id).getWorkers())
                m_resource_name_cb.Items.Add(w);
            foreach (Machine w in ProblemCreatorState.Instance(monitor_id).getMachines())
                m_resource_name_cb.Items.Add(w);
        }

        public int monitor_id
        {
            get { return m_monitor_id; }
            set
            {
                m_monitor_id = value;
            }
        }

        private void m_add_new_mode_button_Click(object sender, EventArgs e)
        {            
            GroupBox box = new GroupBox();
            box.Text = "Mode " + m_box_counter.ToString();
            DataGridView newgrid = new DataGridView();
            newgrid.Columns.Add("Resource", "Resource");
            newgrid.Columns["Resource"].ReadOnly = true;
            newgrid.Columns.Add("StartTime", "Start Time");
            newgrid.Columns.Add("EndTime", "End Time");
            newgrid.RowCount = 1;
            m_modes_index_to_table[m_box_counter] = newgrid;
            m_box_counter++;
            box.Controls.Add(newgrid);
            newgrid.Dock = DockStyle.Fill;
            box.Parent = m_center_panel;
            box.Dock = DockStyle.Top;
        }

        public Dictionary<int, DataGridView> getModesDictionary()
        {
            return m_modes_index_to_table;
        }
    }
}
