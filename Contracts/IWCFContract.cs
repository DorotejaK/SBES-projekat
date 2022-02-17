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
        [FaultContract(typeof(SecurityEx))]
        string DodajProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte);


        //samo clan grupe Admin
        [OperationContract]
        [FaultContract(typeof(SecurityEx))]
        string IzmeniProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte);

        //read projections
        [OperationContract]
        [FaultContract(typeof(SecurityEx))]
        string ProcitajProjekcije();


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
