using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceApp
{
    public class Database
    {

        internal static Dictionary<int, Projekcija> projekcije = new Dictionary<int, Projekcija>();
        internal static Dictionary<int, Rezervacija> rezervacije = new Dictionary<int, Rezervacija>();
        internal static Dictionary<int, Korisnik> korisnici = new Dictionary<int, Korisnik>();


        static Database()
        {
            DateTime dt1 = new DateTime(2015, 12, 31);
            DateTime dt2 = new DateTime(2016, 11, 21);
            DateTime dt3 = new DateTime(2017, 10, 01);


            Projekcija p1 = new Projekcija(1, "Into the wild", dt1, 2021, 300);
            Projekcija p2 = new Projekcija(2, "Lion", dt2, 2023, 500);
            Projekcija p3 = new Projekcija(3, "Loving Vincent", dt3, 2022, 350);

            projekcije.Add(1, p1);
            projekcije.Add(2, p2);
            projekcije.Add(3, p3);


            Korisnik k1 = new Korisnik("Ana", 300);
            Korisnik k2 = new Korisnik("Dora", 1000);
            Korisnik k3 = new Korisnik("Mika", 500);

        }
    }
}
