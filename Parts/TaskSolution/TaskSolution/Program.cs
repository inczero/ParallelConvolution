using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace TaskSolution {
    internal class Program {
        static void Main(string[] args) {
            Bitmap bitmap = new Bitmap("C:\\Users\\incze\\Desktop\\workdir\\mercedes-medium.jpg");

            Kernel kernel = new Kernel();

            kernel.GenerateGaussianFilter(21, 0.85);

            //kernel.PrintWeights();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Bitmap filtered = Utilities.Filter(bitmap, kernel);
            sw.Stop();

            Console.WriteLine("serial time:");
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine();

            //filtered.Save("C:\\Users\\incze\\Desktop\\workdir\\filtered.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();

            List<Bitmap> pieces = Slicer.RipApart(bitmap, 4);

            List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();

            foreach (Bitmap item in pieces) {
                //Kernel kernel = new Kernel();

                //kernel.GenerateGaussianFilter(3, 1.2);

                taskList.Add(new Task<Bitmap>(() => Utilities.Filter(item, kernel)));
            }

            taskList.ForEach(task => task.Start());

            Task.WaitAll(taskList.ToArray());

            List<Bitmap> resultList = new List<Bitmap>();

            int index = 0;
            foreach (Task<Bitmap> task in taskList) {
                resultList.Add(task.Result);

                index++;
                task.Result.Save("C:\\Users\\incze\\Desktop\\workdir\\parallel_filtered_" + index.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            Bitmap result = Slicer.PutTogether(resultList, bitmap.VerticalResolution, bitmap.HorizontalResolution);

            sw1.Stop();
            Console.WriteLine("parallel time:");
            Console.WriteLine(sw1.Elapsed);
            Console.WriteLine();

            //result.Save("C:\\Users\\incze\\Desktop\\workdir\\parallel_merged_new.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //  -> cut to pieces with a boundary + piece length -> 
            // create filtered image -> the size will be framed image width/height - boundary -> assemble filtered image parts

        }
    }
}
