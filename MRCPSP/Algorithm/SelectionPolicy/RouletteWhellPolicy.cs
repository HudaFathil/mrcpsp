using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    class RouletteWheelPolicy : SelectionPolicyBase
    {

     

        public RouletteWheelPolicy(int percent) : base()
        {
            
        }

        public override List<Solution> keepOnlySuitedSolutions(List<Solution> parent_solutions,
                                                     List<Solution> child_solutions,
                                                     int populationSize)
        {
            parent_solutions.AddRange(child_solutions);
            parent_solutions.Sort(new SolutionComparer<Solution>());
            parent_solutions.Reverse();
            int all_blocks_sizes = 0;//1 + parent_solutions.Count) * parent_solutions.Count / 2;
            int size_of_zero_block = 1;
            int sizeof_worst_block = 10;
            int value_of_worst = 1;
            int last_index = 0;
            Dictionary<KeyValuePair<int, int>, Solution> mapping = new Dictionary<KeyValuePair<int, int>, Solution>();
            bool foundWorstNotZero = false;
            for (int i = 0; i < parent_solutions.Count; i++)
            {
                if (!foundWorstNotZero && parent_solutions[i].scoreFromLindo != 0)
                {
                    foundWorstNotZero = true;
                    value_of_worst = (int)parent_solutions[i].scoreFromLindo;

                }
                if (foundWorstNotZero)
                {
                    int block_ends = last_index + ((value_of_worst / (int)parent_solutions[i].scoreFromLindo) * sizeof_worst_block);
                    mapping[new KeyValuePair<int, int>(last_index, block_ends)] = parent_solutions[i];
                    all_blocks_sizes = block_ends;
                    last_index = block_ends;
                } else 
                {
                    mapping[new KeyValuePair<int, int>(last_index, last_index + size_of_zero_block)] = parent_solutions[i];              
                    last_index+= size_of_zero_block;
                }
              
            }

            return getListFromMapping(mapping, all_blocks_sizes, populationSize);
        }
    
    }
}
