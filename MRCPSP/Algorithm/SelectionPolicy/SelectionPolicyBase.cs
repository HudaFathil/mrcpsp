using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Algorithm.SelectionPolicy
{
    abstract class SelectionPolicyBase
    {
        protected Random m_rand;
        public SelectionPolicyBase() {
            m_rand = new Random();
        }

        public abstract List<Solution> keepOnlySuitedSolutions(List<Solution> parent_solution, List<Solution> child_solutions, int populationSize);

        public List<Solution> getListFromMapping(Dictionary<KeyValuePair<int, int>, Solution> mapping, int all_blocks_sizes, int populationSize)
        {
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
    }
}
