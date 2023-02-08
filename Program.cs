using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace QuickCseveges
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Beszelgetes> beszelgetesek = new List<Beszelgetes>();
            beszelgetesek = File.ReadAllLines("csevegesek.txt").Skip(1)
                .Select(b => new Beszelgetes(b)).ToList();
            var tagok = File.ReadAllLines("tagok.txt").ToList();

            Console.WriteLine("4. feladat: Tagok száma: {0} fő - Beszélgetések: {1} db", tagok.Count, beszelgetesek.Count);
            var leghoszabb = beszelgetesek.OrderByDescending(b => b.Vege.Ticks - b.Kezdete.Ticks).First();
            Console.WriteLine("5. feladat: leghosszabb beszélgetés adatai");
            Console.WriteLine("\tKezdeményező:\t" + leghoszabb.Kezdemenyezo);
            Console.WriteLine("\tFogadó:\t" + leghoszabb.Fogado);
            Console.WriteLine("\tKezdete:\t" + leghoszabb.Kezdete);
            Console.WriteLine("\tVége:\t" + leghoszabb.Vege);
            Console.WriteLine("\tHossz:\t" + (leghoszabb.Vege - leghoszabb.Kezdete).TotalSeconds + "mp");

            Console.Write("6. feladat: Adja meg egy tag nevét:");
            var input = Console.ReadLine();
            Console.Write("A beszélgetések összes ideje: ");
            if (input is "" || !tagok.Contains(input!)) Console.WriteLine("00:00:00");
            else
            {
                var beszTicks = beszelgetesek.Where(b => b.Kezdemenyezo == input || b.Fogado == input).Sum(b => (b.Vege.Ticks - b.Kezdete.Ticks));
                var beszTring = new TimeSpan(beszTicks).ToString();
                Console.WriteLine(beszTring);
            }

            var csendesek = tagok.Where(t => !beszelgetesek.Any(b => b.Kezdemenyezo == t || b.Fogado == t)).ToList();
            Console.WriteLine("7. feladat: Nem beszélgettek senkivel");
            csendesek.ForEach(t => Console.WriteLine('\t'+t));

            
            var startTime = DateTime.Parse("2021.09.27 15:00:00");
            var endTime = beszelgetesek.Max(b =>b.Vege);
            
            /*var csendesIdok = beszelgetesek.Select(b =>
            {
                var veg = b.Vege;
                var next = beszelgetesek.SkipWhile(c => c.Kezdete < veg).FirstOrDefault();
                var nextKezd = next?.Kezdete;
                var csend = beszelgetesek.IndexOf(next) == beszelgetesek.IndexOf(b)+1;
                return $"{veg} => {nextKezd}; csend?: {csend}";
            }).ToList();*/
            
            var csendesIdok = beszelgetesek.Where(b =>
            {
                var veg = b.Vege;
                var next = beszelgetesek.SkipWhile(c => c.Kezdete < veg).FirstOrDefault();
                var nextKezd = next?.Kezdete;
                var csend = beszelgetesek.IndexOf(next) != beszelgetesek.IndexOf(b)+1;
                return csend && nextKezd.HasValue;
            }).Select(f => { 
                var veg = f.Vege;
                var next = beszelgetesek.SkipWhile(c => c.Kezdete < veg).FirstOrDefault();
                var nextKezd = next?.Kezdete;
                return new KeyValuePair<DateTime, DateTime>(veg, nextKezd.Value);
                }).ToList();

            var maxCsend = csendesIdok.Max(i => {
                
                if(csendesIdok.Last().Value == i.Value) return 0;
                return csendesIdok.ElementAt(csendesIdok.IndexOf(i)+1).Value.Ticks-i.Key.Ticks;
                });
            Console.WriteLine("Max csend: {0} ???", TimeSpan.FromTicks(maxCsend));
        }

    } 
}
