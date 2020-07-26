using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.DAO;


namespace Biblioteka.Moduli
{
    class Knjiga
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public int GodIzdavanja { get; set; }
        public int BrojKopija { get; set; }

        public Knjiga()
        {

        }

        public Knjiga(int id, string naslov, string autor, int godIzd, int brKopija)
        {
            Id = id;
            Naslov = naslov;
            Autor = autor;
            GodIzdavanja = godIzd;
            BrojKopija = brKopija;
        }
        public override string ToString()
        {
           return String.Format("ID: {0} | Naslov: {1} | Autor: {2} | Godina objavljivanja: {3} | Broj kopija na lageru: {4}",
                            Id, Naslov, Autor, GodIzdavanja, BrojKopija);
        }
    }
}
