using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elso_labor
{
    class Program
    {
        static void Main(string[] args)
        {
            Asztal[,] iroda = new Asztal[8, 10];
            IrodaFeltolt(iroda);
            Megjelenit(iroda);
            Console.ReadKey();

        }
        static Random rnd = new Random();
        static public void IrodaFeltolt(Asztal[,] asztalok)
        {
            for (int i = 0; i < asztalok.GetLength(0); i++)
            {
                for (int j = 0; j < asztalok.GetLength(1); j++)
                {
                    Monitor screen = new Monitor("4k", "LG");
                    Szamitogep szg = new Szamitogep("AMD FX", 16);
                    Ember ember = new Ember("Bela", Ember.beosztasok[rnd.Next(0, 3)]);
                    asztalok[i, j] = new Asztal("fekete",10000);
                    asztalok[i, j].Ember = ember;
                    asztalok[i, j].Monitor = screen;
                    asztalok[i, j].Szamitogep = szg;
                }
            }
        }

        static public void Eloleptet(Asztal[,] asztalok)
        { int db = 3;


            do
            {
                int x = rnd.Next(0, asztalok.GetLength(0));
                int y = rnd.Next(0, asztalok.GetLength(1));
                if (asztalok[x,y].Ember.Beosztas!="vezeto")
                {
                    asztalok[x, y].Ember.Beosztas = "vezeto";
                    db--;
                }
            } while (db>0);
                
                   }

        static public void Megjelenit(Asztal[,] iroda)
        {
            for (int i = 0; i < iroda.GetLength(0); i++)

            {
                for (int j = 0; j < iroda.GetLength(1); j++)
                {
                    switch (iroda[i,j].Ember.Beosztas)
                    {
                        case "fejleszto":Console.Write("F\t");
                            break;
                        case "vezeto":Console.Write("V\t");
                            break;
                        case "hr":Console.Write("H\t");
                            break;
                        case "tesztelo":Console.Write("T\t");
                            break;
                        default: Console.Write("");
                            break;
                    }



                }
                Console.WriteLine();
            }
        }



        static public void Lustalkodas(Asztal[,]iroda)
        {
            int lustalkodas = 5;

            iroda[rnd.Next(0, iroda.GetLength(0)), rnd.Next(0, iroda.GetLength(1))].Ember.Teljesitmeny -= 25;
        }
        }

        static public void Kirugas(Asztal[]iroda)
        {
        for (int i = 0; i < iroda.GetLength(0); i++)
        {

            for (int j = 0; j < iroda.GetLength(1); j++)
            {

            }
        }
        }
    }
}
