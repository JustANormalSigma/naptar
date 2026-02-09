using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NaptariFeladat
{
    struct Esemeny
    {
        public string tulajdonos;
        public DateTime idopont;
        public int idotartam;
    }

    class Program
    {
        static List<Esemeny> esemenyek = new List<Esemeny>();
        static Random random = new Random();

        static void Main(string[] args)
        {
            KezdetiFeltoltes();

            Console.WriteLine("Csaladi naptar program");

            bool fut = true;
            while (fut)
            {

                Console.WriteLine("1 - Naptár megjelenítése");
                Console.WriteLine("2 - Új esemény rögzítése");
                Console.WriteLine("3 - Legközelebbi esemény megjelenítése");
                Console.WriteLine("4 - Kilépés");

                Console.Write("\nVálasszon menüpontot (1-4): ");
                string valasztas = Console.ReadLine();

                if (valasztas == "1")
                {
                    NaptarMegjelenites();
                }
                else if (valasztas == "2")
                {
                    UjEsemeny();
                }
                else if (valasztas == "3")
                {
                    LegkozelabbiEsemeny();
                }
                else if (valasztas == "4")
                {
                    MentesFajlba();
                    Console.WriteLine("Viszlát!");
                    fut = false;
                }
                else
                {
                    Console.WriteLine("Hibás választás! Válasszon 1 és 4 között!");
                }
            }
        }

        static void KezdetiFeltoltes()
        {
            string[] tulajdonosok = { "apa", "anya" };

            for (int i = 0; i < 40; i++)
            {
                Esemeny esemeny;
                esemeny.tulajdonos = tulajdonosok[random.Next(2)];

                int nap = random.Next(1, 30);
                int ora = random.Next(8, 20);
                int perc = random.Next(0, 60);

                esemeny.idopont = new DateTime(2028, 2, nap, ora, perc, 0);
                esemeny.idotartam = random.Next(30, 121);

                esemenyek.Add(esemeny);
            }
        }

        static void NaptarMegjelenites()
        {
            if (esemenyek.Count == 0)
            {
                Console.WriteLine("Nincs még esemény a naptárban.");
                return;
            }

            List<Esemeny> rendezett = esemenyek.OrderBy(e => e.idopont).ToList();

            Console.WriteLine("\n Csaladi Naptar 2028");
            Console.WriteLine(new string('-', 60));

            foreach (Esemeny esemeny in rendezett)
            {
                Console.WriteLine("Tulajdonos: " + esemeny.tulajdonos);
                Console.WriteLine("Időpont: " + esemeny.idopont.ToString("yyyy.MM.dd. HH:mm"));
                Console.WriteLine("Időtartam: " + esemeny.idotartam + " perc");
                Console.WriteLine(new string('-', 60));
            }
        }

        static void UjEsemeny()
        {
            Console.WriteLine("\nÚJ ESEMÉNY RÖGZÍTÉSE");

            Esemeny esemeny;

            string tulajdonos = "";
            while (tulajdonos != "apa" && tulajdonos != "anya")
            {
                Console.Write("Tulajdonos (apa/anya): ");
                tulajdonos = Console.ReadLine().ToLower();
                if (tulajdonos != "apa" && tulajdonos != "anya")
                {
                    Console.WriteLine("Hibás érték! Csak 'apa' vagy 'anya' lehet!");
                }
            }
            esemeny.tulajdonos = tulajdonos;

            int nap = 0;
            while (nap < 1 || nap > 29)
            {
                Console.Write("Nap (1-29): ");
                nap = int.Parse(Console.ReadLine());
                if (nap < 1 || nap > 29)
                {
                    Console.WriteLine("Hibás nap! 1 és 29 között adjon meg!");
                }
            }

            int ora = 0;
            while (ora < 8 || ora > 20)
            {
                Console.Write("Óra (8-20): ");
                ora = int.Parse(Console.ReadLine());
                if (ora < 8 || ora > 20)
                {
                    Console.WriteLine("Hibás óra! 8 és 20 között adjon meg!");
                }
            }

            int perc = -1;
            while (perc < 0 || perc > 59)
            {
                Console.Write("Perc (0-59): ");
                perc = int.Parse(Console.ReadLine());
                if (perc < 0 || perc > 59)
                {
                    Console.WriteLine("Hibás perc! 0 és 59 között adjon meg!");
                }
            }

            int idotartam = 0;
            while (idotartam < 30 || idotartam > 120)
            {
                Console.Write("Időtartam percben (30-120): ");
                idotartam = int.Parse(Console.ReadLine());
                if (idotartam < 30 || idotartam > 120)
                {
                    Console.WriteLine("Hibás időtartam! 30 és 120 perc között adjon meg!");
                }
            }

            esemeny.idopont = new DateTime(2028, 2, nap, ora, perc, 0);
            esemeny.idotartam = idotartam;

            esemenyek.Add(esemeny);
            Console.WriteLine("\nAz esemény sikeresen rögzítve!");
        }

        static void LegkozelabbiEsemeny()
        {
            if (esemenyek.Count == 0)
            {
                Console.WriteLine("Nincs esemény a naptárban.");
                return;
            }

            int refNap = random.Next(1, 30);
            int refOra = random.Next(0, 24);
            int refPerc = random.Next(0, 60);
            DateTime referencia = new DateTime(2028, 2, refNap, refOra, refPerc, 0);

            Console.WriteLine("\n LEGKÖZELEBBI ESEMÉNY ");
            Console.WriteLine("Viszonyítási időpont: " + referencia.ToString("yyyy.MM.dd. HH:mm"));
            Console.WriteLine(new string('-', 60));

            bool talalat = false;
            Esemeny legkozelebbi = new Esemeny();
            TimeSpan legkisebbKulonbseg = TimeSpan.MaxValue;

            foreach (Esemeny esemeny in esemenyek)
            {
                if (esemeny.idopont >= referencia)
                {
                    TimeSpan kulonbseg = esemeny.idopont - referencia;

                    if (!talalat || kulonbseg < legkisebbKulonbseg)
                    {
                        legkozelebbi = esemeny;
                        legkisebbKulonbseg = kulonbseg;
                        talalat = true;
                    }
                }
            }

            if (!talalat)
            {
                Console.WriteLine("Nincs a viszonyítási időpont után esemény.");
            }
            else
            {
                Console.WriteLine("Tulajdonos: " + legkozelebbi.tulajdonos);
                Console.WriteLine("Időpont: " + legkozelebbi.idopont.ToString("yyyy.MM.dd. HH:mm"));
                Console.WriteLine("Időtartam: " + legkozelebbi.idotartam + " perc");
            }
        }

        static void MentesFajlba()
        {
            StreamWriter fajl = new StreamWriter("naptar.txt");

            foreach (Esemeny esemeny in esemenyek)
            {
                string sor = esemeny.tulajdonos + ";" +
                             esemeny.idopont.ToString("yyyy.MM.dd. HH:mm") + ";" +
                             esemeny.idotartam;
                fajl.WriteLine(sor);
            }

            fajl.Close();
            Console.WriteLine("\nAz események sikeresen mentve a naptar.txt fájlba!");
        }
    }
}