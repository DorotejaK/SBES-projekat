using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class Projekcija
    {
        //id(int),naziv(string),
        //vremeProjekcije(DateTime),
        //sala(int),
        //cenaKarte(double).

        int id;
        string naziv;
        DateTime vremeProjekcije;
        int sala;
        double cenaKarte;

        public Projekcija(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.VremeProjekcije = vremeProjekcije;
            this.sala = sala;
            this.CenaKarte = cenaKarte;
        }

        public Projekcija(string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)
        {

            this.Naziv = naziv;
            this.VremeProjekcije = vremeProjekcije;
            this.sala = sala;
            this.CenaKarte = cenaKarte;
        }

        public Projekcija() { }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string Naziv { get => naziv; set => naziv = value; }
        [DataMember]
        public DateTime VremeProjekcije { get => vremeProjekcije; set => vremeProjekcije = value; }
        [DataMember]
        public int Sala { get => sala; set => sala = value; }
        [DataMember]
        public double CenaKarte { get => cenaKarte; set => cenaKarte = value; }

       /* public override string ToString()
        {
            return String.Format("\n\nPROJEKCIJA:\n\nID : {0}, naziv : {1}, vreme projekcije : {2}, cena karte : {3}", Id, Naziv, VremeProjekcije, CenaKarte);

        }
        */
    }
}

