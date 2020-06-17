using System;
using System.Collections.Generic;
using System.Linq;

namespace DaneZPlikuConsole
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
            Console.ForegroundColor = ConsoleColor.White;
            string nazwaPlikuZDanymi = @"australian.txt";
            string nazwaPlikuZTypamiAtrybutow = @"australian-type.txt";

            string[][] wczytaneDane = StringToTablica(nazwaPlikuZDanymi);
            string[][] atrType = StringToTablica(nazwaPlikuZTypamiAtrybutow);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("System data");
            Console.ForegroundColor = ConsoleColor.White;
            string wynik = TablicaDoString(wczytaneDane);
            Console.Write(wynik);

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Data from file with types");
            Console.ForegroundColor = ConsoleColor.White;

            string wynikAtrType = TablicaDoString(atrType);
            Console.Write(wynikAtrType + "\n");

            /****************** Miejsce na rozwiązanie *********************************/

            // Exercises 3
            ShowDecisionSymbols(wczytaneDane);
            ShowNumberOfObjectsInClass(wczytaneDane);
            ShowMinimumAndMaximumValuesOfParticularAtributes(wczytaneDane, atrType);
            CountNumberOfDifferentAvailableValuesForEachAtributes(wczytaneDane, atrType);
            ShowListOfAllDifferentAvailableValuesForEachAtributes(wczytaneDane, atrType);
            ComputeStandardDeviation(wczytaneDane, atrType);

            // Exercises 4
            Console.WriteLine("\n----------Preprocessing----------\n");
            string[][] dataWithMissingValues = GenerateTenPercentOfMissingValues(wczytaneDane, atrType);
            FixTenPercentOfMissingValues(dataWithMissingValues, atrType);
            int leftInterval = -1;
            int rightInterval = 1;
            // Other options mentioned in exercise
                //NormalizeAttributeValuesIntoIntervals(wczytaneDane, atrType, leftInterval, rightInterval);
                //leftInterval = 0;
                //rightInterval = 1;
                    //NormalizeAttributeValuesIntoIntervals(wczytaneDane, atrType, leftInterval, rightInterval);
                    //leftInterval = -10;
                    //rightInterval = 10;
            NormalizeAttributeValuesIntoIntervals(wczytaneDane, atrType, leftInterval, rightInterval);
            StandardizeAttributes(wczytaneDane, atrType);

            string nazwaPlikuZDanymiCSV = @"Churn_Modelling.csv";
            string[][] wczytaneDaneCsv = StringToTablica(nazwaPlikuZDanymiCSV);
            //string wynikCSV = TablicaDoString(wczytaneDaneCsv);
            //Console.WriteLine(wynikCSV);

            ConvertSymbolicValuesOfAttribute(wczytaneDaneCsv);
            // The definitions of methods are below

            /****************** Koniec miejsca na rozwiązanie ********************************/
            Console.ReadKey();
        }
        static void ShowDecisionSymbols(string[][] data)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Decision class symbols");
            Console.ForegroundColor = ConsoleColor.White;
            int atributes = data[0].Length;

            for (int i = 0; i < data.Length; i++)
                Console.Write(data[i][atributes - 1] + " ");
            Console.WriteLine("\n");
        }

        static void ShowNumberOfObjectsInClass(string[][] data)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Number of objects in class");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(data[0].Length + "\n");
        }

        static void ShowMinimumAndMaximumValuesOfParticularAtributes(string[][] data, string[][] data2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minimum and maximum values of particular atributes (Only numeric attributes)");
            Console.ForegroundColor = ConsoleColor.White;
            float minimumAtribut = 0;
            float maximumAtribut = 0;
            for (int i = 0; i < data2.Length; i++)
                if (data2[i][1] == "n")
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (float.Parse(data[j][i]) < minimumAtribut)
                            minimumAtribut = float.Parse(data[j][i]);
                        if (float.Parse(data[j][i]) > maximumAtribut)
                            maximumAtribut = float.Parse(data[j][i]);
                    }
                    Console.WriteLine(data2[i][0] + " " + minimumAtribut + " " + maximumAtribut);
                    minimumAtribut = 0;
                    maximumAtribut = 0;
                }
            Console.WriteLine("");
        }

        static float[] ShowMinimumAndMaximumValuesOfParticularAtributesReturnTable(string[][] data, string[][] data2)
        {
            float[] minMaxTable = new float[data2.Length];
            int indexTable = 0;
            float minimumAtribut = 0;
            float maximumAtribut = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                if (data2[i][1] == "n")
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (float.Parse(data[j][i]) < minimumAtribut)
                            minimumAtribut = float.Parse(data[j][i]);
                        if (float.Parse(data[j][i]) > maximumAtribut)
                            maximumAtribut = float.Parse(data[j][i]);
                    }
                    minMaxTable[indexTable] = minimumAtribut;
                    minMaxTable[indexTable + 1] = maximumAtribut;
                    indexTable += 2;
                    minimumAtribut = 0;
                    maximumAtribut = 0;
                }
            }
            Console.WriteLine("");
            return minMaxTable;
        }

        static void CountNumberOfDifferentAvailableValuesForEachAtributes(string[][] data, string[][] data2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Number of different available values for each atributes");
            Console.ForegroundColor = ConsoleColor.White;
            Dictionary<float, int> dictionaryUse = new Dictionary<float, int>();
            int atributes = data[0].Length;
            for (int i = 0; i < atributes - 1; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    if (dictionaryUse.ContainsKey(float.Parse(data[j][i])))
                        dictionaryUse[float.Parse(data[j][i])] += 1;
                    else
                        dictionaryUse.Add(float.Parse(data[j][i]), 1);
                }
                int uniqueValueOfNumber = 0;
                foreach (var item in dictionaryUse)
                {
                    if (item.Value == 1)
                        uniqueValueOfNumber++;
                }
                Console.Write(data2[i][0] + " " + uniqueValueOfNumber + "\n");
                dictionaryUse.Clear();
            }
        }

        static void ShowListOfAllDifferentAvailableValuesForEachAtributes(string[][] data, string[][] data2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nList of all different available values for each atributes");
            Console.ForegroundColor = ConsoleColor.White;
            Dictionary<float, int> dictionaryUse = new Dictionary<float, int>();
            int atributes = data[0].Length;
            for (int i = 0; i < atributes - 1; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    if (dictionaryUse.ContainsKey(float.Parse(data[j][i])))
                        dictionaryUse[float.Parse(data[j][i])] += 1;
                    else
                        dictionaryUse.Add(float.Parse(data[j][i]), 1);
                }
                Console.WriteLine(data2[i][0] + " ");
                foreach (var item in dictionaryUse)
                {
                    if (item.Value == 1)
                        Console.Write(item.Key + " ");
                }
                Console.WriteLine("\n");
                dictionaryUse.Clear();
            }
        }

        static void ComputeStandardDeviation(string[][] data, string[][] data2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Standard deviation for numeric atributes and decision class");
            Console.ForegroundColor = ConsoleColor.White;
            Dictionary<string, float> Average = ComputeAverage(data, data2);
            double[] tableVariance = ComputeVaration(data, data2, Average);
            int atributes = data[0].Length;
            int counter = 0;
            for (int i = 0; i < atributes - 1; i++)
            {
                if (data2[i][1] == "n")
                {
                    Console.WriteLine(data2[i][0] + " " + tableVariance[counter]);
                    counter++;
                }
            }
        }

        static Dictionary<string, float> ComputeAverage(string[][] data, string[][] data2)
        {
            float sum = 0;
            int atributes = data[0].Length;
            Dictionary<string, float> AtributesSum = new Dictionary<string, float>();
            Dictionary<string, float> Average = new Dictionary<string, float>();
            for (int i = 0; i < atributes - 1; i++)
            {
                if (data2[i][1] == "n")
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (data[j][i] != "?")
                            sum += float.Parse(data[j][i]);
                    }
                    AtributesSum.Add(data2[i][0], sum);
                    Average.Add(data2[i][0], sum / data.Length);
                    sum = 0;
                }
            }
            return Average;
        }
        static double[] ComputeVaration(string[][] data, string[][] data2, Dictionary<string, float> Average)
        {
            int atributes = data[0].Length;
            int averageQuantity = Average.Count;
            double result = 0;
            double endResult = 0;
            double variance = 0;
            int k = 0;
            double[] varianceTable = new double[data2.Length];
            for (int i = 0; i < atributes - 1; i++)
            {
                if (data2[i][1] == "n")
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        result = float.Parse(data[j][i]) - Average[Average.Keys.ElementAt(k)];
                        result = Math.Pow(result, 2);
                        endResult += result;
                    }
                    variance = endResult / data.Length;
                    variance = ComputeSqrt(variance);
                    variance = Math.Round(variance, 2);
                    varianceTable[k] = variance;
                    k++;
                }
            }
            Console.WriteLine("");
            return varianceTable;
        }

        static double ComputeSqrt(double x)
        {
            return x = Math.Sqrt(x);
        }

        static string[][] GenerateTenPercentOfMissingValues(string[][] data, string[][] data2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Original system data");
            Console.ForegroundColor = ConsoleColor.White;
            string tableResult = TablicaDoString(data);
            Console.Write(tableResult);
            int atributes = data[0].Length;
            for (int i = 0; i < atributes - 1; i++)
            {
                if (data2[i][1] == "n")
                {
                    double tenPercentToChange = data.Length * 0.1;
                    tenPercentToChange = (int)tenPercentToChange;
                    Random rnd = new Random();
                    for (int j = 0; j < tenPercentToChange; j++)
                    {
                        int indexToChange = rnd.Next(0, data.Length);
                        data[indexToChange][i] = "?";
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSystem data after generated ten percent of missing values");
            Console.ForegroundColor = ConsoleColor.White;
            string tableResult2 = TablicaDoString(data);
            Console.Write(tableResult2);
            return data;
        }

        static void FixTenPercentOfMissingValues(string[][] data, string[][] data2)
        {
            Dictionary<string, float> AverageAfter = ComputeAverage(data, data2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSystem data after completed the missing values with mean values");
            Console.ForegroundColor = ConsoleColor.White;
            int atributes = data[0].Length;
            int dictionaryLenght = AverageAfter.Count();
            int dictionaryIndexStart = 0;
            double temporary;
            for (int i = 0; i < atributes - 1; i++)
            {
                if (data2[i][1] == "n")
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (data[j][i] == "?")
                        {
                            temporary = Math.Round((AverageAfter.Values.ElementAt(dictionaryIndexStart)), 2);
                            data[j][i] = temporary.ToString();
                        }
                    }
                    dictionaryIndexStart++;
                }
            }
            string tableResult2 = TablicaDoString(data);
            Console.Write(tableResult2);
        }

        static void NormalizeAttributeValuesIntoIntervals(string[][] data, string[][] data2, int a, int b)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSystem data before normalization of descriptor <" + a + "," + b + ">");
            Console.ForegroundColor = ConsoleColor.White;
            string tableResult2 = TablicaDoString(data);
            Console.Write(tableResult2);
            float[] minMaxTable = ShowMinimumAndMaximumValuesOfParticularAtributesReturnTable(data, data2); // minimum first, next maximum etc.
            int atributes = data[0].Length;
            int indexTable = 0;
            float temporary;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("System data after normalization of descriptor <" + a + "," + b + ">");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < data.Length; i++) // objects
            {
                for (int j = 0; j < atributes - 1; j++) // atributes
                {
                    if (data2[j][1] == "n")
                    {
                        temporary = float.Parse(data[i][j]);
                        temporary = (((temporary - minMaxTable[indexTable]) * (b - a)) / (minMaxTable[indexTable + 1] - minMaxTable[indexTable])) + a;
                        double result = Math.Round(temporary, 2);
                        Console.Write(result + " ");
                        indexTable += 2;
                    }
                }
                Console.WriteLine("");
                indexTable = 0;
            }
        }

        static void StandardizeAttributes(string[][] data, string[][] data2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSystem data before standarize attributes");
            Console.ForegroundColor = ConsoleColor.White;
            string tableResult2 = TablicaDoString(data);
            Console.Write(tableResult2);
            Dictionary<string, float> dictionaryWithAverage = ComputeAverage(data, data2);
            float[] averageTable = new float[dictionaryWithAverage.Count];

            int index = 0;
            foreach (var item in dictionaryWithAverage)
            {
                averageTable[index] = item.Value;
                index++;
            }
            double[] varationTable = ComputeVaration(data, data2, dictionaryWithAverage);

            int atributes = data[0].Length;
            int index2 = 0;
            double result = 0;
            int numberOfNumericAtribute = 0;
            for (int i = 0; i < atributes - 1; i++) // atribute counting
            {
                if (data2[i][1] == "n")
                    numberOfNumericAtribute++;
            }
            int numberOfNumericAtributeResult = numberOfNumericAtribute;
            double[,] tableWithStandarizedAttributes = new double[data.Length, numberOfNumericAtribute];
            int atributeIndex = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("System data after standarize attributes");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < atributes - 1; i++) // atributes
            {
                if (data2[i][1] == "n")
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        result = (float.Parse(data[j][i]) - averageTable[index2]) / varationTable[index2];
                        result = Math.Round(result, 2);
                        tableWithStandarizedAttributes[j, atributeIndex] = result;
                        if (numberOfNumericAtribute - 1 > 0)
                        {
                            Console.Write(result + " ");
                            numberOfNumericAtribute--;
                        }
                        else
                        {
                            Console.WriteLine(result + " ");
                            numberOfNumericAtribute = numberOfNumericAtributeResult;
                        }
                    }
                    index2++;
                    atributeIndex++;
                }
            }
            numberOfNumericAtribute = numberOfNumericAtributeResult;
            double sum = 0;
            int numberOfObjects = 0;
            double[] finalAverageTable = new double[numberOfNumericAtributeResult];
            double[] finalVarianceTable = new double[numberOfNumericAtributeResult];
            for (int i = 0; i < (tableWithStandarizedAttributes.GetLength(1)); i++)
            {
                for (int j = 0; j < tableWithStandarizedAttributes.GetLength(0); j++)
                {
                    if (numberOfNumericAtribute - 1 > 0)
                    {
                        Console.Write(tableWithStandarizedAttributes[j, i] + " ");
                        numberOfNumericAtribute--;
                    }
                    else
                    {
                        Console.WriteLine(tableWithStandarizedAttributes[j, i] + " ");
                        numberOfNumericAtribute = numberOfNumericAtributeResult;
                    }
                    sum += tableWithStandarizedAttributes[j, i];
                    numberOfObjects += 1;
                }
                finalAverageTable[i] = sum / numberOfObjects;
            }
            double sum2 = 0;
            for (int i = 0; i < (tableWithStandarizedAttributes.GetLength(1)); i++)
            {
                for (int j = 0; j < tableWithStandarizedAttributes.GetLength(0); j++)
                {
                    sum2 = Math.Pow((tableWithStandarizedAttributes[j, i] - finalAverageTable[i]), 2);
                    finalVarianceTable[i] = ComputeSqrt(sum2);
                }
            }
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Mean value of numeric attribute and value of variance");
            Console.ForegroundColor = ConsoleColor.White;
            for (int k = 0; k < numberOfNumericAtributeResult; k++)
                Console.WriteLine("Average = " + Math.Round(finalAverageTable[k], 2) + ", " + "Variance = " + Math.Round((finalVarianceTable[k]), 2));
        }

        static string ReadCsvFile(string fileLocalization)
        {
            try
            {
                string st = System.IO.File.ReadAllText(fileLocalization);
                Console.WriteLine(st);
                return st;
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem with opening the file.");
                Console.WriteLine(e.Message);
                return "";
            }
        }

        static void ConvertSymbolicValuesOfAttribute(string[][] data)
        {
            //for (int i=0; i<data.GetLength(0)-1; i++)
            //{
            //Console.WriteLine(data[i][4]);
            //}
        }
    }
}