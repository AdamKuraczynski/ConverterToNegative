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

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {

    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void label7_Click(object sender, EventArgs e)
    {

    }

    private void label9_Click(object sender, EventArgs e)
    {

    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      // Obsługa zdarzenia zmiany wartości suwaka
      int currentValue = trackBar1.Value;
      label2.Text = currentValue.ToString();
    }

    private void button3_Click(object sender, EventArgs e)
    {

    }

    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {

    }

    /*private void btnConvertToSepia_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        string filePath = openFileDialog1.FileName;

        // Wczytaj obraz
        Bitmap originalImage = new Bitmap(filePath);

        // Konwertuj na sepię
        Bitmap sepiaImage = ConvertToSepia(originalImage);

        // Wyświetl oba obrazy (oryginalny i sepiowy)
        pictureBox1_Click.Image = originalImage;
        pictureBox2_Click.Image = sepiaImage;
      }
    }

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
