using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Controllers;
using MRCPSP.Algorithm;

namespace MRCPSP.Database.MsSqlServer
{
    class SolutionLoader
    {

        public static void loadSolution(int solutionID, int problemID)
        {
            DBHandler.Instance.loadSolution(solutionID,problemID);
            ProblemLoader.loadProblemToMemory();
            ResultSummary rs = new ResultSummary();
            rs.CrossoverType = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Crossover_Type"].ToString() ;
            rs.SelectionType = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Selection_Type"].ToString();
            rs.SizeOfPopulation = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Population_Size"].ToString();
            rs.MutationPercent = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Mutation_precent"].ToString();
            rs.StartTime = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Start_Time"].ToString();
            rs.FinishTime = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Finish_Time"].ToString();
            rs.NumberOfIterations = Convert.ToInt32(DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Iteration_Number"].ToString());
            rs.GeneratePopulationType = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["First_Population"].ToString();
            rs.Title = DBHandler.Instance.DataSet.Tables["Problems"].Rows[0]["Description"].ToString();
           
            Solution newSolution = new Solution(true);
            newSolution.resultFromLindo = new Dictionary<Resource, List<ResultParameter>>();
            foreach (Product f in ApplicManager.Instance.CurrentProblem.Products)
            {
                foreach (Step s in ApplicManager.Instance.CurrentProblem.StepsInProduct[f])
                {
                    foreach (Resource r in ApplicManager.Instance.CurrentProblem.Resources)
                    {
                        if (! newSolution.resultFromLindo.ContainsKey(r))
                            newSolution.resultFromLindo.Add(r, new List<ResultParameter>());
                        foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["BestSolution"].Rows)
                        {
                            if (Convert.ToInt32(dr["Resource_ID"].ToString()) == r.Id &&
                                Convert.ToInt32(dr["Family_ID"].ToString()) == f.Id &&
                                Convert.ToInt32(dr["Step_ID"].ToString()) == s.Id)
                            {
                                ResultParameter rp = new ResultParameter();
                                rp.product = f;
                                rp.step = s;
                                rp.jobID = Convert.ToInt32(dr["Job_ID"].ToString());
                                rp.finishTime = Convert.ToDouble(dr["Finish_Time"].ToString());
                                rp.startTime = Convert.ToDouble(dr["Start_Time"].ToString());
                                newSolution.resultFromLindo[r].Add(rp);
                            }
                        }
                    }
                }
            }
            int numOfGenerations = DBHandler.Instance.DataSet.Tables["Generations"].Rows.Count;
            rs.NumOfGeneration = numOfGenerations+"";
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Generations"].Rows)
            {
                double min = Convert.ToDouble(dr["Generation_Min_Score"].ToString());
                double max = Convert.ToDouble(dr["Generation_Max_Score"].ToString());
                rs.MinMaxPerGeneration.Add(new KeyValuePair<double,double>(min,max));

            }
            rs.BestSolutions.Add(newSolution);
            newSolution.scoreFromLindo = Convert.ToDouble(DBHandler.Instance.DataSet.Tables["Generations"].Rows[numOfGenerations - 1]["Generation_Min_Score"].ToString());
            ApplicManager.Instance.SavedResults.Add(rs);
        }
    }
}
