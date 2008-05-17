using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
using MRCPSP.CommonTypes;

namespace MRCPSP.Algorithm
{
    class MatrixCell
    {
        public Product product;
        public int jobId;
        public Step step;
        public Mode mode;

        public MatrixCell(Product p, int job, Step s, Mode m)
        {
            product = p;
            jobId = job;
            step = s;
            mode = m;
        }

        public MatrixCell(MatrixCell toCopy)
        {
            product = toCopy.product; 
            jobId = toCopy.jobId;
            step = toCopy.step;
            mode = toCopy.mode;
        }
    }
}
