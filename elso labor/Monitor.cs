using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elso_labor
{
    class Monitor
    {

        string felbontas, gyarto;

        public Monitor(string felbontas, string gyarto)
        {
            this.felbontas = felbontas;
            this.gyarto = gyarto;
        }

        public string Felbontas
        {
            get { return felbontas; }

        }

        public string Gyarto
        {
            get { return gyarto; }
           
        }

       
    }
}
