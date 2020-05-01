using System;
using Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            test();
            Console.ReadLine();
        }

        public static void test()
        {
            
            Word.Application WordApp = new Word.Application();

            object fileName = @"D:\Template.docx";

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

            Microsoft.Office.Interop.Word.Document doc = WordApp.Documents.Open(ref fileName, ref confirmConversions, ref readOnly, ref addToRecentFiles,

            ref passwordDoc, ref passwordTemplate, ref revert, ref writepwdoc,

            ref writepwTemplate, ref format, ref encoding, ref visible, ref openRepair,

            ref docDirection, ref notEncoding, ref xmlTransform);

            ReplaceBookmarkText(doc, "test", "I LOVE JENS <3");
        }

        private static void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, string text)

        {

            if (doc.Bookmarks.Exists(bookmarkName))

            {

                Object name = bookmarkName;

                Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref name).Range;

                range.Text = text;

                object newRange = range;

                doc.Bookmarks.Add(bookmarkName, ref newRange);

            }

        }
    }
}
