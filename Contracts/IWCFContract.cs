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

        [OperationContract]
        string DodajProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte);

        [OperationContract]
        string IzmeniProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte);

        [OperationContract]
        string ProcitajProjekcije();

        [OperationContract]
        string ProcitajRezervacije();

        [OperationContract]
        string ProcitajKorisnika();

        [OperationContract]
        string NapraviRezervaciju(int idRezervacije,int idProjekcije, int idKorisnika, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanje);

        //[OperationContract]
        //string PlatiRezervaciju(Rezervacija rezervacija, Projekcija projekacija, Korisnik korisnik);

        [OperationContract]
        int IzmeniPopust(int popust);

       
    }
}
