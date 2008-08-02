using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MRCPSP.Database.MsSqlServer
{
    class DBHandler
    {
        private DataSet1 m_dataset;
        private SqlConnection m_sqlConn;
        SqlDataAdapter m_dataAdapter;
        private static DBHandler m_dbHandler = null;

        private DBHandler()
        {
            m_dataset = new DataSet1();
            m_sqlConn = new SqlConnection("Server=PC\\SQLEXPRESS;Database=MRSCSP;connection timeout=10 ; Trusted_Connection=True;");
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

        public void fillDataSet(int problemID, String tableName)
        {
            m_dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM  "+tableName+" WHERE Problem_ID = " + problemID);
            m_dataAdapter.SelectCommand.Connection = m_sqlConn;
            m_dataAdapter.Fill(m_dataset, tableName);
        }

        public void loadSolution(int solutionID)
        {
            m_dataset.Clear();
            m_sqlConn.Open();
            fillDataSet(solutionID, "StatisticsSolutions");
            fillDataSet(solutionID, "Generations");
            fillDataSet(solutionID, "BestSolution");

            int problemID = Convert.ToInt32(DBHandler.Instance.DataSet.Tables["StatisticsSolutions"].Rows[0]["Problem_ID"]);
            fillDataSet(problemID, "Problems");
            fillDataSet(problemID, "Families");
            fillDataSet(problemID, "Jobs");
            fillDataSet(problemID, "Operations");
            fillDataSet(problemID, "OperationsToFamilies");
            fillDataSet(problemID, "Modes");
            fillDataSet(problemID, "Resources");
            fillDataSet(problemID, "ResourceUsage");
            fillDataSet(problemID, "FamilyCapacityOnResource");
            fillDataSet(problemID, "Precedence");
            fillDataSet(problemID, "LoadingTimes");
            fillDataSet(problemID, "ConstantDelays");
            
            m_sqlConn.Close();
        }

        public void loadProblem(int problemID)
        {
            m_dataset.Clear();
            m_sqlConn.Open();
            fillDataSet(problemID, "Problems");
            fillDataSet(problemID, "Families");
            fillDataSet(problemID, "Jobs");
            fillDataSet(problemID, "Operations");
            fillDataSet(problemID, "OperationsToFamilies");
            fillDataSet(problemID, "Modes");
            fillDataSet(problemID, "Resources");
            fillDataSet(problemID, "ResourceUsage");
            fillDataSet(problemID, "FamilyCapacityOnResource");
            fillDataSet(problemID, "Precedence");
            fillDataSet(problemID, "LoadingTimes");
            fillDataSet(problemID, "ConstantDelays");
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
