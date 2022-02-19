using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class Korisnik
    {

        string korisnickoIme;
        //string lozinka;
        double stanjeRacuna;

        public Korisnik(string korisnickoIme, double stanjeRacuna)
        {
            this.korisnickoIme = korisnickoIme;
           // this.lozinka = lozinka;
            this.stanjeRacuna = stanjeRacuna;
        }

        [DataMember]
        public string KorisnickoIme { get; set; }
        //[DataMember]
        //public string Lozinku { get; set; }
        [DataMember]
        public double StanjeRacuna { get; set; }

    }
}
