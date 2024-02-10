using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConverterToNegativeC_
{
    public class ConverterC
    {
        /// <summary>
        /// Konwertuje podany oryginalny obraz na negatyw z określonym stopniem.
        /// </summary>
        /// <param name="originalImage">Oryginalny obraz do konwersji.</param>
        /// <param name="degree">Stopień negatywu dla konwersji.</param>
        /// <returns>Obraz w negatywie.</returns>
        unsafe
        public Bitmap ConvertToNegative(Bitmap originalImage, int degree, int maxDegreeOfParallelism)
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
                ParallelOptions parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism
                };

                // zablokuj bitmape w pamieci systemu
                BitmapData bitmapData =
                    originalImage.LockBits(new Rectangle(0, 0, originalImage.Width,
                    originalImage.Height), ImageLockMode.ReadWrite, originalImage.PixelFormat);

                // definicja zmiennych dla bajtow na pixel, wysokosci w pixelach i szerokosci w bajtach
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(originalImage.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                // definicja wskaznika na pierwszy pixel zablokowanej bitmapu
                // Scan0 odpowiedzialny jest za pierwszy pixel
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                // petla przechodzaca przez kazdy pixel uzywajac wskaznikow
                // Parallel.For - petla rownolegla
                Parallel.For(0, heightInPixels, parallelOptions, y =>
                {
                    // Stride to szerokosc w bajtach jednego wiersza bitmapy
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
    }
}
