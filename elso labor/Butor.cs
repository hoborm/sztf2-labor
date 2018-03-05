using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elso_labor
{
    class Butor
    {
        string szin;
        int ar;

        public Butor(string szin,int ar)
        {
            this.szin = szin;
            this.ar = ar;
        }

        public string Szin { get => szin; set => szin = value; }


    }
}
