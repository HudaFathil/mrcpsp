using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    class ElitismPolicy : SelectionPolicyBase
    {

        private double m_percent_to_keep = 0.1;

        public ElitismPolicy(int percent) : base()
        {
            m_percent_to_keep = (double)percent / 100.0;
        }

        public override List<Solution> keepOnlySuitedSolutions(List<Solution> parent_solutions,
                                                     List<Solution> child_solutions,
                                                     int populationSize)
        {
            int num_to_keep_in_parent = (int)((double)parent_solutions.Count * m_percent_to_keep);
            if (num_to_keep_in_parent % 2 == 1)
                num_to_keep_in_parent++;
            if (num_to_keep_in_parent == 0)
                num_to_keep_in_parent = 2;
            parent_solutions.Sort(new SolutionComparer<Solution>());
            List<Solution> temp = parent_solutions.GetRange(0, num_to_keep_in_parent);
            temp.AddRange(child_solutions.GetRange(0, child_solutions.Count - num_to_keep_in_parent));
            return temp;
        }

        public override string ToString()
        {
            return "Elitism";
        }
    }
   
}
