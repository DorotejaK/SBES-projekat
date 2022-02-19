using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contracts;
using Manager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace ClientApp
{
	public class WCFClient : ChannelFactory<IWCFContract>, IWCFContract, IDisposable
	{
		IWCFContract factory;

		public WCFClient(NetTcpBinding binding, EndpointAddress address)
			: base(binding, address)
		{
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            
            // 4.1 - 6
            string cltCertCN = Formatter1.ParseName(WindowsIdentity.GetCurrent().Name); // vraca ime zorana/wcfclient ili servoce

            //4.2 validacija
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
            //4.1 - 7
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN); //dobavljanje sertifikata naseg klijenta

         
            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            //this.Credentials.ClientCertificate.Certificate

            factory = this.CreateChannel();
		}

        public string DodajProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)
        {

            string retVal = "";
            try
            {
                retVal = factory.DodajProjekciju(id, naziv, vremeProjekcije, sala, cenaKarte);
                //Console.WriteLine("Uneta je nova projekcija");

                return retVal;
            }
            catch (FaultException<SecurityEx> e)
            {
                Console.WriteLine("Greska prilikom pokusaja DodajProjekciju : {0}", e.Detail.Message);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Greska prilikom pokusaja DodajProjekciju: {0}", e.Message);
                return retVal;
            }

        }

        public string IzmeniProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)
        {


            string retVal = "";
            try
            {
                retVal = factory.IzmeniProjekciju(id, naziv, vremeProjekcije, sala, cenaKarte);
                //Console.WriteLine("Izmena projekcije dozvoljena");

                return retVal;
            }
            catch (FaultException<SecurityEx> e)
            {
                Console.WriteLine("Greska prilikom pokusaja IzmeneProjekcije : {0}", e.Detail.Message);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Greska prilikom pokusaja IzmeneProjekcije: {0}", e.Message);
                return retVal;
            }
           
        }

        public string NapraviRezervaciju(int id, int idProjekcije, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanje)
        {

            string retVal = "";
            try
            {
                retVal = factory.NapraviRezervaciju(id, idProjekcije, vremeRezervacije, kolicinaKarata, stanje);
                
                return retVal;
            }
            catch (FaultException<SecurityEx> e)
            {
                Console.WriteLine("Greska prilikom pokusaja NapraviRezervaciju : {0}", e.Detail.Message);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Greska prilikom pokusaja NapraviRezervaciju: {0}", e.Message);
                return retVal;
            }

        }

        public string PlatiRezervaciju(Rezervacija rezervacija, Projekcija projekacija, Korisnik korisnik)
        {
            string retVal = "";
            try
            {
                retVal = factory.PlatiRezervaciju(rezervacija, projekacija, korisnik);

                return retVal;
            }
            catch (FaultException<SecurityEx> e)
            {
                Console.WriteLine("Greska prilikom pokusaja PlatiRezervaciju : {0}", e.Detail.Message);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Greska prilikom pokusaja PlatiRezervaciju: {0}", e.Message);
                return retVal;
            }


        }
        public string ProcitajProjekcije()
        {
            return factory.ProcitajProjekcije();
        }

        public string ProcitajRezervacije()
        {
            return factory.ProcitajRezervacije();
        }

        public void Dispose()
		{
			if (factory != null)
			{
				factory = null;
			}

			this.Close();
		}

        public string KorisnikPostoji(string korisnickoIme)
        {
            string retVal = "";
            try
            {

                retVal = factory.KorisnikPostoji(korisnickoIme);
                return retVal;

            }
            catch (Exception e)
            {

                Console.WriteLine("[KorisnikPostoji] ERROR = {0}", e.Message);
                return retVal;
            }
        }
    }
}
