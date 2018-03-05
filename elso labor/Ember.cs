using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elso_labor
{

    enum Gender { ferfi,no}
    class Ember
    {
       public static string[] beosztasok = new string[] { "fejleszto", "tesztelo", "hr", "vezeto" };
        string nev, beosztas;
        int teljesitmeny, berezes;
        Gender nem;
        static Random rnd = new Random();

        public string Nev { get => nev; set => nev = value; }
        public string Beosztas { get => beosztas; set => beosztas = value; }

        public Ember(string nev,string beosztas)
        {
            this.nev = nev;
            this.beosztas = beosztas;
            this.nem = (Gender)rnd.Next(0, 2);
          
        }
        public int Berezes
        {
            set
            {
                switch (beosztas)
                {
                    case "fejleszto":
                        berezes = rnd.Next(3000, 4001);
                        break;
                    case "vezeto":
                        berezes = rnd.Next(6000, 7001);
                        break;
                    case "hr":
                        berezes = rnd.Next(2500, 3501);
                        break;
                    case "tesztelo":
                        berezes = rnd.Next(2000, 3001);
                        break;
                }


            }
        }

            public int Teljesitmeny
        {
            get
            {
                return teljesitmeny;
            }
            set => teljesitmeny = rnd.Next(70, 101);
        }
        }
    }

