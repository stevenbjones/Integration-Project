using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Word = Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace POCWord
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object oMissing = System.Reflection.Missing.Value;

            //Instantie van word applicatie
            Application winword = new Application();

            Word.Document document = winword.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            object oEndOfDoc = "\\endofdoc";


            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = document.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Heading 1";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            Word.Range wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            Word.Table oTable;

            //Insert a 5 x 2 table, fill it with data, and change the column widths.
            wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = document.Tables.Add(wrdRng, 5, 2, ref oMissing, ref oMissing);
            string strText;
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            for (int r = 1; r <= 5; r++)
                for (int c = 1; c <= 2; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Columns[1].Width = winword.InchesToPoints(2); //Change width of columns 1 & 2
            oTable.Columns[2].Width = winword.InchesToPoints(3);


            //Add text after the chart.
            wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();
            wrdRng.InsertAfter("THE END.");

            this.Close();

        }
    }
}
