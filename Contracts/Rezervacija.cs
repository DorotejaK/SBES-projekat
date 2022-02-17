using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public enum StanjeRezervacije {NEPLACENA, PLACENA}

    [DataContract]
    public class Rezervacija
    {

        //id(int),
        //idProjekcije(int),
        //vremeRezervacije(DateTime),
        //kolicinaKarata(int), 
        //stanje(StanjeRezervacije: NEPLACENA ili PLACENA).

        StanjeRezervacije stanje;
        int id;
        int idProjekcije;
        DateTime vremeRezervacije;
        int kolicinaKarata;

        public Rezervacija(StanjeRezervacije stanje, int id, int idProjekcije, DateTime vremeRezervacije, int kolicinaKarata)
        {
            this.Stanje = stanje;
            this.Id = id;
            this.IdProjekcije = idProjekcije;
            this.VremeRezervacije = vremeRezervacije;
            this.KolicinaKarata = kolicinaKarata;
        }



        [DataMember]
       public StanjeRezervacije Stanje { get => stanje; set => stanje = value; }

       [DataMember]
       public int Id { get => id; set => id = value; }

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
