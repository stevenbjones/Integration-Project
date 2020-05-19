using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ConnectDatabase.CreateDatabaseIfNotExists();

            ConnectDatabase.FillDataGrid(dtgStudent);
        }

        private void chkEditCells_Click(object sender, RoutedEventArgs e)
        {
            if (chkEditCells.IsChecked == true)
            {
                dtgStudent.IsReadOnly = false;
                btnSubmit.IsEnabled = true;
            }
            else
            {
                dtgStudent.IsReadOnly = true;
                btnSubmit.IsEnabled = false;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            chkEditCells.IsChecked = false;
            btnSubmit.IsEnabled = false;
            dtgStudent.IsReadOnly = true;
            ConnectDatabase.UpdateDatabase(dtgStudent);
        }

        private void cmboPartnerLogo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private async void btnUploadData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV files (*.csv)|*.csv";

            if (dlg.ShowDialog() == true)
            {
                string fileName;
                fileName = dlg.FileName;
                ConnectDatabase.UploadCSV(fileName, dtgStudent);
            }
            await Task.Run(() => this.Dispatcher.Invoke(() => ConnectDatabase.FillDataGrid(dtgStudent)));
            MessageBox.Show("Items toegevoegd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnGenerateRapport_Click(object sender, RoutedEventArgs e)
        {
            MaakWordDocument.Create();
        }
    }
}