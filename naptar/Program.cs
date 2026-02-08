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
    }
}