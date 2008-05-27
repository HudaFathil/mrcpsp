using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
using MRCPSP.Controllers;
using MRCPSP.CommonTypes;
namespace MRCPSP.Tests
{
    class ProblemFactory
    {

        public static Problem getSimpleProblem()
        {
            int product_size = 2;
            List<Resource> resource_list = new List<Resource>();
            Resource r1 = new Resource("r1");
            Resource r2 = new Resource("r2");
            resource_list.Add(r1); resource_list.Add(r2);

            List<Product> products_list = new List<Product>();
            Product p = new Product(1,"p",product_size);
            products_list.Add(p);

            List<Step> step_list = new List<Step>();
            Step s1 = new Step(1,"step1");
            Step s2 = new Step(2,"step2");
            step_list.Add(s1); step_list.Add(s2);

            Mode m1 = new Mode();
            m1.operations.Add(new Operation(0,10,r1));

            Mode m2 = new Mode();
            m2.operations.Add(new Operation(0,5,r2));

            Dictionary<Step, List<Mode>> modes_in_step = new Dictionary<Step,List<Mode>>();
            modes_in_step[s1] = new List<Mode>();
            modes_in_step[s1].Add(m1);

            modes_in_step[s2] = new List<Mode>();
            modes_in_step[s2].Add(m2);

            List<Constraint> constraints_list = new List<Constraint>();
            constraints_list.Add(new Constraint(p, s1, s2,0, double.PositiveInfinity));

            Dictionary<Product, List<Job>> jobs_in_product = new Dictionary<Product,List<Job>>();
            List<Job> jobs = new List<Job>();
            for (int i=0; i < product_size; i++) {
                jobs.Add(new Job(0, double.PositiveInfinity));
            }
            jobs_in_product[p] = jobs;
            return new Problem(resource_list, modes_in_step, step_list, constraints_list, products_list, jobs_in_product);
        }
    }
}
