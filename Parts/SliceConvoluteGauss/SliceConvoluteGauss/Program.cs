using System;
using System.Drawing;

namespace SliceConvoluteGauss {
    internal class Program {
        static void Main(string[] args) {
            //Bitmap bitmap = new Bitmap("E:\\Wallpapers\\lighthouse.jpg");

            Console.WriteLine("Mask generated with 3x3 function:");
            Kernel kernel1 = new Kernel();
            kernel1.Generate3x3GaussianFilter(2);
            kernel1.PrintWeights();

            Console.WriteLine();

            Console.WriteLine("Mask generated with general function:");
            Kernel kernel2 = new Kernel();
            kernel2.GenerateGaussianFilter(5, 1.5);
            kernel2.PrintWeights();
        }
    }
}
