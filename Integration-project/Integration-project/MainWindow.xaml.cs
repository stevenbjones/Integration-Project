using System;
using System.Collections.Generic;
using System.IO;
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

namespace Integration_project
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //C:\Users\steve\OneDrive\Documenten\TEST
            StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\steve\OneDrive\Documenten\TEST\geg met klas.csv"));

            List<string> Naam = new List<String>();
            List<string> Voornaam = new List<String>();
            List<string> Geboorte = new List<String>();
            List<string> GeboorteJaar = new List<String>();
            List<string> Geslacht = new List<String>();
            List<string> Nationaliteit = new List<String>();
            List<string> Module = new List<String>();
            List<string> Klas = new List<String>();
            //string vara1, vara2, vara3, vara4;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(';');
                    
                    Naam.Add(values[0]);
                    Voornaam.Add(values[1]);
                    Geboorte.Add(values[2]);
                    GeboorteJaar.Add(values[3]);
                    Geslacht.Add(values[4]);
                    Nationaliteit.Add(values[5]);
                    Module.Add(values[6]);
                    Klas.Add(values[7]);
                }
            }

            lstbxNaam.ItemsSource = Naam;
            lstbxVoorNaam.ItemsSource = Voornaam;
            lstbxGeboorteDatum.ItemsSource = Geboorte;
            lstbxGeboorteJaar.ItemsSource = GeboorteJaar;
            lstbxGeslacht.ItemsSource = Geslacht;
            lstbxNationaliteit.ItemsSource = Nationaliteit;
            lstbxModule.ItemsSource = Module;
            lstbxKlas.ItemsSource = Klas;
            
        }



    }
}

