using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

using System.Collections;

using MRCPSP.CommonTypes;

using ZedGraph;

namespace MRCPSP.Gui.StatisticsViewer.Graph
{
    class PieGraph  : GraphPanel
    {
      
        public PieGraph() : base()
        {
          
        }  

        public void setPieData(Dictionary<Resource, int> vals)
        {
            // Set the legend to an arbitrary location
            m_graph_pane.Legend.Position = LegendPos.Float;
            m_graph_pane.Legend.Location = new Location( 0.95f, 0.15f, CoordType.PaneFraction, AlignH.Right, AlignV.Top );
            m_graph_pane.Legend.FontSpec.Size = 10f;
            m_graph_pane.Legend.IsHStack = false;

            String[] names = new String[vals.Count];
            double[] values = new double[vals.Count];
            int i=0;
            foreach (Resource r in vals.Keys)
            {
                names[i] = r.ToString();
                values[i] = (double)vals[r];
                i++;
            }
            m_graph_pane.CurveList.Clear();
            m_graph_pane.AddPieSlices(values, names);
              
            TextObj text = new TextObj( "most used resource \na", 0.18F, 0.40F, CoordType.PaneFraction );
            text.Location.AlignH = AlignH.Center;
            text.Location.AlignV = AlignV.Bottom;
            text.FontSpec.Border.IsVisible = false;
            text.FontSpec.Fill = new Fill( Color.White, Color.FromArgb( 255, 100, 100 ), 45F );
            text.FontSpec.StringAlignment = StringAlignment.Center;
            m_graph_pane.GraphObjList.Add( text );
            // Create a drop shadow for the total value text item
            TextObj text2 = new TextObj( text );
            text2.FontSpec.Fill = new Fill( Color.Black );
            text2.Location.X += 0.008f;
            text2.Location.Y += 0.01f;
            m_graph_pane.GraphObjList.Add( text2 );
            m_graph_control.AxisChange();
            m_graph_control.Location = new Point(10, 10);
            m_graph_control.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }
    }
}
