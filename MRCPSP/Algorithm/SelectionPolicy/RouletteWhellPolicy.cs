using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    class RouletteWheelPolicy : SelectionPolicyBase
    {

     

        public RouletteWheelPolicy(double elitisem) : base(elitisem)
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
                if (foundWorstNotZero && parent_solutions[i].scoreFromLindo != 0)
                {
                    int block_ends = last_index + (int)((double)value_of_worst / (parent_solutions[i].scoreFromLindo) * sizeof_worst_block);
                    mapping[new KeyValuePair<int, int>(last_index, block_ends)] = parent_solutions[i];
                    last_index = block_ends;
                } else 
                {
                    mapping[new KeyValuePair<int, int>(last_index, last_index + size_of_zero_block)] = parent_solutions[i];              
                    last_index+= size_of_zero_block;
                }
              
            }
            all_blocks_sizes = last_index;
            List<Solution> elitisem_solutions = new List<Solution>();
            int num_to_keep_in_parent = (int)(populationSize * m_elitisem_ratio);
            if (num_to_keep_in_parent % 2 == 1)
                num_to_keep_in_parent++;
            if (num_to_keep_in_parent == 0)
                num_to_keep_in_parent = 2;
            for (int i = parent_solutions.Count - 1; i >= 0 && num_to_keep_in_parent > 0; i--)
            {
                num_to_keep_in_parent--;
                elitisem_solutions.Add(parent_solutions[i]);
            }
            return getListFromMapping(mapping, all_blocks_sizes, populationSize, elitisem_solutions);
        }
    
    }
}
