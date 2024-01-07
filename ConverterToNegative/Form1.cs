using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConverterToNegative
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
            pictureBox2.Image = null;
    }

    private void button1_Click(object sender, EventArgs e)
    {
            //TODO: Dodać obsługę zapisu pliku z użyciem saveFileDialog1
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      // Obsługa zdarzenia zmiany wartości suwaka
            int currentValue = trackBar1.Value;
            label2.Text = currentValue.ToString();
    }

    private void button3_Click(object sender, EventArgs e)
    {
            openFileDialog1.Filter = "Image Files(*.bmp)|*.bmp";
            openFileDialog1.ShowDialog();
    }

    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {
            pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
    }

    private void trackBar2_Scroll(object sender, EventArgs e)
    {
            int currentValue = trackBar2.Value;
            label4.Text = currentValue.ToString();
    }

    private void button4_Click(object sender, EventArgs e)
    {
            //TODO: Dodać kod uruchamiający proces konwersji bitmapy
    }

        /*

        private Bitmap ConvertToSepia(Bitmap originalImage)
        {
          Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);

          for (int y = 0; y < originalImage.Height; y++)
          {
            for (int x = 0; x < originalImage.Width; x++)
            {
              Color originalColor = originalImage.GetPixel(x, y);

              int sepiaR = (int)(originalColor.R * 0.393 + originalColor.G * 0.769 + originalColor.B * 0.189);
              int sepiaG = (int)(originalColor.R * 0.349 + originalColor.G * 0.686 + originalColor.B * 0.168);
              int sepiaB = (int)(originalColor.R * 0.272 + originalColor.G * 0.534 + originalColor.B * 0.131);

              // Zapewnij, aby wartości kolorów nie przekroczyły 255
              sepiaR = Math.Min(sepiaR, 255);
              sepiaG = Math.Min(sepiaG, 255);
              sepiaB = Math.Min(sepiaB, 255);

              newImage.SetPixel(x, y, Color.FromArgb(sepiaR, sepiaG, sepiaB));
            }
          }

          return newImage;
        }*/
    }
}
