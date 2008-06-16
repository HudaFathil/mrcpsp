using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using MRCPSP.CommonTypes;
using MRCPSP.Domain;
using MRCPSP.Algorithm;

namespace MRCPSP.Database.MySql
{
    class DBHandler
    {

        private OdbcConnection m_con;
        private static DBHandler m_dbhandler;
        private static String DBName = "mrcpsp";


        private DBHandler()
        {
            string MyConString = "DRIVER={MySQL ODBC 3.51 Driver};" +
                                    "SERVER=localhost;" +
                                    "DATABASE=" + DBName + ";" +
                                    "OPTION=3";

            //Connect to MySQL using MyODBC
            m_con = new OdbcConnection(MyConString);
            m_con.Open();

            Console.WriteLine("\n !!! success, connected successfully !!!\n");

            //Display connection information
            Console.WriteLine("Connection Information:");
            Console.WriteLine("\tConnection String:" + m_con.ConnectionString);
            Console.WriteLine("\tConnection Timeout:" + m_con.ConnectionTimeout);
            Console.WriteLine("\tDatabase:" + m_con.Database);
            Console.WriteLine("\tDataSource:" + m_con.DataSource);
            Console.WriteLine("\tDriver:" + m_con.Driver);
            Console.WriteLine("\tServerVersion:" + m_con.ServerVersion);

          


        }

        public void reconnectToDB(String dbName)
        {
            string MyConString = "DRIVER={MySQL ODBC 3.51 Driver};" +
                                    "SERVER=localhost;" +
                                    "DATABASE=" + dbName + ";" +
                                    "OPTION=3";

            //Connect to MySQL using MyODBC
            m_con = new OdbcConnection(MyConString);
            m_con.Open();

            Console.WriteLine("\n !!! success, connected successfully !!!\n");

            //Display connection information
            Console.WriteLine("Connection Information:");
            Console.WriteLine("\tConnection String:" + m_con.ConnectionString);
            Console.WriteLine("\tConnection Timeout:" + m_con.ConnectionTimeout);
            Console.WriteLine("\tDatabase:" + m_con.Database);
            Console.WriteLine("\tDataSource:" + m_con.DataSource);
            Console.WriteLine("\tDriver:" + m_con.Driver);
            Console.WriteLine("\tServerVersion:" + m_con.ServerVersion);
        }

        public static DBHandler Instance
        {
            get
            {
                if (m_dbhandler == null)
                    m_dbhandler = new DBHandler();
                return m_dbhandler;
            }
        }

        public void executeQuery(String cmdStr)
        {
            OdbcCommand MyCommand = new OdbcCommand(cmdStr, m_con);
            Console.WriteLine("INSERT, Total rows affected:" + MyCommand.ExecuteNonQuery());
        }

        public OdbcDataReader queryForElement(String cmdStr)
        {
            OdbcCommand MyCommand = new OdbcCommand(cmdStr, m_con);
            OdbcDataReader MyDataReader = MyCommand.ExecuteReader();
            return MyDataReader;
        }
/*        
        public void prepareDataSet(int problemID)
        {
            // *** Prepare dataset	
            DataSet1 dataSet = new DataSet1();

            operationsAdapter = new OdbcDataAdapter("SELECT * FROM Operations WHERE Problem_ID = " + problemID, m_con);
            jobsAdapter = new OdbcDataAdapter("SELECT * FROM Jobs WHERE Problem_ID = " + problemID, m_con);
            modesAdapter = new OdbcDataAdapter("SELECT * FROM Modes WHERE Problem_ID = " + problemID, m_con);
            resourceAdapter = new OdbcDataAdapter("SELECT * FROM Resources WHERE Problem_ID = " + problemID, m_con);
            familiesAdapter = new OdbcDataAdapter("SELECT * FROM Families WHERE Problem_ID = " + problemID, m_con);
            operationsToFamiliesAdapter = new OdbcDataAdapter("SELECT * FROM OperationsToFamilies WHERE Problem_ID = " + problemID, m_con);
            resourceUsageAdapter = new OdbcDataAdapter("SELECT * FROM ResourceUsage WHERE Problem_ID = " + problemID, m_con);
            familyCapacityOnResourceAdapter = new OdbcDataAdapter("SELECT * FROM FamilyCapacityOnResource WHERE Problem_ID = " + problemID, m_con);
            precedenceAdapter = new OdbcDataAdapter("SELECT * FROM Precedence WHERE Problem_ID = " + problemID, m_con);
            loadingTimesAdapter = new OdbcDataAdapter("SELECT * FROM LoadingTimes WHERE Problem_ID = " + problemID, m_con);
            constantDelaysAdapter = new OdbcDataAdapter("SELECT * FROM ConstantDelays WHERE Problem_ID = " + problemID, m_con);
            exclusivesAdapter = new OdbcDataAdapter("SELECT * FROM Exclusives WHERE Problem_ID = " + problemID, m_con);
            complementariesAdapter = new OdbcDataAdapter("SELECT * FROM Complementaries WHERE Problem_ID = " + problemID, m_con);
            exclusive_FamiliesAdapter = new OdbcDataAdapter("SELECT * FROM Exclusive_Families WHERE Problem_ID = " + problemID, m_con);
            exclusive_JobsAdapter = new OdbcDataAdapter("SELECT * FROM Exclusive_Jobs WHERE Problem_ID = " + problemID, m_con);
            dataSet.Clear();

            // *** Fill Dataset
            familiesAdapter.Fill(dataSet);
            operationsAdapter.Fill(dataSet);
            jobsAdapter.Fill(dataSet);
            modesAdapter.Fill(dataSet);
            resourceAdapter.Fill(dataSet);
            operationsToFamiliesAdapter.Fill(dataSet);
            resourceUsageAdapter.Fill(dataSet);
            familyCapacityOnResourceAdapter.Fill(dataSet);
            precedenceAdapter.Fill(dataSet);
            loadingTimesAdapter.Fill(dataSet);
            constantDelaysAdapter.Fill(dataSet);
            exclusivesAdapter.Fill(dataSet);
            complementariesAdapter.Fill(dataSet);
            exclusive_FamiliesAdapter.Fill(dataSet);
            exclusive_JobsAdapter.Fill(dataSet);

            DataRelation familiesOperationsToFamiliesRelation = dataSet.Relations["FamiliesOperationsToFamilies"];
            DataRelation operationsModesRelation = dataSet.Relations["OperationsModes"];
            DataRelation familiesJobesRelation = dataSet.Relations["FamiliesJobs"];
            DataRelation resourceResourcesUsageRelation = dataSet.Relations["ResourcesResourceUsage"];
            DataRelation modesResourceUsageRelation = dataSet.Relations["ModesResourceUsage"];
            DataRelation familiesFamilyCapacityOnResourceRelation = dataSet.Relations["FamiliesFamilyCapacityOnResource"];
            DataRelation familiesPrecedenceRelation = dataSet.Relations["FamiliesPrecedence"];
            DataRelation resourcesLoadingTimes = dataSet.Relations["ResourcesLoadingTimes"];
            DataRelation operationsOperationsToFamiliesTimes = dataSet.Relations["OperationsOperationsToFamilies"];

            m_con.Close();
            
        }

*/



/*
        public void test()
        {
            try
            {
                //Connection string for MyODBC 2.50
                //string MyConString = "DRIVER={MySQL};" + 
                 //                    "SERVER=localhost;" +
                  //                   "DATABASE=test;" +
                   //                  "UID=venu;" +
                    //                 "PASSWORD=venu;" +
                     //                "OPTION=3";
                //
                //Connection string for MyODBC 3.51
                string MyConString = "DRIVER={MySQL ODBC 3.51 Driver};" +
                                     "SERVER=localhost;" +
                                     "DATABASE=test;" +
                                     "OPTION=3";

                //Connect to MySQL using MyODBC
                OdbcConnection MyConnection = new OdbcConnection(MyConString);
                MyConnection.Open();

                Console.WriteLine("\n !!! success, connected successfully !!!\n");

                //Display connection information
                Console.WriteLine("Connection Information:");
                Console.WriteLine("\tConnection String:" + MyConnection.ConnectionString);
                Console.WriteLine("\tConnection Timeout:" + MyConnection.ConnectionTimeout);
                Console.WriteLine("\tDatabase:" + MyConnection.Database);
                Console.WriteLine("\tDataSource:" + MyConnection.DataSource);
                Console.WriteLine("\tDriver:" + MyConnection.Driver);
                Console.WriteLine("\tServerVersion:" + MyConnection.ServerVersion);

                //Create a sample table
                OdbcCommand MyCommand = new OdbcCommand("DROP TABLE IF EXISTS my_odbc_net", MyConnection);
                MyCommand.ExecuteNonQuery();
                MyCommand.CommandText = "CREATE TABLE my_odbc_net(id int, name varchar(20), idb bigint)";
                MyCommand.ExecuteNonQuery();

                //Insert
                MyCommand.CommandText = "INSERT INTO my_odbc_net VALUES(10,'venu', 300)";
                Console.WriteLine("INSERT, Total rows affected:" + MyCommand.ExecuteNonQuery()); ;

                //Insert
                MyCommand.CommandText = "INSERT INTO my_odbc_net VALUES(20,'mysql',400)";
                Console.WriteLine("INSERT, Total rows affected:" + MyCommand.ExecuteNonQuery());

                //Insert
                MyCommand.CommandText = "INSERT INTO my_odbc_net VALUES(20,'mysql',500)";
                Console.WriteLine("INSERT, Total rows affected:" + MyCommand.ExecuteNonQuery());

                //Update
                MyCommand.CommandText = "UPDATE my_odbc_net SET id=999 WHERE id=20";
                Console.WriteLine("Update, Total rows affected:" + MyCommand.ExecuteNonQuery());

                //COUNT(*)        
                MyCommand.CommandText = "SELECT COUNT(*) as TRows FROM my_odbc_net";
                Console.WriteLine("Total Rows:" + MyCommand.ExecuteScalar());

                //Fetch
                MyCommand.CommandText = "SELECT * FROM my_odbc_net";
                OdbcDataReader MyDataReader;
                MyDataReader = MyCommand.ExecuteReader();
                while (MyDataReader.Read())
                {
                    if (string.Compare(MyConnection.Driver, "myodbc3.dll") == 0)
                    {
                        Console.WriteLine("Data:" + MyDataReader.GetInt32(0) + " " +
                                                    MyDataReader.GetString(1) + " " +
                                                    MyDataReader.GetInt64(2)); //Supported only by MyODBC 3.51
                    }
                    else
                    {
                        Console.WriteLine("Data:" + MyDataReader.GetInt32(0) + " " +
                                                    MyDataReader.GetString(1) + " " +
                                                    MyDataReader.GetInt32(2)); //BIGINTs not supported by MyODBC
                    }
                }

                //Close all resources
                MyDataReader.Close();
                MyConnection.Close();
            }
            catch (OdbcException MyOdbcException)//Catch any ODBC exception ..
            {
                for (int i = 0; i < MyOdbcException.Errors.Count; i++)
                {
                    Console.Write("ERROR #" + i + "\n" +
                      "Message: " + MyOdbcException.Errors[i].Message + "\n" +
                      "Native: " + MyOdbcException.Errors[i].NativeError.ToString() + "\n" +
                      "Source: " + MyOdbcException.Errors[i].Source + "\n" +
                      "SQL: " + MyOdbcException.Errors[i].SQLState + "\n");
                }
            }
        }
 */
    }
}
