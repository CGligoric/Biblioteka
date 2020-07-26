using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.DAO;
using Biblioteka.UI;

namespace Biblioteka.Moduli
{
    class ClanKnjige
    {
        public int Id { get; set; }
        public Clan Clan { get; set; }
        public Knjiga Knjiga { get; set; }

        public ClanKnjige(int id, Clan clan, Knjiga knjiga)
        {
            Id = id;
            Clan = clan;
            Knjiga = knjiga;
        }

        public override string ToString()
        {
            string str = String.Format("Naslov: {0} | Autor: {1} | Broj kopija: {2} | Imena i prezimena clanova " +
                "pozajmioca: ", Knjiga.Naslov, Knjiga.Autor, Knjiga.BrojKopija);
            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            List<int> listaIdClanova = ClanKnjigeUI.IdClanovaKnjige(Knjiga);

            for (int i = 0; i < listaIdClanova.Count; i++)
            {
                if (listaIdClanova.Count > 1)
                {
                    Clan clan = ClanDAO.GetClanById(Program.conn, listaIdClanova[i]);
                    sb.Append(clan.Ime + " " + clan.Prezime + ",");
                }
                else
                {
                    Clan clan = ClanDAO.GetClanById(Program.conn, listaIdClanova[i]);
                    sb.Append(clan.Ime + " " + clan.Prezime +  " ");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
