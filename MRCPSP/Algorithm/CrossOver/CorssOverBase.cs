﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Util;

namespace MRCPSP.Algorithm.CrossOver
{
    abstract class CorssOverBase
    {
        public abstract List<Solution> doCrossOver(List<Solution> solutions);

        public List<KeyValuePair<Solution, Solution>> createPairsForCrossOver(List<Solution> solutions)
        {
            int n = solutions.Count;
            int[] permutation  = CommonFunctions.Instance.createPermutation(n);
            List<KeyValuePair<Solution, Solution>> list_of_pairs = new List<KeyValuePair<Solution, Solution>>();
            for (int i=0; i < n - 1; i+=2) {
                list_of_pairs.Add(new KeyValuePair<Solution,Solution>(solutions[permutation[i] - 1], solutions[permutation[i + 1] - 1]));
            }
            return list_of_pairs;
        }
    }

}
