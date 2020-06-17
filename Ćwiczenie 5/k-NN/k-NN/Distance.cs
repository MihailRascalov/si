using System;
using System.Collections.Generic;

namespace k_NN
{
    class Distance
    {
        public Dictionary<int, int> F_Number_of_object(string[][] testSystem)
        {
            var NumberOfObject = new Dictionary<int, int>();
            int lastColumn = testSystem[0].Length - 1;
            for (int i = 0; i < testSystem.Length; i++)
            {
                if (!NumberOfObject.ContainsKey(Convert.ToInt16(testSystem[i][lastColumn])))
                    NumberOfObject.Add(Convert.ToInt16(testSystem[i][lastColumn]), 0);
            }
            for (int i = 0; i < testSystem.Length; i++)
                NumberOfObject[Convert.ToInt16(testSystem[i][lastColumn])]++;
            return NumberOfObject;
        }
        public Dictionary<int, double> F_Accuracy(string[][] testSystemClassified, string[][] testSystem, Dictionary<int, int> numberOfObject, Dictionary<int, List<int>> divisionIntoClasses)
        {
            var Accuracy = new Dictionary<int, double>();
            int lastColumn = testSystemClassified[0].Length - 1;
            foreach (var kvp in divisionIntoClasses)
            {
                double variable = 0;
                for (int j = 0; j < divisionIntoClasses[kvp.Key].Count; j++)
                {
                    if (testSystemClassified[divisionIntoClasses[kvp.Key][j]][lastColumn] != testSystem[divisionIntoClasses[kvp.Key][j]][lastColumn] && testSystemClassified[divisionIntoClasses[kvp.Key][j]][lastColumn] != "!")
                        variable += 1;
                }
                if (variable == 0)
                    Accuracy.Add(kvp.Key, 1);
                else
                {
                    int value;
                    numberOfObject.TryGetValue(kvp.Key, out value);
                    Accuracy.Add(kvp.Key, (variable / value));
                }
            }
            return Accuracy;
        }
        public Dictionary<int, double> F_Coverage(string[][] testSystemClassified, string[][] testSystem, Dictionary<int, int> numberOfObject, Dictionary<int, List<int>> divisionIntoClasses)
        {
            int lastColumn = testSystemClassified[0].Length - 1;
            var Coverage = new Dictionary<int, double>();
            foreach (var kvp in divisionIntoClasses)
            {
                double variable = 0;
                for (int j = 0; j < divisionIntoClasses[kvp.Key].Count; j++)
                {
                    if (testSystemClassified[divisionIntoClasses[kvp.Key][j]][lastColumn] == "!")
                        variable += 1;
                }
                if (variable == 0)
                    Coverage.Add(kvp.Key, 1);
                else
                {
                    int value;
                    numberOfObject.TryGetValue(kvp.Key, out value);
                    Coverage.Add(kvp.Key, variable / value);
                }
            }
            return Coverage;
        }
        public Dictionary<int, Dictionary<int, int>> F_Prediction_Dictionary(string[][] testSystemClassified, string[][] testSystem, Dictionary<int, List<int>> divisionIntoClasses)
        {
            int lastColumn = testSystemClassified[0].Length - 1;
            var predictionDictionary = new Dictionary<int, Dictionary<int, int>>();
            for (int j = 0; j < divisionIntoClasses.Count; j++)
                predictionDictionary.Add(Convert.ToInt16(testSystem[j][lastColumn]), new Dictionary<int, int>());
            for (int j = 0; j < divisionIntoClasses.Count; j++)
                for (int i = 0; i < testSystem.Length; i++)
                    if (!predictionDictionary[(Convert.ToInt16(testSystem[j][lastColumn]))].ContainsKey((Convert.ToInt16(testSystem[i][lastColumn]))))
                        predictionDictionary[Convert.ToInt16(testSystem[j][lastColumn])].Add(Convert.ToInt16(testSystem[i][lastColumn]), 0);
            foreach (var kvp in divisionIntoClasses)
                for (int j = 0; j < divisionIntoClasses[kvp.Key].Count; j++)
                    if (testSystemClassified[divisionIntoClasses[kvp.Key][j]][lastColumn] != "!")
                        predictionDictionary[Convert.ToInt16(testSystem[divisionIntoClasses[kvp.Key][j]][lastColumn])][Convert.ToInt16(testSystemClassified[divisionIntoClasses[kvp.Key][j]][lastColumn])]++;
            return predictionDictionary;
        }
        public Dictionary<int, double> F_Tpr(string[][] testSystem, List<List<int>> class_list, string[][] testSystemClassified, Dictionary<int, List<int>> divisionIntoClasses)
        {
            var tempo = new Dictionary<int, double>();
            int lastColumn = testSystemClassified[0].Length - 1;
            var classListDecision = new List<int>();
            var classList = new List<List<int>>();
            for (int i = 0; i < class_list.Count; i++)
                for (int j = 0; j < class_list[i].Count; j++)
                    if (!classListDecision.Contains(Convert.ToInt16(testSystem[class_list[i][j]][lastColumn])))
                        classListDecision.Add(Convert.ToInt16(testSystem[class_list[i][j]][lastColumn]));
            var tmpSystemClassified = new string[testSystemClassified.Length][];
            for (int x = 0; x < tmpSystemClassified.Length; x++)
            {
                tmpSystemClassified[x] = new string[testSystemClassified[0].Length];
                for (int xx = 0; xx < testSystemClassified[0].Length; xx++)
                {
                    if (testSystemClassified[x][lastColumn] != "!")
                        tmpSystemClassified[x][xx] = testSystemClassified[x][xx];
                    else
                        tmpSystemClassified[x][xx] = "-1";
                }
            }
            var classDivision = new Dictionary<int, List<int>>();
            classDivision = F_ClassDivision(tmpSystemClassified);
            foreach (var kvp in classDivision)
            {
                double variable = 0;
                double X = 0;
                if (kvp.Key != -1)
                {
                    for (int j = 0; j < classDivision[kvp.Key].Count; j++)
                    {
                        if ((testSystemClassified[classDivision[kvp.Key][j]][lastColumn] != testSystem[classDivision[kvp.Key][j]][lastColumn]))
                            variable += 1;
                        else if (testSystemClassified[classDivision[kvp.Key][j]][lastColumn] == testSystem[classDivision[kvp.Key][j]][lastColumn])
                            X += 1;
                    }
                    if (X != 0)
                        tempo.Add(kvp.Key, X / (X + variable));
                    else
                        tempo.Add(kvp.Key, 0);
                }
            }
            if (classDivision.ContainsKey(-1) && classDivision.Count == 2)
            {
                foreach (var kvp in divisionIntoClasses)
                    if (!classDivision.ContainsKey(kvp.Key))
                        tempo.Add(kvp.Key, 0);
            }
            return tempo;
        }
        public Dictionary<int, List<int>> F_ClassDivision(string[][] testSystem)
        {
            int[] columnDecision = new int[testSystem.Length];
            var classList = new List<int>();
            List<int> unique = new List<int>();
            var classDivisioned = new Dictionary<int, List<int>>();
            columnDecision = F_column(testSystem, (testSystem[0].Length - 1));
            unique = F_unique(columnDecision);
            for (int j = 0; j < unique.Count; j++)
            {
                for (int i = 0; i < testSystem.Length; i++)
                    if (columnDecision[i] == unique[j])
                        classList.Add(i);
                classDivisioned.Add(unique[j], classList);
                classList = new List<int>();
            }
            return classDivisioned;
        }
        static List<int> F_unique(int[] tab)
        {
            List<int> list = new List<int>();
            list.Add(tab[0]);
            for (int i = 1; i < tab.Length; i++)
                if (!list.Contains(tab[i]))
                    list.Add(tab[i]);
            return list;
        }
        static int[] F_column(string[][] table, int n_columns)
        {
            int[] column = new int[table.Length];
            for (int i = 0; i < column.Length; i++)
                column[i] = Convert.ToInt16(table[i][n_columns]);
            return column;
        }
        static List<List<int>> F_divisionedClass(string[][] testSystem)
        {
            int[] decisionColumn = new int[testSystem.Length];
            var classList = new List<int>();
            List<int> unique = new List<int>();
            decisionColumn = F_column(testSystem, (testSystem[0].Length - 1));
            unique = F_unique(decisionColumn);
            var classesList = new List<List<int>>();
            for (int j = 0; j < unique.Count; j++)
            {
                for (int i = 0; i < testSystem.Length; i++)
                {
                    if (decisionColumn[i] == unique[j])
                        classList.Add(i);
                }
                classesList.Add(classList);
                classList = new List<int>();
            }
            return classesList;
        }
        static string F_whicClassIsNearest(Dictionary<int, List<double>> dictionary)
        {
            string whichClass = "!";
            double result;
            var listV2 = new List<int>();
            var list = new List<double>();
            var dictionaryV2 = new Dictionary<int, double>();
            foreach (var kvp in dictionary)
            {
                double variable = 0;
                for (int j = 0; j < kvp.Value.Count; j++)
                    variable += kvp.Value[j];
                dictionaryV2.Add(kvp.Key, variable);
            }
            result = 10000;
            foreach (var kvp in dictionaryV2)
                if (result > kvp.Value)
                    result = kvp.Value;
            int tempo = 0;
            foreach (var kvp in dictionaryV2)
                if (result == kvp.Value)
                    tempo++;
            if (tempo == dictionaryV2.Count)
                whichClass = "!";
            else
                foreach (var kvp in dictionaryV2)
                    if (kvp.Value == result)
                        whichClass = Convert.ToString(kvp.Key);
            return whichClass;
        }
        public string F_nearestObject(Dictionary<int, double> dictionaryAfterMetric, int K, string[][] trainingSystem)
        {
            var dictionaryWithNearestNeighbor = new Dictionary<int, List<double>>();
            var listSelectedNeighbors = new List<double>();
            var divisionList = new List<int>();
            var list = new List<double>();
            var classTrainingList = new List<List<int>>();
            classTrainingList = F_divisionedClass(trainingSystem);
            double minimum;
            string decision;
            for (int j = 0; j < classTrainingList.Count; j++)
            {
                list = new List<double>();
                divisionList = classTrainingList[j];
                minimum = 0;
                for (int ii = 0; ii < K; ii++)
                {
                    minimum = 1000;
                    for (int i = 0; i < divisionList.Count; i++)
                    {
                        if (dictionaryAfterMetric.ContainsKey(divisionList[i]))
                        {
                            double value;
                            dictionaryAfterMetric.TryGetValue(divisionList[i], out value);
                            if (list.Contains(value)) {}
                            else if (minimum > value)
                                minimum = value;
                        }
                    }
                    list.Add(minimum);
                }
                dictionaryWithNearestNeighbor.Add(divisionList[0], list);
            }
            decision = F_whicClassIsNearest(dictionaryWithNearestNeighbor);
            return decision;
        }
    }
}