using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;
using MRCPSP.Controllers;

namespace MRCPSP.Lindo.Constrains
{
    abstract class IConstrain
    {
        private static List<IConstrain> m_constarinList;

        protected IConstrain()
        {   
        }

        public abstract void createConstrain(Solution sol , Problem prob);

        private static void addConstrains()
        {
            m_constarinList = new List<IConstrain>();
            m_constarinList.Add(new ConstrainNo1());
            m_constarinList.Add(new ConstrainNo2());
            m_constarinList.Add(new ConstrainNo3());
            //m_constarinList.Add(new ConstrainNo4());
            m_constarinList.Add(new ConstrainNo10());
            m_constarinList.Add(new ConstrainNo11A());
            m_constarinList.Add(new ConstrainNo11B());
            m_constarinList.Add(new ConstrainNo14());
            m_constarinList.Add(new ConstrainNo17());
            m_constarinList.Add(new ConstrainNo18());
        }

        public static void createAllConstrains () 
        {
            addConstrains();
            foreach (IConstrain c in m_constarinList)
            {
                c.createConstrain(ApplicManager.Instance.CurrentSolution , ApplicManager.Instance.CurrentProblem);
            }
        }

    }
}
