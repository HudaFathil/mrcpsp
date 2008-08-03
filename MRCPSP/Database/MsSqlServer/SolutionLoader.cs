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

        public static void loadSolution(int solutionID)
        {
            DBHandler.Instance.loadSolution(solutionID);
            
            ResultSummary rs = new ResultSummary();
            rs.CrossoverType = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Crossover_Type"].ToString() ;
            rs.SelectionType = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Seletion_Type"].ToString();
            rs.SizeOfPopulation = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Population_Size"].ToString();
            rs.MutationPercent = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Mutation_precent"].ToString();
            rs.StartTime = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Start_Time"].ToString();
            rs.FinishTime = DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Finish_Time"].ToString();
            rs.BestSolutions.Add(new Solution());
            foreach (Product f in ApplicManager.Instance.CurrentProblem.Products)
            {
                foreach (Step s in ApplicManager.Instance.CurrentProblem.StepsInProduct[f])
                {
                    foreach (Resource r in ApplicManager.Instance.CurrentProblem.Resources)
                    {
                        rs.BestSolutions[0].resultFromLindo.Add(r, new List<ResultParameter>());
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
                                rs.BestSolutions[0].resultFromLindo[r].Add(rp);
                            }
                        }
                    }
                }
            }
                
            foreach (DataRow dr in DBHandler.Instance.DataSet.Tables["Generations"].Rows)
            {
                double min = Convert.ToDouble(dr["Generation_Min_Score"].ToString());
                double max = Convert.ToDouble(dr["Generation_Max_Score"].ToString());
                rs.MinMaxPerGeneration.Add(new KeyValuePair<double,double>(min,max));

            }


        }
    }
}
