using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub_GUI
{
    class Tag
    {
        string csaladnev;
        int id;
        string utonev;
        public Tag(int id, string csaladnev, string utonev)
        {
           this.id = id;
            this.csaladnev = csaladnev;
            this.utonev = utonev;

        }
        public int Id { get => id; }
        public string Csaladnev { get => csaladnev; }
        public string Utonev { get => utonev; }
        public string Nev { get => csaladnev + " " + utonev; }

        public override string ToString()
        {
            return  Nev ;
        }
    }
}
