using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LegyOnIsMilliomosSzoftverfejleszto
{
    struct Kerdes
    {
        public string kategoria;
        public string kerdes;
        public string helyesValasz;
        public string rosszValasz1;
        public string rosszValasz2;
        public string rosszValasz3;
    }

    struct Valasz
    {
        public string valaszSzovege;
        public bool helyesValasz;
    }
    class Program
    {
        static Random rnd = new Random();
        static List<Kerdes> teszt;
        static void Main(string[] args)
        {
            Beolvas();
            Kiiras();

            //Console.ReadKey();
        }

        private static void Kiiras()
        {
            
            for (int i = 1; i < teszt.Count; i++)
            {
                FeladatMegjelenitese(i);
               
                


                //Console.WriteLine("Folytatáshoz nyomjon ENTER-t...");
                //while (Console.ReadKey().Key != ConsoleKey.Enter)
                //{
                //    Console.Write("\b \b"); //amíg nem enter üt le a felhasználó, addig backslash space backslash-sel visszatörlődik a beírt karakter
                //}

            }
            
        }

        private static void FeladatMegjelenitese(int i)
        {
           Valasz[] valaszok = new Valasz[4];
            valaszok[0] = new Valasz()
            {
                valaszSzovege = teszt[i].helyesValasz,
                helyesValasz = true,
            };
            valaszok[1] = new Valasz()
            {
                valaszSzovege = teszt[i].rosszValasz1,
                helyesValasz = false,
            };
            valaszok[2] = new Valasz()
            {
                valaszSzovege = teszt[i].rosszValasz2,
                helyesValasz = false,
            };
            valaszok[3] = new Valasz()
            {
                valaszSzovege = teszt[i].rosszValasz3,
                helyesValasz = false,
            };

            Valasz[] kevertValaszok = TombKever(valaszok);

            Console.WriteLine($"{(i)}. kérdés: {teszt[i].kategoria} kategóriában\n");
            Console.WriteLine($"{teszt[i].kerdes}");
            Console.WriteLine(  $"\ta) {kevertValaszok[0].valaszSzovege}\n" +
                                $"\tb) {kevertValaszok[1].valaszSzovege}\n" +
                                $"\tc) {kevertValaszok[2].valaszSzovege}\n" +
                                $"\td) {kevertValaszok[3].valaszSzovege}\n");
                                   
            Console.WriteLine("Írd a helyes válasz betűjelét!");
            //while (Console.ReadKey().Key != ConsoleKey.A && Console.ReadKey().Key != ConsoleKey.B && Console.ReadKey().Key != ConsoleKey.C && Console.ReadKey().Key != ConsoleKey.D && Console.ReadKey().Key != ConsoleKey.Enter)
            //{
            //    Console.Write("\b \b"); //amíg nem enter üt le a felhasználó, addig backslash space backslash-sel visszatörlődik a beírt karakter
            //}
            char valasz = char.Parse(Console.ReadLine());

            

            char[] valaszBetujel = { 'a', 'b', 'c', 'd' };
            int valaszindex = Array.IndexOf(valaszBetujel, valasz);

            //Console.WriteLine($"{valaszindex}");
            //Console.WriteLine($"{kevertValaszok[valaszindex].helyesValasz}");

            if (valaszindex == -1) //Because most arrays have a lower bound of zero, this method generally returns - 1 ifvalue isn't found. 
            {
                Console.WriteLine("\b \b");
            }

            else if (kevertValaszok[valaszindex].helyesValasz == true)
            {
                Console.WriteLine("Helyes válasz!");
                Thread.Sleep(3000);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Helytelen válasz!\n");
                Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine("Köszönöm a játékot, ma csak tapasztalatot nyertél!");
            }

            //while (Console.ReadKey().Key != ConsoleKey.Escape)
                //Console.Write("\b \b");




        }

        private static Valasz[] TombKever(Valasz[] valaszok)
        {
            for (int i = 0; i < valaszok.Length; i++)
            {
                int x = rnd.Next(valaszok.Length);
                int y = rnd.Next(valaszok.Length);

                Valasz cs = valaszok[x];//letárolom egy csereváltozóban
                valaszok[x] = valaszok[y];
                valaszok[y] = cs;

            }
            return valaszok;
        }

        private static void Beolvas()
        {
            teszt = new List<Kerdes>();

            var sr = new StreamReader(@"..\..\res\kerdesek_valaszok.csv", Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine().Split(';');

                var k = new Kerdes()
                {
                    kategoria = sor[0],
                    kerdes = sor[1],
                    helyesValasz = sor[2],
                    rosszValasz1 = sor[3],
                    rosszValasz2 = sor[4],
                    rosszValasz3 = sor[5],

                };

                teszt.Add(k);

            };

            sr.Close();
        }
    }
}
