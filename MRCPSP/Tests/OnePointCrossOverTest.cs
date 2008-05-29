using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Algorithm.CrossOver;
using MRCPSP.Algorithm.FirstGeneration;
using MRCPSP.Algorithm;
using MRCPSP.Controllers;
using MRCPSP.Domain;

using NUnit.Framework;

namespace MRCPSP.Tests
{
    [TestFixture]
    public class OnePointCrossOverTest 
    {
        private Problem m_problem;
        private Solution m_sol1;
        private Solution m_sol2;


        [SetUp]
        public void init()
        {
            m_problem = ProblemFactory.getSimpleProblem();
            ApplicManager.Instance.CurrentProblem = m_problem;
            GeneratePolicyBase generate = new GenerateRandomPopulation();
            m_sol1 = new Solution();
            generate.GenerateSolution(m_sol1);
            m_sol2 = new Solution();
            generate.GenerateSolution(m_sol2);
        }

        [Test]
        public void testCrossOver()
        {
            List<Solution> parents = new List<Solution>();
            parents.Add(m_sol1); parents.Add(m_sol2);
            OnePointCrossOver cross = new OnePointCrossOver();
            List<Solution> children = cross.doCrossOver(parents);
           
            for (int i = 0; i < cross.CrossPoint; i++)
            {
                Assert.AreEqual(children[0].SelectedModeList[i], m_sol1.SelectedModeList[i]);
                Assert.AreEqual(children[1].SelectedModeList[i], m_sol2.SelectedModeList[i]);               
                for (int j = 0; j < ApplicManager.Instance.CurrentProblem.getNumberOfResources(); j++)
                {
                    Assert.AreEqual(children[0].DistributionMatrix[j, i], m_sol1.DistributionMatrix[j, i]);
                    Assert.AreEqual(children[1].DistributionMatrix[j, i], m_sol2.DistributionMatrix[j, i]);
                }
            }
            for (int i = cross.CrossPoint; i < ApplicManager.Instance.CurrentProblem.getTotalDistributionSize(); i++)
            {
                Assert.AreEqual(children[1].SelectedModeList[i], m_sol1.SelectedModeList[i]);
                Assert.AreEqual(children[0].SelectedModeList[i], m_sol2.SelectedModeList[i]);
                // need to check rest
            }
         
        }

    }
}

 