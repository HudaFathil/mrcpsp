using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    abstract class SelectionPolicyBase
    {
        public abstract List<Solution> keepOnlySuitedSolutions(List<Solution> parent_solution, List<Solution> child_solutions, int populationSize);
    }
}
