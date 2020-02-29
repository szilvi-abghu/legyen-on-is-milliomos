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
            var dic = new Dictionary<char, string>(); // char, struct

            dic.Add('a', teszt[i].helyesValasz); // dic.a = teszt[i].helyesValasz; dic['a'] = teszt[i].helyesValasz;
            dic.Add('b', teszt[i].rosszValasz1); // dic.b = { helyes: false, szoveg: teszt[i].helyesValasz  }
            dic.Add('c', teszt[i].rosszValasz2);
            dic.Add('d', teszt[i].rosszValasz3);

            Console.WriteLine($"{(i)}. kérdés: {teszt[i].kategoria} kategóriában\n");
            Console.WriteLine($"{teszt[i].kerdes}");
            Console.WriteLine($"\ta) {teszt[i].helyesValasz}");
            Console.WriteLine($"\tb) {teszt[i].rosszValasz1}");
            Console.WriteLine($"\tc) {teszt[i].rosszValasz2}");
            Console.WriteLine($"\td) {teszt[i].rosszValasz3}");
                                   
            Console.Write("Írd a helyes válasz betűjelét!");
            char valasz = char.Parse(Console.ReadLine());

            Console.WriteLine($"A válasz: {dic[valasz]}"); // dic[valasz].szoveg !dic[valasz].helyes 

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
