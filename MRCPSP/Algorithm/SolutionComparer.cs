using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Algorithm;

namespace MRCPSP.Algorithm
{
    class SolutionComparer<T> : IComparer<T> where T: Solution
    {
        public SolutionComparer() : base() { }
        public int Compare(T x, T y) 
        {
            if (x.scoreFromLindo == y.scoreFromLindo )
                return 0;
            if (x.scoreFromLindo == 0) 
                return 1;
            if (y.scoreFromLindo == 0)
                return -1;
            if (x.scoreFromLindo <= y.scoreFromLindo)
                return -1;
            else
                return 1;
        }
    }
}
