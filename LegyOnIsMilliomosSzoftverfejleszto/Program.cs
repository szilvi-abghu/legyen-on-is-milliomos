using System;
using System.Collections.Generic;
using System.IO; //0. lépés
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LegyOnIsMilliomosSzoftverfejleszto
{
    struct Kerdes //1.lépés
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
        static List<Kerdes> teszt; //2.lépés
        static int[] nyeremény = { 0, 100, 1000, 2000, 10000, 50000, 100000, 500000, 1000000, 5000000, 10000000 };
        static int kerdesekSzama = 3;
        static bool ok = true;

        static void Main(string[] args)
        {
            Beolvas(); // 3. lépés
           
            Kiiras(); //4. lépés

            //Console.ReadKey();
        }

        
        private static void Kiiras()
        {
            
            for (int i = 1; i < (kerdesekSzama+1); i++) // 5.lépés: i=1-től indítva, mert a 0 a táblázat fejléce
            {
                if (ok) // ha ok=true, azaz mindaddig, amíg nem rontja el a feladatot
                {
                    FeladatMegjelenitese(i);
                }

                else // ha ok=false, azaz amikor elrontja a feladatot(hiszen ebben az esetben átváltjuk ok értékét false-ra)
                { 
                    break;
                }



            }

            if (ok)
            {
                Kiertekeles();
            }
            else
            {
                Console.Clear();
                Console.WriteLine(  "Köszönöm a játékot, ma csak tapasztalatot nyertél!\n" +
                                    "Kilépéshez nyomd le az Esc billenytűt!");
                while (Console.ReadKey().Key != ConsoleKey.Escape)
                    Console.Write("\b \b");

            }
            

        }

        private static void Kiertekeles()
        {
            
            Console.WriteLine("Gratulálok, minden kérdésre tudtad a választ!\n" +
                              "Nyereményed még egyszer: {0, 0:N0} Ft\n" +
                              "Ha van kedved, játsz újra!\n" +
                              "Kilépéshez nyomd le az Esc billenytűt!", nyeremény[kerdesekSzama]);
            while (Console.ReadKey().Key != ConsoleKey.Escape)
                    Console.Write("\b \b");
        }

        private static void FeladatMegjelenitese(int i)
        {
            //List<Kerdes> kevertTeszt = ListaKever(teszt);
            int randomindex = rnd.Next(1, teszt.Count);
            Valasz[] valaszok = new Valasz[4];
            valaszok[0] = new Valasz()
            {
                valaszSzovege = teszt[randomindex].helyesValasz,
                helyesValasz = true,
            };
            valaszok[1] = new Valasz()
            {
                valaszSzovege = teszt[randomindex].rosszValasz1,
                helyesValasz = false,
            };
            valaszok[2] = new Valasz()
            {
                valaszSzovege = teszt[randomindex].rosszValasz2,
                helyesValasz = false,
            };
            valaszok[3] = new Valasz()
            {
                valaszSzovege = teszt[randomindex].rosszValasz3,
                helyesValasz = false,
            };

            Valasz[] kevertValaszok = TombKever(valaszok);

            

            Console.WriteLine($"10/{i}. kérdés: {teszt[randomindex].kategoria} kategóriában\n"); //kategória és kérdés és lehetséges válaszok kiírása
            Console.WriteLine($"{teszt[randomindex].kerdes}");
            Console.WriteLine(  $"\ta) {kevertValaszok[0].valaszSzovege}\n" +
                                $"\tb) {kevertValaszok[1].valaszSzovege}\n" +
                                $"\tc) {kevertValaszok[2].valaszSzovege}\n" +
                                $"\td) {kevertValaszok[3].valaszSzovege}\n");
            teszt.RemoveAt(randomindex);
                                   
            Console.WriteLine("Írd a helyes válasz betűjelét, és nyomj egy enter-t!");
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
                Console.WriteLine("Nyereményed: {0, 0:N0} Ft", nyeremény[i]);
                Thread.Sleep(1000);
                Console.Clear();
                
            }
            else
            {
                ok = false;
                Console.WriteLine("Helytelen válasz!\n");
                Thread.Sleep(3000);
            }
                       
        }


        private static List<Kerdes> ListaKever(List<Kerdes> teszt)
        {
            for (int i = 0; i < teszt.Count; i++)
            {
                int x = rnd.Next(teszt.Count);
                int y = rnd.Next(teszt.Count);

                Kerdes cs = teszt[x];//letárolom egy csereváltozóban
                teszt[x] = teszt[y];
                teszt[y] = cs;

            }
            return teszt;
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
