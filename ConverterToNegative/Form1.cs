/*
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
using System.Threading;

namespace ConverterToNegative
{
    /// <summary>
    /// Główna klasa formularza aplikacji konwertera na negatyw.
    /// </summary>
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\DELL\Desktop\ConverterToNegative\x64\Debug\ConverterToNegativeAsm.dll")]
        static extern int MyProc1(int a, int b);
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
            return radioButton1.Checked ? ConvertToNegative(originalImage, degree, maxDegreeOfParallelism) : ConvertToNegativeAsm(originalImage, degree, maxDegreeOfParallelism);
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
        /// Konwertuje podany oryginalny obraz na negatyw z określonym stopniem.
        /// </summary>
        /// <param name="originalImage">Oryginalny obraz do konwersji.</param>
        /// <param name="degree">Stopień negatywu dla konwersji.</param>
        /// <returns>Obraz w negatywie.</returns>
        unsafe
        private Bitmap ConvertToNegative(Bitmap originalImage, int degree, int maxDegreeOfParallelism)
        {
            // Validate degree parameter
            degree = Math.Max(0, Math.Min(100, degree));


            int maxLogicalProcessors = Environment.ProcessorCount;

            if (maxDegreeOfParallelism > maxLogicalProcessors)
            {
                MessageBox.Show("Wybrałeś wartość wątków większą niż ilość procesorów logicznych twojego komputera -> " + maxLogicalProcessors + " ", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                // Create ParallelOptions with specified maxDegreeOfParallelism
                ParallelOptions parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism
                };

                BitmapData bitmapData =
                    originalImage.LockBits(new Rectangle(0, 0, originalImage.Width,
                    originalImage.Height), ImageLockMode.ReadWrite, originalImage.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(originalImage.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, parallelOptions, y =>
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int newR = (int)((currentLine[x + 2] * (100 - degree) + (255 - currentLine[x + 2]) * degree) / 100);
                        int newG = (int)((currentLine[x + 1] * (100 - degree) + (255 - currentLine[x + 1]) * degree) / 100);
                        int newB = (int)((currentLine[x] * (100 - degree) + (255 - currentLine[x]) * degree) / 100);

                        currentLine[x + 2] = (byte)Math.Max(0, Math.Min(255, newR));
                        currentLine[x + 1] = (byte)Math.Max(0, Math.Min(255, newG));
                        currentLine[x] = (byte)Math.Max(0, Math.Min(255, newB));
                    }
                });

                originalImage.UnlockBits(bitmapData);

                return originalImage;
            }
            
        }


        /// <summary>
        /// Konwertuje podany oryginalny obraz na negatyw za pomocą zewnętrznej asemblerowej biblioteki z określonym stopniem.
        /// </summary>
        /// <param name="originalImage">Oryginalny obraz do konwersji.</param>
        /// <param name="degree">Stopień negatywu dla konwersji.</param>
        /// <returns>Obraz w negatywie.</returns>
        /// 

        /*private Bitmap ConvertToNegativeAsm(Bitmap originalImage, int degree, int maxDegreeOfParallelism)
        {
            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);
            for (int y = 0; y < originalImage.Height; y++) {
              for (int x = 0; x < originalImage.Width; x++) {
                Color originalColor = originalImage.GetPixel(x, y);

                int newAsmR = MyProc1(originalColor.R, degree);
                int newAsmG = MyProc1(originalColor.G, degree);
                int newAsmB = MyProc1(originalColor.B, degree);

                newImage.SetPixel(x, y, Color.FromArgb(newAsmR, newAsmG, newAsmB));
              }
            } 
            return newImage;
        }*/
        unsafe
        private Bitmap ConvertToNegativeAsm(Bitmap originalImage, int degree, int maxDegreeOfParallelism)
    {
      // Validate degree parameter
      degree = Math.Max(0, Math.Min(100, degree));
      
      int maxLogicalProcessors = Environment.ProcessorCount;

            if (maxDegreeOfParallelism > maxLogicalProcessors)
            {
                MessageBox.Show("Wybrałeś wartość wątków większą niż ilość procesorów logicznych twojego komputera -> " + maxLogicalProcessors + " ", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                // Create ParallelOptions with specified maxDegreeOfParallelism
                ParallelOptions parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism
                };

                BitmapData bitmapData =
                    originalImage.LockBits(new Rectangle(0, 0, originalImage.Width,
                    originalImage.Height), ImageLockMode.ReadWrite, originalImage.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(originalImage.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, parallelOptions, y =>
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        //int newR = MyProc1((int)currentLine[x + 2], degree);
                        //int newG = MyProc1((int)currentLine[x + 1], degree);
                        //int newB = MyProc1((int)currentLine[x], degree);

                        currentLine[x + 2] = (byte)Math.Max(0, Math.Min(255, MyProc1((int)currentLine[x + 2], degree)));
                        currentLine[x + 1] = (byte)Math.Max(0, Math.Min(255, MyProc1((int)currentLine[x + 1], degree)));
                        currentLine[x] = (byte)Math.Max(0, Math.Min(255, MyProc1((int)currentLine[x], degree)));
                    }
                });

                originalImage.UnlockBits(bitmapData);
                return originalImage;
            }

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