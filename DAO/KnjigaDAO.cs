using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Moduli;

namespace Biblioteka.DAO
{
    class KnjigaDAO
    {
        public static Knjiga GetKnjigaById(SqlConnection conn, int id)
        {
            Knjiga knjiga = null;

            try
            {
                string query = "SELECT * " +
                               "FROM knjiga WHERE id = " + id;

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string naslov = (string)r["naslov"];
                    string autor = (string)r["autor"];
                    int godIzd = (int)r["god_izdanja"];
                    int brojKopija = (int)r["broj_kopija"];

                    knjiga = new Knjiga(id, naslov, autor, godIzd, brojKopija);
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return knjiga;
        }

        public static List<Knjiga> GetAll(SqlConnection conn)
        {
            List<Knjiga> sveKnjige = new List<Knjiga>();

            try
            {
                string query = "SELECT * " +
                               "FROM knjiga";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = (int)r["id"];
                    string naslov = (string)r["naslov"];
                    string autor = (string)r["autor"];
                    int godIzd = (int)r["god_izdanja"];
                    int brojKopija = (int)r["broj_kopija"];

                    sveKnjige.Add(new Knjiga(id, naslov, autor, godIzd, brojKopija));
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return sveKnjige;
        }

        public static bool Add(SqlConnection conn, Knjiga knjiga)
        {
            bool retVal = false;

            try
            {
                string query = "INSERT INTO knjiga (id, naslov, autor,god_izdanja,broj_kopija) " +
                               "VALUES (@id, @naslov, @autor,@god_izdanja,@broj_kopija)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", knjiga.Id);
                cmd.Parameters.AddWithValue("@naslov", knjiga.Naslov);
                cmd.Parameters.AddWithValue("@autor", knjiga.Autor);
                cmd.Parameters.AddWithValue("@god_izdanja", knjiga.GodIzdavanja);
                cmd.Parameters.AddWithValue("@broj_kopija", knjiga.BrojKopija);

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

        public static bool Update(SqlConnection conn, Knjiga knjiga)
        {
            bool retVal = false;

            try
            {
                string query = "UPDATE knjiga " +
                               "SET naslov=@naslov, autor=@autor,god_izdanja=@god_izdanja,broj_kopija=@broj_kopija " +
                               "WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", knjiga.Id);
                cmd.Parameters.AddWithValue("@naslov", knjiga.Naslov);
                cmd.Parameters.AddWithValue("@autor", knjiga.Autor);
                cmd.Parameters.AddWithValue("@god_izdanja", knjiga.GodIzdavanja);
                cmd.Parameters.AddWithValue("@broj_kopija", knjiga.BrojKopija);

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
                string query = "DELETE FROM knjiga " +
                               "WHERE id = " + id;

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

        public static List<Knjiga> GetKnjigeByNaslov(SqlConnection conn, string substring)
        {
            List<Knjiga> sveKnjige = new List<Knjiga>();

            try
            {
                string query = "SELECT * " +
                               "FROM knjiga " +
                               "WHERE naslov " +
                               "LIKE '%" + substring + "%'";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = (int)r["id"];
                    string naslov = (string)r["naslov"];
                    string autor = (string)r["autor"];
                    int godIzdanja = (int)r["god_izdanja"];
                    int brKopija = (int)r["broj_kopija"];

                    sveKnjige.Add(new Knjiga(id, naslov, autor, godIzdanja, brKopija));
                }
                r.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sveKnjige;
        }
    }
}
