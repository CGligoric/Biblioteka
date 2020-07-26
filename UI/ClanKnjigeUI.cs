using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Moduli;
using Biblioteka.DAO;

namespace Biblioteka.UI
{
    class ClanKnjigeUI
    {
        static List<ClanKnjige> sviCK = ClanKnjigeDAO.GetAll(Program.conn);

        public static void IspisiMeni()
        {
            Console.WriteLine("1 - ispis pozajmice po id");
            Console.WriteLine("2 - ispis pozajmica");
            Console.WriteLine("3 - dodavanje nove pozajmice");
            Console.WriteLine("4 - osvezavanje podataka pozajmice");
            Console.WriteLine("5 - brisanje pozajmice");
        }

        public static void IspisiKompletMeniCK()
        {
            bool nastavak = false;
            do
            {
                IspisiMeni();
                int izbor = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                switch (izbor)
                {
                    case 1:
                        IspisiCkPoId();
                        break;
                    case 2:
                        IspisiSve();
                        break;
                    case 3:
                        Dodaj();
                        break;
                    case 4:
                        Osvezi();
                        break;
                    case 5:
                        Obrisi();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Zelite li da nastavite s radom nad podacima pozajmica? y/n");
                string odg = Console.ReadLine();

                if (odg == "y")
                {
                    nastavak = true;
                }
                Console.Clear();
            } while (nastavak);
        }

        public static void IspisiCkPoId()
        {
            Console.WriteLine("Upisite id pozajmice: ");
            int id = Convert.ToInt32(Console.ReadLine());

            ClanKnjige ck = ClanKnjigeDAO.GetClanKnjigaById(Program.conn, id);

            Console.WriteLine(ck.ToString());
        }

        public static void IspisiSve()
        {
            StringBuilder sb = new StringBuilder();


            for (int i = 0; i < sviCK.Count; i++)
            {
                if (sb.ToString().Contains(sviCK[i].ToString()))
                {
                    continue;
                }
                Console.WriteLine(sviCK[i].ToString() + "\n");
                sb.Append(sviCK[i].ToString());
            }
        }

        public static int BrojPozajmiocaPoKnjizi(Knjiga knjiga)
        {
            int brojPozajmioca = 0;
            for (int i = 0; i < sviCK.Count; i++)
            {
                if (sviCK[i].Knjiga.Id == knjiga.Id)
                {
                    brojPozajmioca++;
                }
            }
            return brojPozajmioca;
        }

        public static List<int> IdClanovaKnjige(Knjiga knjiga)
        {
            List<int> sviPozajmiociKnjige = new List<int>();
            for (int i = 0; i < sviCK.Count; i++)
            {
                if (sviCK[i].Knjiga.Id == knjiga.Id)
                {
                    sviPozajmiociKnjige.Add(sviCK[i].Clan.Id);
                }
            }
            return sviPozajmiociKnjige;
        }

        public static void Dodaj()
        {
            Console.WriteLine("Upisite ID nove pozajmice");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite ID pozajmljene knjige: ");
            int knjigaId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite ID clana pozajmioca za knjigu (ID:" + knjigaId + "):");
            int clanId = Convert.ToInt32(Console.ReadLine());

            Clan clan = ClanDAO.GetClanById(Program.conn, clanId);
            Knjiga knjiga = KnjigaDAO.GetKnjigaById(Program.conn, knjigaId);

            if (GetBrojKnjigaZaClana(clan) >= 4)
            {
                Console.WriteLine("Taj clan vec ima maksimalan dozvoljen broj pozajmljenih knjiga (4).");
                return;
            }

            ClanKnjige ck = new ClanKnjige(id, clan, knjiga);
            ClanKnjigeDAO.Add(Program.conn, ck);
        }

        public static void Osvezi()
        {
            Console.WriteLine("Upisite ID pozajmice");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite nov ID pozajmljene knjige: ");
            int knjigaId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite ID novog clana pozajmioca za knjigu (ID:" + knjigaId + "):");
            int clanId = Convert.ToInt32(Console.ReadLine());

            Clan clan = ClanDAO.GetClanById(Program.conn, clanId);
            Knjiga knjiga = KnjigaDAO.GetKnjigaById(Program.conn, knjigaId);

            if (GetBrojKnjigaZaClana(clan) == 4)
            {
                Console.WriteLine("Taj clan vec ima maksimalan dozvoljen broj pozajmljenih knjiga (4).");
                return;
            }

            ClanKnjige ck = new ClanKnjige(id, clan, knjiga);
            ClanKnjigeDAO.Update(Program.conn, ck);
        }

        public static int GetBrojKnjigaZaClana(Clan clan)
        {
            int brojKnjiga = 1;
            List<Knjiga> sveKnjige = KnjigaDAO.GetAll(Program.conn);

            for (int i = 0; i < sviCK.Count; i++)
            {
                if (clan.Id == sviCK[i].Clan.Id)
                {
                    brojKnjiga++;
                }
            }
            return brojKnjiga;
        }

        public static void Obrisi()
        {
            Console.WriteLine("Upisite ID pozajmice");
            int id = Convert.ToInt32(Console.ReadLine());

            ClanKnjigeDAO.Delete(Program.conn, id);
        }
    }
}
