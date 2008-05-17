using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;

namespace MRCPSP.Gui.ProblemCreator
{
  
    public class ConstraintItem : System.Windows.Forms.UserControl
    {
        private CanvasEditor m_canvas;
        private Pen m_pen;
        private StepItem m_from_step;
        private StepItem m_to_step;
        private Point m_point_from;
        private Point m_point_to;
        private ConstraintProperties m_properties;
        private double m_max_queue_time;
        private double m_min_queue_time;

        public ConstraintItem(CanvasEditor c, StepItem from, StepItem to)
        {
            m_canvas = c;
            m_from_step = from;
            m_to_step = to;
            m_max_queue_time = Double.PositiveInfinity;
            m_min_queue_time = 0.0;
            Graphics graphics = m_canvas.CreateGraphics();
            m_pen = new Pen(ProblemCreatorState.Instance(m_from_step.monitor_id).CurrentProduct.ConstraintsColor, 5);
            m_pen.StartCap = LineCap.Round;
            m_pen.EndCap = LineCap.ArrowAnchor;

            m_point_from = new Point();
            m_point_to = new Point();

            setArrowPoints();
            graphics.DrawLine(m_pen, m_point_from, m_point_to); 
            graphics.Dispose();
            this.Paint += new PaintEventHandler(ConstraintItem_Paint);
            this.Resize += new EventHandler(ConstraintItem_Resize);
            m_properties = new ConstraintProperties();
        }

        public void ConstraintItem_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void ConstraintItem_Paint(object sender, PaintEventArgs e)
        {
            setArrowPoints();
            e.Graphics.DrawLine(m_pen, m_point_from, m_point_to);
        }

        private void setArrowPoints()
        {
            int height_diff = m_from_step.Location.Y - m_to_step.Location.Y;
            int width_diff =  m_from_step.Location.X - m_to_step.Location.X;
            if (Math.Abs(width_diff) >= Math.Abs(height_diff)) {
                m_point_from.Y = m_from_step.Location.Y + m_from_step.Height / 2;
                m_point_to.Y = m_to_step.Location.Y + m_to_step.Height / 2;
                if (width_diff < 0) {                  
                    m_point_from.X = m_from_step.Location.X + m_from_step.Width; 
                    m_point_to.X = m_to_step.Location.X; 
                } else {
                    m_point_from.X = m_from_step.Location.X;    
                    m_point_to.X = m_to_step.Location.X + m_to_step.Width;
                }
            } else {
                m_point_from.X = m_from_step.Location.X + m_from_step.Width / 2;
                m_point_to.X = m_to_step.Location.X + m_to_step.Width / 2;
                if (height_diff < 0) {
                    m_point_from.Y = m_from_step.Location.Y + m_from_step.Height; 
                    m_point_to.Y = m_to_step.Location.Y; 
                } else {
                    m_point_from.Y = m_from_step.Location.Y;    
                    m_point_to.Y = m_to_step.Location.Y + m_to_step.Height / 2;
                }
            }
        }

        public void openPropertiesWindow()
        {
            m_properties.MinQueueTime.Text = m_min_queue_time.ToString();
            m_properties.MaxQueueTime.Text = m_max_queue_time.ToString();
            m_properties.ShowDialog(new Form());
            if (m_properties.DialogResult == DialogResult.OK)
            {
                m_min_queue_time = Convert.ToDouble(m_properties.MinQueueTime.Text);
                m_max_queue_time = Convert.ToDouble(m_properties.MaxQueueTime.Text);
            }
        }

        public double MinQueueTime
        {
            get { return m_min_queue_time; }
        }

        public double MaxQueueTime
        {
            get { return m_min_queue_time; }
        }

        public StepItem getFromStep()
        {
            return this.m_from_step;
        }

        public StepItem getToStep()
        {
            return this.m_to_step;
        }

        public Point getFromPoint()
        {
            return m_point_from;
        }

        public Point getToPoint()
        {
            return m_point_to;
        }
    }
}
