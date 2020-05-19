using System;
using System.Collections.Generic;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;


namespace ProjectSAI
{
    public static class MaakWordDocument
    {
        public static void Create()
        {
            List<Leerling> leerlingen = ConnectDatabase.GetAllLeerlingenFromDatabase();
            Word.Application WordApp = new Word.Application();

            object fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Template2.docx";

            object confirmConversions = Type.Missing;

            object readOnly = Type.Missing;

            object addToRecentFiles = Type.Missing;

            object passwordDoc = Type.Missing;

            object passwordTemplate = Type.Missing;

            object revert = Type.Missing;

            object writepwdoc = Type.Missing;

            object writepwTemplate = Type.Missing;

            object format = Type.Missing;

            object encoding = Type.Missing;

            object visible = Type.Missing;

            object openRepair = Type.Missing;

            object docDirection = Type.Missing;

            object notEncoding = Type.Missing;

            object xmlTransform = Type.Missing;
            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Document doc = WordApp.Documents.Open(ref fileName, ref confirmConversions, ref readOnly, ref addToRecentFiles,

            ref passwordDoc, ref passwordTemplate, ref revert, ref writepwdoc,

            ref writepwTemplate, ref format, ref encoding, ref visible, ref openRepair,

            ref docDirection, ref notEncoding, ref xmlTransform);

            //////////////////////////////////////////////////
            
            object start = 0;
            object end = 0;
            Word.Range tableLocation = doc.Range(ref start, ref end);
            doc.Tables.Add(tableLocation, 3, 4);

            doc.Tables[1].set_Style("Table Grid 8");
            doc.Tables[1].Cell(2, 1).Range.Text = leerlingen[0].Stamnummer;
            doc.Tables[1].Cell(2, 2).Range.Text = leerlingen[0].Thuistaal;
            doc.Tables[1].Cell(2, 3).Range.Text = "Freaking";
            doc.Tables[1].Cell(2, 4).Range.Text = "Awesome";

            
            ReplaceBookmarkText(doc, "Dropouts", doc.Tables[1]);
            Microsoft.Office.Interop.Word.Document document = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            document = doc;
            object filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\exportData.docx";
            doc.SaveAs2(ref filename);
            doc.Close(ref missing, ref missing, ref missing);
            doc = null;
            WordApp.Quit(ref missing, ref missing, ref missing);
            WordApp = null;
            MessageBox.Show("Document created successfully !");
        }

        private static void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, Word.Table text)

        {

            if (doc.Bookmarks.Exists(bookmarkName))

            {

                Object name = bookmarkName;

                Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref name).Range;

                range.Text = text.ToString();

                object newRange = range;

                doc.Bookmarks.Add(bookmarkName, ref newRange);

            }

        }

    }


}
