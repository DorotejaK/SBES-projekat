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

        // foreach (var rezervacija in Database.rezervacije.Values)
        public string KorisnikPostoji(string korisnickoIme)
        {
            // var rezervacija in Database.rezervacije.Values
            string retVal;

            //foreach (Korisnik k in Database.korisnici.ToValue())
            foreach(var korisnici in Database.korisnici.Values)
            {
                if(korisnici.KorisnickoIme==korisnickoIme)
                {
                    retVal = "Uspesno ste ulogovani.";
                    return retVal;
                }
            }

            retVal = "Ne postoji korisnicko ime u bazi";
            return retVal;
        }

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

                    db.SacuvajProjekcije(new Projekcija(naziv, vremeProjekcije, sala, cenaKarte));

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


            string retVal;


                if (grupa == "admin")
                {

                    if (Database.projekcije.ContainsKey(id))
                    {
                        Projekcija p = new Projekcija(id, naziv, vremeProjekcije, sala, cenaKarte);
                        Database.projekcije[id] = p;

                        retVal = "Uspesno izmenjena projekcija.";
                        return retVal;
                    }
                    else
                    {
                        //ovo nije trazeno u zadatku, moze da se prilagodi
                        /*string name = Thread.CurrentPrincipal.Identity.Name;
                        DateTime time = DateTime.Now;
                        string message = String.Format("Access is denied. User {0} try to call IzmeniProjekciju method (time : {1} ). " +
                            "For this method need to be member of group Admin.", name);
                        throw new FaultException<SecurityEx>(new SecurityEx(message));*/

                        retVal = "Neuspesno izmenjena projekcija.";
                        return retVal;

                    }
                }
                else
                {
                    retVal = "Samo ADMIN ima pravo na ovu operaciju.";
                    return retVal;
                }

        }

        public string NapraviRezervaciju(int id, int idProjekcije, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanje)
        {

            X509Certificate2 cltCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine,
                Formatter1.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name));

            string grupa = CertManager.GetUserGroup(cltCert);

            string retVal;

            if(grupa == "korisnik" || grupa == "vip")
            {
                if (Database.projekcije.ContainsKey(idProjekcije))
                {
                    Rezervacija r = new Rezervacija(id, idProjekcije, vremeRezervacije, kolicinaKarata, stanje);
                    Database.rezervacije[id] = r;

                    retVal = "Rezervacija uspesno obavljena.";
                    return retVal;
                }
                else
                {
                    retVal = "Rezervacija nije uspela.";
                    return retVal;

                }
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

                    if (Database.rezervacije.ContainsKey(rezervacija.Id))
                    {
                        double zaNaplatu = projekacija.CenaKarte * rezervacija.KolicinaKarata;

                        if ((rezervacija.Stanje == 0) && zaNaplatu <= korisnik.StanjeRacuna)
                        {
                            korisnik.StanjeRacuna = korisnik.StanjeRacuna - zaNaplatu;
                            rezervacija.Stanje = StanjeRezervacije.PLACENA;

                            Database.rezervacije[rezervacija.Id] = rezervacija;

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

            List<Projekcija> sveProejkcije = new List<Projekcija>();

            sveProejkcije = db.DobaviSveProjekcije();
            //foreach (var projekcija in Database.projekcije.Values) {
            foreach (var projekcija in sveProejkcije)
            {
                retString += $"Id:{projekcija.Id} -> Naziv {projekcija.Naziv} + \n";
            }
            

            return retString;
        }

        public string ProcitajRezervacije()
        {
            string retString = "";
            foreach (var rezervacija in Database.rezervacije.Values)
            {
                retString += $"Id:{rezervacija.Id} -> Id projekcije {rezervacija.IdProjekcije} + \n";
            }
            return retString;
        }
        //3. IzmeniPopust - Samo Admin

        //4. NapraviRezervaciju - Korisnik ili VIP - stvara novu rezervaciju cije je stanje NEPLACENA




    }
}
