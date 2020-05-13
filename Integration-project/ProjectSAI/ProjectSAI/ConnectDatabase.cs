﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ProjectSAI
{
    internal class ConnectDatabase
    {
        public static string connString = ConfigurationManager.AppSettings["connStringMaster"];
        public static SqlConnection connection = new SqlConnection(connString); //connstring converte naar het juiste var type
        public static DataSet dataSet;
        public static SqlDataAdapter sqlDataAdapter;
        public static DataTable dataTable;

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
                connection.Close();
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
                dataSet = new DataSet();
                //commando sql
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tblStudentGegevens", connection);
                //sql adaptop aanmake
                sqlDataAdapter = new SqlDataAdapter(cmd);
                //datatable aanmaken van de databank
                dataTable = new DataTable("dbStudentGegevens");
                //de datatable vullen met gegevens van de databank
                sqlDataAdapter.Fill(dataTable);

                List<Leerling> listLeerling = new List<Leerling>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Leerling leerling = new Leerling();
                    leerling.Stamnummer = dataTable.Rows[i]["Stamnummer"].ToString();
                    leerling.Nationaliteit = dataTable.Rows[i]["Nationaliteit"].ToString();
                    leerling.Geslacht =  dataTable.Rows[i]["Geslacht"].ToString();
                    DateTime.TryParse(dataTable.Rows[i]["Geboortedatum"].ToString(), out DateTime geboorteDatum);
                    leerling.Geboortedatum = geboorteDatum.Date;
                    listLeerling.Add(leerling);
                }
                //grid vullen met gegevens
                dataGrid.ItemsSource = listLeerling;                   
            }
        }

        public static void UpdateDatabase(DataGrid datagrid)
        {
            
            connString = ConfigurationManager.AppSettings["connStringDB"];
            connection = new SqlConnection(connString); //connstring converte naar het juiste var type
            SqlCommand cmdClearTable = new SqlCommand("Delete from dbo.tblStudentGegevens", connection);
            SqlCommand cmdInsertTable;

            try
            {
                connection.Open();
                cmdClearTable.ExecuteNonQuery();

                for (int i = 0; i < datagrid.Items.Count-1; i++)
                {
                    Leerling leerling = (Leerling)datagrid.Items[i];
                    cmdInsertTable = new SqlCommand($"INSERT INTO dbo.tblStudentGegevens (Nationaliteit, Geboortedatum) VALUES('{leerling.Nationaliteit}', '{leerling.Geboortedatum}')", connection);
                    cmdInsertTable.ExecuteNonQuery();
                }
                MessageBox.Show("Data succesvol verandert", "Gelukt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            
            {
                MessageBox.Show("Er is een onverwachte fout opgetreden bij het updaten van de databank: " + ex.ToString(), "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);             
            }

        }

        public static void UploadCSV(string filePath)
        {

            //string filepath = filePath;

            //string line1 = File.ReadLines(filepath).First(); // gets the first line from file.

            //MessageBox.Show(line1);

            using (StreamReader sr = new StreamReader(filePath))
            {
                 string connString = ConfigurationManager.AppSettings["connStringMaster"];
                 SqlConnection connection = new SqlConnection(connString); 

        string headerLine = sr.ReadLine(); //Leest 1e lijn csv

                //leest de rest.
                var lines = File.ReadAllLines(filePath);
                if (lines.Count() == 0) return;
                var table = new DataTable();

                for (int i = 1; i < lines.Count() - 1 ; i++)
                {

                    List<string> listStrLineElements;
                    listStrLineElements = lines[i].Split(',').ToList();

                    connection.Open();

                    string insertQuery = "INSERT INTO  dbStudentGegevens.dbo.tblStudentGegevens VALUES (" +
                             "'" + listStrLineElements[0] + "'," +
                             "'" + listStrLineElements[1] + "'," +
                             "'" + listStrLineElements[2] + "'," +
                             "'" + listStrLineElements[3] + "'," +
                             "'" + listStrLineElements[4] + "'," +
                             "'" + listStrLineElements[5] + "'," +
                             "'" + listStrLineElements[6] + "'," +
                             "'" + listStrLineElements[7] + "'," +
                             "'" + listStrLineElements[8] + "'," +
                             "'" + listStrLineElements[9] + "'," +
                             "'" + listStrLineElements[10] + "'," +
                             "'" + listStrLineElements[11] + "'," +
                             "'" + listStrLineElements[12] + "'," +
                             "'" + listStrLineElements[13] + "'," +
                             "'" + listStrLineElements[14] + "'," +
                             "'" + listStrLineElements[15] + "'," +
                             "'" + listStrLineElements[16] + "'," +
                             "'" + listStrLineElements[17] + "'," +
                             "'" + listStrLineElements[18] + "'," +
                             "'" + listStrLineElements[19] + "'," +
                             "'" + listStrLineElements[20] + "'," +
                             "'" + listStrLineElements[21] + "'," +
                             "'" + listStrLineElements[22] + "'," +
                             "'" + listStrLineElements[23] + "'," +
                             "'" + listStrLineElements[24] + "'," +
                             "'" + listStrLineElements[25] + "'," +
                             "'" + listStrLineElements[26] + "'," +
                             "'" + listStrLineElements[27] + "'," +
                             "'" + listStrLineElements[28] + "')";

                    try
                    {
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                    connection.Close();



                }
            }

        }


    }
}