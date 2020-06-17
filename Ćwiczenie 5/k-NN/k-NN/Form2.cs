using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace k_NN
{
    public partial class Form2 : Form
    {
        string[] F_classification(string[] testObject, string[][] trainingSystem, string metricString, int K)
        {
            Metrics m = new Metrics();
            Distance o = new Distance();
            var objectClassified = new string[testObject.Length];
            for (int i = 0; i < testObject.Length; i++)
                objectClassified[i] = testObject[i];
            int lastColumn = objectClassified.Length - 1;
            switch (metricString)
            {
                case "Euclid":
                    var dictionaryAfterMetric = new Dictionary<int, double>();
                    for (int i = 0; i < trainingSystem.Length; i++)
                        dictionaryAfterMetric.Add(i, m.Euclid_metric(testObject, trainingSystem[i]));
                    string decision = o.F_nearestObject(dictionaryAfterMetric, K, trainingSystem);
                    if (decision == "!")
                        objectClassified[lastColumn] = "!";
                    else
                    {
                        string[] object0 = trainingSystem[Convert.ToInt16(decision)];
                        objectClassified[lastColumn] = object0[lastColumn];
                    }
                    break;
                case "Manhattan":
                    var dictionaryAfterMetric1 = new Dictionary<int, double>();
                    for (int i = 0; i < trainingSystem.Length; i++)
                        dictionaryAfterMetric1.Add(i, m.Manhattan_metric(testObject, trainingSystem[i]));
                    string decision1 = o.F_nearestObject(dictionaryAfterMetric1, K, trainingSystem);
                    if (decision1 == "!")
                        objectClassified[lastColumn] = "!";
                    else
                    {
                        string[] object1 = trainingSystem[Convert.ToInt16(decision1)];
                        objectClassified[lastColumn] = object1[lastColumn];
                    }
                    break;
                case "Canberr":
                    var dictionaryAfterMetric2 = new Dictionary<int, double>();
                    for (int i = 0; i < trainingSystem.Length; i++)
                        dictionaryAfterMetric2.Add(i, m.Canberr_metric(testObject, trainingSystem[i]));
                    string decision2 = o.F_nearestObject(dictionaryAfterMetric2, K, trainingSystem);
                    if (decision2 == "!")
                        objectClassified[lastColumn] = "!";
                    else
                    {
                        string[] object2 = trainingSystem[Convert.ToInt16(decision2)];
                        objectClassified[lastColumn] = object2[lastColumn];
                    }
                    break;
                case "Chebyshev":
                    var dictionaryAfterMetric3 = new Dictionary<int, double>();
                    for (int i = 0; i < trainingSystem.Length; i++)
                        dictionaryAfterMetric3.Add(i, m.Chebyshev_metric(testObject, trainingSystem[i]));
                    string decision3 = o.F_nearestObject(dictionaryAfterMetric3, K, trainingSystem);
                    if (decision3 == "!")
                        objectClassified[lastColumn] = "!";
                    else
                    {
                        string[] object3 = trainingSystem[Convert.ToInt16(decision3)];
                        objectClassified[lastColumn] = object3[lastColumn];
                    }
                    break;
                case "Pearson":
                    var dictionaryAfterMetric4 = new Dictionary<int, double>();
                    for (int i = 0; i < trainingSystem.Length; i++)
                        dictionaryAfterMetric4.Add(i, m.Chebyshev_metric(testObject, trainingSystem[i]));
                    string decision4 = o.F_nearestObject(dictionaryAfterMetric4, K, trainingSystem);
                    if (decision4 == "!")
                        objectClassified[lastColumn] = "!";
                    else
                    {
                        string[] object4 = trainingSystem[Convert.ToInt16(decision4)];
                        objectClassified[lastColumn] = object4[lastColumn];
                    }
                    break;
            }
            return objectClassified;
        }
        public void PopulateDataGridView(string[][] testSystemClassified, string[][] testSystem, List<List<int>> classList)
        {
            Distance o = new Distance();
            int lastColumn = testSystemClassified[0].Length - 1;
            var numberOfObjects = new Dictionary<int, int>();
            var Accuracy = new Dictionary<int, double>();
            var Coverage = new Dictionary<int, double>();
            var tempo = new Dictionary<int, double>();
            var dictionaryAfterPrediction = new Dictionary<int, Dictionary<int, int>>();
            numberOfObjects = o.F_Number_of_object(testSystem);
            Accuracy = o.F_Accuracy(testSystemClassified, testSystem,numberOfObjects,divisionIntoClasses1);
            Coverage = o.F_Coverage(testSystemClassified, testSystem,numberOfObjects,divisionIntoClasses1);
            tempo = o.F_Tpr(testSystem, classList, testSystemClassified, divisionIntoClasses1);
            dictionaryAfterPrediction =o.F_Prediction_Dictionary(testSystemClassified, testSystem,divisionIntoClasses1);
            Grid.Columns.Add("", "");
            foreach (var kvp in divisionIntoClasses1)
                Grid.Columns.Add("klasadecyzyjna", Convert.ToString(testSystem[divisionIntoClasses1[kvp.Key][0]][lastColumn]));
            Grid.Columns.Add("numerobiektow", "Number_of_object");
            Grid.Columns.Add("skutecznosc", "Accuracy");
            Grid.Columns.Add("pokrycie", "Coverage");
            for (int i = 0; i < classList.Count; i++)
            {
                int value;
                double acc_value;
                double cov_value;
                var pre_value = new Dictionary<int, int>();
                int value1;
                int value2;
                int key2;
                int key = Convert.ToInt16(testSystem[classList[i][0]][lastColumn]);
                if (i == 0 && i < classList.Count - 1)
                    key2 = Convert.ToInt16(testSystem[classList[i + 1][0]][lastColumn]);
                else
                    key2 = Convert.ToInt16(testSystem[classList[i - 1][0]][lastColumn]);
                Accuracy.TryGetValue(key, out acc_value);
                Coverage.TryGetValue(key, out cov_value);
                numberOfObjects.TryGetValue(key, out value);
                dictionaryAfterPrediction.TryGetValue(key, out pre_value);
                pre_value.TryGetValue(key, out value1);
                pre_value.TryGetValue(key2, out value2);
                var Matrix01 = new int[classList.Count];
                Grid.Rows.Add(Convert.ToString(testSystem[classList[i][0]][lastColumn]), value1, value2, value, acc_value, cov_value);
            }
            int variable = 0;
            var table = new double[tempo.Count];
            foreach (var kvp in tempo)
            {
                tempo.TryGetValue(kvp.Key, out table[variable]);
                variable++;
            }
            Grid.Rows.Add("True Positive Rate", table[1], table[0]);
        }
        static Dictionary<int,List<int>> F_divisionIntoClasses(string[][] testSystem)
        {
            int[] decisionColumn = new int[testSystem.Length];
            var classList = new List<int>();
            List<int> unique = new List<int>();
            var divisionedClasses = new Dictionary<int, List<int>>();
            decisionColumn = F_column(testSystem, (testSystem[0].Length - 1));
            unique = F_unique(decisionColumn);
            for (int j = 0; j < unique.Count; j++)
            {
                for (int i = 0; i < testSystem.Length; i++)
                    if (decisionColumn[i] == unique[j])
                        classList.Add(i);
                divisionedClasses.Add(unique[j],classList);
                classList = new List<int>();
            }
            return divisionedClasses;
        }
        public Form2(string[][] testSystem, string[][] trainingSystem)
        {
            InitializeComponent();
            string[][] testSystemClassified = new string[testSystem.Length][];
            var classList = new List<List<int>>();
            var frequencyDictionary = new Dictionary<List<int>, int>();
            var divisionedClasses = new Dictionary<int, List<int>>();
            divisionedClasses = F_divisionIntoClasses(testSystem);
            divisionIntoClasses1 = divisionedClasses;
            int K = 0;
            Combobax.Items.Add("Euclid");
            Combobax.Items.Add("Manhattan");
            Combobax.Items.Add("Canberr");
            Combobax.Items.Add("Chebyshev");
            Combobax.Items.Add("Pearson");
            classList = F_divisionedClasses(trainingSystem);
            var list = new List<List<int>>();
            list = F_divisionedClasses(testSystem);
            frequencyDictionary = F_frequency(classList);
            K = F_minimum_k(frequencyDictionary);
            Nup.Maximum = K;
            Combobax.SelectedIndex = 0;
            testSystem1 = testSystem;
            trainingSystem1 = trainingSystem;
            list1 = list;
            lastColumn1 = testSystem[0].Length - 1; ;
        }
        static int F_minimum_k(Dictionary<List<int>, int> frequencyDictionary)
        {
            int minimum = 10000;
            foreach (var kvp in frequencyDictionary)
            {
                if (minimum > kvp.Value)
                    minimum = kvp.Value;
            }
            return minimum;
        }
        static int[] F_column(string[][] table, int n_columns)
        {
            int[] column = new int[table.Length];
            for (int i = 0; i < column.Length; i++)
                column[i] = Convert.ToInt16(table[i][n_columns]);
            return column;
        }
        static List<int> F_unique(int[] table)
        {
            List<int> list = new List<int>();
            list.Add(table[0]);
            for (int i = 1; i < table.Length; i++)
                if (!list.Contains(table[i]))
                    list.Add(table[i]);
            return list;
        }
        static List<List<int>> F_divisionedClasses(string[][] testSystem)
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
                    if (decisionColumn[i] == unique[j])
                        classList.Add(i);
                classesList.Add(classList);
                classList = new List<int>();
            }
            return classesList;
        }
        static Dictionary<List<int>, int> F_frequency(List<List<int>> list)
        {
            var sl = new Dictionary<List<int>, int>();
            for (int i = 0; i < list.Count; i++)
                sl.Add(list[i], 0);
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list[i].Count; j++)
                    sl[list[i]]++;
            return sl;
        }
        string[][] testSystem1;
        string[][] trainingSystem1;
        List<List<int>> list1;
        Dictionary<int, List<int>> divisionIntoClasses1;
        int lastColumn1;
        private void bt1_Click(object sender, EventArgs e)
        {
            string stringMetric;
            int K1;
            K1 = Convert.ToInt16(Nup.Value);
            stringMetric = Combobax.SelectedItem.ToString();
            string[][] testSystemClassified = new string[this.testSystem1.Length][];
            for (int i = 0; i < this.testSystem1.Length; i++)
                testSystemClassified[i] = F_classification(testSystem1[i], trainingSystem1, stringMetric, K1);
            Grid.Rows.Clear();
            Grid.Columns.Clear();
            Grid.Refresh();
            PopulateDataGridView(testSystemClassified, testSystem1, list1);
        }
        private void Form2_Load(object sender, EventArgs e) {}
        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e) {}

        private void Combobax_SelectedIndexChanged(object sender, EventArgs e) {}

        private void Nup_ValueChanged(object sender, EventArgs e) {}

        private void label1_Click(object sender, EventArgs e) {}
    }
}