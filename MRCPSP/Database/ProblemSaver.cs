using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;

namespace MRCPSP.Database
{
    class ProblemSaver
    {
        // Families
        private static void saveFamiliy(int problemID,Product p)
        {
            String cmdStr = "INSERT INTO Families VALUES(" + problemID + "," + p.Id + "," + p.Size + ",'" + p.Name + "','')";

            DBHandler.Instance.executeQuery(cmdStr);
        }

        // Resource  
        private static void saveResource(int problemID, Resource res)
        {
            String cmdStr = "INSERT INTO Resources VALUES(" + problemID + "," + res.Id + ",'" + res.Name + "'," + 1 + "," + 1 + ")";
            DBHandler.Instance.executeQuery(cmdStr);
        }

        // Step 
        private static void saveStep(int problemID, Step s, Product p)
        {
            String cmdStr1 = "INSERT INTO Operations VALUES(" + problemID + "," + s.Id + ",'Step " + s.Id + "','')";
            DBHandler.Instance.executeQuery(cmdStr1);
            String cmdStr2 = "INSERT INTO OperationsToFamilies VALUES(" + problemID + "," + s.Id + "," + p.Id + ")";
            DBHandler.Instance.executeQuery(cmdStr2);
        }

        // Modes
        private static void saveMode(int problemID, Mode m)
        {
            String cmdStr = "INSERT INTO Modes VALUES(" + problemID + "," + m.BelongToStep.Id + "," + m.Id + ",'" + m.name + "',1,1)";
            DBHandler.Instance.executeQuery(cmdStr);
        }

        // Mode Resource Usage
        private static void saveResourceUsage(int problemID, Mode m, Operation op)
        {
            String cmdStr = "INSERT INTO ResourceUsage VALUES(" + problemID + "," + m.BelongToStep.Id + "," + m.Id + "," + op.Rseource.Id + "," + op.StartTime + "," + op.EndTime + ")";
            DBHandler.Instance.executeQuery(cmdStr);
        }

        // Constraint
        private static void saveConstraint(int problemID, Constraint c)
        {
            String cmdStr = "INSERT INTO Precedence VALUES(" + problemID + "," + c.Product.Id + "," + c.StepFrom.Id + "," + c.StepTo.Id + "," + c.MinQueueTime + "," + c.MaxQueueTime + ")";
            DBHandler.Instance.executeQuery(cmdStr);
        }

        // Jobs
        private static void saveJob(int problemID, Product p, Job j)
        {
            String cmdStr = "INSERT INTO Jobs VALUES(" + problemID + "," + p.Id + "," + j.Id + "," + j.ArriveTime + "," + 9999 + ",1)";
            DBHandler.Instance.executeQuery(cmdStr);
        }

        //Resource Constraint
        private static void saveResourceConstraint(int problemID, ResourceConstraint rc)
        {
            String cmdStr = "INSERT INTO ConstantDelays VALUES(" + problemID + "," + rc.FromMode.BelongToStep.Id + "," + rc.FromMode.Id + "," + rc.ToMode.BelongToStep.Id + "," + rc.ToMode.Id +","+rc.CurrentResource.Id+","+rc.DelayTime+")";
            DBHandler.Instance.executeQuery(cmdStr);
        }



        // Problem 
        public static void saveProblem(Problem pr)
        {
            int problemID = pr.Title.GetHashCode();
            foreach (Resource r in pr.Resources)
                saveResource(problemID,r);

            foreach (Product f in pr.Products)
            {
                saveFamiliy(problemID, f);
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
            foreach (ResourceConstraint rc in pr.ResourceConstraints)
                saveResourceConstraint(problemID,rc);
            foreach (Constraint c in pr.Constraints)
                saveConstraint(problemID,c);
            String cmdStr = "INSERT INTO Problems VALUES(" + problemID + ",'" + pr.Title + "')";
            DBHandler.Instance.executeQuery(cmdStr);
        }

    }
}
