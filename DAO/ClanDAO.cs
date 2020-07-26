using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Moduli;

namespace Biblioteka.DAO
{
    class ClanDAO
    {
        public static Clan GetClanById(SqlConnection conn, int id)
        {
            Clan clan = null;

            try
            {
                string query = "SELECT * " +
                               "FROM clan WHERE id = " + id;

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string ime = (string)r["ime"];
                    string prezime = (string)r["prezime"];

                    clan = new Clan(id, ime, prezime);
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return clan;
        }

        public static List<Clan> GetAll(SqlConnection conn)
        {
            List<Clan> sviClanovi = new List<Clan>();
            try
            {
                string query = "SELECT * FROM clan ";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = (int)r["id"];
                    string ime = (string)r["ime"];
                    string prezime = (string)r["prezime"];

                    sviClanovi.Add(new Clan(id, ime, prezime));
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return sviClanovi;
        }

        public static bool Add(SqlConnection conn, Clan clan)
        {
            bool retVal = false;

            try
            {
                string query = "INSERT INTO clan (id, ime, prezime) " +
                               "VALUES (@id, @ime, @prezime)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", clan.Id);
                cmd.Parameters.AddWithValue("@ime", clan.Ime);
                cmd.Parameters.AddWithValue("@prezime", clan.Prezime);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public static bool Update(SqlConnection conn, Clan clan)
        {
            bool retVal = false;

            try
            {
                string query = "UPDATE clan " +
                               "SET ime=@ime, prezime=@prezime " +
                               "WHERE id=@id"; 
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", clan.Id);
                cmd.Parameters.AddWithValue("@ime", clan.Ime);
                cmd.Parameters.AddWithValue("@prezime", clan.Prezime);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public static bool Delete(SqlConnection conn, int id)
        {
            bool retVal = false;

            try
            {
                string query = "DELETE clan " +
                               "WHERE id= " + id;
                SqlCommand cmd = new SqlCommand(query, conn);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

      
    }
}
