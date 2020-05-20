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
    public static class MaakWordDocument
    {

        public static void Create()
        {
            //Laad de template in
            Document testdoc = new Document();
            testdoc.LoadFromFile(@"D:\test\Template2.docx");

            //Maak table
            Table table = new Table(testdoc, true);

            //sql string
            string sql = "select Module, count(Geslacht) as Aantal , MONTH([Module begindatum]) as maand, YEAR([Module begindatum]) as jaar from tblStudentGegevens group by module, MONTH([Module begindatum]) , YEAR([Module begindatum])";

            
            
         

            BookmarksNavigator navigator = new BookmarksNavigator(testdoc);
            

            TextBodyPart part = new TextBodyPart(testdoc);
            part.BodyItems.Add(table);
            navigator.ReplaceBookmarkContent(part);

            MaakTabelInBookmark(testdoc, sql, table, navigator, "test",part);
          
            testdoc.SaveToFile("output.docx", FileFormat.Docx2013);
            System.Diagnostics.Process.Start("output.docx");

        }

        public static void MaakTabelInBookmark(Document document,string sql, Table table, BookmarksNavigator navigator, string bookmark, TextBodyPart txtbodyPart)
        {            


            //Voer sql query uit en steek deze in datatable
            DataTable resultaat = ConnectDatabase.getTable(sql);

            //Vul de table met data van de datatable
            table.ResetCells(resultaat.Rows.Count, resultaat.Columns.Count);

            for (int i = 0; i < resultaat.Rows.Count; i++)
            {
                for (int j = 0; j < resultaat.Columns.Count; j++)
                {
                    table.Rows[i].Cells[j].AddParagraph().AppendText(resultaat.Rows[i][j].ToString());
                }
            }

            navigator.MoveToBookmark(bookmark);

        }
    }
}
