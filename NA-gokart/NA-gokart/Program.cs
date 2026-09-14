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

        public class Idopontok
        {
            public DateTime datum;
            public int idopont;
            public int foglalasok;
            public Idopontok(DateTime datum, int idopont, int foglalasok)
            {
                this.datum = datum;
                this.idopont = idopont;
                this.foglalasok = foglalasok;
            }
        }
        static DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1966, 1, 1);
            int range = (new DateTime(2012, 1, 1) - start).Days;
            return start.AddDays(gen.Next(range));
        }

        static string EkezetMentesito(string stoveg)
        {
            string ekezetes_karakterek = "áéíóöőúüűÁÉÍÓÖŐÚÜŰ";
            string ekezet_nelkuli_karakterek = "aeiooouuuAEIOOOUUU";

            StringBuilder sb = new StringBuilder(stoveg);
            for (int i = 0; i < ekezetes_karakterek.Length; i++)
            {
                sb.Replace(ekezetes_karakterek[i], ekezet_nelkuli_karakterek[i]);
            }
            return sb.ToString();
        }
        static void Tablazat()
        {
            DateTime today = DateTime.Today;
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            int startHour = 8;
            int endHour = 18;

            int dateColWidth = 12;
            int timeColWidth = 7;

            // Fejléc kiírása
            Console.ResetColor();
            Console.Write("\nDátum".PadRight(dateColWidth + 1) + "│");
            for (int hour = startHour; hour <= endHour; hour++)
            {
                Console.Write($"{hour}:00".PadLeft(timeColWidth - 1).PadRight(timeColWidth - 1) + "│");
            }
            Console.WriteLine();

            // Fejléc alatti elválasztó vonal
            Console.Write(new string('─', dateColWidth) + "┼");
            for (int hour = startHour; hour <= endHour; hour++)
            {
                Console.Write(new string('─', timeColWidth - 1) + "┼");
            }
            Console.WriteLine();

            // Sorok generálása a mai naptól a hónap végéig
            for (int day = today.Day; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(today.Year, today.Month, day);

                // Dátum oszlop
                Console.ResetColor();
                Console.Write(currentDate.ToString("yyyy.MM.dd").PadRight(dateColWidth) + "│");

                // Cellák kirajzolása váltakozó zöld háttérrel és elválasztó vonallal
                for (int hour = startHour; hour <= endHour; hour++)
                {
                    // Sötétzöld és zöld cellák váltakozása a jobb láthatóságért
                    if ((day + hour) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    // Cella tartalma (szóközökkel kitöltve)
                    Console.Write(" ".PadRight(timeColWidth - 1));

                    // Elválasztó függőleges vonal visszaállított színekkel
                    Console.ResetColor();
                    Console.Write("│");
                }

                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            /*
             NA- Gokart időpontfoglaló - Egyéni kisprojekt
             2026.09.07
             */

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

            string ekezetes_karakterek = "áéíóöőúüűÁÉÍÓÖŐÚÜŰ";
            string ekezet_nelkuli_karakterek = "aeiooouuuAEIOOOUUU";

            List<Versenyzok> versenyzok = new List<Versenyzok>();
            for (int i = 0; i < versenyzok_szama; i++)
            {
                string vezeteknev = vezeteknevek[rnd.Next(vezeteknevek.Length)];
                string keresztnev = keresztnevek[rnd.Next(keresztnevek.Length)];
                DateTime szuletesi_ido = RandomDay();
                bool nagykoru = (DateTime.Now.Year - szuletesi_ido.Year) >= 18;
                if (DateTime.Now.Year - szuletesi_ido.Year == 18 && DateTime.Now.DayOfYear < szuletesi_ido.DayOfYear)
                {
                    nagykoru = false;
                }
                string versenyzo_id = $"GO-{EkezetMentesito(vezeteknev)}{EkezetMentesito(keresztnev)}-{szuletesi_ido.Year}{szuletesi_ido.Month.ToString("00")}{szuletesi_ido.Day.ToString("00")}";
                string email = $"{EkezetMentesito(vezeteknev.ToLower())}.{EkezetMentesito(keresztnev.ToLower())}@gmail.com";
                
                versenyzok.Add(new Versenyzok(vezeteknev, keresztnev, szuletesi_ido, nagykoru, versenyzo_id, email));
            }
            string projekt_nev = "NA - Gokart időpontfoglaló - Egyéni kisprojekt";
            Console.WriteLine($"{projekt_nev}\n" +
                              "2026.09.07");
            for (int i = 0; i < projekt_nev.Length; i++)
            {
                Console.Write("*");
            }
            Tablazat(); 
        }
    }
}
