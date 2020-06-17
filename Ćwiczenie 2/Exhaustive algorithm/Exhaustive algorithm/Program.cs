using System;
using System.Collections.Generic;
using System.Linq;
using Kw.Combinatorics;

namespace Exhaustive_algorithm
{
    public class Program
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
            Console.WriteLine(ExhaustiveAlgorithm(decisionSystem));
            Console.Write("Press anything to quit...\n");
            Console.ReadKey();
        }

        public static int[][][] indiscernibilityMatrix(string[][] system)
        {
            int[][][] matrix = new int[system.Length][][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[system.Length][];
                for (int j = 0; j < system.Length; j++)
                    matrix[i][j] = matrixCell(system[i], system[j]);
            }
            return matrix;
        }

        public static int[] matrixCell(string[] obj1, string[] obj2)
        {
            List<int> cell = new List<int>();
            if (obj1.Last() == obj2.Last())
                return cell.ToArray();
            for (int i = 0; i < obj1.Length - 1; i++)
                if (obj1[i] == obj2[i])
                    cell.Add(i);
            return cell.ToArray();
        }

        public static bool ifCombinationInCell(int[] cell, int[] combination)
        {
            for (int i = 0; i < combination.Length; i++)
                if (!cell.Contains(combination[i]))
                    return false;
            return true;
        }

        public static bool ifCombinationInRow(int[][] row, int[] combinations)
        {
            for (int i = 0; i< row.Length; i++)
                if(ifCombinationInCell(row[i], combinations))
                    return true;
            return false;
        }

        public static string[] obj(int number, string[][] system)
        {
            int temporary = 0;
            string[] ob = new string[system[0].Length - 1];
            foreach (var x in system)
            {
                if(temporary==number)
                    ob = x;
                temporary++;
            }
            return ob;
        }

        static public string ExhaustiveAlgorithm(string[][] decisionSystem)
        {
            List<Rule> rules = new List<Rule>();
            int[] combination;
            string result = "";

            for (int row = 1; row < decisionSystem[0].Length - 1; row++)
            {
                int temporary = 0;
                foreach (var matrix in indiscernibilityMatrix(decisionSystem))
                {
                    foreach (Combination combo in new Combination(decisionSystem[0].Length - 1, row).GetRows())
                    {
                        combination = combo.ToArray();
                        if (!ifCombinationInRow(matrix, combination))
                        {
                            Rule rule = new Rule(obj(temporary, decisionSystem), combination);

                            if (!rule.ifRuleContainsRuleFromList(rules))
                            {
                                rule.RuleSupport(decisionSystem);
                                rules.Add(rule);
                            }
                        }
                    }
                    temporary++;
                }
            }

            foreach (var r in rules)
                result += r.ToString();
            return result;
        }
    }
}