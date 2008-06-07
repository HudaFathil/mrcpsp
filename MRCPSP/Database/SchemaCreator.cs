using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Database
{
    class SchemaCreator
    {
        public  static void createSchema()
        {
            //Create a sample table
            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Resources");
            DBHandler.Instance.executeQuery("CREATE TABLE Resources(Problem_ID INT NOT NULL, Resource_ID INT NOT NULL , PRIMARY KEY (Problem_ID,Resource_ID) , Name varchar(20), Capacity_Kr int , Release_Date int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Families");
            DBHandler.Instance.executeQuery("CREATE TABLE Families(Problem_ID INT NOT NULL , Family_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Family_ID) , Size int ,Name varchar(20), Description varchar(20))");


            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Operations");
            DBHandler.Instance.executeQuery("CREATE TABLE Operations(Problem_ID INT NOT NULL, Operation_ID INT NOT NULL,  PRIMARY KEY (Problem_ID,Operation_ID) , Name varchar(20), Description varchar(20))");


            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Modes");
            DBHandler.Instance.executeQuery("CREATE TABLE Modes(Problem_ID INT NOT NULL , Operation_ID INT NOT NULL , Mode_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Operation_ID,Mode_ID) , Name varchar(20), Capacity_Kr int , Release_Date int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS OperationsToFamilies");
            DBHandler.Instance.executeQuery("CREATE TABLE OperationsToFamilies(Problem_ID INT NOT NULL, Operation_ID INT NOT NULL, Family_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Operation_ID,Family_ID) )");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Precedence");
            DBHandler.Instance.executeQuery("CREATE TABLE Precedence(Problem_ID INT NOT NULL, Family_ID INT NOT NULL , Previous_Operation_ID INT NOT NULL  , Subsequent_Operation_ID INT NOT NULL  ,PRIMARY KEY(Problem_ID,Family_ID,Previous_Operation_ID,Subsequent_Operation_ID) , MinLfi1i int , MaxLfi1i2 int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Jobs");
            DBHandler.Instance.executeQuery("CREATE TABLE Jobs(Problem_ID INT NOT NULL, Family_ID INT NOT NULL , Job_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Family_ID,Job_ID) , arrive_time real , latest_time real , weight int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS ResourceUsage");
            DBHandler.Instance.executeQuery("CREATE TABLE ResourceUsage(Problem_ID INT NOT NULL, Operation_ID INT NOT NULL , Mode_ID INT NOT NULL ,Resource_ID INT NOT NULL , PRIMARY KEY (Problem_ID,Operation_ID , Mode_ID , Resource_ID) , Ts int , Tf int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Problems");
            DBHandler.Instance.executeQuery("CREATE TABLE Problems(Problem_ID INT NOT NULL PRIMARY KEY, Title varchar(20))");

            

        }


    }
}
