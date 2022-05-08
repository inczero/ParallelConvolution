using System;

namespace ParallelConvolution {
    public class Kernel {
        private double[,] weights;

        public Kernel() { }

        public Kernel(double[,] kernel) {
            weights = kernel;
        }

        public double[,] Weights {
            get { return weights; }
            set { weights = value; }
        }

        public int GetSize() {
            return weights.GetLength(0);
        }

        public double GetWeightSum() {
            double sum = 0;

            for (int i = 0; i < weights.GetLength(0); i++) {
                for (int j = 0; j < weights.GetLength(1); j++) {
                    sum += weights[i, j];
                }
            }

            return sum;
        }

        private void normalizeWeights() {
            double weightSum = this.GetWeightSum();

            double correctionValue = (1 - weightSum) / (weights.GetLength(0) * weights.GetLength(1));

            for (int i = 0; i < weights.GetLength(0); i++) {
                for (int j = 0; j < weights.GetLength(1); j++) {
                    weights[i, j] += correctionValue;
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

            weights = mask;

            normalizeWeights();

            return 0;
        }

        private double CalculateGaussValue(int x, int y, double sigma) {
            double firstPart = 1 / (2 * Math.PI * Math.Pow(sigma, 2));
            double exponent = -((Math.Pow(x, 2) + Math.Pow(y, 2)) / Math.Pow(sigma, 2));

            return firstPart * Math.Exp(exponent);
        }

        public void PrintWeights() {
            if (weights == null) {
                return;
            }

            for (int i = 0; i < weights.GetLength(0); i++) {
                for (int j = 0; j < weights.GetLength(1); j++) {
                    Console.Write("{0}  ", weights[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
