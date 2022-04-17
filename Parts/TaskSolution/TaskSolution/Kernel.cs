using System;

namespace TaskSolution {
    public class Kernel {
        private double[,] _weights;

        public Kernel() { }

        public Kernel(double[,] kernel) {
            // check if kernel is symmetric -> height=width
            _weights = kernel;
        }

        public double[,] Weights {
            get { return _weights; }
            set { _weights = value; }
        }

        public int GetSize() {
            return _weights.GetLength(0);
        }

        public double GetWeightSum() {
            double sum = 0;

            for (int i = 0; i < _weights.GetLength(0); i++) {
                for (int j = 0; j < _weights.GetLength(1); j++) {
                    sum += _weights[i, j];
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

        private void normalizeWeights() {
            double weightSum = this.GetWeightSum();

            double correctionValue = (1 - weightSum) / (_weights.GetLength(0) * _weights.GetLength(1));

            for (int i = 0; i < _weights.GetLength(0); i++) {
                for (int j = 0; j < _weights.GetLength(1); j++) {
                    _weights[i, j] += correctionValue;
                }
            }
        }

        public void GenerateGaussianFilter(int size, double sigma) {
            // find better solution to check size
            if (size % 2 == 0) {
                return;
            }

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

            _weights = mask;

            normalizeWeights();
        }

        private double CalculateGaussValue(int x, int y, double sigma) {
            double firstPart = 1 / (2 * Math.PI * Math.Pow(sigma, 2));
            double exponent = -((Math.Pow(x, 2) + Math.Pow(y, 2)) / Math.Pow(sigma, 2));

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
