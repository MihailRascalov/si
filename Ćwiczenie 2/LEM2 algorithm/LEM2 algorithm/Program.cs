using System;
using System.Collections.Generic;

namespace LEM2_algorithm
{
    class Program
    {
        public static float[,] decisionSystem;

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

        public static float[,] ReadSystem(string pathToFile)
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);
            int numberOfColumns = 0;
            int numberOfRows = 0;
            var line2 = lines[0].Trim();
            var numbers2 = line2.Split(" ");
            numberOfRows = lines.Length;
            numberOfColumns = numbers2.Length;
            decisionSystem = new float[numberOfRows, numberOfColumns];
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                var numbers = line.Split(' ');
                for (int j = 0; j < numbers.Length; j++)
                    decisionSystem[i, j] = float.Parse(numbers[j].Trim());
            }
            return decisionSystem;
        }

        static void Main(string[] args)
        {
            // string fileNameWithData = @"decision_system_from_pdf.txt";
            string fileNameWithData = @"decision_system_from_pdf_lem2.txt";
            // string fileNameWithData = @"decision_system_generated.txt";
            // string fileNameWithData = @"decision_system_generated2.txt";
            string[][] decisionSystemPrint = StringToTablica(fileNameWithData);
            string inputData = TablicaDoString(decisionSystemPrint);
            decisionSystem = ReadSystem(fileNameWithData);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Input data (decision system)\n");
            Console.ResetColor();
            Console.Write(inputData);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nOutput data (set of rules)\n");
            Console.ResetColor();
            Console.WriteLine(Lem2Algorithm(decisionSystem));
            Console.Write("Press anything to quit...\n");
            Console.ReadKey();
        }

        public static string Lem2Algorithm (float[,] decisionSystem)
        {
            string rules = "";
            var decisionsList = new List<float>();
            for (int i = 0; i < decisionSystem.GetLength(0); i++)
            {
                bool existsInList = false;
                for (int existing = 0; existing < decisionsList.Count; existing++)
                    if (decisionSystem[i, decisionSystem.GetLength(1) - 1] == decisionsList[existing])
                        existsInList = true;
                if (existsInList == false)
                    decisionsList.Add(decisionSystem[i, decisionSystem.GetLength(1) - 1]);
            }
            var rulesList = new List<float[]>();
            for (int i = 0; i < decisionsList.Count; i++)
            {
                float[,] mask = GenerateMask(decisionSystem);
                for (int row = 0; row < decisionSystem.GetLength(0); row++)
                    if (decisionSystem[row, decisionSystem.GetLength(1) - 1] != decisionsList[i])
                        for (int column = 0; column < decisionSystem.GetLength(1); column++)
                            mask[row, column] = 999;
                bool allCoveredConcept = false;
                while (allCoveredConcept == false)
                {
                    float[,] replicationTable = new float[3, 2];
                    float[,] ruleComprasionTable = new float[2, decisionSystem.GetLength(1)];
                    for (int replenishment = 0; replenishment < decisionSystem.GetLength(1); replenishment++)
                    {
                        ruleComprasionTable[0, replenishment] = -1;
                        ruleComprasionTable[1, replenishment] = -1;
                    }
                    allCoveredConcept = true;
                    bool conflictingRule = true;
                    while(conflictingRule == true)
                    {
                        int earliestRepetitionRow = 0;
                        for (int firstRow = 0; firstRow < decisionSystem.GetLength(0); firstRow++)
                            for (int col = 0; col < decisionSystem.GetLength(1) - 1; col++)
                            {
                                if (mask[firstRow, 0] == 999)
                                    col = decisionSystem.GetLength(1);
                                if (mask[firstRow, 0] != 999 && ruleComprasionTable[0, col] == -1)
                                {
                                    replicationTable[0, 0] = col;
                                    replicationTable[1, 0] = mask[firstRow, col];
                                    int replicationDescriptor = 0;
                                    for (int replications = 0; replications < decisionSystem.GetLength(0); replications++)
                                        if (replicationTable[1, 0] == mask[replications, col])
                                            replicationDescriptor++;
                                    replicationTable[2, 0] = replicationDescriptor;
                                    earliestRepetitionRow = firstRow;
                                    firstRow = decisionSystem.GetLength(0);
                                    col = decisionSystem.GetLength(1);
                                }
                            }   
                        for (int column = 0; column < decisionSystem.GetLength(1) - 1; column++)
                            if( ruleComprasionTable[0, column] == -1)
                                for (int row = 0; row < decisionSystem.GetLength(0); row++)
                                    if (mask[row, column] != 999)
                                    {
                                        replicationTable[0, 1] = column;
                                        replicationTable[1, 1] = mask[row, column];
                                        int replicationsDescriptor = 0;
                                        for (int replications = 0; replications < decisionSystem.GetLength(0); replications++)
                                            if (replicationTable[1, 1] == mask[replications, column])
                                                replicationsDescriptor++;
                                        replicationTable[2, 1] = replicationsDescriptor;
                                        if (replicationTable[2, 1] > replicationTable[2, 0])
                                        {
                                            replicationTable[0, 0] = replicationTable[0, 1];
                                            replicationTable[1, 0] = replicationTable[1, 1];
                                            replicationTable[2, 0] = replicationTable[2, 1];
                                        }
                                    }
                        int temporaryAttributeNumber = Convert.ToInt32(replicationTable[0, 0]) + 1;
                        rules = rules + "(a" + temporaryAttributeNumber + "=" + replicationTable[1, 0] + ")" + ' ';
                        int attributeNumber = Convert.ToInt32(replicationTable[0, 0]);
                        ruleComprasionTable[0, attributeNumber] = replicationTable[1, 0];
                        ruleComprasionTable[0, decisionSystem.GetLength(1) - 1] = decisionSystem[earliestRepetitionRow, decisionSystem.GetLength(1) - 1];
                        for (int checkingRow = 0; checkingRow < decisionSystem.GetLength(0); checkingRow++)
                        {
                            for (int replenishments = 0; replenishments < decisionSystem.GetLength(1); replenishments++)
                                if (ruleComprasionTable[0, replenishments] != -1)
                                    ruleComprasionTable[1, replenishments] = decisionSystem[checkingRow, replenishments];
                            ruleComprasionTable[1, decisionSystem.GetLength(1) - 1] = decisionSystem[checkingRow, decisionSystem.GetLength(1) - 1];
                            bool identicalAttributes = true;
                            for (int attributesChecking = 0; attributesChecking < decisionSystem.GetLength(1) - 1; attributesChecking++)
                                if (ruleComprasionTable[0, attributesChecking] != ruleComprasionTable[1, attributesChecking])
                                {
                                    identicalAttributes = false;
                                    conflictingRule = false;
                                    attributesChecking = decisionSystem.GetLength(1);
                                }
                            if (identicalAttributes == true && ruleComprasionTable[0, decisionSystem.GetLength(1) - 1] != ruleComprasionTable[1, decisionSystem.GetLength(1) - 1])
                            {
                                conflictingRule = true;
                                checkingRow = decisionSystem.GetLength(0);
                            }
                            else
                                conflictingRule = false;
                        }
                        if (conflictingRule == true)
                        {
                            for (int obj = 0; obj < decisionSystem.GetLength(0); obj++)
                                if (mask[obj, attributeNumber] != replicationTable[1, 0] && mask[obj, attributeNumber] != 999)
                                    for (int kol = 0; kol < decisionSystem.GetLength(1); kol++)
                                        mask[obj, kol] = 999;
                            rules = rules + "^ ";
                        }
                        if (conflictingRule == false)
                        {
                            int ruleLength = 0;
                            for (int counter = 0; counter < decisionSystem.GetLength(1) - 1; counter++)
                                if (ruleComprasionTable[0, counter] != -1)
                                    ruleLength++;
                            float[,] temporaryTable = new float[3, ruleLength];
                            int temporaryCounter = 0;
                            for (int replenishment = 0; replenishment < decisionSystem.GetLength(1) - 1; replenishment++)
                                if (ruleComprasionTable[0, replenishment] != -1)
                                {
                                    temporaryTable[0, temporaryCounter] = replenishment;
                                    temporaryTable[1, temporaryCounter] = ruleComprasionTable[0, replenishment];
                                    temporaryCounter++;
                                }
                            int coverage = GenerateCoverage(decisionSystem, temporaryTable);
                            rules = rules + "=> (d=" + ruleComprasionTable[0, decisionSystem.GetLength(1) - 1] + ")";
                            if (coverage > 1)
                                rules = rules + " [" + coverage + "]" + "\r\n";
                            else
                                rules = rules + "\r\n";
                        }
                    }
                    float[] rule = new float[decisionSystem.GetLength(1)];
                    for (int rewriting = 0; rewriting < decisionSystem.GetLength(1); rewriting++)
                        rule[rewriting] = ruleComprasionTable[0, rewriting];
                    rulesList.Add(rule);
                    mask = GenerateMask(decisionSystem);
                    for (int row = 0; row < decisionSystem.GetLength(0); row++)
                        if (decisionSystem[row, decisionSystem.GetLength(1) - 1] != decisionsList[i])
                            for (int column = 0; column < decisionSystem.GetLength(1); column++)
                                mask[row, column] = 999;
                    for (int covering = 0; covering < rulesList.Count; covering++)
                    {
                        float[] temporaryTable = rulesList[covering];
                        for (int objChecking = 0; objChecking < decisionSystem.GetLength(0); objChecking++)
                        {
                            bool identicalObj = true;
                            for (int checking = 0; checking < decisionSystem.GetLength(1); checking++)
                            {
                                if (temporaryTable[checking] != mask[objChecking, checking] && temporaryTable[checking] != -1)
                                {
                                    identicalObj = false;
                                    checking = decisionSystem.GetLength(1);
                                }
                            }
                            if (identicalObj == true)
                                for (int attribute = 0; attribute < decisionSystem.GetLength(1); attribute++)
                                    mask[objChecking, attribute] = 999;
                        }
                    }
                    for (int coveredConcept = 0; coveredConcept < decisionSystem.GetLength(0); coveredConcept++)
                        if (mask[coveredConcept, 0] != 999)
                        {
                            allCoveredConcept = false;
                            coveredConcept = decisionSystem.GetLength(0);
                        }
                 }
            }
            return rules;
        }

        public static float[,] GenerateMask(float[,] decisionSystem)
        {
            float[,] mask = new float[decisionSystem.GetLength(0), decisionSystem.GetLength(1)];
            for (int i = 0; i < decisionSystem.GetLength(0); i++)
                for (int j = 0; j < decisionSystem.GetLength(1); j++)
                    mask[i, j] = decisionSystem[i, j];
            return mask;
        }

        public static int GenerateCoverage(float[,] decisionSystem, float[,] rule)
        {
            int systemCoverage = 0;
            float[,] setOfRules = new float[rule.GetLength(0), rule.GetLength(1)];
            for (int i = 0; i< rule.GetLength(1); i++)
            {
                setOfRules[0, i] = rule[0, i];
                setOfRules[1, i] = rule[1, i];
            }
            for (int i = 0; i < decisionSystem.GetLength(0); i++)
            {
                bool overlaps = true;
                for (int column = 0; column < rule.GetLength(1); column++)
                    setOfRules[2, column] = decisionSystem[i, Convert.ToInt32(rule[0, column])];
                for (int s = 0; s < rule.GetLength(1); s++)
                    if (setOfRules[1, s] != setOfRules[2, s])
                        overlaps = false;
                if (overlaps == true)
                    systemCoverage++;
            }
            return systemCoverage;
        }
    }
 }