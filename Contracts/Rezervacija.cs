using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public enum StanjeRezervacije {
        [EnumMember]
        NEPLACENA = 0,
        [EnumMember]
        PLACENA = 1}

    [DataContract]
    public class Rezervacija
    {

        //id(int),
        //idProjekcije(int),
        //vremeRezervacije(DateTime),
        //kolicinaKarata(int), 
        //stanje(StanjeRezervacije: NEPLACENA ili PLACENA).

        StanjeRezervacije stanje;
        int idRezervacije;
        int idKorisnika;
        int idProjekcije;
        DateTime vremeRezervacije;
        int kolicinaKarata;
        
        public Rezervacija(int idRezervacije, int idProjekcije, int idKorisnika, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanje)
        {
          
            this.IdRazervacije = idRezervacije;
            this.IdProjekcije = idProjekcije;
            this.IdKorisnika = idKorisnika;
            this.VremeRezervacije = vremeRezervacije;
            this.KolicinaKarata = kolicinaKarata;
            this.Stanje = stanje;
        }



        [DataMember]
       public StanjeRezervacije Stanje { get => stanje; set => stanje = value; }

       [DataMember]
       public int IdRazervacije { get => idRezervacije; set => idRezervacije = value; }

        [DataMember]
        public int IdKorisnika { get => idKorisnika; set => idKorisnika = value; }

        [DataMember]
       public int IdProjekcije { get => idProjekcije; set => idProjekcije = value; }

       [DataMember]
       public DateTime VremeRezervacije { get => vremeRezervacije; set => vremeRezervacije = value; }

       [DataMember]
       public int KolicinaKarata { get => kolicinaKarata; set => kolicinaKarata = value; }

        /*public override string ToString()
        {
            return String.Format("\n\nREZERVACIJA\n\nstanje : {0}, ID : {1}, id projekcije : {2}, vreme rezervacije : {3}, kolicina karata : {4}", Stanje, Id, IdProjekcije, VremeRezervacije, KolicinaKarata);
        }*/

    }
}
