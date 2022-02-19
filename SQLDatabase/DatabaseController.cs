﻿using Contracts;
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
                string querry = $"insert into Projekcija ('naziv','vremeProjekcije','sala','cenaKarte')" +
                                 $" values (@naz,@vrp,@sala,@cenk)";
                
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                
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
                        retVal.Add(new Projekcija(Convert.ToString(result["naziv"]), Convert.ToDateTime(result["datumProjekcije"]), Convert.ToInt16(result["sala"]), Convert.ToDouble(result["cenaKarte"])));
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
