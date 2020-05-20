using System;
using System.Collections.Generic;
using System.Windows;
using Spire.Doc.Documents;
using System.Security.Cryptography.X509Certificates;
using Spire;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.Doc;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProjectSAI
{
    public static class ExportWordDocument
    {
        

        public static void Create()
        {
            //Laad de template in
            Document testdoc = new Document();          
            testdoc.LoadFromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Template.docx");
            //Maak table
            Table table = new Table(testdoc, true);

      

            
            
         

            BookmarksNavigator navigator = new BookmarksNavigator(testdoc);
            

            TextBodyPart part = new TextBodyPart(testdoc);


            /*Aantal studenten /module/sesmter is momenteel dezelfde query, hier wachten op input van jens */
            //Aantal studenten / module / semester2
            AddTableInBookmark("select Module, count(Geslacht), CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]), semester", table, navigator, "TotaalAantalStudentenFebJun", part);

            ////Aantal studenten / module /semster1
            //AddTableInBookmark("select Module, count(Geslacht), CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]), semester", table, navigator, "TotaalAantalStudentenSepJan", part);


            ////Geslaagde mensen / module
            //AddTableInBookmark("select Module, COUNT([Module attest]) as geslaagd , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where[Module attest] = 'Geslaagd' group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]), semester", table, navigator, "Slaagpercentage", part);


            ////Aantal afgestudeerden /semester           
            //AddTableInBookmark("select COUNT(Stamnummer) as [aantal afgestudeerden], CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where Module = 'Module Toegepaste verpleegkunde (40 weken)' and[Module attest] = 'Geslaagd' group by  CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])", table, navigator, "AantalAfgestudeerdeStudenten", part);

            ////RedenStoppen          
            //AddTableInBookmark("select[Reden stoppen] ,count([Reden stoppen]) as aantal,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester , YEAR([Module begindatum]) from tblStudentGegevens where [Reden stoppen] != '' group by [Reden stoppen],CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]) ASC, semester ASC ", table, navigator, "RedenStoppen", part);


            ////school leren kennen        
            //AddTableInBookmark("select coalesce(nullif([School leren kennen],''), 'onbekend') as [school leren kennen], count([School leren kennen]) as aantal, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) from tblStudentGegevens where Module = 'Module Initiatie verpleegkunde (20 weken)' group by [School leren kennen], CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5) order by  YEAR([Module begindatum]) ASC, semester ASC", table, navigator, "SchoolLerenKennen", part);


            

            testdoc.SaveToFile("output.docx", FileFormat.Docx2013);
            System.Diagnostics.Process.Start("output.docx");



        }

        public static void AddTableInBookmark(string sql, Table table, BookmarksNavigator navigator, string bookmark, TextBodyPart txtbodyPart)
        {       
            //Voer sql query uit en steek deze in datatable
            DataTable result = ConnectDatabase.getTable(sql);

            //Vul de table met data van de datatable

             
            table.ResetCells(result.Rows.Count + 1, result.Columns.Count);

            List<string> years = new List<string>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                if (!years.Contains(result.Rows[i]["jaar"].ToString()))
                {
                    years.Add(result.Rows[i]["jaar"].ToString());
                }
            }
            years.Sort();
            table.Rows[0].Cells[0].AddParagraph().AppendText(bookmark);
            for (int i = 0; i < years.Count; i++)
            {
                table.Rows[0].Cells[i+1].AddParagraph().AppendText(years[i]);
            }

            //for (int i = 0; i < result.Rows.Count; i++)
            //{

                

               
            //    }
            //    //table.Rows[i].Cells[0].AddParagraph().AppendText(result.Rows[i][0].ToString());
            //    //for (int j = 0; j < result.Columns.Count; j++)
            //    //{
            //    //    table.Rows[i].Cells[j].AddParagraph().AppendText(result.Rows[i][j].ToString());
            //    //}
            //}

            navigator.MoveToBookmark(bookmark);

            txtbodyPart.BodyItems.Add(table);
            navigator.ReplaceBookmarkContent(txtbodyPart);

        }

      
    }
}
