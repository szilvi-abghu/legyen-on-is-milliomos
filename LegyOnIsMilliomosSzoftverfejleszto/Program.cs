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
        static int[] nyeremeny = { 0, 100, 1000, 2000, 10000, 50000, 100000, 500000, 1000000, 5000000, 10000000 };
        static int kerdesekSzama = 10;
        static bool ok = true;
        static int hk = Console.WindowHeight / 2;
        static int vk = Console.WindowWidth / 2;

        static void Main(string[] args)
        {
            Beolvas(); // 3. lépés
            Kiiras(); //4. lépés           
        }


        private static void Kiiras()
        {
            for (int i = 1; i < (kerdesekSzama + 1); i++) // 5.lépés: i=1-től indítva, mert a 0 a táblázat fejléce
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
                Console.SetCursorPosition(vk - 25, hk - 1);
                Console.WriteLine("Gratulálok, minden kérdésre tudtad a választ!");
                Console.SetCursorPosition(vk - 21, hk);
                Console.WriteLine("Nyereményed még egyszer: {0, 0:N0} Ft", nyeremeny[kerdesekSzama]);
                Console.SetCursorPosition(vk - 15, hk+1);
                Console.WriteLine("Ha van kedved, játsz újra!");            
            }
            else
            {
                
                Console.Clear();
                Console.SetCursorPosition(vk - 15, hk - 1);
                Console.WriteLine("Köszönöm a játékot!");
                Console.WriteLine("\n");
                Console.SetCursorPosition(vk - 27, hk);
                Console.WriteLine("Ma nem nyertél, de ha van kedved, játsz újra!");                                  
            }
            Console.SetCursorPosition(0, hk*2);
            Console.WriteLine("\nKilépéshez nyomd le az Esc billenytűt!");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
                Console.Write("\b \b");
        }

       
        private static void FeladatMegjelenitese(int i)
        {
            
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

            
            Console.Write($"\n10/{i}. kérdés: {teszt[randomindex].kategoria} kategóriában"); //kategória és kérdés és lehetséges válaszok kiírása
            Console.SetCursorPosition(80, 1);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Eddigi nyereményed: {0, 0:N0} Ft", nyeremeny[i - 1]);
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.WriteLine($"{teszt[randomindex].kerdes}\n");
            Console.WriteLine(  $"\ta) {kevertValaszok[0].valaszSzovege}\n" +
                                $"\tb) {kevertValaszok[1].valaszSzovege}\n" +
                                $"\tc) {kevertValaszok[2].valaszSzovege}\n" +
                                $"\td) {kevertValaszok[3].valaszSzovege}\n");
            teszt.RemoveAt(randomindex);
                                   
            Console.WriteLine("\nÍrd a helyes válasz betűjelét, és nyomj egy enter-t!");

            BeiroBox(5, 7);

            Console.SetCursorPosition(vk, hk);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            
            char valasz = char.Parse(Console.ReadLine()); // csak egy enter nyomásra, itt ad Exception unhandled-t.

            Console.ResetColor();
            char[] valaszBetujel = { 'a', 'b', 'c', 'd' };

            int valaszindex = Array.IndexOf(valaszBetujel, valasz); // a lesz a 0-as indexű, b lesz az 1-es indexű, c lesz a 2-es indexű és d lesz a 3-as indexű


            if (valaszindex == -1) //Because most arrays have a lower bound of zero, this method generally returns - 1 if value isn't found. De mi a helyzet, ha csak egy enter-t nyom a felhasználó? Exception unhandled-be fut.
            {
                ok = false;
               
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\n\nSAJNÁLOM, ILYEN VÁLASZLEHETŐSÉG NINCS!");
                Thread.Sleep(3000);
                Console.ResetColor();
            }

            else if (kevertValaszok[valaszindex].helyesValasz == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
               
                Console.WriteLine("\n\n\n\nHELYES VÁLASZ!");
                Console.WriteLine("Nyereményed: {0, 0:N0} Ft", nyeremeny[i]);
                Thread.Sleep(3000);
                Console.ResetColor();
                Console.Clear();                
            }

            else
            {
                ok = false;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\n\nHELYTELEN VÁLASZ!\n");
                Thread.Sleep(3000);
                Console.ResetColor();
            }
        }

        private static void BeiroBox(int m, int sz)
        {
                     
            for (int i = 0; i < sz; i++)
            {
                Console.SetCursorPosition((vk - (sz / 2)) + i, hk - (m / 2));
                Console.Write("$");
                Console.SetCursorPosition(vk - (sz / 2) + i, hk + (m / 2));
                Console.Write("$");
            }
 
            for (int i = 0; i < m; i++)
            {
                Console.SetCursorPosition(vk - (sz / 2), hk - (m / 2) + i);
                Console.Write("$");
                Console.SetCursorPosition(vk + (sz / 2), hk - (m / 2) + i);
                Console.Write("$");
            }
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
