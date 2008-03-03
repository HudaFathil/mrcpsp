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
  
    class ConstraintItem : System.Windows.Forms.UserControl
    {
        private CanvasEditor m_canvas;
        private Pen m_pen;
        private StepItem m_from_step;
        private StepItem m_to_step;
        private Point m_point_from;
        private Point m_point_to;

        public ConstraintItem(CanvasEditor c, StepItem from, StepItem to)
        {
            m_canvas = c;
            m_from_step = from;
            m_to_step = to;
            Graphics graphics = m_canvas.CreateGraphics();
            m_pen = new Pen(Color.Black, 5);
            m_pen.StartCap = LineCap.Round;
            m_pen.EndCap = LineCap.ArrowAnchor;

            m_point_from = new Point();
            m_point_to = new Point();

            setArrowPoints();
            graphics.DrawLine(m_pen, m_point_from, m_point_to); 
            graphics.Dispose();
            this.Paint += new PaintEventHandler(ConstraintItem_Paint);
            this.Resize += new EventHandler(ConstraintItem_Resize);

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
