using System;
using System.Diagnostics;
using System.Drawing;

namespace SliceConvoluteGauss {
    internal class Program {
        static void Main(string[] args) {
            Bitmap bitmap = new Bitmap("E:\\Wallpapers\\train.jpg");

            //Console.WriteLine("Mask generated with 3x3 function:");
            //Kernel kernel1 = new Kernel();
            //kernel1.Generate3x3GaussianFilter(2);
            //kernel1.PrintWeights();

            //Console.WriteLine();

            //Console.WriteLine("Mask generated with general function:");
            //Kernel kernel2 = new Kernel();
            //kernel2.GenerateGaussianFilter(5, 1.5);
            //kernel2.PrintWeights();

            Kernel kernel = new Kernel();
            kernel.GenerateGaussianFilter(7, 1.5);
            kernel.PrintWeights();
            var sum = kernel.GetWeightSum();
            Console.WriteLine(sum.ToString());

            Convolution convolution = new Convolution(bitmap);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Bitmap filteredImage = convolution.Filter(kernel);
            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            filteredImage.Save("E:\\Wallpapers\\filtered_train.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
