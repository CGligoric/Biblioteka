using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.UI;

namespace Biblioteka
{
    class Program
    {
        public static SqlConnection conn;

        private static void LoadConnection()
        {
            try
            {
                string connectionStringZaPoKuci = "Data Source=.\\SQLEXPRESS;Initial Catalog=Biblioteka;Integrated Security=True;MultipleActiveResultSets=True";

                // Parametar "MultipleActiveResultSets=True" je neophodan kada zelimo da imamo istovremeno
                // otvorena dva data readera ka bazi podataka. Zasto je u ovom programu to neophodno?
                conn = new SqlConnection(connectionStringZaPoKuci);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void OsnovniMeni()
        {
            Console.WriteLine("DB Biblioteka");
            Console.WriteLine("1 - rad sa clanovima");
            Console.WriteLine("2 - rad sa knjigama");
            Console.WriteLine("3 - knjige i njihovi pozajmioci");
            Console.Write("Opcija: ");
        }

        static void Main(string[] args) // TODO dodati jos dodatne 
        {
            LoadConnection();

            bool nastavak = false;
            do
            {
                OsnovniMeni();
                int izbor = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (izbor)
                {
                    case 1:
                        ClanUI.IspisiKompletMeniClana();
                        break;
                    case 2:
                        KnjigaUI.IspisiKompletMeniKnjige();
                        break;
                    case 3:
                        ClanKnjigeUI.IspisiKompletMeniCK();
                        break;
                    default:
                        Console.WriteLine("Nevazeca komanda!");
                        break;
                }
                Console.WriteLine("Da li biste zeleli da nastavite s radom nad bazom podataka? y/n");
                string odg = Console.ReadLine();
                if (odg == "y")
                {
                    nastavak = true;
                }
                Console.Clear();

            } while (nastavak);
        }
    }
}
