using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace footgolf
{

    class versenyzo
    {
        public string nev { get; private set; }
        public string kategoria { get; private set; }
        public string egyesulet { get; private set; }
        public byte[] pontok { get; set; }

        public versenyzo(string sor)
        {
            string[] m = sor.Split(';');
            nev = m[0];
            kategoria = m[1];
            egyesulet = m[2];
            pontok = new byte[8];
            for (int i = 0; i < pontok.Length; i++)
            {
                pontok[i] = byte.Parse(m[i + 3]);
            }
        }

        public int osszpontszam
        {
            get
            {
                int osszpont = 0;
                Array.Sort(pontok);
                for (int i = 2; i < pontok.Length; i++)
                {
                    osszpont += pontok[i];
                }
                if (pontok[0] != 0) osszpont += 10;
                if (pontok[1] != 0) osszpont += 10;
                return osszpont;
            }
        }
        static void Main(string[] args)
        {

            double versenyzokszama = 0;
            List<versenyzo> vers = new List<versenyzo>();
            foreach (var i in File.ReadAllLines("fob2016.txt"))
            {
                vers.Add(new versenyzo(i));
            }
            versenyzokszama = vers.Count();
            Console.WriteLine("3. feladat: Versenyzők száma: {0}", versenyzokszama);
             double noiversenyzokszama = 0;
            foreach (var i in vers)
            {
                if (i.kategoria == "Noi") noiversenyzokszama++;
            }

            Console.WriteLine("4. feladat: A női versenyzők aránya: {0}%",
           Math.Round((noiversenyzokszama / versenyzokszama * 100), 2));
            int maxpont = 0;
            string bajnoknoegyesulet = "";
            string bajnoknonev = "";
            foreach (var i in vers)
            {
                if (i.kategoria == "Noi" && i.osszpontszam > maxpont)
                {
                    bajnoknonev = i.nev;
                    bajnoknoegyesulet = i.egyesulet;
                    maxpont = i.osszpontszam;
                }
            }
            if (maxpont != 0)
            {
                Console.WriteLine("6. feladat: A bajnok női versenyző");
                Console.WriteLine("\tNév: {0}", bajnoknonev);
                Console.WriteLine("\tEgyesület: {0}", bajnoknoegyesulet);
                Console.WriteLine("\tÖsszpont: {0}", maxpont);
            }
        List<string> kiirsor = new List<string>();
            foreach (var i in vers)
            {
                if (i.kategoria == "Felnott ferfi")
                {
                    kiirsor.Add($"{i.nev};{i.osszpontszam}");
                }
            }
            File.WriteAllLines("osszpontFF.txt", kiirsor);
        Console.WriteLine("8. feladat: Egyesület statisztika");
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (var i in vers)
            {
                if (d.ContainsKey(i.egyesulet))
                {
                    d[i.egyesulet]++;
                }
                else
                {
                    d.Add(i.egyesulet, 1);
                }
            }
            foreach (var i in d)
            {
                if (i.Key != "n.a." && i.Value >= 3)
                {
                    Console.WriteLine("\t{0} - {1} fő", i.Key, i.Value);
                }
            }

            Console.ReadKey();
        }
    }
}