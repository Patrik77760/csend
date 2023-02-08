using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace QuickCseveges
{
    class Beszelgetes
    {
        public DateTime Kezdete { get; private set; }
        public DateTime Vege { get; private set; }
        public string Kezdemenyezo { get; private set; }
        public string Fogado { get; private set; }
        
        public Beszelgetes(string sor)
        {
            string[] m = sor.Split(';');
            m[0] = m[0].Replace('-','.');
            m[1] = m[1].Replace('-','.');
            Kezdete = DateTime.Parse(m[0]);
            Vege = DateTime.Parse(m[1]);    //:)
            Kezdemenyezo = m[2];
            Fogado = m[3];
        }
    }
}