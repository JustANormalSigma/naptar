using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NaptariFeladat
{

    struct Esemeny
    {
        public string Ki;
        public DateTime Mikor;
        public int Meddig;
    }

    class Program
    {
        static List<Esemeny> lista = new List<Esemeny>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Családi naptár ===\n");

            Betoltes();

            if (lista.Count == 0)
            {
                Console.WriteLine("Generálás...");
                VeletlenEsemenyek();
            }
            bool fut = true;
            while (fut)
            {
                Menu();
                string valasztas = Console.ReadLine();

                if (valasztas == "1")
                {
                    Naptar();
                }

            }
        }


        static void Betoltes()
        {
            try
            {
                StreamReader f = new StreamReader("esemenyek.txt");

                string sor;
                while ((sor = f.ReadLine()) != null)
                {
                    string[] darabok = sor.Split(';');
                    string ki = darabok[0];
                    string idopont = darabok[1];
                    int meddig = int.Parse(darabok[2]);

                    string[] datum_ido = idopont.Split(' ');
                    string[] datum = datum_ido[0].Split('-');
                    string[] ido = datum_ido[1].Split(':');

                    int ev = int.Parse(datum[0]);
                    int honap = int.Parse(datum[1]);
                    int nap = int.Parse(datum[2]);
                    int ora = int.Parse(ido[0]);
                    int perc = int.Parse(ido[1]);

                    DateTime mikor = new DateTime(ev, honap, nap, ora, perc, 0);

                    Esemeny e = new Esemeny();
                    e.Ki = ki;
                    e.Mikor = mikor;
                    e.Meddig = meddig;

                    lista.Add(e);
                }

                f.Close();
                Console.WriteLine("Betöltve!");
            }
            catch
            {
                Console.WriteLine("Nincs mentett fájl");

            }

        }
        static void VeletlenEsemenyek()
        {
            Random rnd = new Random();

            for (int i = 0; i < 20; i++)
            {
                string ki = i < 10 ? "apa" : "anya";

                int nap = rnd.Next(1, 30);
                int ora = rnd.Next(8, 20);
                int perc = new int[] { 0, 15, 30, 45 }[rnd.Next(4)];

                DateTime mikor = new DateTime(2028, 2, nap, ora, perc, 0);
                int meddig = rnd.Next(30, 121);

                Esemeny e = new Esemeny();
                e.Ki = ki;
                e.Mikor = mikor;
                e.Meddig = meddig;

                lista.Add(e);
            }
        }

        static void Naptar()
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("\nNincs esemény a naptárban");
                return;
            }

            Console.WriteLine("\n--- Naptár 2028 február ---\n");
            foreach (Esemeny e in lista)
            {
                Console.WriteLine($"{e.Ki} - {e.Mikor.Day}. {e.Mikor.Hour}:{e.Mikor.Minute:D2} - {e.Meddig} perc");
            }
            Console.WriteLine();
        }

        static void Menu()
        {
            Console.WriteLine("\n1 - Naptár");
            Console.WriteLine("2 - Új esemény");
            Console.WriteLine("3 - Legközelebbi esemény");
            Console.WriteLine("4 - Kilépés");
            Console.Write("\nVálassz: ");
        }
    }
}