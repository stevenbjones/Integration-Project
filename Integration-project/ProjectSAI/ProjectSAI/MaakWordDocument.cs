using System;
using System.Collections.Generic;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;
using Spire.Doc.Documents;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Office.Interop.Word;
using Spire;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.Doc;
using System.Data;

namespace ProjectSAI
{
    public static class MaakWordDocument
    {
        public static void Create()
        {
            List<Leerling> leerlingen = ConnectDatabase.GetAllLeerlingenFromDatabase();

            Spire.Doc.Document testdoc = new Spire.Doc.Document();
            testdoc.LoadFromFile(@"D:\test\Template2.docx");

            Spire.Doc.Table table = new Spire.Doc.Table(testdoc, true);

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(new string[] { "Stamnummer", "Nationaliteit "});
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

            testdoc.SaveToFile("output.docx", FileFormat.Docx2013);
            System.Diagnostics.Process.Start("output.docx");


        }

        private static void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, Word.Table text)

        {


            if (doc.Bookmarks.Exists(bookmarkName))

            {

                Object name = bookmarkName;


               object test = doc.Bookmarks.get_Item(ref name);

                Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref name).Range;

                range.Text = text.ToString();


                object newRange = range;

               // doc.Bookmarks.Add(bookmarkName, ref newRange);

            }

        }

    }


}
