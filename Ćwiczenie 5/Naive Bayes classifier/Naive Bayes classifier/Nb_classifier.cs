using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Naive_Bayes_classifier
{
    class Nb_classifier
    {
        private List<double[]> testData = new List<double[]>();
        private List<double[]> trainingData = new List<double[]>();

        private List<string> testClass = new List<string>();
        private List<string> trainingClass = new List<string>();
        
        private int numberOfTestObjects = 0;
        private int numberOfTrainingObjects = 0;

        private List<string> uniqueClass = new List<string>();

        private List<string> Logs = new List<string>();
        private List<string> Logs2 = new List<string>();

        private List<int> Bads = new List<int>();
        private List<double> Goods = new List<double>();

        public void Start()
        {
            LoadData("testSystem.txt", "trainingSystem.txt"); // data from txt files
            GetUniqueClass();
            BayesAlgorithm();
            ShowResult();
        }
        public void ShowResult()
        {
            foreach (var item in Logs2)
                Console.WriteLine(item);
            Console.WriteLine("");
            foreach (var item in Logs)
                Console.WriteLine(item);
        }
        public void LoadData(string TestPath, string TrainingPath)
        {
            var temp = File.ReadAllLines(TestPath);
            foreach (var item in temp)
            {
                numberOfTestObjects++;
                var table = item.Split(' ');
                double[] numberdata = new double[table.Length - 1];
                for (int i = 0; i < table.Length - 1; i++)
                    numberdata[i] = Convert.ToDouble(table[i]);
                testData.Add(numberdata);
                testClass.Add(table[table.Length - 1]);
            }
            temp = File.ReadAllLines(TrainingPath);
            foreach (var item in temp)
            {
                numberOfTrainingObjects++;
                var table = item.Split(' ');
                double[] numberdata = new double[table.Length - 1];
                for (int i = 0; i < table.Length - 1; i++)
                    numberdata[i] = Convert.ToDouble(table[i]);
                trainingData.Add(numberdata);
                trainingClass.Add(table[table.Length - 1]);
            }
        }
        private void BayesAlgorithm()
        {
            Logs.Add("Test object\t\tHidden expert decision\t\tThe decision of our classifier");
            Logs2.Add("Gobal Accuracy\t\t\tBalanced Accuracy");
            for (int obj = 0; obj < testData.Count; obj++)
            {
                string expertDecission = testClass[obj];
                string decision = "";
                var row = GetRow(testData, obj);
                List<double> Sums = new List<double>();
                foreach (var decissionClass in uniqueClass)
                {
                    double decisionProbability = ClassProbability(decissionClass);
                    double result = 0.0;
                    for (int i = 0; i < row.Length; i++)
                    {
                        var column = GetColumn(trainingData, i);
                        double proba = (CommonPart(column, row[i], decissionClass)) / decisionProbability;
                        result += proba;
                    }
                    result = result * decisionProbability;
                    Sums.Add(result);
                }
                decision = uniqueClass[Sums.IndexOf(Sums.Max())];
                if (decision != expertDecission)
                    Bads[Sums.IndexOf(Sums.Max())] += 1;
                else
                    Goods[Sums.IndexOf(Sums.Max())] += 1;
                Logs.Add(String.Format("X{0}\t\t\t\t" + expertDecission + "\t\t\t\t" + decision, (obj + 1)));
            }
            double global = (double)(numberOfTestObjects - Bads.Sum()) / numberOfTestObjects;
            double balanced = 0.0;
            List<double> lenght = new List<double>();
            foreach (var item in uniqueClass)
            {
                int tempo = 0;
                foreach (var tmp in testClass)
                    if (tmp == item)
                        tempo++;
                lenght.Add(tempo);
            }
            for (int i = 0; i < uniqueClass.Count; i++)
                balanced += (Goods[i] / lenght[i]);
            balanced = balanced / (double)uniqueClass.Count;
            Logs2.Add(global + "\t\t\t" + Math.Round(balanced, 2));
        }
        private double[] GetColumn(List<double[]> Data, int n)
        {
            double[] column = new double[Data[n].Length];
            for (int i = 0; i < Data[n].Length; i++)
                column[i] = Data[i][n];
            return column;
        }
        private double[] GetRow(List<double[]> Data, int n)
        {
            double[] row = new double[Data[n].Length];
            for (int i = 0; i < Data[n].Length; i++)
                row[i] = Data[n][i];
            return row;
        }
        private double ClassProbability(string decission)
        {
            int result = 0;
            for (int i = 0; i < trainingClass.Count; i++)
                if (decission == trainingClass[i])
                    result++;
            return ((double)result / (double)trainingClass.Count);
        }

        private double CommonPart(double[] col, double val, string dec)
        {
            int result = 0;
            for (int i = 0; i < col.Length; i++)
                if (val == Convert.ToDouble(trainingClass[i]) && dec == trainingClass[i])
                    result++;
            return ((double)result / (double)col.Length) / ClassProbability(dec);
        }
        private void GetUniqueClass()
        {
            uniqueClass.Clear();
            foreach (var item in testClass)
                if (!uniqueClass.Contains(item))
                {
                    uniqueClass.Add(item);
                    Bads.Add(0);
                    Goods.Add(0);
                }
        }
    }
}