using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footgolf
{
    class versenyzo
    {
        public string nev { get; set; }
        public string kategoria { get; set; }
        public string egyesulet { get; set; }
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
    }
}
