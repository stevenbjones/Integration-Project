using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSAI
{

    class ConnectDatabase
    {
       
        

        public static void Test()
        {
            string connString = ConfigurationManager.AppSettings["connString"];
            SqlConnection connection = new SqlConnection(connString); //connstring converte naar het juiste var type

            string txtDatabase = File.ReadAllText("DatabaseSQL.txt");
           if (!CheckDatabaseExists(connection, "testingdatabase"))
                {
                CreateDatabase(txtDatabase, connection);
                }
        }
        public static bool CheckDatabaseExists(SqlConnection connection, string databaseName)
        {
            bool result = false; //var voor return waarde

            try
            {
               
                string sqlQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName); //query
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, connection)) //verbinding maken
                {
                    connection.Open();
                    object resultObj = sqlCmd.ExecuteScalar();//resultaat ophalen en in een object steken
                    int databaseID = 0;
                    if (resultObj != null)
                    {
                        int.TryParse(resultObj.ToString(), out databaseID); //als hij iets heeft gevonden dat een int is steken we dit in databaseID
                    }
                    connection.Close();
                    result = (databaseID > 0); //als de id niet 0 is bestaat de database al
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static bool CreateDatabase(string txtDatabase, SqlConnection connection)
        {

            SqlCommand cmdCreateDatabase = new SqlCommand("Create Database dbStudentGegevens", connection);
            SqlCommand cmdCreateTable = new SqlCommand(txtDatabase, connection);

            try
            {
                connection.Open();
                cmdCreateDatabase.ExecuteNonQuery();
                cmdCreateTable.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        


        }

    }
}