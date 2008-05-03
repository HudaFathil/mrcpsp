using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Util
{
    class CommonFunctions
    {
        private static CommonFunctions instance = null;
        private Random m_random;

        private CommonFunctions()
        {
            m_random = new Random();
        }

        public static CommonFunctions Instance
        {
            get {
                if (instance == null)
                    instance = new CommonFunctions();
                return instance; 
            }
        }

        public int[] createPermutation(int n) // 1 .. n
        {
          
            System.Collections.ArrayList numbers = new System.Collections.ArrayList();
            int[] permutation = new int[n];

            for (int i = 1; i <= permutation.Length; i++)
            {
                numbers.Add(i);
            }
            for (int i = 0; i < permutation.Length; i++)
            {
                int o = m_random.Next(numbers.Count);
                permutation[i] = (int)numbers[o];
                numbers.RemoveAt(o);
            }
            return permutation;
        }
    }
}
