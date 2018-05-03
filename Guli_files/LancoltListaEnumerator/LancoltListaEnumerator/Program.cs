using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancoltListaEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            LancoltLista<string> lista = new LancoltLista<string>();
            lista.ElejereBeszur("Józsi");
            lista.ElejereBeszur("Pista");
            lista.ElejereBeszur("Béla");
            lista.ElejereBeszur("Feri");

            foreach(string s in lista)
            {
                Console.WriteLine(s);
            }

            Console.ReadLine();
        }
    }

    // Ahhoz hogy az osztályunkat foreach ciklus segítségével bejárhatóvá tegyük, szükségünk van két interfacere:
    // IEnumerable: vagyis bejárható. Mivel a bejárandó osztályunk a lista, így a láncolt lista osztályunk valósítja meg ezt az interfacet. Egyetlen metódusra van szükségünk, a GetEnumerator metódusra, amely az adatszerkezet bejárását végző Bejáró objektumot adja vissza.
    // IEnumerator: vagyis bejáró. Ez az objektum fogja bejárni a bejárható osztályunkat, vagyis a listát. Ehhez külön létrehozunk egy bejáró osztályt amely megvalósítja ezt az interfacet, majd ennek egy példányát adjuk át a GetEnumerator metódusnak.

    class LancoltLista<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            return new ListaEnumerator(fej);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        class ListaEnumerator : IEnumerator<T>
        {
            ListaElem fej;
            ListaElem akt;

            public ListaEnumerator(ListaElem elso)
            {
                fej = elso;
                akt = null;
            }
            public T Current
            {
                get
                {
                    return akt.tartalom;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            public void Dispose()
            {
                fej = null;
                akt = null;
            }

            public bool MoveNext()
            {
                if(akt == null)
                { akt = fej; }
                else
                {
                    akt = akt.kovetkezo;
                }

                return akt != null;
            }

            public void Reset()
            {
                akt = null;
            }
        }

        class ListaElem
        {
            public ListaElem kovetkezo;
            public T tartalom;
        }

        ListaElem fej;

        public void ElejereBeszur(T elem)
        {
            ListaElem uj = new ListaElem();
            uj.tartalom = elem;
            uj.kovetkezo = fej;
            fej = uj;
        }

        public void VegereBeszur(T elem)
        {
            ListaElem uj = new ListaElem();
            uj.tartalom = elem;

            if (fej == null)
            {
                fej = uj;
            }
            else
            {
                ListaElem p = fej;
                while (p.kovetkezo != null)
                {
                    p = p.kovetkezo;
                }
                p.kovetkezo = uj;
            }
        }

        public void Torol(T elem)
        {
            ListaElem e = null;
            ListaElem p = fej;
            while (p != null && !p.tartalom.Equals(elem))
            {
                e = p;
                p = p.kovetkezo;
            }
            if (p != null)
            {
                if (e == null)
                {
                    fej = fej.kovetkezo;
                }
                else
                {
                    e.kovetkezo = p.kovetkezo;
                }
            }
        }

        public delegate void Muvelet(T elem);
        public void Bejar(Muvelet M)
        {
            ListaElem p = fej;
            while (p != null)
            {
                M(p.tartalom);
                p = p.kovetkezo;
            }
        }


    }
}
