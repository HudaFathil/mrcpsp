using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MRCPSP.Util;

namespace MRCPSP.Database.MsSqlServer
{
    class DBHandler
    {
        private DataSet1 m_dataset;
        private SqlConnection m_sqlConn;
        SqlDataAdapter m_dataAdapter;
        private static DBHandler m_dbHandler = null;
        private static String SOLOUTION_ID = "SOLUTION_ID";
        private static String PROBLEM_ID = "PROBLEM_ID";

        private DBHandler()
        {
            m_dataset = new DataSet1();
            m_sqlConn = new SqlConnection("Server="+ConfigUtil.Instance.getStringValue(ConfigConsts.DB_SERVER_NAME)+";Database=MRSCSP;connection timeout=10 ; Trusted_Connection=True;");
            Console.WriteLine(ConfigUtil.Instance.getStringValue(ConfigConsts.DB_SERVER_NAME));
            m_dataAdapter = new SqlDataAdapter();
           // m_sqlConn.Open();
        }

        public static DBHandler Instance
        {
            get {
                if (m_dbHandler == null)
                    m_dbHandler = new DBHandler();
                return m_dbHandler; 
            }
        }

        public void fillDataSet(String keyName, int keyValue, String tableName)
        {
            m_dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM  "+tableName+" WHERE "+keyName+" = " + keyValue);
            m_dataAdapter.SelectCommand.Connection = m_sqlConn;
            m_dataAdapter.Fill(m_dataset, tableName);
        }

        public void loadSolution(int solutionID , int problemID)
        {
            m_dataset.Clear();
            m_sqlConn.Open();
            fillDataSet(PROBLEM_ID, problemID, "Problems");
            fillDataSet(PROBLEM_ID, problemID, "Families");
            fillDataSet(PROBLEM_ID, problemID, "Jobs");
            fillDataSet(PROBLEM_ID, problemID, "Operations");
            fillDataSet(PROBLEM_ID, problemID, "OperationsToFamilies");
            fillDataSet(PROBLEM_ID, problemID, "Modes");
            fillDataSet(PROBLEM_ID, problemID, "Resources");
            fillDataSet(PROBLEM_ID, problemID, "ResourceUsage");
            fillDataSet(PROBLEM_ID, problemID, "FamilyCapacityOnResource");
            fillDataSet(PROBLEM_ID, problemID, "Precedence");
            fillDataSet(PROBLEM_ID, problemID, "LoadingTimes");
            fillDataSet(PROBLEM_ID, problemID, "ConstantDelays");

            fillDataSet(SOLOUTION_ID,solutionID, "StatisticsSolutions");
            fillDataSet(SOLOUTION_ID,solutionID, "Generations");
            fillDataSet(SOLOUTION_ID,solutionID, "BestSolution");

            //int problemID = Convert.ToInt32(DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Problem_ID"]);
          
            
            m_sqlConn.Close();
        }

        public void loadProblemToDataSet(int problemID)
        {
            m_dataset.Clear();
            m_sqlConn.Open();
            fillDataSet(PROBLEM_ID, problemID, "Problems");
            fillDataSet(PROBLEM_ID, problemID, "Families");
            fillDataSet(PROBLEM_ID, problemID, "Jobs");
            fillDataSet(PROBLEM_ID, problemID, "Operations");
            fillDataSet(PROBLEM_ID, problemID, "OperationsToFamilies");
            fillDataSet(PROBLEM_ID, problemID, "Modes");
            fillDataSet(PROBLEM_ID, problemID, "Resources");
            fillDataSet(PROBLEM_ID, problemID, "ResourceUsage");
            fillDataSet(PROBLEM_ID, problemID, "FamilyCapacityOnResource");
            fillDataSet(PROBLEM_ID, problemID, "Precedence");
            fillDataSet(PROBLEM_ID, problemID, "LoadingTimes");
            fillDataSet(PROBLEM_ID, problemID, "ConstantDelays");
            m_sqlConn.Close();
        }

        public List<String> getProblemNameList()
        {
            m_sqlConn.Open();
            String cmd = "SELECT * FROM Problems";
            m_dataAdapter.SelectCommand = new SqlCommand(cmd);
            m_dataAdapter.SelectCommand.Connection = m_sqlConn;
            SqlDataReader data = m_dataAdapter.SelectCommand.ExecuteReader();
            List<String> prNameList = new List<String>();
            while (data.Read())
            {
                prNameList.Add(data.GetString(1));
            }
            m_sqlConn.Close();
            return prNameList;
            
        }

        public List<String> getSolutionNameList(String problemName)
        {
           
            int problemID = queryProblemForProblemID(problemName);
            m_sqlConn.Open();
            String cmd = "SELECT * FROM StatisticsSolutions WHERE PROBLEM_ID=" + problemID;
            m_dataAdapter.SelectCommand = new SqlCommand(cmd);
            m_dataAdapter.SelectCommand.Connection = m_sqlConn;
            SqlDataReader data = m_dataAdapter.SelectCommand.ExecuteReader();
            List<String> solNameList = new List<String>();
            while (data.Read())
            {
                String solutionName = data["SOLUTION_NAME"].ToString();
                char[] c = { ' ','\0','\r','\b' };
                solutionName.TrimEnd(c);
                solNameList.Add(solutionName);
            }
            m_sqlConn.Close();
            return solNameList;

        }


        public int queryProblemForProblemID(String problemTitle)
        {
            m_sqlConn.Open();
            String cmd = "SELECT * FROM Problems";
            m_dataAdapter.SelectCommand = new SqlCommand(cmd);
            m_dataAdapter.SelectCommand.Connection = m_sqlConn;
            SqlDataReader data = m_dataAdapter.SelectCommand.ExecuteReader();
            int value = -1;
            while (data.Read())
            {
                if (data.GetString(1).Equals(problemTitle))
                    value = data.GetInt32(0);
            }
            m_sqlConn.Close();
            return value;

        }

        public void updateDatabase(String cmd , String tableName)
        {
            m_sqlConn.Open();
            m_dataAdapter.SelectCommand = m_sqlConn.CreateCommand();
            m_dataAdapter.InsertCommand = new SqlCommand(cmd);
                    //m_dataAdapter.InsertCommand.CommandType = CommandType.Text;
            m_dataAdapter.InsertCommand.Connection = m_sqlConn;
            m_dataAdapter.Update(m_dataset, tableName);
             
            m_sqlConn.Close();
        }


        public DataSet DataSet
        {
            get { return m_dataset; }
        }
    }
}
