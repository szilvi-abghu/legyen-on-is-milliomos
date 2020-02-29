using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    class Program
    {
        static Random rnd = new Random();
        static List<Kerdes> teszt;
        static void Main(string[] args)
        {
            Beolvas();
            Kiiras();

            Console.ReadKey();
        }

        private static void Kiiras()
        {
            
            for (int i = 1; i < teszt.Count; i++)
            {
                FeladatMegjelenitese(i);
                

                Console.WriteLine("Folytatáshoz nyomjon ENTER-t...");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    Console.Write("\b \b"); //amíg nem enter üt le a felhasználó, addig backslash space backslash-sel visszatörlődik a beírt karakter
                }
                Console.Clear();
            }
            
        }

        private static void FeladatMegjelenitese(int i)
        {
            string[] valaszok = {teszt[i].helyesValasz, teszt[i].rosszValasz1, teszt[i].rosszValasz2, teszt[i].rosszValasz3 };
            string[] kevertValaszok = TombKever(valaszok);

            Console.WriteLine($"{(i)}. kérdés: {teszt[i].kategoria} kategóriában\n");
            Console.WriteLine($"{teszt[i].kerdes}");
            Console.WriteLine($"\ta) {kevertValaszok[0]}");
            Console.WriteLine($"\tb) {kevertValaszok[1]}");
            Console.WriteLine($"\tc) {kevertValaszok[2]}");
            Console.WriteLine($"\td) {kevertValaszok[3]}");
                                   
            Console.Write("Írd a helyes válasz betűjelét!");
            char valasz = char.Parse(Console.ReadLine());

           

        }

        private static string[] TombKever(string[] valaszok)
        {
            for (int i = 0; i < valaszok.Length; i++)
            {
                int x = rnd.Next(valaszok.Length);
                int y = rnd.Next(valaszok.Length);

                string cs = valaszok[x];//letárolom egy csereváltozóban
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
