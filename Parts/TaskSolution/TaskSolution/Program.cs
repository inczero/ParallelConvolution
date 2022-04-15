using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace TaskSolution {
    internal class Program {
        static void Main(string[] args) {
            Bitmap bitmap = new Bitmap("C:\\Users\\incze\\Desktop\\workdir\\train.jpg");

            List<Bitmap> pieces = Slicer.RipApart(bitmap, 2);

            //int fileIndex = 1;
            //foreach (Bitmap piece in pieces) {
            //    string filepath = "C:\\Users\\incze\\Desktop\\workdir\\sliced_" + fileIndex.ToString() + ".jpg";
            //    piece.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);

            //    fileIndex++;
            //}

            Kernel kernel = new Kernel();

            kernel.GenerateGaussianFilter(23, 1.5);
            kernel.PrintWeights();

            Convolution convolution = new Convolution(pieces[1]);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Bitmap filteredImage = convolution.Filter(kernel);
            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            filteredImage.Save("C:\\Users\\incze\\Desktop\\workdir\\filtered.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            // draw boundary wide frame (125,125,125) to the picture -> cut to pieces with a boundary + piece length -> 
            // create filtered image -> the size will be framed image width/height - boundary -> assemble filtered image parts
            
        }
    }
}
