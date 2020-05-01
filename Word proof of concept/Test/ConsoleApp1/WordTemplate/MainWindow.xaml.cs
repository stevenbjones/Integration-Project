using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace WordTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            test();
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

            //////////////////////
            ///
            object start = 0;
            object end = 0;
            Word.Range tableLocation = doc.Range(ref start, ref end);
            doc.Tables.Add(tableLocation, 3, 4);

            doc.Tables[1].set_Style("Table Grid 8");
            doc.Tables[1].Cell(2, 1).Range.Text = "Steven";
            doc.Tables[1].Cell(2, 2).Range.Text = "Is";
            doc.Tables[1].Cell(2, 3).Range.Text = "Freaking";
            doc.Tables[1].Cell(2, 4).Range.Text = "Awesome";

            ReplaceBookmarkText(doc, "test", "I LOVE JENS <3");
            ReplaceBookmarkText2(doc, "test2", doc.Tables[1]);
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

        private static void ReplaceBookmarkText2(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, Word.Table text)

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
