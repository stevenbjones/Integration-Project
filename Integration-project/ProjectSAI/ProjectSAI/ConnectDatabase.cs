﻿

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSAI
{
    internal class ConnectDatabase
    {
        public static string connString = ConfigurationManager.AppSettings["connStringMaster"];
        public static SqlConnection connection = new SqlConnection(connString); //connstring converte naar het juiste var type
        public static DataSet dataSet;
        public static SqlDataAdapter sqlDataAdapter;
        public static System.Data.DataTable dataTable;

        public static void CreateDatabaseIfNotExists()
        {
            
            string txtDatabase = File.ReadAllText(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\DatabaseSQL.txt"); //text file lezen en in var steken
           

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

            List<Leerling> listLeerling = GetAllLeerlingenFromDatabase();
            //grid vullen met gegevens
            dataGrid.ItemsSource = listLeerling;
        }

        public static void UpdateDatabase(DataGrid datagrid)
        {
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("nl-NL");
            connString = ConfigurationManager.AppSettings["connStringDB"];
            connection = new SqlConnection(connString); //connstring converte naar het juiste var type
            SqlCommand cmdClearTable = new SqlCommand("Delete from dbo.tblStudentGegevens", connection);
            SqlCommand cmdInsertTable;

            for (int i = 0; i < datagrid.Items.Count; i++)
            {
                Leerling leerling = (Leerling)datagrid.Items[i];
                if (leerling.Geboortedatum.Year < 1800 || leerling.Geboortedatum.Year > 9999 || leerling.ModuleBegindatum.Year < 1800 || leerling.ModuleBegindatum.Year > 9999 || leerling.ModuleEinddatum.Year < 1800 || leerling.ModuleEinddatum.Year > 9999 || leerling.EinddatumInschrijving.Year < 1800 || leerling.EinddatumInschrijving.Year > 9999)
                {
                    MessageBox.Show("Opgelet! Controleer op het volgende: de datum mag niet minder dan het jaar 1800 zijn en meer dan het jaar 9999. Controleer dat u overal een goede datum heeft ingevuld. ", "Opgepast!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                connection.Open();
                cmdClearTable.ExecuteNonQuery();

                for (int i = 0; i < datagrid.Items.Count; i++)
                {
                    Leerling leerling = (Leerling)datagrid.Items[i];

                    cmdInsertTable = new SqlCommand($"INSERT INTO dbo.tblStudentGegevens VALUES( " +
                        $"@geboortedatum,'" +
                        $"{leerling.Geslacht}','" +
                        $"{leerling.Nationaliteit}','" +
                        $"{leerling.Thuistaal}','" +
                        $"{leerling.ProevenVerpleegkunde}', '" +
                        $"{leerling.HoogstBehaaldDiploma}', '" +
                        $"{leerling.HerkomstStudent}', '" +
                        $"{leerling.ProjectSO_CVO}', '" +
                        $"{leerling.FaciliteitenLeermoeilijkheden_Anderstaligen}', '" +
                        $"{leerling.DiplomaSOnaCVO}', '" +
                        $"{leerling.RedenStoppen}', '" +
                        $"{leerling.DiplomaSOnaHBO}', '" +
                        $"{leerling.VDAB}', '" +
                        $"{leerling.SchoolLerenKennen}', '" +
                        $"{leerling.Module}', '" +
                        $"{leerling.ModuleAttest}', " +
                        $"@moduleBegindatum, " +
                        $"@moduleEinddatum,'" +
                        $"{leerling.Stamnummer}', " +
                        $"@einddatumInschrijving, '" +
                        $"{leerling.AfdelingsCode}', '" +
                        $"{leerling.Klas}', '" +
                        $"{leerling.InstellingnummerVorigJaar}', '" +
                        $"{leerling.AttestVorigSchooljaar}', '" +
                        $"{leerling.VerleendeStudiebewijzen1steZit}', '" +
                        $"{leerling.VerleendeStudiebewijzen1steZitVorigSchooljaar}', '" +
                        $"{leerling.KlasVorigSchooljaar}', '" +
                        $"{leerling.InstellingnummerVorigeInschrijving}', '" +
                        $"{leerling.AttestVorigeInschrijving}')", connection);
                    cmdInsertTable.Parameters.AddWithValue("@geboortedatum", leerling.Geboortedatum);
                    cmdInsertTable.Parameters.AddWithValue("@moduleBegindatum", leerling.ModuleBegindatum);
                    cmdInsertTable.Parameters.AddWithValue("@moduleEinddatum", leerling.ModuleEinddatum);
                    cmdInsertTable.Parameters.AddWithValue("@einddatumInschrijving", leerling.EinddatumInschrijving);


                    cmdInsertTable.ExecuteNonQuery();
                }
                MessageBox.Show("Data succesvol veranderd", "Gelukt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlTypeException ex)

            {
                MessageBox.Show("Fout bij het toevoegen van een student: de datum mag niet minder dan het jaar 1800 zijn en meer dan het jaar 9999. Controleer dat u overal een goede datum heeft ingevuld. ", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)

            {
                MessageBox.Show("Er is een onverwachte fout opgetreden bij het updaten van de databank: " + ex.ToString(), "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public static List<Leerling> GetAllLeerlingenFromDatabase()
        {
            connString = ConfigurationManager.AppSettings["connStringDB"];
            connection = new SqlConnection(connString); //connstring converte naar het juiste var type

            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tblStudentGegevens", connection);
            //sql adaptop aanmake
            sqlDataAdapter = new SqlDataAdapter(cmd);
            //datatable aanmaken van de databank
            dataTable = new System.Data.DataTable("dbStudentGegevens");
            //de datatable vullen met gegevens van de databank
            sqlDataAdapter.Fill(dataTable);
            List<Leerling> listLeerling = new List<Leerling>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Leerling leerling = new Leerling();
                leerling.Nationaliteit = dataTable.Rows[i]["Nationaliteit"].ToString();
                leerling.Geslacht = dataTable.Rows[i]["Geslacht"].ToString();

                leerling.Geboortedatum = Convert.ToDateTime(dataTable.Rows[i]["Geboortedatum"]);
                leerling.Stamnummer = dataTable.Rows[i]["Stamnummer"].ToString();
                leerling.Thuistaal = dataTable.Rows[i]["Thuistaal"].ToString();
                leerling.ProevenVerpleegkunde = dataTable.Rows[i]["Proeven_verpleegkunde"].ToString();
                leerling.HoogstBehaaldDiploma = dataTable.Rows[i]["Hoogst_behaald_diploma"].ToString();
                leerling.HerkomstStudent = dataTable.Rows[i]["Herkomst_Studenten"].ToString();
                leerling.ProjectSO_CVO = dataTable.Rows[i]["Project SO-CVO"].ToString();
                leerling.FaciliteitenLeermoeilijkheden_Anderstaligen = dataTable.Rows[i]["Faciliteiten leermoeilijkheden/Anderstaligen"].ToString();
                leerling.DiplomaSOnaCVO = dataTable.Rows[i]["Diploma SO na CVO"].ToString();
                leerling.RedenStoppen = dataTable.Rows[i]["Reden stoppen"].ToString();
                leerling.VDAB = dataTable.Rows[i]["VDAB"].ToString();
                leerling.SchoolLerenKennen = dataTable.Rows[i]["School leren kennen"].ToString();
                leerling.Module = dataTable.Rows[i]["Module"].ToString();
                leerling.ModuleAttest = dataTable.Rows[i]["Module attest"].ToString();

                leerling.ModuleBegindatum = Convert.ToDateTime(dataTable.Rows[i]["Module begindatum"]);
                leerling.ModuleEinddatum = Convert.ToDateTime(dataTable.Rows[i]["Module einddatum"]);
                leerling.EinddatumInschrijving = Convert.ToDateTime(dataTable.Rows[i]["Einddatum inschrijving"]);
                leerling.AfdelingsCode = dataTable.Rows[i]["Afdelingscode"].ToString();
                leerling.Klas = dataTable.Rows[i]["Klas"].ToString();
                leerling.InstellingnummerVorigJaar = dataTable.Rows[i]["Instellingnummer vorig schooljaar"].ToString();
                leerling.AttestVorigSchooljaar = dataTable.Rows[i]["Attest vorig schooljaar"].ToString();
                leerling.VerleendeStudiebewijzen1steZit = dataTable.Rows[i]["Verleende studiebewijzen 1ste zit"].ToString();
                leerling.VerleendeStudiebewijzen1steZitVorigSchooljaar = dataTable.Rows[i]["Verleende studiebewijzen 1ste zit vorig schooljaar"].ToString();
                leerling.KlasVorigSchooljaar = dataTable.Rows[i]["Klas vorig schooljaar"].ToString();
                leerling.InstellingnummerVorigeInschrijving = dataTable.Rows[i]["Instellingnummer vorige inschrijving"].ToString();
                leerling.AttestVorigeInschrijving = dataTable.Rows[i]["Attest vorige inschrijving"].ToString();
                listLeerling.Add(leerling);
            }
            return listLeerling;
        }

        public static void UploadCSV(string filePath, DataGrid dataGrid)
        {
            //string filepath = filePath;

            //string line1 = File.ReadLines(filepath).First(); // gets the first line from file.

            //MessageBox.Show(line1);
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("nl-NL");

                    string connString = ConfigurationManager.AppSettings["connStringMaster"];
                    SqlConnection connection = new SqlConnection(connString);

                    string headerLine = sr.ReadLine(); //Leest 1e lijn csv               

                    //leest de rest.
                    var lines = File.ReadAllLines(filePath, System.Text.Encoding.GetEncoding("iso-8859-1"));
                    bool dataHasComma = false;
                    if (lines.Count() == 0) return;

                    for (int i = 0; i < lines.Count(); i++)
                    {
                        List<string> listStrLineElements;
                        listStrLineElements = lines[i].Split(',').ToList();

                        if (listStrLineElements.Count > 29)
                        {
                            MessageBox.Show("Er zit een fout in de data. Controleer op komma's die data scheiden binnen een kolom. Gelieve het excell bestand te openen. Ctrl+f ',' vervangen met '/' of '-'. Fout op lijn: " + i, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                            dataHasComma = true;
                        }
                    }

                    if (dataHasComma) return;
                    for (int i = 1; i < lines.Count(); i++)
                    {

                        List<string> listStrLineElements;
                        listStrLineElements = lines[i].Split(',').ToList();


                        listStrLineElements[0] = listStrLineElements[0].Replace('.', '/');
                        listStrLineElements[16] = listStrLineElements[16].Replace('.', '/');
                        listStrLineElements[17] = listStrLineElements[17].Replace('.', '/');
                        listStrLineElements[19] = listStrLineElements[19].Replace('.', '/');

                        DateTime geboortedatum = new DateTime(); ;
                        DateTime moduleBeginDatum = new DateTime();
                        DateTime moduleEindDatum = new DateTime();
                        DateTime einddatumInschrijving = new DateTime();



                        if (listStrLineElements[0] == "")
                        {
                            listStrLineElements[0] = DateTime.MaxValue.ToString();
                        }
                        else
                        {
                            geboortedatum = DateTime.ParseExact(listStrLineElements[0], "dd/MM/yyyy", cultureinfo);
                            listStrLineElements[0] = geboortedatum.ToString();
                        }

                        if (listStrLineElements[16] == "")
                        {
                            listStrLineElements[16] = DateTime.MaxValue.ToString();
                        }
                        else
                        {
                            moduleBeginDatum = DateTime.ParseExact(listStrLineElements[16], "dd/MM/yyyy", cultureinfo);
                            listStrLineElements[16] = moduleBeginDatum.ToString();
                        }

                        if (listStrLineElements[17] == "")
                        {
                            listStrLineElements[17] = DateTime.MaxValue.ToString();
                        }
                        else
                        {
                            moduleEindDatum = DateTime.ParseExact(listStrLineElements[17], "dd/MM/yyyy", cultureinfo);
                            listStrLineElements[17] = moduleEindDatum.ToString();
                        }

                        if (listStrLineElements[19] == "")
                        {
                            listStrLineElements[19] = DateTime.MaxValue.ToString();
                        }
                        else
                        {
                            einddatumInschrijving = DateTime.ParseExact(listStrLineElements[19], "dd/MM/yyyy", cultureinfo);
                            listStrLineElements[19] = einddatumInschrijving.ToString();
                        }






                        connection.Open();

                        string insertQuery = "INSERT INTO  dbStudentGegevens.dbo.tblStudentGegevens  VALUES  (" +
                        "@geboortedatum ," +
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
                        "@modulebegindatum ," +
                        "@moduleeinddatum ," +
                        "'" + listStrLineElements[18] + "'," +
                        "@einddatuminschrijving ," +
                        "'" + listStrLineElements[20] + "'," +
                        "'" + listStrLineElements[21] + "'," +
                        "'" + listStrLineElements[22] + "'," +
                        "'" + listStrLineElements[23] + "'," +
                        "'" + listStrLineElements[24] + "'," +
                        "'" + listStrLineElements[25] + "'," +
                        "'" + listStrLineElements[26] + "'," +
                        "'" + listStrLineElements[27] + "'," +
                        "'" + listStrLineElements[28] + "')";
                        SqlCommand command = new SqlCommand(insertQuery, connection);

                        command.Parameters.AddWithValue("@geboortedatum", geboortedatum);
                        command.Parameters.AddWithValue("@modulebegindatum", moduleBeginDatum);
                        command.Parameters.AddWithValue("@moduleeinddatum", moduleEindDatum);
                        command.Parameters.AddWithValue("@einddatuminschrijving", einddatumInschrijving);


                        try
                        {

                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                        connection.Close();
                    }
                }
                MessageBox.Show("Items worden toegevoegd, klik op: OK.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Er is iets onverwachts foutgegaan bij het inladen van de data. Gelieve alle data eens na te kijken: "+ ex, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
              
            }            
        }

        public static System.Data.DataTable getTable(string query)
        {
            connString = ConfigurationManager.AppSettings["connStringDB"];
            connection = new SqlConnection(connString); //connstring converte naar het juiste var type
            try
            {
                SqlDataReader dataReader;
                dataSet = new DataSet();
                List<string> output = new List<string>();
                //commando sql
                SqlCommand cmd = new SqlCommand(query, connection);




                //sql adaptop aanmake
                sqlDataAdapter = new SqlDataAdapter(cmd);
                //datatable aanmaken van de databank
                dataTable = new System.Data.DataTable("dbStudentGegevens");
                //de datatable vullen met gegevens van de databank
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (System.Exception ex)
            {
                return dataTable;
            }
        }

        public static bool ExecuteQuery(string query)
        {
            try
            {
                connString = ConfigurationManager.AppSettings["connStringDB"];
                connection = new SqlConnection(connString); //connstring converte naar het juiste var type
                SqlCommand cmdClearTable = new SqlCommand(query, connection);
                connection.Open();
                cmdClearTable.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij het uitvoeren van query: " + query + "Volledige foutboodschap: " + ex, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}