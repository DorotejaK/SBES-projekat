using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using Manager;
using Contracts;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{
            /// Define the expected service certificate. It is required to establish cmmunication using certificates.
            //string srvCertCN = String.Empty;

            //4.1 - 8
            string srvCertCN = "wcfservice";

            NetTcpBinding binding = new NetTcpBinding();
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
           
            //4.1 - 9
            X509Certificate2 srvCert =
                                      CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN); //trusted people - pogledati sliku gde se sta nalazi
			EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
									  new X509CertificateEndpointIdentity(srvCert));


			using (WCFClient proxy = new WCFClient(binding, address))
			{
                while (true)
                {
                    Console.WriteLine("Izaberite jednu od opcija: \n\n");
                    Console.WriteLine("1. Dodaj projekciju \n2.Izmeni projekciju \n3. Izmeni popust \n4. Naplati rezervaciju \n5. Plati rezervaciju \n6. Ispisi projekcije");

                    var opp = Console.ReadLine();

                    if (opp == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Dodaj projekciju============\n\n");

                        Console.WriteLine("Unesite ID projekcije: \n");
                        var id = Console.ReadLine();

                        Console.WriteLine("Unesite ime projekcije: \n");
                        var na = Console.ReadLine();

                        Console.WriteLine("Unesite cenu karte projekcije: \n");
                        var ck = Console.ReadLine();

                        Console.WriteLine("Unesite vreme projekcije: \n");
                        var vp = Console.ReadLine();

                        Console.WriteLine("Unesite broj sale: \n");
                        var sa = Console.ReadLine();

                        var retVal = proxy.DodajProjekciju(Convert.ToInt32(id), na.ToString(), Convert.ToDateTime(vp), Convert.ToInt32(sa), Convert.ToDouble(ck));
                        Console.WriteLine(retVal);
                    }
                    else if (opp=="2")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Izmeni projekciju============\n\n");

                        Console.WriteLine("Unesite ID projekcije koju zelite da izmenite: \n");
                        var id = Console.ReadLine();

                        Console.WriteLine("Unesite novo ime projekcije: \n");
                        var na = Console.ReadLine();

                        Console.WriteLine("Unesite novo vreme projekcije: \n");
                        var vp  = Console.ReadLine();

                        Console.WriteLine("Unesite novi broj sale: \n");
                        var sa = Console.ReadLine();

                        Console.WriteLine("Unesite novu cenu karte projekcije: \n");
                        var ck = Console.ReadLine();

                        var retVal = proxy.IzmeniProjekciju(Convert.ToInt32(id), na.ToString(), Convert.ToDateTime(vp), Convert.ToInt32(sa), Convert.ToDouble(ck));
                        Console.WriteLine(retVal);
                    }
                    else if (opp=="3")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Izmeni popust============\n\n");

                    }
                    else if (opp=="4")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Napravi rezervaciju============\n\n");

                    }
                    else if (opp=="5")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Plati rezervaciju============\n\n");

                    }
                    else if (opp == "6")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Ispisi projekcije============\n\n");
                        Console.WriteLine(proxy.ProcitajProjekcije());
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Morate izabrati jednu od ponudjenih opcija.");
                    }


                }

                Console.ReadLine();
            }

			
		}
	}
}
