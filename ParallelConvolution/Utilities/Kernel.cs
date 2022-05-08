using System;

namespace TaskSolution {
    public class Kernel {
        private double[,] _weights;

        public Kernel() { }

        public Kernel(double[,] kernel) {
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

        private void normalizeWeights() {
            double weightSum = this.GetWeightSum();

            double correctionValue = (1 - weightSum) / (_weights.GetLength(0) * _weights.GetLength(1));

            for (int i = 0; i < _weights.GetLength(0); i++) {
                for (int j = 0; j < _weights.GetLength(1); j++) {
                    _weights[i, j] += correctionValue;
                }
            }
        }

        public byte GenerateGaussianFilter(int size, double sigma) {
            if (size % 2 == 0) {
                return 1;
            }

            double[,] mask = new double[size, size];

            int boundary = Convert.ToInt32(Math.Floor(Convert.ToDouble(size) / 2));

            for (int i = -boundary; i <= boundary; i++) {
                for (int j = -boundary; j <= boundary; j++) {
                    double weight = CalculateGaussValue(i, j, sigma);

                    mask[i + boundary, j + boundary] = weight;
                }
            }

            _weights = mask;

            normalizeWeights();

            return 0;
        }

        private double CalculateGaussValue(int x, int y, double sigma) {
            double firstPart = 1 / (2 * Math.PI * Math.Pow(sigma, 2));
            double exponent = -((Math.Pow(x, 2) + Math.Pow(y, 2)) / Math.Pow(sigma, 2));

            return firstPart * Math.Exp(exponent);
        }

        public void PrintWeights() {
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
