using System;
using Contracts;
using Manager;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using SQLDatabase;
using System.Collections.Generic;

namespace ServiceApp
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WCFService : IWCFContract
    {

        public DatabaseController db = new DatabaseController();
     

        public string DodajProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)
        {
       
             X509Certificate2 cltCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine,
             Formatter1.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name));
            string grupa = CertManager.GetUserGroup(cltCert);
             
             

            string retVal;

            if (grupa =="admin")
            {

                if (!Database.projekcije.ContainsKey(id))
                {
                    Projekcija p = new Projekcija(id, naziv, vremeProjekcije, sala, cenaKarte);
                    Database.projekcije.Add(id, p);

                    db.SacuvajProjekcije(new Projekcija(id, naziv, vremeProjekcije, sala, cenaKarte));

                    retVal = "Uspesno dodata projekcija.";
                    return retVal;
                }
                else
                {
                    retVal = "Nije moguce dodati projekciju.";
                    return retVal;
                }
            }
            else
            {
                    retVal="Samo ADMIN ima pravo na ovu operaciju.";
                    return retVal;
            }
        }


        public string IzmeniProjekciju(int id, string naziv, DateTime vremeProjekcije, int sala, double cenaKarte)
        {

            X509Certificate2 cltCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine,
                Formatter1.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name));

            string grupa = CertManager.GetUserGroup(cltCert);

            string retVal="";

                if (grupa == "admin")
                {
       
                        List<Projekcija> projekcije = new List<Projekcija>();

                        bool pronasao = false;

                        projekcije = db.DobaviSveProjekcije();

                        foreach (Projekcija p in projekcije)
                        {
                            if (p.Id == id)
                            {
                                pronasao = true;
                                db.AzurirajProjekcije(new Projekcija(id, naziv, vremeProjekcije, sala, cenaKarte));

                            break;

                            }

                        }

                        if (pronasao == true)
                        {
                            retVal = $"Projekcija uspesno azurirana";
                        }
                        else
                        {
                            retVal = "Azuriranje projekcije nije uspelo.";
                        }

                        return retVal;
            
                }
                else
                {
                    retVal = "Samo ADMIN ima pravo na ovu operaciju.";
                    return retVal;
                }

        }


        public int IzmeniPopust(int popust)
        {

            X509Certificate2 cltCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine,
                Formatter1.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name));

            string grupa = CertManager.GetUserGroup(cltCert);          

            int p = 10;
            int noviPopust=0;

            if (grupa == "admin")
            {



                if(popust!=p)
                {
                    noviPopust = popust;
                    Console.WriteLine("Trenutni popust je: {0}", noviPopust);
                    return noviPopust;
                }
                else
                {
                    noviPopust = p;
                    Console.WriteLine("Popust nije izmenjen. Trenutni popust je {0}", p);
                    return p;
                }
            }
            else
            {
                return 1;
            }
        }


        public string NapraviRezervaciju(int idRezervacije, int idProjekcije, int idKorisnika, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanje)
        {

            X509Certificate2 cltCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine,
                Formatter1.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name));

            string grupa = CertManager.GetUserGroup(cltCert);

            string retVal;

            if(grupa == "korisnik" || grupa == "vip")
            {

                List<Projekcija> sveProjekcije = new List<Projekcija>();

                sveProjekcije = db.DobaviSveProjekcije();

                foreach (var projekcija in sveProjekcije)
                {
                    if(projekcija.Id==idProjekcije)
                    {
                        Rezervacija r = new Rezervacija(idRezervacije, idProjekcije, idKorisnika, vremeRezervacije, kolicinaKarata, stanje);
                        Database.rezervacije[idRezervacije] = r;
                    
                        db.SacuvajRezervaciju(new Rezervacija(idRezervacije, idProjekcije, idKorisnika, vremeRezervacije, kolicinaKarata, stanje));
                    
                        retVal = "Rezervacija uspesno obavljena.";
                        return retVal;
                    }
                    else
                    {
                        retVal = "Rezervacija nije uspela.";
                        return retVal;

                    }

                   
                }

                retVal = "??";
                return retVal;

            }
            else
            {
                retVal = "Samo korisnici i vip korisnici imaju pravo pristupa.";
                return retVal;
            }
        }

        //22IzmeniPopust(može samo član grupe Admin)        /*public string IzmeniPopust(double popust)
        {

        }*/

        //5. PlatiRezervaciju - Korisnik ili VIP - procitaj
        /* PlatiRezervaciju(može član grupe Korisnik ili član grupe VIP) – ukoliko je rezervacija
            NEPLACENA i ukoliko ima dovoljno sredstava na računu, umanjuje stanje računa tog
            korisnika za iznos cene karte projekcije * kolicina karata rezervacije(ako je u pitanju
            VIP član onda se cena umanjuje za količinu popusta), i menja stanje rezervacije u
            PLACENA.*/

          public string PlatiRezervaciju(Rezervacija rezervacija, Projekcija projekacija, Korisnik korisnik)
          {

                X509Certificate2 cltCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine,
                    Formatter1.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name));

                string grupa = CertManager.GetUserGroup(cltCert);

                string retVal;

                if (grupa == "korisnik" || grupa == "vip")
                {

                    if (Database.rezervacije.ContainsKey(rezervacija.IdRazervacije))
                    {
                        double zaNaplatu = projekacija.CenaKarte * rezervacija.KolicinaKarata;

                        if ((rezervacija.Stanje == 0) && zaNaplatu <= korisnik.StanjeRacuna)
                        {
                            korisnik.StanjeRacuna = korisnik.StanjeRacuna - zaNaplatu;
                            rezervacija.Stanje = StanjeRezervacije.PLACENA;

                            Database.rezervacije[rezervacija.IdRazervacije] = rezervacija;

                            retVal = "Uspesno ste platili projekciju.";
                            return retVal;
                        }

                        retVal = "Nemate dovoljno novca na stanju. ";
                        return retVal;

                    }
                    else
                    {
                        retVal = "Ne postoji rezervacija sa brojem ID-a.";
                        return retVal;
                    }

                }
                else
                {
                    retVal = "Samo korisnici i vip korisnici imaju pravo pristupa.";
                    return retVal;
                }

          } 

        public string ProcitajProjekcije()
        {
            string retString = "";

            List<Projekcija> sveProjekcije = new List<Projekcija>();

            sveProjekcije = db.DobaviSveProjekcije();
            //foreach (var projekcija in Database.projekcije.Values) {
            foreach (var projekcija in sveProjekcije)
            {
                retString += $"Id:{projekcija.Id} -> Naziv {projekcija.Naziv} + \n";
            }
            

            return retString;
        }

        public string ProcitajRezervacije()
        {
            string retString = "";

            List<Rezervacija> sveRezervacije = new List<Rezervacija>();

           // sveRezervacije = db.DobaviSveRezervacije();

            foreach (var rezervacija in Database.rezervacije.Values)
            {
                retString += $"Id:{rezervacija.IdRazervacije} -> Id projekcije {rezervacija.IdProjekcije} + \n";
            }
            return retString;
        }

        public string ProcitajKorisnika()
        {
            string retString = "";

            List<Korisnik> sviKorisnici = new List<Korisnik>();

            sviKorisnici = db.DobaviSveKorisnike();
            foreach (var korisnik in Database.korisnici.Values)
            {
                retString += $"Id:{korisnik.IdKorisnika} -> Korisnicko ime {korisnik.KorisnickoIme} + \n";
            }
            return retString;
        }
        //3. IzmeniPopust - Samo Admin

        //4. NapraviRezervaciju - Korisnik ili VIP - stvara novu rezervaciju cije je stanje NEPLACENA




    }
}
