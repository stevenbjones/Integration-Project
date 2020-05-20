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

        private async void chkEnableDatagrid_Click(object sender, RoutedEventArgs e)
        {
            if (chkEnableDatagrid.IsChecked == true)
            {

                await Task.Run(() => this.Dispatcher.Invoke(() => lblOutput.Content = "Data word geladen..."));
                await Task.Run(() => this.Dispatcher.Invoke(() => ConnectDatabase.FillDataGrid(dtgStudent)));
                lblOutput.Content = "Data succesvol geladen.";
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Waarschuwing, uw gewijzigde gegevens zullen niet opgeslaan worden. Bent u zeker?", "Opgelet", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    dtgStudent.ItemsSource = null;
                    dtgStudent.Items.Clear();
                    lblOutput.Content = "";
                }
                else
                {
                    chkEnableDatagrid.IsChecked = true;
                }
            }
        }
    }
}