using System;
using System.Collections.Generic;
using System.Linq;
using Kw.Combinatorics;

namespace Sequential_covering_algorithm
{
    class Program
    {
        static string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }
            return wynik;
        }

        static double StringToDouble(string liczba)
        {
            double wynik; liczba = liczba.Trim();
            if (!double.TryParse(liczba.Replace(',', '.'), out wynik) && !double.TryParse(liczba.Replace('.', ','), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do double");

            return wynik;
        }

        static int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");

            return wynik;
        }

        static string[][] StringToTablica(string sciezkaDoPliku)
        {
            string trescPliku = System.IO.File.ReadAllText(sciezkaDoPliku); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            string[][] wczytaneDane = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                wczytaneDane[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    wczytaneDane[i][j] = cyfra;
                }
            }
            return wczytaneDane;
        }

        static void Main(string[] args)
        {
            string fileNameWithData = @"decision_system_from_pdf.txt";
            // string fileNameWithData = @"decision_system_generated.txt";
            // string fileNameWithData = @"decision_system_generated2.txt";
            string[][] decisionSystem = StringToTablica(fileNameWithData);
            string inputData = TablicaDoString(decisionSystem);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Input data (decision system)\n");
            Console.ResetColor();
            Console.Write(inputData);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nOutput data (set of rules)");
            Console.ResetColor();
            Console.WriteLine(SequentialCoveringAlgorithm(decisionSystem));
            Console.Write("Press anything to quit...\n");
            Console.ReadKey();
        }

        static public List<int> Mask(string[][] system)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < system.Length; i++)
                list.Add(i);
            return list;
        }

        static public string SequentialCoveringAlgorithm(string[][] decisionSystem)
        {
            List<Rule> rules = new List<Rule>();
            List<int> maskList = new List<int>();

            foreach (var m in Mask(decisionSystem))
                maskList.Add(m);

            if (maskList.Count() != 0)
            {
                for (int row = 1; row < decisionSystem[0].Length - 1; row++)
                {
                    int temporary = 0;
                    foreach (var obj in decisionSystem)
                    {
                        foreach (Combination combo in new Combination(decisionSystem[0].Length - 1, row).GetRows())
                        {
                            Rule rule = new Rule(obj, combo.ToArray());
                            if (maskList.Contains(temporary))
                                if (rule.ifConflictingRule(decisionSystem))
                                {
                                    rules.Add(rule);
                                    rule.RuleSupport(decisionSystem);
                                    maskList = rule.GenerateCoverage(decisionSystem, maskList);
                                    break;
                                }
                        }
                        temporary++;
                    }
                }
            }

            string result = "\n";
            foreach (var r in rules)
            {
                result += r.ToString();
                result += "\n";
            }
            return result;
        }
    }
}