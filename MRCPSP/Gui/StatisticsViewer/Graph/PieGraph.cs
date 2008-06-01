using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

using System.Collections;

using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Controllers;
using MRCPSP.Algorithm;
using MRCPSP.Lindo;

using ZedGraph;

namespace MRCPSP.Gui.StatisticsViewer.Graph
{
    class PieGraph  : GraphPanel
    {
      
        public PieGraph() : base()
        {
          
        }  

        public void setPieData(ResultSummary summary)
        {
            // Set the legend to an arbitrary location
            m_graph_pane.Legend.Position = LegendPos.Float;
            m_graph_pane.Legend.Location = new Location( 0.95f, 0.15f, CoordType.PaneFraction, AlignH.Right, AlignV.Top );
            m_graph_pane.Legend.FontSpec.Size = 13f;
            m_graph_pane.Legend.IsHStack = false;

            Solution best_solution = summary.getBestSolution();
            m_graph_pane.CurveList.Clear();

            String[] names = new String[best_solution.resultFromLindo.Keys.Count];
            double[] values = new double[best_solution.resultFromLindo.Keys.Count];
            int resource_counter = 0;
            foreach (Resource r in best_solution.resultFromLindo.Keys)
            {
                names[resource_counter] = r.Name;
                List<ResultParameter> tasks_for_resource =  best_solution.resultFromLindo[r];         
                for (int i = 0; i < tasks_for_resource.Count; i++)
                {
                    values[resource_counter] += (tasks_for_resource[i].finishTime - tasks_for_resource[i].startTime);                    
                }
                resource_counter++;
               
            }
            m_graph_pane.AddPieSlices(values, names);

            int best_iter = 0;
            for (int i = 1; i < values.Length; i++)
                if (values[i] > values[best_iter])
                    best_iter = i;
            TextObj text = new TextObj( "most used resource \n"+ names[best_iter].ToString() + " = " + values[best_iter].ToString(), 0.18F, 0.40F, CoordType.PaneFraction );
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
            text2.FontSpec.Size = 13f;
            m_graph_pane.GraphObjList.Add( text2 );
            m_graph_control.AxisChange();
            m_graph_control.Location = new Point(10, 10);
            m_graph_control.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }
    }
}
