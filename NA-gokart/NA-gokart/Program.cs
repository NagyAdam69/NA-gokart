using System;
using System.Collections;
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
            public string foglalo_id;
            public Idopontok(DateTime datum, int idopont, string foglalo_id)
            {
                this.datum = datum;
                this.idopont = idopont;
                this.foglalo_id = foglalo_id;
            }
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

        static void VersenyzokListazas(List<Versenyzok> versenyzok)
        {

            string projekt_nev = "NA - Gokart időpontfoglaló - Egyéni kisprojekt";
            Console.WriteLine($"{projekt_nev}\n" +
                              "2026.09.07");
            for (int i = 0; i < projekt_nev.Length; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("\nVERSENYZŐK:\n");

            string leghosszabb_nev = "kis tas";

            for (int i = 0; i < versenyzok.Count; i++)
            {
                string teljes_nev = $"{versenyzok[i].vezeteknev} {versenyzok[i].keresztnev}";
                if (teljes_nev.Length > leghosszabb_nev.Length)
                {
                    leghosszabb_nev = teljes_nev;
                }
            }

            for (int i = 0; i < versenyzok.Count; i++)
            {
                string versenyzo = $"{versenyzok[i].vezeteknev} {versenyzok[i].keresztnev}";
                Console.Write($"{i + 1}.".PadRight(4));
                Console.Write(versenyzo.PadRight(leghosszabb_nev.Length + 2));
                Console.Write($"-({versenyzok[i].szuletesi_ido:yyyy.MM.dd})-");
                Console.WriteLine($"  ID: {versenyzok[i].versenyzo_id}");
            }
        }

        static void Tablazat(List<Idopontok> idopontok)
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

            Console.Write(new string('─', dateColWidth) + "┼");
            for (int hour = startHour; hour <= endHour; hour++)
            {
                Console.Write(new string('─', timeColWidth - 1) + "┼");
            }
            Console.WriteLine();

            for (int day = today.Day; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(today.Year, today.Month, day);

                // Dátum oszlop
                Console.ResetColor();
                Console.Write(currentDate.ToString("yyyy.MM.dd").PadRight(dateColWidth) + "│");

                for (int hour = startHour; hour <= endHour; hour++)
                {
                    int foglalasokSzama = idopontok.Count(x => x.datum == currentDate && x.idopont == hour);

                    // Színezés: ha tele van (20 fő), piros background, egyébként váltakozó zöld
                    if (foglalasokSzama >= 20 && (day + hour) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if(foglalasokSzama >= 20)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if ((day + hour) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write($"{foglalasokSzama,2}/20".PadRight(timeColWidth - 1));

                    Console.ResetColor();
                    Console.Write("│");
                }

                Console.WriteLine();
            }
        }

        static void Kiiras(List<Versenyzok> versenyzok, List<Idopontok> idopontok, string uzenet)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            VersenyzokListazas(versenyzok);
            Tablazat(idopontok);
            Console.WriteLine($"\n{uzenet}");
        }

        static void Main(string[] args)
        {
            DateTime ma = DateTime.Now;

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

            Random gen = new Random();
            DateTime start = new DateTime(1966, 1, 1);
            int range = (new DateTime(2012, 1, 1) - start).Days;

            List<Versenyzok> versenyzok = new List<Versenyzok>();
            for (int i = 0; i < versenyzok_szama; i++)
            {
                string vezeteknev = vezeteknevek[rnd.Next(vezeteknevek.Length)];
                string keresztnev = keresztnevek[rnd.Next(keresztnevek.Length)];
                DateTime szuletesi_ido = start.AddDays(gen.Next(range));
                bool nagykoru = (DateTime.Now.Year - szuletesi_ido.Year) >= 18;
                if (DateTime.Now.Year - szuletesi_ido.Year == 18 && DateTime.Now.DayOfYear < szuletesi_ido.DayOfYear)
                {
                    nagykoru = false;
                }
                string versenyzo_id = $"GO-{EkezetMentesito(vezeteknev)}{EkezetMentesito(keresztnev)}-{szuletesi_ido.Year}{szuletesi_ido.Month.ToString("00")}{szuletesi_ido.Day.ToString("00")}";
                string email = $"{EkezetMentesito(vezeteknev.ToLower())}.{EkezetMentesito(keresztnev.ToLower())}@gmail.com";

                versenyzok.Add(new Versenyzok(vezeteknev, keresztnev, szuletesi_ido, nagykoru, versenyzo_id, email));
            }

            List<Idopontok> idopontok = new List<Idopontok>();
            string uzenet = "";

            while (true)
            {
                Kiiras(versenyzok, idopontok, uzenet);
                uzenet = "";

                Console.Write("Válasszon opciót: \n-(1) Foglalás hozzáadása\n-(2) Foglalás módosítása \n-(3) Kilépés\n:");
                string valasztas = Console.ReadLine();

                if (valasztas == "1")
                {
                    Kiiras(versenyzok, idopontok, uzenet);

                    Console.Write($"Melyik időpontra szeretne foglalni? (nap/óra; pl. 30/9): ");
                    string valasz = Console.ReadLine();
                    int nap = Convert.ToInt32(valasz.Split('/')[0]);
                    int ora = Convert.ToInt32(valasz.Split('/')[1]);

                    if (nap < DateTime.Now.Day || nap > DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) || ora < 8 || ora > 18)
                    {
                        uzenet = "Nincs ilyen opció!";
                        continue;
                    }

                    DateTime targetDatum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, nap);
                    if (idopontok.Count(i => i.datum == targetDatum && i.idopont == ora) >= 20)
                    {
                        uzenet = "Ez az időpont már betelt (elérte a maximális 20 főt)!";
                        continue;
                    }

                    Kiiras(versenyzok, idopontok, uzenet);
                    Console.WriteLine($"({nap}-án, {ora}. órakor)");

                    Console.Write("Hányas számú versenyzőt szeretné foglalni? (1-{0}): ", versenyzok.Count);
                    int sorszam = Convert.ToInt32(Console.ReadLine());

                    if (sorszam < 1 || sorszam > versenyzok.Count)
                    {
                        uzenet = "Nincs ilyen opció!";
                        continue;
                    }

                    string kivalasztottVersenyzoId = versenyzok[sorszam - 1].versenyzo_id;

                    if (idopontok.Any(i => i.foglalo_id == kivalasztottVersenyzoId && i.datum == targetDatum && i.idopont == ora))
                    {
                        uzenet = "Ez a versenyző erre az időpontra már rendelkezik foglalással!";
                        continue;
                    }

                    var versenyzoMaiFoglalásai = idopontok
                        .Where(i => i.foglalo_id == kivalasztottVersenyzoId && i.datum == targetDatum)
                        .Select(i => i.idopont)
                        .ToList();

                    if (versenyzoMaiFoglalásai.Count >= 2)
                    {
                        uzenet = "Ez a versenyző ezen a napon már elérte a maximális 2 foglalást!";
                        continue;
                    }

                    if (versenyzoMaiFoglalásai.Count == 1 && Math.Abs(versenyzoMaiFoglalásai[0] - ora) != 1)
                    {
                        uzenet = $"A két foglalásnak egymást követő órában kell lennie! (Már meglévő foglalás: {versenyzoMaiFoglalásai[0]}:00)";
                        continue;
                    }

                    Idopontok uj_foglalas = new Idopontok(targetDatum, ora, kivalasztottVersenyzoId);
                    idopontok.Add(uj_foglalas);
                    uzenet = $"Sikeres foglalás: {uj_foglalas.datum:yyyy.MM.dd} - {uj_foglalas.idopont}:00 - {uj_foglalas.foglalo_id}";
                }

                if (valasztas == "2")
                {
                    Kiiras(versenyzok, idopontok, uzenet);
                    Console.Write("Keresés időpont alapján (nap/óra; pl 30/9): ");

                    var reszek = Console.ReadLine().Split('/');
                    int nap = Convert.ToInt32(reszek[0]);
                    int ora = Convert.ToInt32(reszek[1]);

                    var keresett_idopontok = idopontok.Where(d => d.datum == new DateTime(ma.Year, ma.Month, nap) && d.idopont == ora).ToList();

                    Kiiras(versenyzok, idopontok, uzenet);
                    Console.WriteLine($"Időpontok: {new DateTime(ma.Year, ma.Month, nap):yyyy.MM.dd} - {ora}:00");

                    if (keresett_idopontok.Count == 0)
                    {
                        Console.WriteLine("Még nincs ehhez az időponthoz foglalás.");
                        Console.Write("ENTER a folytatáshoz");
                        Console.ReadLine();
                        continue;
                    }

                    for (int i = 0; i < keresett_idopontok.Count; i++)
                    {
                        var foglalo = versenyzok.FirstOrDefault(v => v.versenyzo_id == keresett_idopontok[i].foglalo_id);
                        string teljes_nev = foglalo != null ? $"{foglalo.vezeteknev} {foglalo.keresztnev}" : "Ismeretlen";
                        Console.Write($"{i + 1}.".PadRight(3));
                        Console.WriteLine($"{teljes_nev.PadRight(20)} -     {keresett_idopontok[i].foglalo_id}");
                    }

                    Console.Write("\nVálasszon egy opciót: \n-(1) Időpont törlése\n-(2) Időpont megváltoztatása\n:");
                    string opcio = Console.ReadLine();

                    if (opcio == "1")
                    {
                        Console.Write($"Hanyadik sorszámú versenyző foglalását szeretné törölni? (1-{keresett_idopontok.Count}): ");
                        int sorszam = Convert.ToInt32(Console.ReadLine());

                        if (sorszam >= 1 && sorszam <= keresett_idopontok.Count)
                        {
                            var torlendo = keresett_idopontok[sorszam - 1];
                            idopontok.Remove(torlendo);
                            uzenet = $"Sikeres törlés: {torlendo.foglalo_id} foglalása törölve lett.";
                        }
                        else
                        {
                            uzenet = "Nincs ilyen sorszámú foglalás!";
                        }
                    }
                    else if (opcio == "2")
                    {
                        Console.Write($"Hanyadik sorszámú versenyző foglalását szeretné megváltoztatni? (1-{keresett_idopontok.Count}): ");
                        int sorszam = Convert.ToInt32(Console.ReadLine());

                        if (sorszam >= 1 && sorszam <= keresett_idopontok.Count)
                        {
                            var modositando = keresett_idopontok[sorszam - 1];

                            Console.Write("Melyik új időpontra szeretné áthelyezni? (nap/óra; pl. 30/10): ");
                            string ujValasz = Console.ReadLine();
                            var ujReszek = ujValasz.Split('/');
                            int ujNap = Convert.ToInt32(ujReszek[0]);
                            int ujOra = Convert.ToInt32(ujReszek[1]);

                            if (ujNap < DateTime.Now.Day || ujNap > DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) || ujOra < 8 || ujOra > 18)
                            {
                                uzenet = "Érvénytelen új időpont!";
                            }
                            else
                            {
                                DateTime ujDatum = new DateTime(ma.Year, ma.Month, ujNap);

                                if (idopontok.Count(i => i.datum == ujDatum && i.idopont == ujOra) >= 20)
                                {
                                    uzenet = "A választott új időpont már betelt (elérte a maximális 20 főt)!";
                                }
                                else
                                {
                                    var versenyzoUjNapiEgyebFoglalásai = idopontok
                                        .Where(i => i != modositando && i.foglalo_id == modositando.foglalo_id && i.datum == ujDatum)
                                        .Select(i => i.idopont)
                                        .ToList();

                                    if (versenyzoUjNapiEgyebFoglalásai.Count >= 2)
                                    {
                                        uzenet = "A versenyző ezen az új napon már elérte a maximális 2 foglalást!";
                                    }
                                    else if (versenyzoUjNapiEgyebFoglalásai.Count == 1 && Math.Abs(versenyzoUjNapiEgyebFoglalásai[0] - ujOra) != 1)
                                    {
                                        uzenet = $"A két foglalásnak egymást követő órában kell lennie! (Már meglévő foglalása ekkor: {versenyzoUjNapiEgyebFoglalásai[0]}:00)";
                                    }
                                    else
                                    {
                                        modositando.datum = ujDatum;
                                        modositando.idopont = ujOra;
                                        uzenet = $"Sikeres módosítás! Új időpont: {modositando.datum:yyyy.MM.dd} - {modositando.idopont}:00 ({modositando.foglalo_id})";
                                    }
                                }
                            }
                        }
                        else
                        {
                            uzenet = "Nincs ilyen sorszámú foglalás!";
                        }
                    }
                }

                if (valasztas == "3")
                {
                    Kiiras(versenyzok, idopontok, uzenet);
                    Console.WriteLine("Program vége..");
                    break;
                }
            }
        }
    }
}