using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Moduli;

namespace Biblioteka.DAO
{
    class ClanKnjigeDAO
    {
        public static ClanKnjige GetClanKnjigaById(SqlConnection conn, int id)
        {
            ClanKnjige clanKnjige = null;
            try
            {
                string query = "SELECT clan_id,knjiga_id " +
                          "FROM clan_knjige " +
                          "WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    int clanId = (int)r["clan_id"];
                    int knjigaId = (int)r["knjiga_id"];

                    Clan clan = ClanDAO.GetClanById(Program.conn, clanId);
                    Knjiga knjiga = KnjigaDAO.GetKnjigaById(Program.conn, knjigaId);

                    clanKnjige = new ClanKnjige(id, clan, knjiga);
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return clanKnjige;
        }

        public static List<ClanKnjige> GetAll(SqlConnection conn)
        {
            List<ClanKnjige> sviCK = new List<ClanKnjige>();
            try
            {
                string query = "SELECT * " +
                          "FROM clan_knjige "
                          ;
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int id = (int)r["id"];
                    int clanId = (int)r["clan_id"];
                    int knjigaId = (int)r["knjiga_id"];

                    Clan clan = ClanDAO.GetClanById(Program.conn, clanId);
                    Knjiga knjiga = KnjigaDAO.GetKnjigaById(Program.conn, knjigaId);

                    sviCK.Add(new ClanKnjige(id, clan, knjiga));
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sviCK;
        }

        public static bool Add(SqlConnection conn, ClanKnjige ck)
        {
            bool retVal = false;

            try
            {
                string query = "INSERT INTO clan_knjige (id, clan_id, knjiga_id) " +
                               "VALUES (@id, @clan_id, @knjiga_id)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", ck.Id);
                cmd.Parameters.AddWithValue("@clan_id", ck.Clan.Id);
                cmd.Parameters.AddWithValue("@knjiga_id", ck.Knjiga.Id);

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

        public static bool Update(SqlConnection conn, ClanKnjige ck)
        {
            bool retVal = false;

            try
            {
                string query = "UPDATE clan_knjige " +
                               "SET clan_id=@clan_id,knjiga_id=@knjiga_id " +
                               "WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", ck.Id);
                cmd.Parameters.AddWithValue("@clan_id", ck.Clan.Id);
                cmd.Parameters.AddWithValue("@knjiga_id", ck.Knjiga.Id);

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
                string query = "DELETE FROM clan_knjige WHERE id = " + id;
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
