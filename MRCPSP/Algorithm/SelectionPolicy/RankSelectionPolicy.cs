using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.Controllers;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    class RankSelectionPolicy : SelectionPolicyBase
    {

        private Random m_rand;
        public RankSelectionPolicy() : base()
        {
            m_rand = new Random();
        }

        public override List<Solution> keepOnlySuitedSolutions(List<Solution> parent_solutions,
                                                     List<Solution> child_solutions,
                                                     int populationSize)
        {
            parent_solutions.AddRange(child_solutions);
            parent_solutions.Sort(new SolutionComparer<Solution>());
            parent_solutions.Reverse();
            int all_blocks_sizes = (1 + parent_solutions.Count) * parent_solutions.Count / 2; 
            int size_of_block = 1;
            int last_index = 0;
            Dictionary<KeyValuePair<int, int>, Solution> mapping = new Dictionary<KeyValuePair<int, int>, Solution>();

            for (int i = 0; i < parent_solutions.Count; i++)
            {
                mapping[new KeyValuePair<int, int>(last_index, last_index + size_of_block)] = parent_solutions[i];
                last_index = last_index + size_of_block;
                size_of_block++;
            }

            List<Solution> return_list = new List<Solution>();
            int found_indexes = 0;
            while (found_indexes < populationSize)
            {
                int indx = m_rand.Next(all_blocks_sizes);
                foreach (KeyValuePair<int, int> key in mapping.Keys)
                {
                    if (key.Key <= indx && indx <= key.Value)
                    {
                        if (!return_list.Contains(mapping[key]))
                        {
                            return_list.Add(mapping[key]);
                            found_indexes++;
                        }
                        break;
                    }
                }
            }
            return return_list;
        }

        public override string ToString()
        {
            return "Rank";
        }
    }
   
}
