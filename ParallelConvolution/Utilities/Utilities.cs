using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelConvolution {
    public static class Utilities {

        public static Bitmap DrawBorder(Bitmap image, int borderSize, Color borderColor) {
            int widthWithBorder = image.Width + 2 * borderSize;
            int heightWithBorder = image.Height + 2 * borderSize;

            Bitmap imageWithBorder = new Bitmap(widthWithBorder, heightWithBorder);

            Rectangle background = new Rectangle(0, 0, widthWithBorder, heightWithBorder);
            Rectangle foregroundImage = new Rectangle(borderSize, borderSize, image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(imageWithBorder)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                using (Brush brush = new SolidBrush(borderColor)) {
                    g.FillRectangle(brush, background);
                }
                g.DrawImage(image, foregroundImage);
            }

            return imageWithBorder;
        }

        public static Bitmap Filter(Bitmap image, Kernel kernel) {
            int boundary = Convert.ToInt32(Math.Floor(Convert.ToDouble(kernel.GetSize()) / 2));

            Bitmap filteredImage = new Bitmap(image.Width - 2 * boundary, image.Height - 2 * boundary);

            for (int i = boundary; i < image.Width - boundary; i++) {
                for (int j = boundary; j < image.Height - boundary; j++) {
                    Rectangle r = new Rectangle(i - boundary, j - boundary, kernel.GetSize(), kernel.GetSize());
                    Bitmap subImage = image.Clone(r, image.PixelFormat);

                    Color newPixel = convolute(subImage, kernel);

                    filteredImage.SetPixel(i - boundary, j - boundary, newPixel);

                    subImage.Dispose();
                }
            }

            return filteredImage;
        }

        

        private static Color convolute(Bitmap image, Kernel kernel) {
            double calcA = 0;
            double calcR = 0;
            double calcG = 0;
            double calcB = 0;

            for (int i = 0; i < image.Width; i++) {
                for (int j = 0; j < image.Height; j++) {
                    Color oldPixel = image.GetPixel(i, j);

                    calcA += oldPixel.A * kernel.Weights[i, j];
                    calcR += oldPixel.R * kernel.Weights[i, j];
                    calcG += oldPixel.G * kernel.Weights[i, j];
                    calcB += oldPixel.B * kernel.Weights[i, j];
                }
            }

            calcA = calcA / kernel.GetWeightSum();
            int chA = calculatePixelChannel(calcA);

            calcR = calcR / kernel.GetWeightSum();
            int chR = calculatePixelChannel(calcR);

            calcG = calcG / kernel.GetWeightSum();
            int chG = calculatePixelChannel(calcG);

            calcB = calcB / kernel.GetWeightSum();
            int chB = calculatePixelChannel(calcB);

            image.Dispose();

            return Color.FromArgb(chA, chR, chG, chB);
        }

        private static int calculatePixelChannel(double channelValue) {
            if (channelValue < 0) {
                channelValue = 0;
            }
            else if (channelValue > 255) {
                channelValue = 255;
            }

            return Convert.ToInt32(channelValue);
        }
    }
}
