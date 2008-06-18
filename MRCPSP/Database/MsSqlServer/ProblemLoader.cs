using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Database.MsSqlServer
{
    class ProblemLoader
    {
        // Families
        private static List<Product> queryFamiliesForProblem()
        {
            List<Product> pList = new List<Product>();

            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Families"].Rows)
            {
                int id = Convert.ToInt32(dr["Family_ID"].ToString());
                String name = dr["Name"].ToString();
                if (name.Equals(""))
                    name = "Product "+id;
                pList.Add(new Product( id,name ));
            }

            return pList;
        }


        // Resource  
        private static List<Resource> queryResourcesForProblem()
        {
            List<Resource> rList = new List<Resource>();
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Resources"].Rows)
            {
                int id = Convert.ToInt32(dr["Resource_ID"].ToString()) ;
                String name = dr["Name"].ToString();
                if (name.Equals(""))
                    name = "Resource " + id;
                rList.Add(new Resource(id , name));
            }

            return rList;
        }

        // Step 
        private static List<Step> querySteps()
        {

            List<Step> aList = new List<Step>();
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Operations"].Rows)
            {
                int id = Convert.ToInt32(dr["Operation_ID"].ToString());
                String name = dr["Name"].ToString();
                if (name.Equals(""))
                    name = "Step " + id;
                aList.Add(new Step(id , name));
            }

            return aList;
        }

        // Modes
        private static List<Mode> queryModesForProblemAndStep(int operationID)
        {
            List<Mode> mList = new List<Mode>();
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Modes"].Rows)
            {
                if (Convert.ToInt32(dr["Operation_ID"].ToString()) == operationID)
                {
                    int id = Convert.ToInt32(dr["Mode_ID"].ToString());
                    String name = dr["Name"].ToString();
                    if (name.Equals(""))
                        name = "Mode " + id;
                    mList.Add(new Mode(id,name));
                }
            }

            return mList;
        }

        // Mode Resource Usage
        private static ArrayList queryModeResourceUsage(Step s, Mode m, Resource r)
        {
            ArrayList opList = new ArrayList();
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["ResourceUsage"].Rows)
            {
                if (Convert.ToInt32(dr["Operation_ID"].ToString()) == s.Id &&
                    Convert.ToInt32(dr["Mode_ID"].ToString()) == m.IdPerStep &&
                    Convert.ToInt32(dr["Resource_ID"].ToString()) == r.Id)
                opList.Add(new Operation(Convert.ToInt32(dr["Ts"].ToString()), Convert.ToInt32(dr["Tf"].ToString()), r));
            }
            return opList;
        }

        // Constraint
        private static MRCPSP.CommonTypes.Constraint queryPrecedence(Product p, Step from, Step to)
        {
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Precedence"].Rows)
            {
                if (Convert.ToInt32(dr["Family_ID"].ToString()) == p.Id &&
                    Convert.ToInt32(dr["Previous_Operation_ID"].ToString()) == from.Id &&
                    Convert.ToInt32(dr["Subsequent_Operation_ID"].ToString()) == to.Id)
                        return new MRCPSP.CommonTypes.Constraint(p, from, to, Convert.ToDouble(dr["MinLfi1i2"].ToString()), Convert.ToDouble(dr["MaxLfi1i2"].ToString()));
            }
            return null;
        }


        // Jobs
        private static List<Job> queryJobsForProblemAndFamiliy(Product p)
        {
            List<Job> jList = new List<Job>();
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Jobs"].Rows)
            {
                if (Convert.ToInt32(dr["Family_ID"].ToString()) == p.Id) {
                    int startTime = Convert.ToInt32(dr["Release_Date"].ToString());
                    int finishTime = Convert.ToInt32(dr["Due_Date"].ToString());
                    if (finishTime == 0)
                        finishTime = Int32.MaxValue;
                    jList.Add(new Job(startTime ,finishTime ));
                }
            }
            return jList;
        }

        // Resource Constraint
        private static ResourceConstraint queryResourceConstraint(Mode from, Mode to, Resource r)
        {
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["ConstantDelays"].Rows)
            {
                if (Convert.ToInt32(dr["Mode1_ID"].ToString()) == from.Id &&
                    Convert.ToInt32(dr["Mode2_ID"].ToString()) == to.Id &&
                    Convert.ToInt32(dr["Resource_ID"].ToString()) == to.Id)
                    return new ResourceConstraint(r, from, to, Convert.ToInt32(dr["di1m1i2m2r"].ToString()));
            }
            return null;
        }

        private static bool isStepBelongToFamily(Step s  , Product p)
        {
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["OperationsToFamilies"].Rows)
            {
                if (Convert.ToInt32(dr["Operation_ID"].ToString()) == s.Id &&
                    Convert.ToInt32(dr["Family_ID"].ToString()) == p.Id)
                    return true;
            }
            return false;
        }
        
        // Problem 
        public static Problem queryProblem(int problemID)
        {
            DBHandler.Instance.loadProblem(problemID);
            //int problemID = problemTitle.GetHashCode();
            List<Resource> rList = queryResourcesForProblem();
            List<Product> pList = queryFamiliesForProblem();
            List<Step> sList = querySteps();
            Dictionary<Product, List<Job>> pjDic = new Dictionary<Product, List<Job>>();
            List<MRCPSP.CommonTypes.Constraint> cList = new List<MRCPSP.CommonTypes.Constraint>();
            List<ResourceConstraint> rcList = new List<ResourceConstraint>();
            Dictionary<Product, List<Step>> stepInProduct = new Dictionary<Product, List<Step>>();

            foreach (Product f in pList)
            {
                List<Job> jobs = queryJobsForProblemAndFamiliy(f);
                f.Size = jobs.Count;
                pjDic.Add(f, jobs);
                stepInProduct.Add(f, new List<Step>());
                foreach (Step s1 in sList)
                {
                    if (isStepBelongToFamily(s1,f))
                        stepInProduct[f].Add(s1);
                    foreach (Step s2 in sList)
                    {
                        MRCPSP.CommonTypes.Constraint c = queryPrecedence( f, s1, s2);
                        if (c != null)
                            cList.Add(c);
                    }
                }

            }
            Dictionary<Step, List<Mode>> modesInStep = new Dictionary<Step, List<Mode>>();

            foreach (Step s in sList)
            {

                List<Mode> mList = queryModesForProblemAndStep(s.Id);
                foreach (Mode m in mList)
                {
                    foreach (Resource r in rList)
                    {
                        m.operations.AddRange(queryModeResourceUsage(s, m, r));

                    }
                }
                modesInStep.Add(s, mList);
                Console.WriteLine("Got Step  'Step " + s.Id + "' from database");
            }

            foreach (Resource r in rList)
            {
                foreach (Step s1 in sList)
                {
                    foreach (Mode m1 in modesInStep[s1])
                    {
                        foreach (Step s2 in sList)
                        {
                            foreach (Mode m2 in modesInStep[s2])
                            {
                                ResourceConstraint rc = queryResourceConstraint( m1, m2, r);
                                if (rc != null)
                                    rcList.Add(rc);
                            }
                        }
                    }
                }
            }

            Problem p = new Problem(rList, modesInStep, sList, cList, pList, pjDic,stepInProduct, rcList, DBHandler.Instance.DataSet.Tables["Problems"].Rows[0]["Description"].ToString());
            return p;    
        }
     
        
    }
}
