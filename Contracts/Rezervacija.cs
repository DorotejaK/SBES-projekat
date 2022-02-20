using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public enum StanjeRezervacije
    {
        [EnumMember]
        NEPLACENA = 0,
        [EnumMember]
        PLACENA = 1
    }

    [DataContract]
    public class Rezervacija
    {

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


    }
}
