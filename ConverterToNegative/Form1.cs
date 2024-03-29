﻿/*
    Temat projektu: Image Converter to Negative
    Krótki opis algorytmu: Aplikacja pobiera zdjęcie zapisane jako bitmapa, po czym piksel po pikselu przelicza wartości kolorów
    RGB na ich wartości w negatywie wg. wzoru: 
    newColor = ((100 - degree) * oldColor + (255 - oldColor) * degree) / 100
    gdzie: 
          newColor: to wartość negatywu
          oldColor: kolor na wejściu
          degree: stopień negatywu
    Data: 12.01.2024r
    Semestr/Rok akademicki: se. V, r.a. 2023/2024
    Nazwisko autora: Krzysztof Adam, Adam Kuraczyński, Bartłomiej Kędroń
    Aktualną wersję programu: 1.0
    Historia zmian: https://github.com/bartlomi/ConverterToNegative
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using ConverterToNegativeC_;

namespace ConverterToNegative
{
    /// <summary>
    /// Główna klasa formularza aplikacji konwertera na negatyw.
    /// </summary>
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\DELL\Desktop\ConverterToNegative\x64\Debug\ConverterToNegativeAsm.dll")]
        static extern void MyProc1(byte[] a, byte[] b, byte[] c);
        [DllImport(@"C:\Users\DELL\Desktop\ConverterToNegative\x64\Debug\ConverterToNegativeAsm.dll")]
        static extern void MyProc2(byte[] a, byte[] b, byte[] c);
        [DllImport(@"C:\Users\DELL\Desktop\ConverterToNegative\x64\Debug\ConverterToNegativeAsm.dll")]
        static extern void MyProc3(byte[] a, byte[] b, byte[] c);

        ConverterC ConverterC = new ConverterC();

        private System.Windows.Forms.Timer systemTimer;

        /// <summary>
        /// Konstruktor głównej formy.
        /// </summary>
        public Form1() {
            InitializeComponent();
            radioButton1.Checked = true;
            InitializeTimer();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku 2 - zeruje obraz wynikowy.
        /// </summary>
        private void button2_Click(object sender, EventArgs e) {
            pictureBox2.Image = null;
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku 1 - zapisuje obraz wynikowy w formacie BMP.
        /// </summary>
        private void button1_Click(object sender, EventArgs e) {
            if (pictureBox2.Image != null) {
                saveFileDialog1.Filter = "Image Files(*.bmp)|*.bmp";
                saveFileDialog1.Title = "Zapisz obraz";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "") {
                    pictureBox2.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    MessageBox.Show("Obraz został pomyślnie zapisany.", "Zapisano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } else {
                MessageBox.Show("Proszę najpierw przekonwertować obraz.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obsługuje zdarzenie przesunięcia suwaka - aktualizuje etykietę z wartością suwaka.
        /// </summary>
        private void trackBar1_Scroll(object sender, EventArgs e) {
            int currentValue = trackBar1.Value;
            label2.Text = currentValue.ToString();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku 3 - otwiera okno dialogowe do wyboru pliku.
        /// </summary>
        private void button3_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "Image Files(*.bmp)|*.bmp";
            openFileDialog1.ShowDialog();
        }

        /// <summary>
        /// Obsługuje zdarzenie wybrania pliku przez OpenFileDialog - wczytuje obraz do negatywu.
        /// </summary>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
        }


        private int maxDegreeOfParallelism = 1;
        /// <summary>
        /// Obsługuje zdarzenie przesunięcia suwaka 2 - aktualizuje etykietę z wartością suwaka.
        /// </summary>
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int[] allowedValues = new int[] { 1, 2, 4, 8, 16, 32, 64 };
            int newValue = trackBar2.Value;
            int nearestValue = allowedValues.OrderBy(v => Math.Abs(newValue - v)).First();
            trackBar2.Value = nearestValue;

            label4.Text = nearestValue.ToString();
            maxDegreeOfParallelism = trackBar2.Value;

        }

        /// <summary>
        /// Wybiera odpowiednią funkcję konwersji w zależności od wyboru użytkownika.
        /// </summary>
        private Bitmap selectedConvertFunction(Bitmap originalImage, int degree, int maxDegreeOfParallelism) {
            return radioButton1.Checked ? ConverterC.ConvertToNegative(originalImage, degree, maxDegreeOfParallelism) : ConvertToNegativeAsm(originalImage, degree, maxDegreeOfParallelism);
        }

        /// <summary>
        /// Kolejka do przechowywania ostatnich pięciu czasów operacji.
        /// </summary>
        private Queue<long> lastFiveTimes = new Queue<long>();

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku 4 - przeprowadza konwersję obrazu i mierzy czas operacji
        /// średni czas ostatnich pięciu operacji oraz ilość cykli procesora.
        /// </summary>
        private void button4_Click(object sender, EventArgs e) {
            if (pictureBox1.Image != null) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                int degree = trackBar1.Value;
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

                Bitmap convertedImage = selectedConvertFunction(originalImage, degree, maxDegreeOfParallelism);

                pictureBox2.Image = convertedImage;

                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;

                lastFiveTimes.Enqueue(elapsedTime.Ticks);

                while (lastFiveTimes.Count > 5) lastFiveTimes.Dequeue();
 
                long meanTicks = lastFiveTimes.Count > 0 ? (long)lastFiveTimes.Average() : 0;

                TimeSpan meanTime = TimeSpan.FromTicks(meanTicks);
                label10.Text = $"{meanTime.Seconds:D2} sec : {meanTime.Milliseconds:D3} ms";

                label9.Text = $"{elapsedTime.Seconds:D2} sec : {elapsedTime.Milliseconds:D3} ms";

                label15.Text = $"{stopwatch.ElapsedTicks}";
            } else MessageBox.Show("Proszę wybrać obraz przed konwersją.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    /// <summary>
    /// Konwertuje podany oryginalny obraz na negatyw za pomocą zewnętrznej asemblerowej biblioteki z określonym stopniem.
    /// </summary>
    /// <param name="originalImage">Oryginalny obraz do konwersji.</param>
    /// <param name="degree">Stopień negatywu dla konwersji.</param>
    /// <returns>Obraz w negatywie.</returns>
    /// 
    unsafe
    private Bitmap ConvertToNegativeAsm(Bitmap originalImage, int degree, int maxDegreeOfParallelism)
    {
      Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);
      byte[] degreeB = { (byte)degree, (byte)degree, (byte)degree };
      byte[] stala1 = { 255, 255, 255 };
      byte[] stala2 = { 100, 100, 100 };
      byte[] wynik1 = { 0, 0, 0 };
      byte[] wynik2 = { 0, 0, 0 };

      for (int y = 0; y < originalImage.Height; y++)
      {
        for (int x = 0; x < originalImage.Width; x++)
        {
          Color originalColor = originalImage.GetPixel(x, y);
          byte[] originalColorB = { originalColor.R, originalColor.G, originalColor.B };

          MyProc2(stala2, degreeB, wynik1);
          MyProc2(stala1, originalColorB, wynik2);

          int newR = (originalColor.R * wynik1[0] + wynik2[0] * degree) / 100;
          int newG = (originalColor.G * wynik1[1] + wynik2[1] * degree) / 100;
          int newB = (originalColor.B * wynik1[2] + wynik2[2] * degree) / 100;

          newImage.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
        }
      }

      return newImage;
    }



    /// <summary>
    /// Konwertuje podany oryginalny obraz na negatyw za pomocą zewnętrznej biblioteki z określonym stopniem.
    /// </summary>
    /// <param name="originalImage">Oryginalny obraz do konwersji.</param>
    /// <param name="degree">Stopień negatywu dla konwersji.</param>
    /// <returns>Obraz w negatywie.</returns>
    private void InitializeTimer() {
            systemTimer = new System.Windows.Forms.Timer();
            systemTimer.Tick += new EventHandler(systemTimer_Tick);
            systemTimer.Interval = 1000;
            systemTimer.Start();
        }

        /// <summary>
        /// Obsługuje zdarzenie systemowego timera - wyświetla bieżący czas systemowy.
        /// </summary>
        private void systemTimer_Tick(object sender, EventArgs e) {
            DisplaySystemTime();
        }

        /// <summary>
        /// Wyświetla bieżący czas systemowy.
        /// </summary>
        private void DisplaySystemTime() {
            DateTime currentTime = DateTime.Now;
            label13.Text = $"{currentTime.ToString()}";
        }
    }
}