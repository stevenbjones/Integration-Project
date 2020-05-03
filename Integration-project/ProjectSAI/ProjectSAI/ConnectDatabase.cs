using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public static string connString = ConfigurationManager.AppSettings["connStringMaster"];
      public static  SqlConnection connection = new SqlConnection(connString); //connstring converte naar het juiste var type


        public static void CreateDatabaseIfNotExists()
        {
            

            string txtDatabase = File.ReadAllText("DatabaseSQL.txt"); //text file lezen en in var steken
           if (!CheckDatabaseExists("dbStudentGegevens")) //check of dbStudentGegevens is angemaakt
                {
                CreateDatabase(txtDatabase); //database maken dat in de textfile zit
                }

            connString = ConfigurationManager.AppSettings["connStringDB"];
            connection = new SqlConnection(connString); //connstring converte naar het juiste var type
        }
        public static bool CheckDatabaseExists(string databaseName)
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

        public static bool CreateDatabase(string txtDatabase)
        {
            //2 commandos, 1 voor de databank aan te maken, andere voor tabellen en eventueel data te importeren. Moet appart, anders kregen we errors
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
                MessageBox.Show("Er is een onverwachte fout opgetreden bij het aanmaken van de databank: " + ex.ToString());
                return false;
            }

            return true;



    }

        public static void FillDataGrid(System.Windows.Controls.DataGrid dataGrid)
        {
            using (connection)
            {   
                //commando sql
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tblStudentGegevens", connection);
                //sql adaptop aanmake
                SqlDataAdapter SqlDataAdapt = new SqlDataAdapter(cmd);
                //datatable aanmaken van de databank
                DataTable dataTable = new DataTable("dbStudentGegevens");
                //de datatable vullen met gegevens van de databank
                SqlDataAdapt.Fill(dataTable);
                //grid vullen met gegevens
                dataGrid.ItemsSource = dataTable.DefaultView;

                DataSet dataSet = new DataSet();
               
         
        }

        //public static bool UpdateDatabase()
        {
           //todo
        }

    }
}