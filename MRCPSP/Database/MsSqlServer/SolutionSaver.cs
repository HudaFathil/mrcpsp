using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MRCPSP.Domain;
using MRCPSP.CommonTypes;

namespace MRCPSP.Database.MsSqlServer
{
    class SolutionSaver
    {
       

        
        // Solution
        public static void saveSolution (ResultSummary rs)
        {
            String solutionName = rs.getSolutionName();
            int solutionID = solutionName.GetHashCode();
            int problemID = Convert.ToInt32(DBHandler.Instance.DataSet.Tables["Problems"].Rows[0]["Problem_ID"].ToString());
            String cmdStr = "INSERT INTO StatisticsSolutions VALUES(" + solutionID + "," + problemID + "," + rs.SizeOfPopulation + ",'" + rs.StartTime + "','" + rs.FinishTime+"','"+rs.MutationPercent+"','"+solutionName +"','"+rs.SelectionType+"','" + rs.CrossoverType+"')";
            Object[] values = { solutionID , problemID, rs.SizeOfPopulation , rs.StartTime , rs.FinishTime , rs.MutationPercent ,solutionName };
            DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows.Add(values);
            DBHandler.Instance.updateDatabase(cmdStr, "StatisticsSolutions");
  
            for( int i = 0 ; i <rs.MinMaxPerGeneration.Count ; i++)
            {
                cmdStr = "INSERT INTO Generations VALUES(" + solutionID + "," + problemID + ","+i+","+rs.MinMaxPerGeneration[i].Key+","+rs.MinMaxPerGeneration[i].Value+")";
                Object[] values1 = { solutionID, problemID, i , rs.MinMaxPerGeneration[i].Key ,rs.MinMaxPerGeneration[i].Value };
                DBHandler.Instance.DataSet.Tables["Generations"].Rows.Add(values1);
                DBHandler.Instance.updateDatabase(cmdStr, "Generations");
            }
           

            foreach (Resource r in rs.getBestSolution().resultFromLindo.Keys)
            {
                List<ResultParameter> rpl = rs.getBestSolution().resultFromLindo[r];
                foreach (ResultParameter rp in rpl)
                {

                    cmdStr = "INSERT INTO BestSolution VALUES(" + solutionID + "," + problemID + "," + r.Id + "," + rp.product.Id + "," + rp.step.Id + "," + rp.jobID + "," + rp.startTime + "," + rp.finishTime + ")";
                    Object[] values2 = { solutionID ,problemID, r.Id , rp.product.Id ,rp.step.Id , rp.jobID, rp.startTime , rp.finishTime };
                    DBHandler.Instance.DataSet.Tables["BestSolution"].Rows.Add(values2);
                    DBHandler.Instance.updateDatabase(cmdStr, "BestSolution");        
                }
                
            }
            
        }
    }
}
