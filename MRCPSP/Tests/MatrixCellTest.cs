using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.CommonTypes;
using MRCPSP.Controllers;
using MRCPSP.Domain;
using MRCPSP.Algorithm;

using NUnit.Framework;

namespace MRCPSP.Tests
{
    [TestFixture]
    public class MatrixCellTest
    {

        private Problem problem;
        private MatrixCell mc1;
        private MatrixCell mc2;
        private MatrixCell mc3;
        private MatrixCell mc4;
        private MatrixCellComparer<MatrixCell> compare;

        [SetUp]
        public void init()
        {
            problem = ProblemFactory.getIntermediateProblem();
            ApplicManager.Instance.CurrentProblem = problem;
            mc1 = new MatrixCell(problem.Products[0], 1, problem.Steps[0]);
            mc2 = new MatrixCell(problem.Products[0], 1, problem.Steps[1]);
            mc3 = new MatrixCell(problem.Products[0], 2, problem.Steps[0]);
            mc4 = new MatrixCell(problem.Products[0], 2, problem.Steps[2]);
            compare = new MatrixCellComparer<MatrixCell>();
        }

        [Test]
        public void testMatrixCells()
        {
            Assert.AreEqual(compare.Compare(mc1, mc2), -1);
            Assert.AreEqual(compare.Compare(mc1, mc3), 0);
            Assert.AreEqual(compare.Compare(mc2, mc3), 0);
            Assert.AreEqual(compare.Compare(mc4, mc3), 1);
        }

        [Test]
        public void testSorting()
        {
            List<MatrixCell> before_sort = new List<MatrixCell>();
            before_sort.Add(mc3);
            before_sort.Add(mc2);
            before_sort.Add(mc4);
            before_sort.Add(mc1);

            before_sort.Sort(compare);
            bool option1 = (before_sort[0] == mc3) && (before_sort[1] == mc1) && (before_sort[2] == mc2) && (before_sort[3] == mc4);
            bool option2 = (before_sort[0] == mc3) && (before_sort[1] == mc1) && (before_sort[3] == mc2) && (before_sort[2] == mc4);
            bool option3 = (before_sort[0] == mc3) && (before_sort[2] == mc1) && (before_sort[3] == mc2) && (before_sort[1] == mc4);

            bool option4 = (before_sort[1] == mc3) && (before_sort[0] == mc1) && (before_sort[2] == mc2) && (before_sort[3] == mc4);
            bool option5 = (before_sort[1] == mc3) && (before_sort[0] == mc1) && (before_sort[3] == mc2) && (before_sort[2] == mc4);

            bool option6 = (before_sort[2] == mc3) && (before_sort[0] == mc1) && (before_sort[1] == mc2) && (before_sort[3] == mc4);
            Assert.IsTrue(option1 || option2 || option3 || option4 || option5 || option6);
        }
    }
}
