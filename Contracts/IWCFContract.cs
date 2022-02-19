using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security;

namespace Contracts
{
	[ServiceContract]
	public interface IWCFContract
	{
        // [OperationContract]
        // void TestCommunication();


        //samo clan grupe Admin
        [OperationContract]
        //[FaultContract(typeof(SecurityEx))]
        string DodajProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte);


        //samo clan grupe Admin
        [OperationContract]
        //[FaultContract(typeof(SecurityEx))]
        string IzmeniProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte);

        //read projections
        [OperationContract]
        //[FaultContract(typeof(SecurityEx))]
        string ProcitajProjekcije();

        [OperationContract]
        //[FaultContract(typeof(SecurityEx))]
        string ProcitajRezervacije();

        [OperationContract]
        //[FaultContract(typeof(SecurityEx))]
        string NapraviRezervaciju(int id, int idProjekcije, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanje);


        [OperationContract]
        string PlatiRezervaciju(Rezervacija rezervacija, Projekcija projekacija, Korisnik korisnik);

        [OperationContract]
        string KorisnikPostoji(string korisnickoIme);

        //dodati posle sta treba za klase 
        /*


                //samo clan grupe Admin
                [OperationContract]
                string IzmeniPropust(int idKorisnika);

                //Clan grupe Korisnik ili VIP 
                [OperationContract]
                string NapraviRezervaciju(int idKorisnika);

                [OperationContract]
                string PlatiRezervaciju(int idKorisnika);
        */
    }
}
