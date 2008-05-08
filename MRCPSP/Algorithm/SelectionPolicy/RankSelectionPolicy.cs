using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    class RankSelectionPolicy : SelectionPolicyBase
    {

        public RankSelectionPolicy() : base()
        {
        }

        public override List<Solution> keepOnlySuitedSolutions(List<Solution> parent_solutions,
                                                     List<Solution> child_solutions,
                                                     int populationSize)
        {
            parent_solutions.AddRange(child_solutions);
            parent_solutions.Sort(new SolutionComparer<Solution>());
            List<Solution> temp = parent_solutions.GetRange(0, populationSize);
            return temp;
        }

        public override string ToString()
        {
            return "Rank";
        }
    }
   
}
