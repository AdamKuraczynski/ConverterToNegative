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

namespace ConverterToNegative
{
    public partial class Form1 : Form
    {
    [DllImport("ConverterToNegativeAsm.dll")]
    static extern int MyProc1(int a, int b);
    public Form1() {
            InitializeComponent();
            radioButton1.Checked = true;
            InitializeTimer();
    }
        private void button2_Click(object sender, EventArgs e) {
            pictureBox2.Image = null;
        }

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

        private void trackBar1_Scroll(object sender, EventArgs e) {
            int currentValue = trackBar1.Value;
            label2.Text = currentValue.ToString();
        }

        private void button3_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "Image Files(*.bmp)|*.bmp";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
        }

        private void trackBar2_Scroll(object sender, EventArgs e) {
            int currentValue = trackBar2.Value;
            label4.Text = currentValue.ToString();
        }

        private Bitmap selectedConvertFunction(Bitmap originalImage, int degree) {
            return radioButton1.Checked ? ConvertToNegative(originalImage, degree) : ConvertToNegativeAsm(originalImage, degree);
        }

        private Queue<long> lastFiveTimes = new Queue<long>();

        private void button4_Click(object sender, EventArgs e) {
            if (pictureBox1.Image != null) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                int degree = trackBar1.Value;
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

                Bitmap convertedImage = selectedConvertFunction(originalImage, degree);

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
        private Bitmap ConvertToNegative(Bitmap originalImage, int degree)
        {
            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);
            for (int y = 0; y < originalImage.Height; y++) {
                for (int x = 0; x < originalImage.Width; x++) {
                    Color originalColor = originalImage.GetPixel(x, y);

                    int newR = (originalColor.R * (100 - degree) + (255 - originalColor.R) * degree) / 100;
                    int newG = (originalColor.G * (100 - degree) + (255 - originalColor.G) * degree) / 100;
                    int newB = (originalColor.B * (100 - degree) + (255 - originalColor.B) * degree) / 100;

                    newImage.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return newImage;
        }

        /// <summary>
        /// Konwertuje podany oryginalny obraz na negatyw za pomocą zewnętrznej asemblerowej biblioteki z określonym stopniem.
        /// </summary>
        /// <param name="originalImage">Oryginalny obraz do konwersji.</param>
        /// <param name="degree">Stopień negatywu dla konwersji.</param>
        /// <returns>Obraz w negatywie.</returns>
        private Bitmap ConvertToNegativeAsm(Bitmap originalImage, int degree)
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
        }

        private System.Windows.Forms.Timer systemTimer;

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

        private void systemTimer_Tick(object sender, EventArgs e) {
            DisplaySystemTime();
        }
        private void DisplaySystemTime() {
            DateTime currentTime = DateTime.Now;
            label13.Text = $"{currentTime.ToString()}";
        }
    }
}