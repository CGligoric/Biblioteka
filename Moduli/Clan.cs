using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Moduli
{
    class Clan
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public Clan() { }

        public Clan(int id, string ime, string prezime)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
        }

        public override string ToString()
        {
            return String.Format("ID: {0} | Ime: {1} | Prezime: {2}", Id, Ime, Prezime);
        }
    }
}
