using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    class RankSelectionPolicy : SelectionPolicyBase
    {


        public RankSelectionPolicy(double elitisem) : base(elitisem)
        {
        }

        public override List<Solution> keepOnlySuitedSolutions(List<Solution> parent_solutions,
                                                     List<Solution> child_solutions,
                                                     int populationSize)
        {
            parent_solutions.AddRange(child_solutions);
            parent_solutions.Sort(new SolutionComparer<Solution>());
            parent_solutions.Reverse();
            int size_of_block = 1;
            int last_index = 0;
            int all_blocks_sizes = 0;

            Dictionary<KeyValuePair<int, int>, Solution> mapping = new Dictionary<KeyValuePair<int, int>, Solution>();

            for (int i = 0; i < parent_solutions.Count; i++)
            {
                mapping[new KeyValuePair<int, int>(last_index, last_index + size_of_block)] = parent_solutions[i];
                last_index = last_index + size_of_block;
                size_of_block++;
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
                elitisem_solutions.Add(parent_solutions[i]);
                num_to_keep_in_parent--;
            }
            return getListFromMapping(mapping, all_blocks_sizes, populationSize, elitisem_solutions);
        }

        public override string ToString()
        {
            return "Rank";
        }
    }
   
}
