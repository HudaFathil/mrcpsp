

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;

namespace MRCPSP.Gui.ProblemCreator
{


    public class CanvasEditor : PictureBox
    {

        public CanvasEditor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.BackColor = System.Drawing.Color.White;
            this.ResumeLayout(false);

            this.MouseClick += new MouseEventHandler(this.onMouseClicked);
            this.ClientSize = new System.Drawing.Size(628, 426);
            this.Resize += new System.EventHandler(this.OnResize2);
            this.Paint += new PaintEventHandler(CanvasEditor_Paint);     
        }

        void CanvasEditor_Paint(object sender, PaintEventArgs e)
        {
            foreach (ConstraintItem c in ProblemCreatorState.Instance.getConstraints())
            {
                c.ConstraintItem_Paint(sender, e);
            }
        }

        private void onMouseClicked(object sender, MouseEventArgs e)
        {
            ProblemCreatorState.Instance.state.onCanvasClicked((CanvasEditor)sender, e);
        }
       
        protected void OnResize2(object sender, System.EventArgs e)
        {
            Invalidate();
            foreach (ConstraintItem c in ProblemCreatorState.Instance.getConstraints())
            {
                c.ConstraintItem_Resize(sender, e);
            }
            
        }



    }
}

/*
User Interfaces in C#: Windows Forms and Custom Controls
by Matthew MacDonald

Publisher: Apress
ISBN: 1590590457
*/
/*
using System.Drawing;
using System.Drawing.Drawing2D;

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace MRCPSP.Gui.ProblemCreator
{
    /// <summary>
    /// Summary description for DrawingShapes.
    /// </summary>
    public class CanvasEditor : System.Windows.Forms.Panel
    {
        internal System.Windows.Forms.ContextMenu mnuLabel;
        internal System.Windows.Forms.MenuItem mnuColorChange;
        internal System.Windows.Forms.ContextMenu mnuForm;
        internal System.Windows.Forms.MenuItem mnuRectangle;
        internal System.Windows.Forms.MenuItem mnuEllipse;
        internal System.Windows.Forms.MenuItem mnuTriangle;
        private System.Windows.Forms.MenuItem mnuRemoveShape;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public CanvasEditor()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.mnuLabel = new System.Windows.Forms.ContextMenu();
            this.mnuColorChange = new System.Windows.Forms.MenuItem();
            this.mnuForm = new System.Windows.Forms.ContextMenu();
            this.mnuRectangle = new System.Windows.Forms.MenuItem();
            this.mnuEllipse = new System.Windows.Forms.MenuItem();
            this.mnuTriangle = new System.Windows.Forms.MenuItem();
            this.mnuRemoveShape = new System.Windows.Forms.MenuItem();
            // 
            // mnuLabel
            // 
            this.mnuLabel.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.mnuColorChange,
                                                                                     this.mnuRemoveShape});
            // 
            // mnuColorChange
            // 
            this.mnuColorChange.Index = 0;
            this.mnuColorChange.Text = "Change Color";
            this.mnuColorChange.Click += new System.EventHandler(this.mnuColorChange_Click);
            // 
            // mnuForm
            // 
            this.mnuForm.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.mnuRectangle,
                                                                                    this.mnuEllipse,
                                                                                    this.mnuTriangle});
            // 
            // mnuRectangle
            // 
            this.mnuRectangle.Index = 0;
            this.mnuRectangle.Text = "Create New Rectangle";
            this.mnuRectangle.Click += new System.EventHandler(this.mnuNewShape_Click);
            // 
            // mnuEllipse
            // 
            this.mnuEllipse.Index = 1;
            this.mnuEllipse.Text = "Create New Ellipse";
            this.mnuEllipse.Click += new System.EventHandler(this.mnuNewShape_Click);
            // 
            // mnuTriangle
            // 
            this.mnuTriangle.Index = 2;
            this.mnuTriangle.Text = "Create New Triangle";
            this.mnuTriangle.Click += new System.EventHandler(this.mnuNewShape_Click);
            // 
            // mnuRemoveShape
            // 
            this.mnuRemoveShape.Index = 1;
            this.mnuRemoveShape.Text = "Remove Shape";
            this.mnuRemoveShape.Click += new System.EventHandler(this.mnuRemoveShape_Click);
            // 
            // DrawingShapes
            // 
    //        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(628, 426);
            this.ContextMenu = this.mnuForm;
            this.Name = "DrawingShapes";
            this.Text = "DrawingShapes";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingShapes_MouseDown);

        }



        // Keep track of when fake drag or resize mode is enabled.
        private bool isDragging = false;
        private bool isResizing = false;

        // Store the location where the user clicked on the control.
        private int clickOffsetX, clickOffsetY;

        private void lbl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Retrieve a reference to the active label.
            Control currentCtrl;
            currentCtrl = (Control)sender;

            if (e.Button == MouseButtons.Right)
            {
                // Show the context menu.
                currentCtrl.ContextMenu.Show(currentCtrl, new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Left)
            {
                clickOffsetX = e.X;
                clickOffsetY = e.Y;

                if ((e.X + 5) > currentCtrl.Width || (e.Y + 5) > currentCtrl.Height)
                {
                    // The mouse pointer is in the bottom right corner,
                    // so resizing mode is appropriate.
                    isResizing = true;
                }
                else
                {
                    // The mouse is somewhere else, so dragging mode is
                    // appropriate.
                    isDragging = true;
                }
            }
        }

        private void lbl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Retrieve a reference to the active shape.
            Control currentCtrl;
            currentCtrl = (Control)sender;

            if (isDragging)
            {
                // Move the control.
                currentCtrl.Left = e.X + currentCtrl.Left - clickOffsetX;
                currentCtrl.Top = e.Y + currentCtrl.Top - clickOffsetY;
            }
            else if (isResizing)
            {
                // Resize the control, according to the resize mode.
                if (currentCtrl.Cursor == Cursors.SizeNWSE)
                {
                    currentCtrl.Width = e.X;
                    currentCtrl.Height = e.Y;
                }
                else if (currentCtrl.Cursor == Cursors.SizeNS)
                {
                    currentCtrl.Height = e.Y;
                }
                else if (currentCtrl.Cursor == Cursors.SizeWE)
                {
                    currentCtrl.Width = e.X;
                }
            }
            else
            {
                // Change the cursor if the mouse pointer is on one of the edges
                // of the control.
                if (((e.X + 5) > currentCtrl.Width) &&
                    ((e.Y + 5) > currentCtrl.Height))
                {
                    currentCtrl.Cursor = Cursors.SizeNWSE;
                }
                else if ((e.X + 5) > currentCtrl.Width)
                {
                    currentCtrl.Cursor = Cursors.SizeWE;
                }
                else if ((e.Y + 5) > currentCtrl.Height)
                {
                    currentCtrl.Cursor = Cursors.SizeNS;
                }
                else
                {
                    currentCtrl.Cursor = Cursors.Arrow;
                }
            }

        }
        private void lbl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isDragging = false;
            isResizing = false;
        }

        private void mnuColorChange_Click(object sender, System.EventArgs e)
        {
            // Show color dialog.
            ColorDialog dlgColor = new ColorDialog();
            dlgColor.ShowDialog();

            // Change label background.
            mnuLabel.SourceControl.BackColor = dlgColor.Color;

        }

        private void DrawingShapes_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.ContextMenu.Show(this, new Point(e.X, e.Y));
            }

        }

        private void mnuNewShape_Click(object sender, System.EventArgs e)
        {
            // Create and configure the shape with some defaults.
            Shape newShape = new Shape();
            newShape.Size = new Size(40, 40);
            newShape.ForeColor = Color.Coral;

            // Configure the appropriate shape depending on the menu option selected.
            if (sender == mnuRectangle)
            {
                newShape.Type = Shape.ShapeType.Rectangle;
            }
            else if (sender == mnuEllipse)
            {
                newShape.Type = Shape.ShapeType.Ellipse;
            }
            else if (sender == mnuTriangle)
            {
                newShape.Type = Shape.ShapeType.Triangle;
            }

            // To determine where to place the shape, you need to convert the 
            // current screen-based mouse coordinates into relative form coordinates.
            newShape.Location = this.PointToClient(Control.MousePosition);

            // Attach a context menu to the shape.
            newShape.ContextMenu = mnuLabel;

            // Connect the shape to all its event handlers.
            newShape.MouseDown += new MouseEventHandler(lbl_MouseDown);
            newShape.MouseMove += new MouseEventHandler(lbl_MouseMove);
            newShape.MouseUp += new MouseEventHandler(lbl_MouseUp);

            // Add the shape to the form.
            this.Controls.Add(newShape);
        }

        private void mnuRemoveShape_Click(object sender, System.EventArgs e)
        {
            Shape ctrlShape = (Shape)mnuLabel.SourceControl;
            this.Controls.Remove(ctrlShape);
        }
    }
    public class Shape : System.Windows.Forms.UserControl
    {
        // The types of shapes supported by this control.
        public enum ShapeType
        {
            Rectangle,
            Ellipse,
            Triangle
        }

        private ShapeType shape = ShapeType.Rectangle;
        private GraphicsPath path = null;

        public ShapeType Type
        {
            get
            {
                return shape;
            }
            set
            {
                shape = value;
                RefreshPath();
                this.Invalidate();
            }
        }

        // Create the corresponding GraphicsPath for the shape, and apply
        // it to the control by setting the Region property.
        private void RefreshPath()
        {
            path = new GraphicsPath();
            switch (shape)
            {
                case ShapeType.Rectangle:
                    path.AddRectangle(this.ClientRectangle);
                    break;
                case ShapeType.Ellipse:
                   
                    path.AddPolygon(new Point[] {
                            new Point(this.Width - 10, 0),
                            new Point(0 , 100),
                            new Point(100, 150)});
                    break;
                case ShapeType.Triangle:
                    Point pt1 = new Point(this.Width / 2, 0);
                    Point pt2 = new Point(0, this.Height);
                    Point pt3 = new Point(this.Width, this.Height);
                    path.AddPolygon(new Point[] { pt1, pt2, pt3 });
                    break;
            }
            this.Region = new Region(path);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            RefreshPath();
            this.Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            if (path != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(new SolidBrush(this.BackColor), path);
                e.Graphics.DrawPath(new Pen(this.ForeColor, 4), path);
            }
        }

    }
}
  */         