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
    public abstract class StateBase
    {
        abstract public void onStepClicked(CanvasEditor c, StepItem s);
        abstract public void onCanvasClicked(CanvasEditor c, MouseEventArgs e);
        abstract public void onStepDoubleClicked(StepItem s);
    }
}
