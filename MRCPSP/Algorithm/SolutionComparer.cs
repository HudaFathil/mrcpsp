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
            
            return (int) x.resultFromLindo - (int)y.resultFromLindo ;
            
        }
    }
}
