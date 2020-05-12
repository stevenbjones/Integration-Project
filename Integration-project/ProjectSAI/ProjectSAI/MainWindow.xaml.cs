using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace ProjectSAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            ConnectDatabase.UpdateDatabase(dtgStudent);

          
        }

        private void cmboPartnerLogo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUploadData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";

            if (dlg.ShowDialog() == true)
            {
                string fileName;
                fileName = dlg.FileName;
                //MessageBox.Show(fileName);
                ConnectDatabase.UploadCSV(fileName);
                //MessageBox.Show("pls work");
                
            }
        }
    } }

