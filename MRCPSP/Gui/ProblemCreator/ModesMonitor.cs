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
        private Panel panel2;
        private Panel m_center_panel;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private int m_monitor_id;

        private System.Collections.ArrayList m_modes_list;

        public ModesMonitor(int monitor_id)
        {
            InitializeComponent();
            m_modes_list = new System.Collections.ArrayList();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_center_panel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_end_time_sb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_start_time_sb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mode_id_sb)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_done_button);
            this.groupBox1.Controls.Add(this.m_add_operation_button);
            this.groupBox1.Controls.Add(this.m_resource_name_cb);
            this.groupBox1.Controls.Add(this.m_end_time_sb);
            this.groupBox1.Controls.Add(this.m_start_time_sb);
            this.groupBox1.Controls.Add(this.m_mode_id_sb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 84);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "add operation";
            // 
            // m_done_button
            // 
            this.m_done_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_done_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_done_button.Location = new System.Drawing.Point(452, 47);
            this.m_done_button.Name = "m_done_button";
            this.m_done_button.Size = new System.Drawing.Size(72, 26);
            this.m_done_button.TabIndex = 5;
            this.m_done_button.Text = "done";
            this.m_done_button.UseVisualStyleBackColor = true;
            // 
            // m_add_operation_button
            // 
            this.m_add_operation_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_add_operation_button.Location = new System.Drawing.Point(449, 17);
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
            this.m_resource_name_cb.Location = new System.Drawing.Point(102, 19);
            this.m_resource_name_cb.Name = "m_resource_name_cb";
            this.m_resource_name_cb.Size = new System.Drawing.Size(121, 21);
            this.m_resource_name_cb.TabIndex = 3;
            // 
            // m_end_time_sb
            // 
            this.m_end_time_sb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_end_time_sb.Location = new System.Drawing.Point(343, 20);
            this.m_end_time_sb.Name = "m_end_time_sb";
            this.m_end_time_sb.Size = new System.Drawing.Size(65, 20);
            this.m_end_time_sb.TabIndex = 2;
            // 
            // m_start_time_sb
            // 
            this.m_start_time_sb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_start_time_sb.Location = new System.Drawing.Point(251, 20);
            this.m_start_time_sb.Name = "m_start_time_sb";
            this.m_start_time_sb.Size = new System.Drawing.Size(60, 20);
            this.m_start_time_sb.TabIndex = 1;
            // 
            // m_mode_id_sb
            // 
            this.m_mode_id_sb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mode_id_sb.Location = new System.Drawing.Point(27, 20);
            this.m_mode_id_sb.Name = "m_mode_id_sb";
            this.m_mode_id_sb.Size = new System.Drawing.Size(49, 20);
            this.m_mode_id_sb.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6);
            this.panel2.Size = new System.Drawing.Size(557, 53);
            this.panel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(71, 6);
            this.label4.MinimumSize = new System.Drawing.Size(120, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "mode id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(191, 6);
            this.label3.MinimumSize = new System.Drawing.Size(120, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "resource";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(311, 6);
            this.label2.MinimumSize = new System.Drawing.Size(120, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "start time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(431, 6);
            this.label1.MinimumSize = new System.Drawing.Size(120, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "end time";
            // 
            // m_center_panel
            // 
            this.m_center_panel.AutoScroll = true;
            this.m_center_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_center_panel.Location = new System.Drawing.Point(0, 53);
            this.m_center_panel.Name = "m_center_panel";
            this.m_center_panel.Size = new System.Drawing.Size(557, 180);
            this.m_center_panel.TabIndex = 4;
            // 
            // ModesMonitor
            // 
            this.ClientSize = new System.Drawing.Size(557, 317);
            this.Controls.Add(this.m_center_panel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ModesMonitor";
            this.Text = "Mode Monitor";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_end_time_sb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_start_time_sb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mode_id_sb)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void m_add_operation_button_Click(object sender, EventArgs e)
        {
            int id_of_operation = Convert.ToInt32(m_mode_id_sb.Value);
            ModeItem current_item = null;
            
            foreach (ModeItem item in m_modes_list) {
                if (id_of_operation == Convert.ToInt32(item.m_id.Text))
                    current_item = item;
            }
             
            if (current_item == null)
            {
                Panel current_panel = new Panel();
                current_item = new ModeItem();

                current_item.m_id.Parent = current_panel;
                current_item.m_resource_list.Parent = current_panel;
                current_item.m_start_time_list.Parent = current_panel;
                current_item.m_end_time_list.Parent = current_panel;
                
                current_panel.Parent = m_center_panel;
                current_panel.Dock = DockStyle.Top;
            
                current_item.m_id.Dock = DockStyle.Right;
                current_item.m_resource_list.Dock = DockStyle.Right;
                current_item.m_start_time_list.Dock = DockStyle.Right;
                current_item.m_end_time_list.Dock = DockStyle.Right;
            }
            m_modes_list.Add(current_item);
            current_item.m_id.Text = m_mode_id_sb.Value.ToString();   
            current_item.m_resource_list.Items.Add((ResourceBase)(m_resource_name_cb.SelectedItem));
            current_item.m_start_time_list.Items.Add(m_start_time_sb.Value.ToString());
            current_item.m_end_time_list.Items.Add(m_end_time_sb.Value.ToString());
        }
        /*
        protected override void  OnClosing(CancelEventArgs e)
        {
 	        this.Hide();
        }

        protected override void OnClosed(EventArgs e)
        {
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Hide();
        }
        protected override void OnLeave(EventArgs e)
        {
            Hide();
        }
        */
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
                Text = "Problem Creator Monitor # " + m_monitor_id.ToString();
            }
        }
    }

    public class ModeItem
    {
        public TextBox m_id;
        public ListBox m_resource_list;
        public ListBox m_start_time_list;
        public ListBox m_end_time_list;

        public ModeItem()
        {
            m_id = new TextBox();
            m_resource_list = new ListBox();
            m_start_time_list = new ListBox();
            m_end_time_list = new ListBox();
            m_resource_list.Height = 40;
            m_start_time_list.Height = 40;
            m_end_time_list.Height = 40;
        }
    }
}
