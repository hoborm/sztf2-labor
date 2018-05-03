using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Graf
{
    class Program
    {
        static SzomszedsagiLista szl;
        static CsucsMatrix csm;

        static void Main(string[] args)
        {
            // A szomszédsági listás, illetve a csúcsmátrixos megoldás futási idejének szemléltetésére nagyméretű gráfokat generálunk. A GrafGen metódus két egyforma gráfot hoz létre, különböző tárolási móddal.
            // Ne felejtsük el, hogy a tárolási mód megválasztásakor nem csak a futási időre kell tekintettel lennünk, hanem a memóriaigényre is! A csúcsmátrixok nagyobb csúcsszám esetén nagy mennyiségű memóriát foglalnak!

            //SzomszedsagiLista szl = GrafGenSZL(5000, 50000);
            //CsucsMatrix csm = GrafGenCSM(5000, 50000);

            GrafGen(5000, 50000);

            DateTime start = DateTime.Now; // Elmentjük az aktuális időt
            szl.SzelessegiBejaras(0);
            Console.WriteLine("Szomszédsági lista: " +(DateTime.Now - start).TotalMilliseconds);  // Kiírjuk a jelenlegi, és az elmentett idő között eltelt miliszekundumok számát.
            start = DateTime.Now;
            csm.SzelessegiBejaras(0);
            Console.WriteLine("Csúcsmátrix: " + (DateTime.Now - start).TotalMilliseconds);

            Console.ReadLine();
        }

        static SzomszedsagiLista GrafGenSZL(int meret, int suruseg)
        {
            SzomszedsagiLista szl = new SzomszedsagiLista(meret);
            Random rnd = new Random();
            for (int i = 0; i < suruseg; i++)
            {
                szl.ElFelvetel(rnd.Next(0, meret), rnd.Next(0, meret));
            }

            return szl;
        }

        static CsucsMatrix GrafGenCSM(int meret, int suruseg)
        {
            CsucsMatrix csm = new CsucsMatrix(meret);
            Random rnd = new Random();
            for (int i = 0; i < suruseg; i++)
            {
                csm.ElFelvetel(rnd.Next(0, meret), rnd.Next(0, meret));
            }

            return csm;
        }

        static void GrafGen(int meret, int suruseg)
        {
            Random rnd = new Random();
            csm = new CsucsMatrix(meret);
            szl = new SzomszedsagiLista(meret);
            for (int i = 0; i < suruseg; i++)
            {
                int rand1 = rnd.Next(0, meret);
                int rand2 = rnd.Next(0, meret);

                csm.ElFelvetel(rand1, rand2);
                szl.ElFelvetel(rand1, rand2);
            }
        }
    }

    abstract class Graf
    {
        public int N;

        public Graf(int csucsokszama)
        {
            N = csucsokszama;
        }

        public List<int> Szomszedok(int cs)
        {
            List<int> szomszedok = new List<int>();
            List<int> csucsok = Csucsok();

            for (int i = 0; i < csucsok.Count; i++)
            {
                if (VezetEl(cs, csucsok[i]))
                {
                    szomszedok.Add(csucsok[i]);
                }
            }

            return szomszedok;
        }
                                                // A szélességi bejárás során kiindulunk egy adott csomópontból (int s), majd ennek szomszédain keresztül kezdjük bejárni a gráfot. Az 's' kiinduló pont minden szomszédját egy sorba helyezzük,
        public void SzelessegiBejaras(int s)    // majd ezt követően a sorból egyesével kivesszük az elemeket, amelyeknek a szomszédait szintén belehelyezzük a sorba. Az F listában elhelyezünk minden már vizsgált elemet, elkerülve így, hogy
        {                                       // amennyiben két különböző csúcsnak közös szomszédja van, akkor az adott csúcsot kétszer vizsgáljuk meg.
            List<int> F = new List<int>();
            Queue<int> S = new Queue<int>();

            F.Add(s);
            S.Enqueue(s);
            while (S.Count != 0)
            {
                int k = S.Dequeue();
                //Console.WriteLine(k); 
                List<int> szomszedok = Szomszedok(k);
                for (int i = 0; i < szomszedok.Count; i++)
                {
                    if(!F.Contains(szomszedok[i]))
                    {
                        S.Enqueue(szomszedok[i]);
                        F.Add(szomszedok[i]);
                    }
                }
            }
        }

        void MelysegiBejarasRek(int cs, List<int> F)      // Mélységi bejárás során ugyanazon az elven indulunk el, mint a szélességi bejáráskor, azonban sor helyett itt verem megoldást alkalmazunk. Ezt gyakorlatban viszont csak rekurzívan
        {                                                 // tudjuk megoldani, ahol a verem a tulajdonképpen a rekurzió során megvalósult hívás-hierarchia.
            F.Add(cs);
            Console.WriteLine(cs);
            foreach (int x in Szomszedok(cs))
            {
                if (!F.Contains(x))
                    MelysegiBejarasRek(x, F);
            }
        }

        public void MelysegiBejaras(int start)
        {
            List<int> F = new List<int>();
            MelysegiBejarasRek(start, F);
        }

        public abstract List<int> Csucsok();
        public abstract bool VezetEl(int honnan, int hova);
        public abstract void ElFelvetel(int honnan, int hova);
    }

                                    // A csúcsmátrixhoz létrehozunk egy NxN-es mátrixot, ahol N a csomópontok száma. A mátrix adott cellája tartalmazza, hogy az adott pontból egy adott másikba vezet-e él.
    class CsucsMatrix : Graf        // Pl.: Ha a mátrix [3,18] indexű eleme 1, akkor a 3-as csomópontból vezet él a 18-as csomópontba. Ha az érték 0, akkor nem vezet él. Súlyozott mátrix esetén közvetlenül a súlyt is eltárolhatjuk.
    {
        public int[,] CS;

        public CsucsMatrix(int csucsokszama) : base(csucsokszama)
        {
            CS = new int[csucsokszama, csucsokszama];
        }

        public override List<int> Csucsok()
        {
            List<int> cs = new List<int>();

            for (int i = 0; i < N; i++)
            {
                cs.Add(i);
            }

            return cs;
        }

        public override void ElFelvetel(int honnan, int hova)
        {
            CS[honnan, hova] = 1;
        }

        public override bool VezetEl(int honnan, int hova)
        {
            return CS[honnan, hova] != 0;
        }
    }
                                                // A szomszédsági listákban való tároláshoz listák tömbjét kell létrehoznunk. Annyi üres listát hozunk létre, ahány csúcs van a gráfban. 
    class SzomszedsagiLista : Graf              // Az egyes listák tárolják az adott csúcs szomszédait, tehát azon csúcsok számát, amelyekbe vezet él. Adott csúcs száma == a lista indexe a tömbben.
    {
        List<int>[] L;

        public SzomszedsagiLista(int csucsokszama) : base(csucsokszama)
        {
            L = new List<int>[csucsokszama];
            for (int i = 0; i < L.Length; i++)
            {
                L[i] = new List<int>();
            }
        }

        public override List<int> Csucsok()
        {
            List<int> cs = new List<int>();

            for (int i = 0; i < N; i++)
            {
                cs.Add(i);
            }

            return cs;
        }

        public override void ElFelvetel(int honnan, int hova)
        {
            L[honnan].Add(hova);
        }

        public override bool VezetEl(int honnan, int hova)
        {
            for (int i = 0; i < L[honnan].Count; i++)
            {
                if(L[honnan][i] == hova)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
