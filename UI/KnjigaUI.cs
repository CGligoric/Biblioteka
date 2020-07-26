using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Moduli;
using Biblioteka.DAO;


namespace Biblioteka.UI
{
    class KnjigaUI
    {
        static List<Knjiga> sveKnjige = KnjigaDAO.GetAll(Program.conn);

        public static void IspisiOpcije()
        {
            Console.WriteLine("Rad sa knjigama biblioteke - opcije: ");
            Console.WriteLine("1 - ispis svih knjiga");
            Console.WriteLine("2 - ispis knjige po ID");
            Console.WriteLine("3 - dodavanje nove knjige");
            Console.WriteLine("4 - osvezavanje podataka postojece knjige");
            Console.WriteLine("5 - brisanje knjige");
            Console.WriteLine("6 - pretraga knjiga po nazivu");
        }

        public static void IspisiKompletMeniKnjige()
        {
            bool nastavak = false;

            do
            {
                IspisiOpcije();
                int izbor = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                switch (izbor)
                {
                    case 1:
                        IspisiSve();
                        break;
                    case 2:
                        IspisiPoId();
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
                    case 6:
                        IspisiKnjiguPoNaslovu();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Zelite li da nastavite s radom nad podacima knjiga? y/n");
                string odg = Console.ReadLine();

                if (odg == "y")
                {
                    nastavak = true;
                }
                Console.Clear();
            } while (nastavak);
        }

        //CRUD

        public static void IspisiSve()
        {
            sveKnjige = KnjigaDAO.GetAll(Program.conn);
            foreach (Knjiga knjiga in sveKnjige)
            {
                Console.WriteLine(knjiga.ToString());
            }
        }
        public static void IspisiPoId()
        {
            Console.WriteLine("Upisite id knjige: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Knjiga knjiga = KnjigaDAO.GetKnjigaById(Program.conn, id);

            Console.WriteLine(knjiga.ToString());
        }
        public static void Dodaj()
        {
            Console.WriteLine("Upisite ID nove knjige: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite naslov: ");
            string naslov = Console.ReadLine();

            Console.WriteLine("Upisite ime i prezime autora: ");
            string autor = Console.ReadLine();

            Console.WriteLine("Upisite godinu objavljivanja: ");
            int god = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite broj kopija knjige koju je biblioteka dobila:");
            int brojKopija = Convert.ToInt32(Console.ReadLine());

            Knjiga knjiga = new Knjiga(id, naslov, autor, god, brojKopija);
            KnjigaDAO.Add(Program.conn, knjiga);
        }
        public static void Osvezi()
        {
            Console.WriteLine("Upisite ID knjige: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite nov naslov: ");
            string naslov = Console.ReadLine();

            Console.WriteLine("Upisite novo ime i prezime autora: ");
            string autor = Console.ReadLine();

            Console.WriteLine("Upisite novu godinu objavljivanja: ");
            int god = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Upisite nov broj kopija koje je knjiga dobila: ");
            int brojKopija = Convert.ToInt32(Console.ReadLine());

            Knjiga knjiga = new Knjiga(id, naslov, autor, god, brojKopija);
            KnjigaDAO.Update(Program.conn, knjiga);

        }
        public static void Izbrisi()
        {
            Console.WriteLine("Upisite ID knjige: ");
            int id = Convert.ToInt32(Console.ReadLine());

            KnjigaDAO.Delete(Program.conn, id);
        }

        public static void IspisiKnjiguPoNaslovu()
        {
            Console.WriteLine("Unesite karaktere koji se nalaze u naslovu knjige: ");
            string deoNaslova = Console.ReadLine();

            List<Knjiga> knjige = KnjigaDAO.GetKnjigeByNaslov(Program.conn, deoNaslova);

            foreach (Knjiga knjiga in knjige)
            {
                Console.WriteLine(knjiga.ToString());
            }
        }
    }
}
