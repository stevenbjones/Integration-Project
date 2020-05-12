using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSAI
{
    class Leerling
    {
        public int Id { get; set; }
        public string Stamnummer { get; set; }
        public string Geslacht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string  Nationaliteit { get; set; }
        public string Thuistaal  { get; set; }
        public string  ProevenVerpleegkunde { get; set; }
        public string  HoogstBehaaldDiploma { get; set; }
        public string HerkomstStudent { get; set; }
        public string ProjectSO_CVO { get; set; }
        public string  FaciliteitenLeermoeilijkheden_Anderstaligen { get; set; }
        public string DiplomaSOnaCVO { get; set; }
        public string  RedenStoppen { get; set; }
        public string DiplomaSOnaHBO { get; set; }
        public string VDAB { get; set; }
        public string SchoolLerenKennen { get; set; }
        public string Module { get; set; }
        public string ModuleAttest { get; set; }
        public DateTime ModuleBegindatum { get; set; }
        public DateTime ModuleEinddatum { get; set; }
        public DateTime EinddatumInschrijving { get; set; }
        public string AfdelingsCode { get; set; }
        public string  Klas { get; set; }
        public string InstellingnummerVorigJaar { get; set; }
        public string AttestVorigSchooljaar { get; set; }
        public string VerleendeStudiebewijzen1steZit { get; set; }
        public string VerleendeStudiebewijzen1steZitVorigSchooljaar { get; set; }
        public string KlasVorigSchooljaar { get; set; }
        public string InstellingnummerVorigeInschrijving { get; set; }
        public string AttestVorigeInschrijving { get; set; }
    }
}
