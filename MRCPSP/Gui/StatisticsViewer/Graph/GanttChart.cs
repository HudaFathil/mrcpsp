using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

using ZedGraph;

namespace MRCPSP.Gui.StatisticsViewer.Graph
{
    class GanttChart : GraphPanel
    { 
        public GanttChart() : base()
        {
           
        }

        public void setGanttData(PointPairList list, String curve_name) {
            m_graph_pane.CurveList.Clear();
     
            Random rand = new Random();
            PointPairList hList = new PointPairList();


            for (int i = 1; i < 7; i++)
            {
                double close = 10.0 + rand.NextDouble() * 5;
                double hi = close + 2.0 * rand.NextDouble();
                double low = close - 2.0 * rand.NextDouble();
                hList.Add(hi, i, low);
            }

            PointPairList tList = new PointPairList();

            for (int i = 1; i < 7; i++)
            {
                double close = 2.0 + rand.NextDouble() * 5;
                double hi = close + 2.0 * rand.NextDouble();
                double low = close - 2.0 * rand.NextDouble();
                tList.Add(hi, i, low);
            }
   
            // Add a blue error bar to the graph
            HiLowBarItem myCurve = m_graph_pane.AddHiLowBar( "Price Range", hList, Color.Blue );
            HiLowBarItem myCurve2 = m_graph_pane.AddHiLowBar( "Price Ra2nge", tList, Color.Red);
    
            myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90 );
            myCurve2.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red, 90);

            m_graph_pane.BarSettings.Base = BarBase.Y;
  
            // Fill the axis background with a color gradient
            m_graph_pane.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166 ), 90F );
            BarItem.CreateBarLabels( m_graph_pane, true, "f0" );

            m_graph_control.AxisChange();
            m_graph_control.Location = new Point(10, 10);
            m_graph_control.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        
           string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
 
            m_graph_pane.YAxis.MajorTic.IsBetweenLabels = true;
    
            // Set the YAxis labels
            m_graph_pane.YAxis.Scale.TextLabels = labels;
            // Set the YAxis to Text type
            m_graph_pane.YAxis.Type = AxisType.Text;
         
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // m_graph_control
            // 
            this.m_graph_control.Load += new System.EventHandler(this.m_graph_control_Load);
            this.ResumeLayout(false);

        }

        private void m_graph_control_Load(object sender, EventArgs e)
        {

        }

    }
}
