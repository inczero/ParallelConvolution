using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SliceConvoluteGauss {
    public class Convolution {
        private Bitmap _image;

        public Convolution(Bitmap image) {
            _image = image;
        }


        public Bitmap Image {
            get { return _image; }
            set { _image = value; }
        }

        public Bitmap Filter(Kernel kernel) {
            int boundary = kernel.Size;
            boundary = Convert.ToInt32(Math.Floor(Convert.ToDouble(boundary) / 2));

            Bitmap filteredImage = new Bitmap(_image.Width, _image.Height);

            for (int i = boundary; i < _image.Width-boundary; i++) {
                for (int j = boundary; j < _image.Height-boundary; j++) {
                    Rectangle r = new Rectangle(i-boundary, j-boundary, kernel.Size, kernel.Size);
                    Bitmap subImage = _image.Clone(r, _image.PixelFormat);
                    
                    Color newPixel = convolute(subImage, kernel);

                    filteredImage.SetPixel(i, j, newPixel);

                    subImage.Dispose();
                }
            }

            return filteredImage;
        }

        private Color convolute(Bitmap image, Kernel kernel) {
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

        private int calculatePixelChannel(double channelValue) {
            if (channelValue < 0) {
                channelValue = 0;
            }
            else if (channelValue > 255) {
                channelValue = 255;
            }

            return Convert.ToInt32(channelValue);
        }

    }

    public class Kernel {
        private double[,] _weights;
        private int _size;

        public Kernel() {}

        public Kernel(double[,] kernel) {
            // check if kernel is symmetric -> height=width
            _weights = kernel;
        }

        public double[,] Weights {
            get { return _weights; }
            set { _weights = value; }
        }

        public int Size {
            get { return _size; }
        }

        public double GetWeightSum() {
            double sum = 0;

            for (int i = 0; i < _size; i++) {
                for (int j = 0; j < _size; j++) {
                    sum += _weights[i,j];
                }
            }

            return sum;
        }

        public void Generate3x3GaussianFilter(double sigma) {
            double[,] mask = new double[3, 3];

            //int helperValue = Convert.ToInt32(Math.Floor(Convert.ToDouble(size) / 2));

            //int x = -helperValue;
            //int y = helperValue;

            mask[0, 0] = CalculateGaussValue(-1, 1, sigma);
            mask[0, 1] = CalculateGaussValue(1, 0, sigma);
            mask[0, 2] = CalculateGaussValue(1, 1, sigma);

            mask[1, 0] = CalculateGaussValue(0, -1, sigma);
            mask[1, 1] = CalculateGaussValue(0, 0, sigma);
            mask[1, 2] = CalculateGaussValue(0, 1, sigma);

            mask[2, 0] = CalculateGaussValue(-1, -1, sigma);
            mask[2, 1] = CalculateGaussValue(0, -1, sigma);
            mask[2, 2] = CalculateGaussValue(1, -1, sigma);

            _weights = mask;
        }

        public void GenerateGaussianFilter(int size, double sigma) {
            // find better solution to check size
            if (size % 2 == 0) {
                return;
            }

            _size = size;

            double[,] mask = new double[size, size];

            int boundary = Convert.ToInt32(Math.Floor(Convert.ToDouble(size) / 2));

            for (int i = -boundary; i <= boundary; i++) {
                for (int j = -boundary; j <= boundary; j++) {
                    double weight = CalculateGaussValue(i, j, sigma);

                    //Console.Write("{0}  ", weight);

                    mask[i + boundary, j + boundary] = weight;
                }

                //Console.WriteLine();
            }

            //normalize values!!


            _weights = mask;
        }

        private double CalculateGaussValue(int x, int y, double sigma) {
            double firstPart = 1 / (2 * Math.PI * Math.Pow(sigma, 2));
            double exponent = - ((Math.Pow(x, 2) + Math.Pow(y, 2)) / Math.Pow(sigma, 2));

            return firstPart * Math.Exp(exponent);
        }

        public void PrintWeights() {
            // find better solution to check size
            if (_weights == null) {
                return;
            }

            for (int i = 0; i < _weights.GetLength(0); i++) {
                for (int j = 0; j < _weights.GetLength(1); j++) {
                    Console.Write("{0}  ", _weights[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
