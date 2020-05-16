using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        Visibility _isGifLoadingVisible;
        public MainWindow()
        {
            InitializeComponent();

            isGifLoadingVisible = Visibility.Hidden;
            ConnectDatabase.CreateDatabaseIfNotExists();

            ConnectDatabase.FillDataGrid(dtgStudent);

        }
        public Visibility isGifLoadingVisible
        {
            get => _isGifLoadingVisible;
            set
            {
                if (_isGifLoadingVisible != value)
                {
                    _isGifLoadingVisible = value;
                }
            }
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
            gifLoading.Visibility = Visibility.Visible;
            chkEditCells.IsChecked = false;
            btnSubmit.IsEnabled = false;
            dtgStudent.IsReadOnly = true;
            ConnectDatabase.UpdateDatabase(dtgStudent);
            gifLoading.Visibility = Visibility.Hidden;

        }

        private void cmboPartnerLogo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        void btnUploadData_Click(object sender, RoutedEventArgs e)
        {
         
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";

            if (dlg.ShowDialog() == true)
            {
                isGifLoadingVisible = Visibility.Visible;
                string fileName;
                fileName = dlg.FileName;
                //MessageBox.Show(fileName);
                
                ConnectDatabase.UploadCSV(fileName, dtgStudent);
                //MessageBox.Show("pls work");
               
            }
           
            dtgStudent.Items.Refresh();
            gifLoading.Visibility = Visibility.Hidden;
        }

        private void btnGenerateRapport_Click(object sender, RoutedEventArgs e)
        {
            MaakWordDocument.Create();
        }


    } }

