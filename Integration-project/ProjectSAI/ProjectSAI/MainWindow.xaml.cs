﻿using System;
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

<<<<<<< HEAD
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            chkEditCells.IsChecked = false;
            btnSubmit.IsEnabled = false;
         //   ConnectDatabase.UpdateDatabase();
=======
        private void cmboPartnerLogo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Code here
>>>>>>> 55a599fc8654d24b0eeae72a931613e8f2cfa186
        }
    }
}

