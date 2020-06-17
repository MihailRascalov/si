using System;
using System.Windows.Forms;

namespace k_NN
{
    public partial class kNN : Form
    {
        private string[][] testSystem;
        private string[][] trainingSystem;

        public kNN()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            /****************** Miejsce na rozwiązanie **********************/
            Form2 forma = new Form2(testSystem, trainingSystem);
            forma.Show();
            /****************** Koniec miejsca na rozwiązanie ***************/
        }

        private void btnSelectTestSystem_Click(object sender, EventArgs e)
        {
            DialogResult resultOfSelectedFile = ofd.ShowDialog();
            if (resultOfSelectedFile != DialogResult.OK)
                return;

            string trescPliku = System.IO.File.ReadAllText(ofd.FileName); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            testSystem = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                testSystem[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    testSystem[i][j] = cyfra;  
                }
            }
            tbTestSystem.Text = TablicaDoString(testSystem);
        }

        private void btnSelectTrainingSystem_Click(object sender, EventArgs e)
        {
            DialogResult resultOfSelectedFile = ofd.ShowDialog();
            if (resultOfSelectedFile != DialogResult.OK)
                return;

            string trescPliku = System.IO.File.ReadAllText(ofd.FileName); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            trainingSystem = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                trainingSystem[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    trainingSystem[i][j] = cyfra;
                }
            }
            tbTrainingSystem.Text = TablicaDoString(trainingSystem);
        }

        private void tbTestSystem_TextChanged(object sender, EventArgs e) { }

        private void tbTrainingSystem_TextChanged(object sender, EventArgs e) { }

        public string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                    wynik += tab[i][j].ToString() + " ";
                wynik = wynik.Trim() + Environment.NewLine;
            }
            return wynik;
        }

        public double StringToDouble(string liczba)
        {
            double wynik; liczba = liczba.Trim();
            if (!double.TryParse(liczba.Replace(',', '.'), out wynik) && !double.TryParse(liczba.Replace('.', ','), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do double");
            return wynik;
        }

        public int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");
            return wynik;
        }
    }
}