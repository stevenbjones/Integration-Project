using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSAI
{
    class ConnectDatabase
    {
        public static bool CheckDatabaseExists(string connString, string databaseName )
        {
            bool result = false;

            SqlConnection sqlConnectionString = new SqlConnection(connString);
            string sqlQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);
            using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConnectionString))
            {
                sqlConnectionString.Open();
                object resultObj = sqlCmd.ExecuteScalar();
                int databaseID = 0;
                if (resultObj != null)
                {
                    int.TryParse(resultObj.ToString(), out databaseID);
                }
                sqlConnectionString.Close();
                result = (databaseID > 0);
            }
        }


    }