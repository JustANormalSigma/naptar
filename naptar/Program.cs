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

                else if (valasztas == "2")
                {
                    UjEsemeny();
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

        static void UjEsemeny()
        {
            Console.WriteLine("\n--- Új esemény ---");

            string ki = "";
            while (ki != "apa" && ki != "anya")
            {
                Console.Write("Ki? (apa/anya): ");
                ki = Console.ReadLine();
                if (ki != "apa" && ki != "anya")
                {
                    Console.WriteLine("Csak apa vagy anya!");
                }
            }

            int nap = 0;
            while (nap < 1 || nap > 29)
            {
                Console.Write("Nap (1-29): ");
                nap = int.Parse(Console.ReadLine());
                if (nap < 1 || nap > 29)
                {
                    Console.WriteLine("1 és 29 között!");
                }
            }

            int ora = 0;
            while (ora < 8 || ora > 20)
            {
                Console.Write("Óra (8-20): ");
                ora = int.Parse(Console.ReadLine());
                if (ora < 8 || ora > 20)
                {
                    Console.WriteLine("8 és 20 között!");
                }
            }

            int perc = -1;
            while (perc < 0 || perc > 59)
            {
                Console.Write("Perc (0-59): ");
                perc = int.Parse(Console.ReadLine());
                if (perc < 0 || perc > 59)
                {
                    Console.WriteLine("0 és 59 között!");
                }
            }

            int meddig = 0;
            while (meddig < 30 || meddig > 120)
            {
                Console.Write("Időtartam percben (30-120): ");
                meddig = int.Parse(Console.ReadLine());
                if (meddig < 30 || meddig > 120)
                {
                    Console.WriteLine("30 és 120 között!");
                }
            }

            DateTime mikor = new DateTime(2028, 2, nap, ora, perc, 0);

            Esemeny e = new Esemeny();
            e.Ki = ki;
            e.Mikor = mikor;
            e.Meddig = meddig;

            lista.Add(e);

            
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = i + 1; j < lista.Count; j++)
                {
                    if (lista[i].Mikor > lista[j].Mikor)
                    {
                        Esemeny temp = lista[i];
                        lista[i] = lista[j];
                        lista[j] = temp;
                    }
                }
            }

            Console.WriteLine("\nKész!\n");
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