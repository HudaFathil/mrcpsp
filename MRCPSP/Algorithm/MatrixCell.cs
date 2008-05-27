using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
using MRCPSP.CommonTypes;
using MRCPSP.Controllers;

namespace MRCPSP.Algorithm
{

    class MatrixCellComparer<T> : IComparer<T> where T : MatrixCell
    {
        public MatrixCellComparer() : base() { }
        public int Compare(T x, T y)
        {
            if (x.product != y.product)
                return 0;
            if (x.jobId != y.jobId)
                return 0;
            if (x.step == y.step)
            {
                return 0;
            }
            if (isStepPrecedenceToNewStep(x.product, x.step, y.step))
                return -1;
            else
                return 1;
        }

        internal bool isStepPrecedenceToNewStep(Product p, Step from, Step to)
        {
            return ApplicManager.Instance.CurrentProblem.isStepSubsequentToStep(p, from, to);
        }
    }

    class MatrixCell
    {
        public Product product;
        public int jobId;
        public Step step;

        public MatrixCell(Product p, int job, Step s)
        {
            product = p;
            jobId = job;
            step = s;
        }

        public MatrixCell(MatrixCell toCopy)
        {
            product = toCopy.product; 
            jobId = toCopy.jobId;
            step = toCopy.step;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(MatrixCell))
                return false;
            return ((this.jobId == ((MatrixCell)obj).jobId) && (this.step.Id == ((MatrixCell)obj).step.Id) && (this.product.Id == ((MatrixCell)obj).product.Id));
        }
    }
}
