/*
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Database.MySql
{
    class ProblemLoader
    {
        // Families
        private static List<Product> queryFamiliesForProblem(int problemID)
        {
            String cmd = "SELECT * FROM Families WHERE Problem_ID = " + problemID;
            OdbcDataReader dataReader = DBHandler.Instance.queryForElement(cmd);
            List<Product> pList = new List<Product>();

            while (dataReader.Read())
            {
                pList.Add(new Product(dataReader.GetInt32(1), dataReader.GetString(2)));
            }

            return pList;
        }



        // Resource  
        private static List<Resource> queryResourcesForProblem(int problemID)
        {
            String cmd = "SELECT * FROM Resources WHERE Problem_ID = " + problemID;
            OdbcDataReader dataReader = DBHandler.Instance.queryForElement(cmd);
            List<Resource> rList = new List<Resource>();
            while (dataReader.Read())
            {
                rList.Add(new Resource(dataReader.GetInt32(1) , dataReader.GetString(2)));
            }

            return rList;
        }

        // Step 
        private static List<Step> queryStepsForProblemAndFamiliy(int problemID, int familiyID)
        {
            String cmd = "SELECT * FROM Operations JOIN OperationsToFamilies ON Operations.Problem_ID = OperationsToFamilies.Problem_ID AND Operations.Operation_ID = OperationsToFamilies.Operation_ID AND Operations.Problem_ID = " + problemID + " Where Family_ID = " + familiyID;
            OdbcDataReader data = DBHandler.Instance.queryForElement(cmd);
            List<Step> sList = new List<Step>();
            while (data.Read())
            {
                sList.Add(new Step(data.GetInt32(1), data.GetString(2)));
            }

            return sList;
        }

        // Modes
        private static List<Mode> queryModesForProblemAndStep(int problemID, int operationID)
        {
            String cmd = "SELECT * FROM Modes WHERE Problem_ID = " + problemID + " AND Operation_ID = " + operationID;
            OdbcDataReader data = DBHandler.Instance.queryForElement(cmd);
            List<Mode> mList = new List<Mode>();
            while (data.Read())
            {
                mList.Add(new Mode(data.GetInt32(2), data.GetString(3)));
            }

            return mList;
        }

        // Mode Resource Usage
        private static ArrayList queryModeResourceUsage(int problemID, Step s, Mode m, Resource r)
        {
            String cmd = "SELECT * FROM ResourceUsage WHERE Problem_ID = " + problemID + " AND Operation_ID = " + s.Id + " AND Mode_ID = " + m.Id + " AND Resource_ID = " + r.Id;
            OdbcDataReader data = DBHandler.Instance.queryForElement(cmd);
            ArrayList opList = new ArrayList();
            while (data.Read())
            {
                opList.Add(new Operation(data.GetInt32(4), data.GetInt32(5), r));
            }
            return opList;
        }

        // Constraint
        private static Constraint queryPrecedence(int problemID, Product p, Step from, Step to)
        {
            String cmd = "SELECT * FROM Precedence WHERE Problem_ID = " + problemID + " AND Family_ID = " + p.Id + " AND Previous_Operation_ID = " + from.Id + " AND Subsequent_Operation_ID = " + to.Id;
            OdbcDataReader data = DBHandler.Instance.queryForElement(cmd);
            if (data.Read())
                return new Constraint(p, from, to, data.GetInt32(4), data.GetInt32(5));
            return null;
        }


        // Jobs
        private static List<Job> queryJobsForProblemAndFamiliy(int problemID, Product p)
        {
            String cmd = "SELECT * FROM Jobs WHERE Problem_ID = " + problemID + " AND Family_ID = " + p.Id;
            OdbcDataReader data = DBHandler.Instance.queryForElement(cmd);
            List<Job> jList = new List<Job>();
            while (data.Read())
            {
                jList.Add(new Job(data.GetInt32(2), data.GetInt32(4), data.GetInt32(6)));
            }
            return jList;
        }

        // Resource Constraint
        private static ResourceConstraint queryResourceConstraint(int problemID, Mode from, Mode to , Resource r)
        {
            String cmd = "SELECT * FROM ConstantDelays WHERE Problem_ID = " + problemID + " AND Mode1_ID = " + from.Id + " AND Mode2_ID = " + to.Id + " AND Resource_ID = " + r.Id;
            OdbcDataReader data = DBHandler.Instance.queryForElement(cmd);
            if (data.Read()) 
                return new ResourceConstraint(r,from,to,data.GetInt32(6));
            return null;
        }



        // Problem 
        public static Problem queryProblem(String problemTitle)
        {
            int problemID = problemTitle.GetHashCode();
            List<Resource> rList = queryResourcesForProblem(problemID);
            List<Product> pList = queryFamiliesForProblem(problemID);
            List<Step> sList = new List<Step>();
            Dictionary<Product, List<Job>> pjDic = new Dictionary<Product, List<Job>>();
            List<Constraint> cList = new List<Constraint>();
            List<ResourceConstraint> rcList = new List<ResourceConstraint>();
            Dictionary<Product, List<Step>> stepInProduct = new Dictionary<Product,List<Step>>();
            foreach (Product f in pList)
            {
                List<Job> jobs = queryJobsForProblemAndFamiliy(problemID, f);
                f.Size = jobs.Count;
                pjDic.Add(f, jobs);
                List<Step> stepForProcuct = queryStepsForProblemAndFamiliy(problemID, f.Id);
                stepInProduct.Add(f, stepForProcuct);
                foreach (Step from in stepForProcuct)
                {
                    sList.Add(from);
                    foreach (Step to in stepForProcuct)
                    {
                        Constraint c = queryPrecedence(problemID, f, from, to);
                        if (c != null)
                            cList.Add(c);
                    }
                }

            }
            Dictionary<Step, List<Mode>> modesInStep = new Dictionary<Step, List<Mode>>();

            foreach (Step s in sList)
            {

                List<Mode> mList = queryModesForProblemAndStep(problemID, s.Id);
                foreach (Mode m in mList)
                {
                    foreach (Resource r in rList)
                    {
                        m.operations.AddRange(queryModeResourceUsage(problemID, s, m, r));

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
                                ResourceConstraint rc = queryResourceConstraint(problemID, m1, m2, r);
                                if (rc != null)
                                    rcList.Add(rc);
                            }
                        }
                    }
                }
            }

            Problem p = new Problem(rList, modesInStep, sList, cList, pList, pjDic, stepInProduct ,rcList, problemTitle);
            return p;
        }


        public static List<String> getProblemList()
        {
            String cmd = "SELECT * FROM Problems";
            OdbcDataReader data = DBHandler.Instance.queryForElement(cmd);
            List<String> prNameList = new List<String>();
            while (data.Read())
            {
                prNameList.Add(data.GetString(1));
            }
            return prNameList;
        }
      
    }
}
*/