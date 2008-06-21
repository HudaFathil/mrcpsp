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


        /* create simple problem with:
         * one product, two jobs
         * 2 resources: r1, r2 no batch
         * 2 steps: step1, step2, where step1 is before step2
         * one mode with one operation only foreach step, each step works on a differnet resource
         * minimum makespan = 25
         * */
        public static Problem getSimpleProblem()
        {
            int product_size = 2;
            List<Resource> resource_list = new List<Resource>();
            Resource r1 = new Resource("r1",0);
            Resource r2 = new Resource("r2",0);
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
                jobs.Add(new Job(0, Int32.MaxValue,1));
            }
            jobs_in_product[p] = jobs;
            List<ResourceConstraint> resource_time_constraints = new List<ResourceConstraint>();

            Dictionary<Product, List<Step>> steps_in_product = new Dictionary<Product, List<Step>>();
            steps_in_product.Add(p, new List<Step>());
            steps_in_product[p].Add(s1);
            steps_in_product[p].Add(s2);

            List<SetupTime> setup_time = new List<SetupTime>();

            return new Problem(resource_list, modes_in_step, step_list, constraints_list, products_list, jobs_in_product, steps_in_product, resource_time_constraints,setup_time,"test1");
        }

        public static Problem getIntermediateProblem()
        {
            int product_size = 4;
            List<Resource> resource_list = new List<Resource>();
            Resource r1 = new Resource("r1",0);
            Resource r2 = new Resource("r2",0);
            Resource r3 = new Resource("r3",0);
            Resource r4 = new Resource("r4",0);
            resource_list.Add(r1); resource_list.Add(r2);
            resource_list.Add(r3); resource_list.Add(r4);

            List<Product> products_list = new List<Product>();
            Product p = new Product(1, "p", product_size);
            products_list.Add(p);

            List<Step> step_list = new List<Step>();
            Step s1 = new Step(1, "step1");
            Step s2 = new Step(2, "step2");
            Step s3 = new Step(3, "step3");
            step_list.Add(s1); step_list.Add(s2); step_list.Add(s3);

            Mode m11 = new Mode();
            m11.operations.Add(new Operation(0, 10, r1));
            m11.operations.Add(new Operation(5, 15, r2));
            Mode m12 = new Mode();
            m12.operations.Add(new Operation(0, 10, r3));
            m12.operations.Add(new Operation(5, 15, r4));

            Mode m21 = new Mode();
            m21.operations.Add(new Operation(0, 5, r2));
            m21.operations.Add(new Operation(0, 7, r3));
            Mode m22 = new Mode();
            m22.operations.Add(new Operation(0, 15, r2));
            m22.operations.Add(new Operation(0, 7, r3));

            Mode m31 = new Mode();
            m31.operations.Add(new Operation(0, 12, r1));
            m31.operations.Add(new Operation(0, 6, r3));
            Mode m32 = new Mode();
            m32.operations.Add(new Operation(0, 5, r2));
            m32.operations.Add(new Operation(5, 15, r4));

            Dictionary<Step, List<Mode>> modes_in_step = new Dictionary<Step, List<Mode>>();
            modes_in_step[s1] = new List<Mode>();
            modes_in_step[s1].Add(m11); modes_in_step[s1].Add(m12);

            modes_in_step[s2] = new List<Mode>();
            modes_in_step[s2].Add(m21); modes_in_step[s2].Add(m22);

            modes_in_step[s3] = new List<Mode>();
            modes_in_step[s3].Add(m31); modes_in_step[s3].Add(m32);

            List<Constraint> constraints_list = new List<Constraint>();
            constraints_list.Add(new Constraint(p, s1, s2, 0, double.PositiveInfinity));
            constraints_list.Add(new Constraint(p, s2, s3, 0, double.PositiveInfinity));

            Dictionary<Product, List<Job>> jobs_in_product = new Dictionary<Product, List<Job>>();
            List<Job> jobs = new List<Job>();
            for (int i = 0; i < product_size; i++)
            {
                jobs.Add(new Job(0, Int32.MaxValue,1));
            }
            jobs_in_product[p] = jobs;
            List<ResourceConstraint> resource_time_constraints = new List<ResourceConstraint>();

            Dictionary<Product, List<Step>> steps_in_product = new Dictionary<Product, List<Step>>();
            steps_in_product.Add(p, new List<Step>());
            steps_in_product[p].Add(s1);
            steps_in_product[p].Add(s2);
            steps_in_product[p].Add(s3);
            List<SetupTime> setup_time = new List<SetupTime>();
            return new Problem(resource_list, modes_in_step, step_list, constraints_list, products_list, jobs_in_product, steps_in_product, resource_time_constraints,setup_time, "test2");
        }


    }
}
