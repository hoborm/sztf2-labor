using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elso_labor
{
    class Asztal:Butor
    {
        Monitor monitor;
        Szamitogep szamitogep;
        Ember ember;
        string szin;
        int ar;


        public Asztal(string szin,int ar): base (szin,ar)
        {
            this.szin = szin;
            this.ar = ar;
        }

        public Monitor Monitor { get => monitor; set => monitor = value; }
        public Szamitogep Szamitogep { get => szamitogep; set => szamitogep = value; }
        public Ember Ember { get => ember; set => ember = value; }
    }
}
