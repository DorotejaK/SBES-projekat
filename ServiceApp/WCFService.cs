using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using Manager;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.ServiceModel;
using System.Security;

namespace ServiceApp
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WCFService : IWCFContract
	{
        /*public void TestCommunication()
		{
			Console.WriteLine("Communication established.");
		}*/

        //1. DodajProjekciju - Samo Admin


        //2. IzmeniProjekciju - Samo Admin
        //[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]

        public string DodajProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)

        {
            //string grupa = CertManager.GetUserGroup(cltCert);

            string retVal;

           // if (grupa =="admin")
           // {

                if (!Database.projekcije.ContainsKey(id))
                {
                    Projekcija p = new Projekcija(id, naziv, vremeProjekcije, sala, cenaKarte);
                    Database.projekcije.Add(id, p);

                    retVal = "Uspesno dodata projekcija.";
                    return retVal;
                }
                else
                {
                    retVal = "Nije moguce dodati projekciju.";
                    return retVal;
                }
           // }
        }

        public string IzmeniProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)
        //radenjana impertivna provera
        {
           // string grupa = CertManager.GetUserGroup(cltCert);

            string retVal;

           // if (Thread.CurrentPrincipal.IsInRole("Korisnik"))
           // if (grupa == "korisnik")
          //  {

                if(Database.projekcije.ContainsKey(id))
                {
                    Projekcija p = new Projekcija(id, naziv, vremeProjekcije, sala, cenaKarte);
                    Database.projekcije[id] = p;

                    retVal = "Uspesno izmenjena projekcija.";
                    return retVal;
                }
                
           // }
            else
            {
                //ovo nije trazeno u zadatku, moze da se prilagodi
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call IzmeniProjekciju method (time : {1} ). " +
                    "For this method need to be member of group Admin.", name);
                throw new FaultException<SecurityEx>(new SecurityEx(message));

            }
        }

        public string ProcitajProjekcije()
        {
            string retString = "";
            foreach (var projekcija in Database.projekcije.Values) {
                retString += $"Id:{projekcija.Id} -> Naziv {projekcija.Naziv} + \n";
            }
            return retString;
        }
        //3. IzmeniPopust - Samo Admin

        //4. NapraviRezervaciju - Korisnik ili VIP - stvara novu rezervaciju cije je stanje NEPLACENA
        //5. PlatiRezervaciju - Korisnik ili VIP - procitaj

        

    }
}
