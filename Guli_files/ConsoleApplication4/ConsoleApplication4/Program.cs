using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public enum Tipus { Preorder, Inorder, Postorder }

    class Program
    {
        static void Main(string[] args)
        {
            BinarisFa<string, int> fa = new BinarisFa<string, int>();

            fa.Beszur("Pista", 50);
            fa.Beszur("Béla", 40);
            fa.Beszur("Józsi", 60);
            fa.Beszur("Feri", 30);
            fa.Beszur("Jani", 20);
            fa.Beszur("Gábor", 35);
            fa.Beszur("Aladár", 10);
            fa.Beszur("Tóni", 70);
            fa.Beszur("Sanyi", 65);

            Console.WriteLine("InOrder bejárás: ");
            fa.Bejaras(Tipus.Inorder);
            Console.WriteLine("\nPreOrder bejárás: ");
            fa.Bejaras(Tipus.Preorder);
            Console.WriteLine("\nPostOrder bejárás: ");
            fa.Bejaras(Tipus.Postorder);
            Console.ReadLine();
        }
    }

    class BinarisFa<T,K> where K : IComparable          // A T generikus típus határozza meg, hogy milyen adatokat szeretnénk tárolni a fában. Mivel bizonyos adattípusokat (pl. saját típusok, vagy stringek) nem tudunk egyértelműen növekvő
    {                                                   // sorrendbe helyezni, ezért szükségünk van egy K generikus típusra, amelyet kulcsként használhatunk. Mivel kötelezzük a K típust arra, hogy valósítsa meg az IComparable interfészt,
        class Csomopont                                 // biztosítjuk, hogy a kulcs olyan típus lehet csak, amelyet összehasonlíthatunk, és ezáltal sorrendbe állíthatjuk a fának az elemeit. 
        {
            public K kulcs;
            public T tartalom;
            public Csomopont jobb;
            public Csomopont bal;

            public Csomopont(K kulcs, T tartalom)
            {
                this.kulcs = kulcs;
                this.tartalom = tartalom;
            }
        }

        Csomopont gyoker;                                   // A gyökér a fa kiemelt eleme, szerepe megegyezik a láncolt lista fej elemével, vagyis minden esetben ettől az elemtől kezdve indulunk el.

        public void Beszur(T elem, K kulcs)                 // Kettő Beszur metódusra van szükségünk, mivel a gyökérhez csak a Fa osztály metódusai férnek hozzá, kívülről nem adhatjuk át paraméterként. A publikus Beszur metódus meghívja
        {                                                   // a private Beszur metódust, és átadja neki paraméterként a fa gyökerét. Ezt követően a private Beszur metódus rekurzívan halad a fán a kulcsok összehasonlításával egészen addig,
            Beszur(ref gyoker, elem, kulcs);                // ameddig az if(akt == null) feltétel nem teljesül. Ha a feltétel teljesül, az akt változóban tárolt pozíció lesz az új elem helye a fában.
        }

        void Beszur(ref Csomopont akt, T elem, K kulcs)
        {
            if(akt == null)
            {
                akt = new Csomopont(kulcs, elem);
            }
            else
            {
                if(akt.kulcs.CompareTo(kulcs) < 0)          // Ha az új elem kulcsa nagyobb mint az aktuális elem kulcsa, az elem jobb ágán haladunk tovább. Amennyiben kisebb, a baloldali ágon.
                {
                    Beszur(ref akt.jobb, elem, kulcs);
                }
                else if (akt.kulcs.CompareTo(kulcs) > 0)
                {
                    Beszur(ref akt.bal, elem, kulcs);
                }
            }
        }

        // Bináris fa bejárásakor három különböző bejárást különböztetünk meg:
        // - Preorder  -  A művelet (ebben a példában kiíratás) az elem aktuális tartalmán hajtódik végre először, ezt követően pedig elindul a baloldali ágon, majd onnan visszatérve a jobboldali ágon.
        // - Inorder   -  A bejárás először végigmegy a baloldali ágon, majd amennyiben elérte a "legtávolabbi" elemet, végrehajtja a műveletet, majd ezt követően indul el a jobboldali ágon.
        // - Postorder -  A bejárás végigmegy a baloldali ágon, majd a jobboldali ágon is, ezt követően pedig végrehajtja a műveletet.

        public void Bejaras(Tipus t)        // A bejárás függvény publikus, ennek segítségével hívjuk meg a tényleges bejárást végző algoritmust.
        {
            if(t == Tipus.Inorder)
            {
                InOrder(gyoker);
            }
            else if (t == Tipus.Postorder)
            {
                PostOrder(gyoker);
            }
            else
            {
                PreOrder(gyoker);
            }
        }

        void InOrder(Csomopont cs)
        {
            if(cs != null)             
            {
                InOrder(cs.bal);
                Console.WriteLine(cs.tartalom);
                InOrder(cs.jobb);
            }
        }

        void PreOrder(Csomopont cs)
        {
            if (cs != null)
            {
                Console.WriteLine(cs.tartalom);
                PreOrder(cs.bal);
                PreOrder(cs.jobb);
            }
        }

        void PostOrder(Csomopont cs)
        {
            if (cs != null)
            {
                PostOrder(cs.bal);
                PostOrder(cs.jobb);
                Console.WriteLine(cs.tartalom);
            }
        }


    }
}
