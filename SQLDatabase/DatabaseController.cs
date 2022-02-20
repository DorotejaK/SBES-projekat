using Contracts;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace SQLDatabase
{
    public class DatabaseController
    {

        public static SQLiteConnection myConnection;


        public DatabaseController()
        {

            try
            {
                myConnection = new SQLiteConnection("Data Source=baza.db", true);
                if (!File.Exists("baza.db"))
                {
                    SQLiteConnection.CreateFile("baza.db");
                    Console.WriteLine("Database file creates");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SacuvajProjekcije(Projekcija projekcija)
        {
            try
            {
                myConnection.Open();
                string querry = $"insert into Projekcija ('idProjekcije','naziv','vremeProjekcije','sala','cenaKarte')" +
                                 $" values (@id,@naz,@vrp,@sala,@cenk)";
                
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                
                cmd.Parameters.AddWithValue("@id", projekcija.Id);
                cmd.Parameters.AddWithValue("@naz", projekcija.Naziv);
                cmd.Parameters.AddWithValue("@vrp", projekcija.VremeProjekcije);
                cmd.Parameters.AddWithValue("@sala", projekcija.Sala);
                cmd.Parameters.AddWithValue("@cenk", projekcija.CenaKarte);

                var result = cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SQLiteException sqle)
            {
                myConnection.Close();
                Console.WriteLine(sqle.Message);
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
            }
        }

        public void SacuvajRezervaciju(Rezervacija rezervacija)
        {
            try
            {
                myConnection.Open();
                string querry = $"insert into Rezervacija ('idRezervacije','idProjekcije','idKorisnika','vremeRezervacije','kolicinaKarata','stanje')" +
                                 $" values (@idr,@idk,@idp,@vr,@kk,@s)";

                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);

                cmd.Parameters.AddWithValue("@idr", rezervacija.IdRazervacije);
                cmd.Parameters.AddWithValue("@idk", rezervacija.IdProjekcije);
                cmd.Parameters.AddWithValue("@idp", rezervacija.IdKorisnika);
                cmd.Parameters.AddWithValue("@vr", rezervacija.VremeRezervacije);
                cmd.Parameters.AddWithValue("@kk", rezervacija.KolicinaKarata);
                cmd.Parameters.AddWithValue("@s", rezervacija.Stanje);

                var result = cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SQLiteException sqle)
            {
                myConnection.Close();
                Console.WriteLine(sqle.Message);
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
            }
        }


        public void AzurirajProjekcije(Projekcija p)
        {
            try
            {
                myConnection.Open();
                string querry = $"UPDATE Projekcija SET  naziv='{p.Naziv}', vremeProjekcije='{p.VremeProjekcije}', sala='{p.Sala}', cenaKarte='{p.CenaKarte}' where idProjekcije='{p.Id}'";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                var result = cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SQLiteException sqle)
            {
                myConnection.Close();
                Console.WriteLine(sqle.Message);
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
            }
        }



        public List<Projekcija> DobaviSveProjekcije()
        {
            try
            {
                List<Projekcija> retVal = new List<Projekcija>();
                myConnection.Open();
                string querry = $"select * from Projekcija";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        retVal.Add(new Projekcija(Convert.ToInt32(result["idProjekcije"]), Convert.ToString(result["naziv"]), Convert.ToDateTime(result["vremeProjekcije"]), Convert.ToInt16(result["sala"]), Convert.ToDouble(result["cenaKarte"])));
                    }
                }
                myConnection.Close();
                return retVal;
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
                return null;
            }

        }

      /*  public List<Rezervacija> DobaviSveRezervacije()
        {
            try
            {
                List<Rezervacija> retVal = new List<Rezervacija>();
                myConnection.Open();
                string querry = $"select * from Rezervacija";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        retVal.Add(new Rezervacija(Convert.ToInt32(result["idRezervacije"]), Convert.ToInt32(result["idProjekcije"]), Convert.ToInt32(result["idKorisnika"]), Convert.ToDateTime(result["vremeRezervacije"]), Convert.ToInt32(result["kolicinaKarata"]), Convert.ToInt64(result["stanje"])));
                    }
                }
                myConnection.Close();
                return retVal;
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
                return null;
            }

        }*/


        public List<Korisnik> DobaviSveKorisnike()
        {
            try
            {
                List<Korisnik> retVal = new List<Korisnik>();
                myConnection.Open();
                string querry = $"select * from Korisnik";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        retVal.Add(new Korisnik(Convert.ToInt32(result["idKorisnika"]), Convert.ToString(result["korisnickoIme"]),  Convert.ToDouble(result["stanjeRacuna"])));
                    }
                }
                myConnection.Close();
                return retVal;
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
                return null;
            }

        }

       

    }
}
