using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Database.MsSqlServer
{
    class ProblemSaver
    {
        // Families
        private static void saveFamily(int problemID,Product p)
        {
            String cmdStr = "INSERT INTO Families VALUES(" + problemID + "," + p.Id + ",'" + p.Name + "','')";
            Object [] values = {problemID , p.Id , p.Name};
            DBHandler.Instance.DataSet.Tables["Families"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr, "Families");
        }

        // Resource  
        private static void saveResource(int problemID, Resource res)
        {
            String cmdStr = "INSERT INTO Resources VALUES(" + problemID + "," + res.Id + ",'" + res.Name + "'," + res.Capacity + "," + res.ArriveTime + ")";
            Object[] values = { problemID, res.Id, res.Name, 1, 1 };
            DBHandler.Instance.DataSet.Tables["Resources"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr, "Resources");
        }

        // Step 
        private static void saveStep(int problemID, Step s, Product p)
        {
            String cmdStr1 = "INSERT INTO Operations VALUES(" + problemID + "," + s.Id + ",'Step " + s.Id + "','')";
            Object [] values1 = {problemID , s.Id , "Step " + s.Id};
            DBHandler.Instance.DataSet.Tables["Operations"].Rows.Add(values1);
            DBHandler.Instance.updateDatabase(cmdStr1, "Operations");

            String cmdStr2 = "INSERT INTO OperationsToFamilies VALUES(" + problemID + "," + p.Id + "," + s.Id + ")";
            Object[] values2 = { problemID, p.Id, s.Id };
            DBHandler.Instance.DataSet.Tables["OperationsToFamilies"].Rows.Add(values2);
            DBHandler.Instance.updateDatabase(cmdStr2, "OperationsToFamilies");
        }

        // Modes
        private static void saveMode(int problemID, Mode m)
        {
            String cmdStr = "INSERT INTO Modes VALUES(" + problemID + "," + m.BelongToStep.Id + "," + m.IdPerStep + ",'" + m.Name + "','')";
            Object [] values = {problemID , m.BelongToStep.Id , m.IdPerStep,m.Name , ""};
            DBHandler.Instance.DataSet.Tables["Modes"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr , "Modes");
            
        }

        // Mode Resource Usage
        private static void saveResourceUsage(int problemID, Mode m, Operation op)
        {
            String cmdStr = "INSERT INTO ResourceUsage VALUES(" + problemID + "," + m.BelongToStep.Id + "," + m.IdPerStep + "," + op.Rseource.Id + "," + op.StartTime + "," + op.EndTime + ")";
            Object [] values = {problemID , m.BelongToStep.Id , m.IdPerStep , op.Rseource.Id ,  op.StartTime,  op.EndTime};
            DBHandler.Instance.DataSet.Tables["ResourceUsage"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr ,"ResourceUsage");
        }

        // Constraint
        private static void saveConstraint(int problemID, MRCPSP.CommonTypes.Constraint c)
        {
            String cmdStr = "INSERT INTO Precedence VALUES(" + problemID + "," + c.Product.Id + "," + c.StepFrom.Id + "," + c.StepTo.Id + "," + c.MinQueueTime + "," + c.MaxQueueTime + ")";
            Object [] values = {problemID , c.Product.Id , c.StepFrom.Id , c.StepTo.Id , c.MinQueueTime , c.MaxQueueTime};
            DBHandler.Instance.DataSet.Tables["Precedence"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr , "Precedence");
        }

        // Jobs
        private static void saveJob(int problemID, Product p, Job j)
        {
            String cmdStr = "INSERT INTO Jobs VALUES(" + problemID + "," + p.Id + "," + j.Id + "," + j.Units + "," + j.ArriveTime + "," + j.LatestTermTime + ","+j.Weight+")";
            Object [] values = {problemID , p.Id , j.Id , j.Units , j.ArriveTime ,  j.LatestTermTime , j.Weight};
            DBHandler.Instance.DataSet.Tables["Jobs"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr, "Jobs");
        }

        //Resource Constraint
        private static void saveResourceConstraint(int problemID, ResourceConstraint rc)
        {
            String cmdStr = "INSERT INTO ConstantDelays VALUES(" + problemID + "," + rc.FromMode.BelongToStep.Id + "," + rc.FromMode.Id + "," + rc.ToMode.BelongToStep.Id + "," + rc.ToMode.Id + "," + rc.CurrentResource.Id + "," + rc.DelayTime + ")";
            Object [] values = {problemID , rc.FromMode.BelongToStep.Id , rc.FromMode.Id , rc.ToMode.BelongToStep.Id ,  rc.ToMode.Id , rc.CurrentResource.Id , rc.DelayTime};
            DBHandler.Instance.DataSet.Tables["ConstantDelays"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr, "ConstantDelays");
        }

        //Resource setup time
        private static void saveSetupTime(int problemID, SetupTime st)
        {
            String cmdStr = "INSERT INTO LoadingTimes VALUES(" + problemID + "," + st.Mode.BelongToStep.Id + "," + st.Resource.Id + "," + st.Mode.Id + "," + st.ResourceSetupTime+")";
            Object[] values = { problemID, st.Mode.BelongToStep.Id , st.Resource.Id, st.Mode.Id ,st.ResourceSetupTime };
            DBHandler.Instance.DataSet.Tables["LoadingTimes"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr, "LoadingTimes");
        }




        // Problem 
        public static void saveProblem(Problem pr)
        {
            int problemID = pr.Title.GetHashCode();
            DBHandler.Instance.DataSet.Clear();
            String cmdStr = "INSERT INTO Problems VALUES(" + problemID + ",'" + pr.Title + "',0,0,0,0,0,0,0,0,0,0,0,0,0,0)";
            Object [] values = {problemID,pr.Title,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
            DBHandler.Instance.DataSet.Tables["Problems"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr, "Problems");
            foreach (Resource r in pr.Resources)
                saveResource(problemID,r);

            foreach (Product f in pr.Products)
            {
                saveFamily(problemID, f);
                foreach (Step s in pr.StepsInProduct[f])
                {
                    saveStep(problemID, s, f);
                    foreach (Mode m in pr.ModesInStep[s])
                    {
                        saveMode(problemID, m);
                        foreach (Operation op in m.operations)
                            saveResourceUsage(problemID, m, op);
                    }
                }

                foreach (Job j in pr.JobsInProduct[f])
                {
                    saveJob(problemID, f, j);
                }
            }

            foreach (SetupTime st in pr.SetupTimeList)
                saveSetupTime(problemID,st);
            foreach (ResourceConstraint rc in pr.ResourceConstraints)
                saveResourceConstraint(problemID,rc);
            foreach (MRCPSP.CommonTypes.Constraint c in pr.Constraints)
                saveConstraint(problemID,c);
            
        }
        
    }
}
