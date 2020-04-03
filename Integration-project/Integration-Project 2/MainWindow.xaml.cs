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

namespace Integration_Project_2
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
            StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\steve\OneDrive\Documenten\TEST\geg met klas.csv"));
            List<Leerling> Leerlingen = new List<Leerling>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(';');

                    Leerlingen.Add(new Leerling
                    {
                        Naam = values[0],
                        Voornaam = values[1],
                        Geboorte = values[2],
                        GeboorteJaar = values[3],
                        Geslacht = values[4],
                        Nationaliteit = values[5],
                        Module = values[6],
                        Klas = values[7],
                    });

                }
            }

           foreach(Leerling l in Leerlingen)
            {
                LstbxLeerlingen.Items.Add(l);
            }
        }

    }
}
