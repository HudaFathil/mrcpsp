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
        private GraphicsPath m_path;
        private Pen m_pen;
        private StepItem m_from_step;
        private StepItem m_to_step;

        public ConstraintItem(CanvasEditor c, StepItem from, StepItem to)
        {
            m_canvas = c;
            m_from_step = from;
            m_to_step = to;
        
            Graphics g = m_canvas.CreateGraphics();
            m_path = new GraphicsPath();
            m_pen = new Pen(Color.Black, 5);
       
            m_pen.StartCap = LineCap.Round;
            m_pen.EndCap = LineCap.ArrowAnchor;            
            m_path.AddLine(new Point(m_from_step.Location.X + m_from_step.Width,
                            m_from_step.Location.Y + m_from_step.Height / 2),
                            new Point(m_to_step.Location.X, 
                            m_to_step.Location.Y + m_to_step.Height / 2));

            this.Location = this.PointToClient(Control.MousePosition);
            this.MouseEnter += new EventHandler(onMouseEntered);
            this.MouseLeave += new EventHandler(onMouseLeave);
            this.MouseClick += new MouseEventHandler(onMouseClicked);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillPath(new SolidBrush(this.BackColor), m_path);           
            this.Region = new Region(m_path);
            g.DrawPath(m_pen, m_path);
            this.Invalidate();
        }
     
        private void onMouseEntered(object sender, EventArgs e)
        {
            Console.WriteLine("entered");
            this.m_pen.Color = Color.CornflowerBlue;
            Graphics g = m_canvas.CreateGraphics();
            g.DrawPath(m_pen, m_path);
            this.Invalidate();
            
        }

        private void onMouseLeave(object sender, EventArgs e)
        {
            Console.WriteLine("left");
            this.m_pen.Color = Color.Black;
            Graphics g = m_canvas.CreateGraphics();
            g.DrawPath(m_pen, m_path);
            this.Invalidate();
         
        }

     
        private void onMouseClicked(object sender, MouseEventArgs e)
        {
            ProblemCreatorState.Instance.state.onConstraintClicked();
        }
    
    }
}
