using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using MRCPSP.Controllers;
using MRCPSP.Algorithm;
using MRCPSP.Lindo;
using ZedGraph;

namespace MRCPSP.Gui.StatisticsViewer.Graph
{
    class GanttChart : GraphPanel
    {
        private Dictionary<Product, Hashtable>  m_list_of_charts_by_type;
        private Dictionary<KeyValuePair<int, int>, string> m_list_of_labels_by_pos;


        public GanttChart() : base()
        {
            m_list_of_charts_by_type = new Dictionary<Product, Hashtable>();
            m_list_of_labels_by_pos = new Dictionary<KeyValuePair<int, int>, string>();
        }

        // Initiatialize color list with dark color's
        private List<Color> initColorList()
        {
            List<Color> colorList = new List<Color>();

            foreach (string colorName in Enum.GetNames(typeof(System.Drawing.KnownColor)))
            {           
                if (colorName.StartsWith("D") == true)
                {
                    colorList.Add(System.Drawing.Color.FromName(colorName));
                }
            }      
            return colorList;
        }
         
        private String[] getSummaryLabels(Resource[] resources)
        {
            String[] labels = new String[resources.Length];
            for (int i = 0; i < resources.Length; i++)
                labels[i] = resources[i].Name;
            return labels;
        }
        
        
        public PointPairList getGanttListByType(Product p, Step step, int JobNum) {
            // need to add family
            if (!m_list_of_charts_by_type.ContainsKey(p))
                m_list_of_charts_by_type.Add(p, new Hashtable());
            if (!m_list_of_charts_by_type[p].Contains(step))
                m_list_of_charts_by_type[p].Add(step, new Hashtable());
            Hashtable map_after_step = (Hashtable)m_list_of_charts_by_type[p][step];
            if (!map_after_step.Contains(JobNum))
            {
                PointPairList ppl = new PointPairList();
                map_after_step.Add(JobNum, ppl);
                for (int i = 1; i <= ApplicManager.Instance.CurrentProblem.Resources.Count; i++)
                    ppl.Add(0, i, 0);
            }
            return (PointPairList)map_after_step[JobNum];
        }
      
        /*
        public void setGanttDataByResource(ResultSummary summary, DataGridView gantt_table)
        {
            Solution best_solution = summary.getBestSolution();
            m_graph_pane.CurveList.Clear();
            m_graph_pane.GraphObjList.Clear();
            List<Color> color_list = initColorList();        
            int color_counter = 0;
            int resource_counter = 1;
         
            foreach (Resource r in best_solution.resultFromLindo.Keys)
            {
                List<ResultParameter> resource_operations_done = best_solution.resultFromLindo[r];
                PointPairList list = new PointPairList();       
                for (int i = 0; i < resource_operations_done.Count; i++)
                {                                     
                    list.Add(resource_operations_done[i].startTime, resource_counter, resource_operations_done[i].finishTime);
                    Object[] val = { r.Name, resource_operations_done[i].product.Name, resource_operations_done[i].jobID.ToString(), resource_operations_done[i].step.Name, resource_operations_done[i].startTime.ToString(), resource_operations_done[i].finishTime.ToString()};
                    gantt_table.Rows.Add(val);    
                }
                HiLowBarItem myCurve = m_graph_pane.AddHiLowBar(r.Name, list, color_list[color_counter % color_list.Count]);
                myCurve.Bar.Fill = new Fill(color_list[color_counter % color_list.Count], Color.White, color_list[color_counter % color_list.Count], 90);
                resource_counter++;
                color_counter++;
            }
            // Set the legend to an arbitrary location
            m_graph_pane.Legend.Position = LegendPos.Float;
            m_graph_pane.Legend.Location = new Location(0.95f, 0.15f, CoordType.PaneFraction, AlignH.Right, AlignV.Top);
            m_graph_pane.Legend.FontSpec.Size = 14f;
            m_graph_pane.Legend.IsHStack = false;
       
            m_graph_pane.BarSettings.Base = BarBase.Y;       
            m_graph_pane.YAxis.IsVisible = false;
           
            m_graph_pane.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);
            m_graph_control.AxisChange();
            m_graph_control.Location = new Point(10, 10);
            m_graph_control.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
         
        }
        */
        
        public void setGanttData(ResultSummary summary)
        {
            Solution best_solution = summary.getBestSolution();
            m_graph_pane.CurveList.Clear();
            m_graph_pane.GraphObjList.Clear();
            
            int resource_counter = 1;
            foreach (Resource r in best_solution.resultFromLindo.Keys)
            {
                List<ResultParameter> resource_operations_done = best_solution.resultFromLindo[r];
                for (int i = 0; i < resource_operations_done.Count; i++)
                {
                    PointPairList list = getGanttListByType(resource_operations_done[i].product, resource_operations_done[i].step, resource_operations_done[i].jobID);
                    for (int j = 0; j < list.Count; j++)
                    {
                        if ((int)list[j].Y == resource_counter && resource_operations_done[i].finishTime != 0)
                        {
                            String title = resource_operations_done[i].product.Name+  " job: " + resource_operations_done[i].jobID + " " + resource_operations_done[i].step.Name;
                            list[j].X = resource_operations_done[i].startTime;
                            list[j].Z = resource_operations_done[i].finishTime;
                            if (m_list_of_labels_by_pos.ContainsKey(new KeyValuePair<int,int>((int)list[j].X, resource_counter))) 
                                m_list_of_labels_by_pos[new KeyValuePair<int,int>((int)list[j].X, resource_counter)]+= "\n" + title;
                            else
                                m_list_of_labels_by_pos.Add(new KeyValuePair<int,int>((int)list[j].X, resource_counter), title);
                        }
                    }  
             
                }
                resource_counter++;
            }
        }

        public void setGanttData2(ResultSummary summary) {
            List<Color> color_list = initColorList();
            Solution best_solution = summary.getBestSolution();
            int color_counter = 0;
            foreach (Product p in m_list_of_charts_by_type.Keys)
            {
                foreach (Step step in m_list_of_charts_by_type[p].Keys)
                {
                    Hashtable list_after_step = (Hashtable)m_list_of_charts_by_type[p][step];

                    for (int job_num = 0; job_num < list_after_step.Keys.Count; job_num++)
                    {
                        PointPairList list = (PointPairList)list_after_step[job_num];
                        String title = "job: " + job_num.ToString() + " step: " + step.Id.ToString();
                        HiLowBarItem myCurve = m_graph_pane.AddHiLowBar(title, list, color_list[color_counter % color_list.Count]);
                        myCurve.Bar.Fill = new Fill(color_list[color_counter % color_list.Count], Color.White, color_list[color_counter % color_list.Count], 90);
                        color_counter++;
                        Console.Out.WriteLine("step: " + step.Id.ToString() + " job_num " + job_num.ToString());
                        for (int i = 0; i < list.Count; i++)
                        {
                            Console.Out.WriteLine(list[i].X.ToString() + " " + list[i].Y.ToString() + " " + list[i].Z.ToString());
                        }
                    }
                }
            }
            m_graph_pane.Legend.IsVisible = false;
            m_graph_pane.BarSettings.Base = BarBase.Y;
            m_graph_pane.BarSettings.ClusterScaleWidth = 15;
            
            //m_graph_pane.BarSettings.Type = BarType.Stack;
            string[] labels = getSummaryLabels(best_solution.resultFromLindo.Keys.ToArray<Resource>());
            // Set the YAxis labels
            m_graph_pane.YAxis.Scale.TextLabels = labels;
            // Set the YAxis to Text type
            m_graph_pane.YAxis.Type = AxisType.Text;
            m_graph_pane.YAxis.MajorTic.IsBetweenLabels = true;         
            // Fill the axis background with a color gradient
            m_graph_pane.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166 ), 90F );
            
            setTitlesToGraph();
            m_graph_control.AxisChange();
            m_graph_control.Location = new Point(10, 10);
            m_graph_control.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
       
        }

        private void setTitlesToGraph()
        {
            foreach (KeyValuePair<int, int> key in m_list_of_labels_by_pos.Keys)
            {
                TextObj text = new TextObj( m_list_of_labels_by_pos[key], (float)( key.Key ), (float)( key.Value) - 0.5);
                text.Location.CoordinateFrame = CoordType.AxisXYScale;
                text.Location.AlignH = AlignH.Left;
                text.Location.AlignV = AlignV.Center;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                m_graph_pane.GraphObjList.Add( text );
            }
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
