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
    class AddProductWidget : Form
    {
        private Label label2;
        private ColorDialog m_colorDialog;
        private Button m_add_product;
        private Label label1;
        private Button m_pick_color_button;
        private TextBox m_product_name_text_box;
        private NumericUpDown m_product_size_sb;
        private Button m_cancel_button;

        private ProductItem m_my_product;

        public AddProductWidget(ProductItem p)
        {
            m_my_product = p;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.m_colorDialog = new System.Windows.Forms.ColorDialog();
            this.m_add_product = new System.Windows.Forms.Button();
            this.m_cancel_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_pick_color_button = new System.Windows.Forms.Button();
            this.m_product_name_text_box = new System.Windows.Forms.TextBox();
            this.m_product_size_sb = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.m_product_size_sb)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "name:";
            // 
            // m_add_product
            // 
            this.m_add_product.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_add_product.Location = new System.Drawing.Point(105, 102);
            this.m_add_product.Name = "m_add_product";
            this.m_add_product.Size = new System.Drawing.Size(76, 24);
            this.m_add_product.TabIndex = 2;
            this.m_add_product.Text = "Add Product";
            this.m_add_product.UseVisualStyleBackColor = true;
            // 
            // m_cancel_button
            // 
            this.m_cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancel_button.Location = new System.Drawing.Point(15, 101);
            this.m_cancel_button.Name = "m_cancel_button";
            this.m_cancel_button.Size = new System.Drawing.Size(74, 25);
            this.m_cancel_button.TabIndex = 3;
            this.m_cancel_button.Text = "Cancel";
            this.m_cancel_button.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Capacity:";
            // 
            // m_pick_color_button
            // 
            this.m_pick_color_button.Location = new System.Drawing.Point(15, 72);
            this.m_pick_color_button.Name = "m_pick_color_button";
            this.m_pick_color_button.Size = new System.Drawing.Size(74, 23);
            this.m_pick_color_button.TabIndex = 5;
            this.m_pick_color_button.Text = "Select Color";
            this.m_pick_color_button.UseVisualStyleBackColor = true;
            this.m_pick_color_button.Click += new System.EventHandler(this.m_pick_color_button_Click);
            // 
            // m_product_name_text_box
            // 
            this.m_product_name_text_box.Location = new System.Drawing.Point(81, 6);
            this.m_product_name_text_box.Name = "m_product_name_text_box";
            this.m_product_name_text_box.Size = new System.Drawing.Size(87, 20);
            this.m_product_name_text_box.TabIndex = 6;
            this.m_product_name_text_box.TextChanged += new System.EventHandler(this.m_product_name_text_box_TextChanged);
            // 
            // m_product_size_sb
            // 
            this.m_product_size_sb.Location = new System.Drawing.Point(81, 35);
            this.m_product_size_sb.Name = "m_product_size_sb";
            this.m_product_size_sb.Size = new System.Drawing.Size(87, 20);
            this.m_product_size_sb.TabIndex = 7;
            this.m_product_size_sb.ValueChanged += new System.EventHandler(this.m_product_size_sb_ValueChanged);
            // 
            // AddProductWidget
            // 
            this.ClientSize = new System.Drawing.Size(222, 160);
            this.Controls.Add(this.m_product_size_sb);
            this.Controls.Add(this.m_product_name_text_box);
            this.Controls.Add(this.m_pick_color_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cancel_button);
            this.Controls.Add(this.m_add_product);
            this.Controls.Add(this.label2);
            this.Name = "AddProductWidget";
            ((System.ComponentModel.ISupportInitialize)(this.m_product_size_sb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void m_product_name_text_box_TextChanged(object sender, EventArgs e)
        {
            m_my_product.Name = m_product_name_text_box.Text;
        }

        private void m_product_size_sb_ValueChanged(object sender, EventArgs e)
        {
            m_my_product.Size = (int)m_product_size_sb.Value;
        }

        private void m_pick_color_button_Click(object sender, EventArgs e)
        {
            m_colorDialog.AnyColor = true;
            m_colorDialog.ShowHelp = true;          
            if (m_colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                Color c = m_colorDialog.Color;
                m_my_product.ConstraintsColor = c;
            }
        }


    }
}
