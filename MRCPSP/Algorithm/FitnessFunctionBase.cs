using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Algorithm
{
    abstract class FitnessFunctionBase
    {
        abstract public void evalFitness(Solution solution, Problem problem);
    }
}
