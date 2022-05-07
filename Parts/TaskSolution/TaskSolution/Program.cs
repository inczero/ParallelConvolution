﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSolution {
    internal class Program {
        static void Main(string[] args) {
            Bitmap bitmap = new Bitmap("C:\\Users\\incze\\Desktop\\workdir\\mercedes-medium-small.jpg");

            int borderSize = 10;

            Kernel kernel = new Kernel();
            kernel.GenerateGaussianFilter(borderSize * 2 + 1, 0.75);

            Bitmap framedBitmap = Utilities.DrawBorder(bitmap, borderSize, Color.Gray);

            Bitmap filteredImage = TaskWithBag(framedBitmap, 16, borderSize, 4, kernel);

            filteredImage.Save("C:\\Users\\incze\\Desktop\\workdir\\sliced_bagged_filtered_full.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //int borderSize = 30;
            //Bitmap imageWithBorder = Utilities.DrawBorder(bitmap, borderSize, Color.Gray);

            //ConcurrentBag<BitmapSlice> pieces = Slicer.RipApartFramedWithOverlap(imageWithBorder, 4, borderSize);

            //int i = 0;
            //foreach (Bitmap piece in pieces) {
            //    i++;
            //    piece.Save("C:\\Users\\incze\\Desktop\\workdir\\sliced_" + i.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //}

            //Console.WriteLine("dun");

            //Kernel kernel = new Kernel();

            //kernel.GenerateGaussianFilter(21, 0.85);

            ////kernel.PrintWeights();

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Bitmap filtered = Utilities.Filter(bitmap, kernel);
            //sw.Stop();

            //Console.WriteLine("serial time:");
            //Console.WriteLine(sw.Elapsed);
            //Console.WriteLine();

            //filtered.Save("C:\\Users\\incze\\Desktop\\workdir\\filtered.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //Stopwatch sw1 = new Stopwatch();
            //sw1.Start();

            //List<Bitmap> pieces = Slicer.RipApart(bitmap, 4);

            //List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();

            //foreach (Bitmap item in pieces) {
            //    //Kernel kernel = new Kernel();

            //    //kernel.GenerateGaussianFilter(3, 1.2);

            //    taskList.Add(new Task<Bitmap>(() => Utilities.Filter(item, kernel)));
            //}

            //taskList.ForEach(task => task.Start());

            //Task.WaitAll(taskList.ToArray());

            //List<Bitmap> resultList = new List<Bitmap>();

            //int index = 0;
            //foreach (Task<Bitmap> task in taskList) {
            //    resultList.Add(task.Result);

            //    index++;
            //    task.Result.Save("C:\\Users\\incze\\Desktop\\workdir\\parallel_filtered_" + index.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //}

            //Bitmap result = Slicer.PutTogether(resultList, bitmap.VerticalResolution, bitmap.HorizontalResolution);

            //sw1.Stop();
            //Console.WriteLine("parallel time:");
            //Console.WriteLine(sw1.Elapsed);
            //Console.WriteLine();

            //result.Save("C:\\Users\\incze\\Desktop\\workdir\\parallel_merged_new.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //  -> cut to pieces with a boundary + piece length -> 
            // create filtered image -> the size will be framed image width/height - boundary -> assemble filtered image parts

        }

        static Bitmap TaskWithBag(Bitmap bitmap, int sliceNumber, int overlap, int taskNumber, Kernel kernel) {
            // create 2 bags -> toBeProcessed and done
            ConcurrentBag<BitmapSlice> slices = Slicer.RipApartFramedWithOverlap(bitmap, sliceNumber, overlap);
            ConcurrentBag<BitmapSlice> filtered = new ConcurrentBag<BitmapSlice>();

            // create x number of tasks to process slices and start them
            //var imageProcessors = Enumerable.Range(0, taskNumber - 1).Select(i => new ImageProcessor(kernel)).ToList();
            //imageProcessors.Select(imageProcessor => new Task(() => imageProcessor.Work(ref slices, ref filtered), TaskCreationOptions.LongRunning)).ToList().ForEach(t => t.Start());

            List<Task> taskList = new List<Task>();

            for (int i = 0; i < taskNumber; i++) {
                ImageProcessor imageProcessor = new ImageProcessor(kernel);

                taskList.Add(new Task(() => imageProcessor.Work(slices, filtered), TaskCreationOptions.LongRunning));
            }

            taskList.ForEach(task => task.Start());

            // wait for them to finish (slices empty and tasks done)
            Console.WriteLine("Waiting for the tasks...");

            Task.WaitAll(taskList.ToArray());

            // assemble
            Console.WriteLine("dun");

            // return image
            //int j = 0;
            //foreach (BitmapSlice piece in filtered) {
            //    j++;
            //    piece.image.Save("C:\\Users\\incze\\Desktop\\workdir\\sliced_bag_" + j.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //}
            return Slicer.PutTogetherFromBag(filtered, bitmap.VerticalResolution, bitmap.HorizontalResolution);
        }
    }
}
