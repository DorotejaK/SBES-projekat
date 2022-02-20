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

    }
}
