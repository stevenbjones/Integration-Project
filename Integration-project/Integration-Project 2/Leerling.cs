using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_Project_2
{

    public class Leerling
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Geboorte { get; set; }
        public string GeboorteJaar { get; set; }
        public string Geslacht { get; set; }
        public string Nationaliteit { get; set; }
        public string Module { get; set; }
        public string Klas { get; set; }

        public override string ToString()
        {
            return $"{Voornaam} {Naam} {Geboorte} {GeboorteJaar} {Geslacht} {Nationaliteit} {Module} {Klas}";
         }
    }

}
