using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRCPSP.Database.MySql
{
    class SchemaCreator
    {
        public  static void createSchema()
        {
            //Create a sample table
            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Resources");
            DBHandler.Instance.executeQuery("CREATE TABLE Resources(Problem_ID INT NOT NULL, Resource_ID INT NOT NULL , PRIMARY KEY (Problem_ID,Resource_ID) , Name varchar(20), `Capacity Kr` int , `Release_Date tr1` int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Families");
            DBHandler.Instance.executeQuery("CREATE TABLE Families(Problem_ID INT NOT NULL , Family_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Family_ID) ,Name varchar(20), Description varchar(20))");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Operations");
            DBHandler.Instance.executeQuery("CREATE TABLE Operations(Problem_ID INT NOT NULL, Operation_ID INT NOT NULL,  PRIMARY KEY (Problem_ID,Operation_ID) , Name varchar(20), Description varchar(20))");


            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Modes");
            DBHandler.Instance.executeQuery("CREATE TABLE Modes(Problem_ID INT NOT NULL , Operation_ID INT NOT NULL , Mode_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Operation_ID,Mode_ID) , Name varchar(20) , Description varchar(20))");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS OperationsToFamilies");
            DBHandler.Instance.executeQuery("CREATE TABLE OperationsToFamilies(Problem_ID INT NOT NULL, Operation_ID INT NOT NULL, Family_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Operation_ID,Family_ID) )");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Precedence");
            DBHandler.Instance.executeQuery("CREATE TABLE Precedence(Problem_ID INT NOT NULL, Family_ID INT NOT NULL , Previous_Operation_ID INT NOT NULL  , Subsequent_Operation_ID INT NOT NULL  ,PRIMARY KEY(Problem_ID,Family_ID,Previous_Operation_ID,Subsequent_Operation_ID) , MinLfi1i int , MaxLfi1i2 int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Jobs");
            DBHandler.Instance.executeQuery("CREATE TABLE Jobs(Problem_ID INT NOT NULL, Family_ID INT NOT NULL , Job_ID INT NOT NULL , PRIMARY KEY(Problem_ID,Family_ID,Job_ID) ,Units int , Release_Date int , Release_D VARBINARY(20) ,  Due_Date int, weight int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS ResourceUsage");
            DBHandler.Instance.executeQuery("CREATE TABLE ResourceUsage(Problem_ID INT NOT NULL, Operation_ID INT NOT NULL , Mode_ID INT NOT NULL ,Resource_ID INT NOT NULL , PRIMARY KEY (Problem_ID,Operation_ID , Mode_ID , Resource_ID) , Ts int , Tf int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS Problems");
            DBHandler.Instance.executeQuery("CREATE TABLE Problems(Problem_ID INT NOT NULL PRIMARY KEY, Description varchar(20) , IF_RR int , IF_NP int , IF_NE int , IF_NM int , IF_AR int , IF_AOR int , IF_ARC int , RF_NZV int , RF_NCV int , RF_NRW int , RF_TCM int , RF_TFC int ,RF_MPNZ int , RF_NBC int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS ConstantDelays");
            DBHandler.Instance.executeQuery("CREATE TABLE ConstantDelays(Problem_ID INT NOT NULL, Operation1_ID INT NOT NULL , Mode1_ID INT NOT NULL, Operation2_ID INT NOT NULL, Mode2_ID INT NOT NULL, Resource_ID INT NOT NULL, PRIMARY KEY(Problem_ID,Operation1_ID, Mode1_ID , Operation2_ID , Mode2_ID , Resource_ID) , di1m1i2m2r int)");

            DBHandler.Instance.executeQuery("DROP TABLE IF EXISTS FamilyCapacityOnResource");
            DBHandler.Instance.executeQuery("CREATE TABLE FamilyCapacityOnResource(Problem_ID INT NOT NULL, Family_ID INT NOT NULL , Resource_ID INT NOT NULL, PRIMARY KEY(Problem_ID,Family_ID , Resource_ID) , `Family_Capacity kfr` int)");
            
            
        }


    }
}
