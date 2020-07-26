using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Moduli;
using Biblioteka.DAO;

namespace Biblioteka.UI
{
    class ClanUI
    {
        static List<Clan> sviClanovi = new List<Clan>();

        public static void IspisiMeniClana()
        {
            Console.WriteLine("Rad sa podacima clanova - opcije: ");
            Console.WriteLine("1 - ispis svih clanova");
            Console.WriteLine("2 - ispis nekog clana po ID");
            Console.WriteLine("3 - dodavanje novog clana");
            Console.WriteLine("4 - menjanje podataka clana");
            Console.WriteLine("5 - brisanje clana");
            Console.WriteLine("Upisite opciju: ");
        }

        public static void IspisiKompletMeniClana()
        {
            bool nastavak = false;

            do
            {
                IspisiMeniClana();
                int izbor = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                switch (izbor)
                {
                    case 1:
                        IspisiSve();
                        break;
                    case 2:
                        IspisiClanaPoId();
                        break;
                    case 3:
                        Dodaj();
                        break;
                    case 4:
                        Osvezi();
                        break;
                    case 5:
                        Izbrisi();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Zelite li da nastavite s radom nad podacima clanova? y/n");
                string odg = Console.ReadLine();

                if (odg == "y")
                {
                    nastavak = true;
                }
                Console.Clear();
            } while (nastavak);
        }

        public static void IspisiSve()
        {
            sviClanovi = ClanDAO.GetAll(Program.conn);
            foreach (Clan clan in sviClanovi)
            {
                Console.WriteLine(clan.ToString());
            }
        }

        public static void IspisiClanaPoId()
        {
            Console.WriteLine("Unesite ID clana: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Clan clan = ClanDAO.GetClanById(Program.conn, id);

            Console.WriteLine(clan.ToString());
        }

        public static void Dodaj()
        {
            Console.WriteLine("Upisite id novog clana: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ime: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Prezime: ");
            string prezime = Console.ReadLine();

            Clan novClan = new Clan(id, ime, prezime);

            ClanDAO.Add(Program.conn, novClan);

        }

        public static void Osvezi()
        {
            Console.WriteLine("Upisite id clana: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Unesite novo ime clanu: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite i prezime:");
            string prezime = Console.ReadLine();

            Clan clan = new Clan(id, ime, prezime);
            ClanDAO.Update(Program.conn, clan);
        }

        public static void Izbrisi()
        {
            Console.WriteLine("Upisite id clana: ");
            int id = Convert.ToInt32(Console.ReadLine());

            ClanDAO.Delete(Program.conn, id);
        }
    }
}
