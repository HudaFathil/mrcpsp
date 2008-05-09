using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

using System.Collections;

using ZedGraph;

namespace MRCPSP.Gui.StatisticsViewer.Graph
{
    class GraphPanel : Panel
    {
        public GraphPane m_graph_pane;
        public ZedGraphControl m_graph_control;
        private System.ComponentModel.IContainer components;

        public GraphPanel()
        {     
            InitializeComponent();
            m_graph_pane = m_graph_control.GraphPane;
            m_graph_pane.Title.Text = "";
            m_graph_pane.XAxis.Title.Text = "";
            m_graph_pane.YAxis.Title.Text = "";
            m_graph_pane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
            m_graph_pane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);
            m_graph_control.Location = new Point(10, 10);
            m_graph_control.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
            this.Controls.Add(m_graph_control);       
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_graph_control = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // m_graph_control
            // 
            this.m_graph_control.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.m_graph_control.Location = new System.Drawing.Point(206, 0);
            this.m_graph_control.Name = "m_graph_control";
            this.m_graph_control.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.m_graph_control.ScrollGrace = 0;
            this.m_graph_control.ScrollMaxX = 0;
            this.m_graph_control.ScrollMaxY = 0;
            this.m_graph_control.ScrollMaxY2 = 0;
            this.m_graph_control.ScrollMinX = 0;
            this.m_graph_control.ScrollMinY = 0;
            this.m_graph_control.ScrollMinY2 = 0;
            this.m_graph_control.Size = new System.Drawing.Size(439, 297);
            this.m_graph_control.TabIndex = 0;
            this.ResumeLayout(false);

        }

        public String Title
        {
            get { return m_graph_pane.Title.Text; }
            set { m_graph_pane.Title.Text = value; }
        }

    }
}
