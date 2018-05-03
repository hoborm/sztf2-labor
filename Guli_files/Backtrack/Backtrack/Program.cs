using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backtrack
{
    class Program
    {
        const int QN = 8;                           // Ez a konstans határozza meg a tábla méretét. Írjuk át, ha szeretnénk különböző méretű táblákon kísérletezni. A megoldások száma exponenciálisan nő,
                                                    // így 10 felett már többezres eredményhalmazra kell számítanunk!
        static int[] eredmeny = new int[QN];        // Az eredmeny tömb fogja tárolni a megoldásokat. A tömb indexe a tábla oszlopa, a benne található érték pedig a tábla sorát jelöli. Így valójában egy kétdimenziós problémát
                                                    // leképeztünk egydimenziósra.
        static int solutions = 0;                   // A solutions változónk fogja tárolni az eddig megtalált megoldások számát. 

        static void Main(string[] args)
        {
            Backtrack(0);                           // A Backtrack függvényt a 0. szintről kezdjük!
            Console.ReadLine();

        }
       
        static void Show(int[] tomb)                // A megjelenítésért felelős metódusunk. A tényleges bejárásban nem vesz részt.
        {
            for (int i = 0; i < tomb.Length; i++)
            {
                for (int j = 0; j < tomb.Length; j++)
                {
                    if (tomb[j] == i)
                    { Console.Write("Q\t"); }
                    else
                    { Console.Write("_\t"); }
                }
                Console.Write("\n");
            }
        }
       
        static void Backtrack(int szint)            // szint : A tábla oszlopa
        {                                           // i : A vizsgált oszlop eleme (tehát sora)
            int i = 0;                              // k : Korábbi esetek száma (vagyis hány királynőt helyeztünk már el)
            do
            {
                
                if (FT(szint, i))                   // Az FT függvény a 8 királynő problémánál minden esetben igazat ad vissza, hiszen az adott oszlopon belül szabadon elhelyezhetőek a királynők, kizárólag
                {                                   // a korábbi királynők helyétől függ. Ezt az FK függvény vizsgálja.
                    int k = 0;
                    while(k < szint && FK(szint,i,k,eredmeny[k]))   // Végigiterálunk az összes korábbi eseten (k < szint), és minden esetnél megvizsgáljuk, hogy a jelenleg kiválasztott mezőnk (szint, i), ütközik-e az adott korábbi
                    {                                               // esettel (k, eredmeny[k]).
                        k++;                                        // Ha nincs ütközés, a következő korábbi esetre ugrunk, amíg el nem fogynak.
                    }
                    if(k == szint)                                  // Megvizsgáljuk, hogy az előző ciklus megvizsgált-e minden korábbi esetet (k == szint). Ha nem, akkor volt olyan korábbi eset, amely ütközött az aktuális mezővel.
                    {                                               // Ha ütközés volt, növeljük i értékét 1-gyel, és ismét megvizsgáljuk a korábbi esetekre.

                        eredmeny[szint] = i;                        // Amennyiben az előző feltétel igaz volt, az eredménytömbben eltároljuk az aktuális mező pozícióját (oszlop = szint, sor = i).

                        //Console.Clear();
                        //Show(eredmeny);   
                        //Thread.Sleep(50);

                        if (szint == QN-1)                          // Amennyiben elértük az utolsó oszlopot, az azt jelenti, hogy minden oszlopban sikeresen találtunk olyan mezőt, ahova ütközés nélkül elhelyezhettünk királynőt.
                        {                                           // Ezt követően írjuk ki a megoldást a képernyőre (Esetleg tároljuk el későbbi használatra. Ehhez célszerű listát alkalmazni).
                            solutions++;
                            Console.WriteLine(solutions + ". megoldás: ");
                            Show(eredmeny);
                            Console.WriteLine();
                        }
                        else
                        {
                            Backtrack(szint + 1);                   // Amennyiben nem értük el még az utolsó oszlopot, hívjuk meg újra a függvényt a következő oszlopra. Ez a rekurzív hívás. Amennyiben a hívásstruktúrában soron következő
                        }                                           // Backtrack függvény lefutott, erre a sorra tér vissza az őt megelőző függvény, és folytatja futását innen.
                    }
                    
                }
                i++;
            }
            while (i < QN);                                         
        }

        static bool FT(int x1, int y1)
        {
            return true;
        }

        static bool FK(int x1, int y1, int x2, int y2)
        {
            if(x1==x2)                                                        // Ha azonos sorban van a két vizsgált királynő, térjen vissza hamis értékkel.
            { return false; }

            if(y1==y2)                                                        // Ha azonos oszlopban van a két vizsgált királynő, térjen vissza hamis értékkel.
            { return false; }

            if((Math.Abs(x1 - x2) == Math.Abs(y1 - y2)))                      // Ha átlóban üti egymást a két vizsgált királynő, térjen vissza hamis értékkel.
            { return false; }

            return true;                                                      // Minden egyéb esetben térjen vissza igaz értékkel.
        }
    }
}
