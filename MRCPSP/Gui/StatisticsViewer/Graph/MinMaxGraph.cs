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
    class MinMaxGraph : GraphPanel
    {
     
        public MinMaxGraph() : base()
        {           
        }

        public void setMinMaxData(List<KeyValuePair<double,double>>  data, String curve_name) {
            m_graph_pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            for (int i = 0; i < data.Count; i++ )
            { 
                list.Add( (double) i + 1, data[i].Value, (data[i].Key == data[i].Value)? data[i].Key - 0.5 : data[i].Key);
            }
            HiLowBarItem myCurve = m_graph_pane.AddHiLowBar( curve_name, list, Color.Red );
            myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );
            m_graph_control.AxisChange();
            m_graph_control.Location = new Point(10, 10);
            m_graph_control.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }

        public String YAxis
        {
            get { return m_graph_pane.YAxis.Title.Text; }
            set { m_graph_pane.YAxis.Title.Text = value; }
        }

        public String XAxis
        {
            get { return m_graph_pane.XAxis.Title.Text; }
            set { m_graph_pane.XAxis.Title.Text = value; }
        }
		
    }
}
