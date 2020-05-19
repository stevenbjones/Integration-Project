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
            List<Leerling> leerlingen = ConnectDatabase.GetAllLeerlingenFromDatabase();

            Document testdoc = new Document();
            testdoc.LoadFromFile(@"D:\test\Template2.docx");

            Table table = new Table(testdoc, true);

            
           
            string sql = "select Module, count(Geslacht) as Aantal , MONTH([Module begindatum]) as maand, YEAR([Module begindatum]) as jaar from tblStudentGegevens group by module, MONTH([Module begindatum]) , YEAR([Module begindatum])";

           
           DataTable resultaat =  ConnectDatabase.getTable(sql);
           List<TabelAantalLlnPerModule> listAantalPerModule = new List<TabelAantalLlnPerModule>();

            for (int i = 0; i < resultaat.Rows.Count; i++)
            {
                TabelAantalLlnPerModule a = new TabelAantalLlnPerModule();
                a.NaamModule = resultaat.Rows[i]["Module"].ToString();
                a.Aantal = Convert.ToInt32(resultaat.Rows[i]["Aantal"]);
                a.Maand = Convert.ToInt32(resultaat.Rows[i]["maand"]);
                a.Jaar = Convert.ToInt32(resultaat.Rows[i]["jaar"]);

                listAantalPerModule.Add(a);
            }

            MessageBox.Show(listAantalPerModule.ToString());

            //IDictionary<int, List<string>> data = new Dictionary<int, List<string>>();

            TabelAantalLlnPerModule test = new TabelAantalLlnPerModule();
          


          



            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(new string[] { "Stamnummer", "Nationaliteit " });
            foreach (Leerling l in leerlingen)
            {
                dt.Rows.Add(new string[] { l.Stamnummer, l.Nationaliteit });
            }


            table.ResetCells(dt.Rows.Count, dt.Columns.Count);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    table.Rows[i].Cells[j].AddParagraph().AppendText(dt.Rows[i][j].ToString());
                }
            }

            BookmarksNavigator navigator = new BookmarksNavigator(testdoc);
            navigator.MoveToBookmark("Dropouts");

            TextBodyPart part = new TextBodyPart(testdoc);
            part.BodyItems.Add(table);
            navigator.ReplaceBookmarkContent(part);

            BookmarksNavigator navigator2 = new BookmarksNavigator(testdoc);
            navigator.MoveToBookmark("TotaalAantalStudentenSepJan");

            TextBodyPart part2 = new TextBodyPart(testdoc);
            part.BodyItems.Add(table);
            navigator.ReplaceBookmarkContent(part);

            testdoc.SaveToFile("output.docx", FileFormat.Docx2013);
            System.Diagnostics.Process.Start("output.docx");

        }
    }
}
