using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elso_labor
{
    class Szamitogep
    {

        string processzor;
        int ram;

        
        public string Processzor
        {
            get { return processzor; }
        }
        public int Ram
        {
            get { return ram; }
        }

        public Szamitogep(string processzor,int ram)
        {
            this.processzor = processzor;
            this.ram = ram;
        }
    }
}
