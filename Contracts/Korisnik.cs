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
        int idKorisnika;
        string korisnickoIme;
        double stanjeRacuna;

        public Korisnik(string korisnickoIme, double stanjeRacuna)
        {
            this.korisnickoIme = korisnickoIme;
            this.stanjeRacuna = stanjeRacuna;
        }

        public Korisnik(int idKorisnika, string korisnickoIme, double stanjeRacuna)
        {
            this.idKorisnika = idKorisnika;
            this.korisnickoIme = korisnickoIme;         
            this.stanjeRacuna = stanjeRacuna;
        }

        public Korisnik() { }

        [DataMember]
        public int IdKorisnika { get; set; }

        [DataMember]
        public string KorisnickoIme { get; set; }

        [DataMember]
        public double StanjeRacuna { get; set; }

    }
}
