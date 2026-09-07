using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA_gokart
{
    internal class Program
    {
        public class Versenyzok
        {
            public string vezeteknev;
            public string keresztnev;
            public DateTime szuletesi_ido;
            public bool nagykoru;
            public string versenyzo_id;
            public string email;
            public Versenyzok(string vezeteknev, string keresztnev, DateTime szuletesi_ido, bool nagykoru, string versenyzo_id, string email)
            {
                this.vezeteknev = vezeteknev;
                this.keresztnev = keresztnev;
                this.szuletesi_ido = szuletesi_ido;
                this.nagykoru = nagykoru;
                this.versenyzo_id = versenyzo_id;
                this.email = email;
            }
        }
        static DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1966, 1, 1);
            int range = (new DateTime(2012, 1, 1) - start).Days;
            return start.AddDays(gen.Next(range));
        }
        static void Main(string[] args)
        {
            Console.WriteLine("NA - Gokart időpontfoglaló - Egyéni kisprojekt\n" +
                              "2026.09.07");
            string go_nev = "NagyiCart";
            string go_cim = "6969 Taktaharkány, Gáspár utca 12.";
            string go_tel = "+36-20-378-9791";
            string go_domain = "www.nagyi-kart.hu";

            Random rnd = new Random();
            int versenyzok_szama = rnd.Next(1, 150);

            StreamReader sr = new StreamReader("vezeteknevek.txt");
            string sor = sr.ReadLine().Replace(" ", "").Replace("'", "");
            string[] vezeteknevek = sor.Split(',');

            StreamReader stre = new StreamReader("keresztnevek.txt");
            string sor2 = stre.ReadLine().Replace(" ", "").Replace("'", "");
            string[] keresztnevek = sor2.Split(',');

            List<Versenyzok> versenyzok = new List<Versenyzok>();
            for (int i = 0; i < versenyzok_szama; i++)
            {
                string vezeteknev = vezeteknevek[rnd.Next(vezeteknevek.Length)];
                string keresztnev = keresztnevek[rnd.Next(keresztnevek.Length)];
                DateTime szuletesi_ido = RandomDay();
                bool nagykoru = (DateTime.Now.Year - szuletesi_ido.Year) >= 18 && szuletesi_ido.Month <= DateTime.Now.Month && szuletesi_ido.Day <= DateTime.Now.Day;
                string versenyzo_id = $"GO-{vezeteknev}";

            }

            
    }
    }
}
