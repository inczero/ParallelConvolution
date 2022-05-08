using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelConvolution {
    public static class Modes {

        public static Bitmap RunSequential(Bitmap bitmap, int kernelSize, int sigma) {
            Kernel kernel = new Kernel();
            kernel.GenerateGaussianFilter(kernelSize, sigma);

            return Utilities.Filter(bitmap, kernel);
        }

        public static Bitmap RunParallelEqual(Bitmap bitmap, int kernelSize, int sigma, int pieceNumber) {
            Kernel kernel = new Kernel();
            kernel.GenerateGaussianFilter(kernelSize, sigma);

            int overlap = Convert.ToInt32(Math.Floor(Convert.ToDouble(kernel.GetSize()) / 2));

            List<BitmapSlice> pieces = Slicer.SliceFramedWithOverlapIntoList(bitmap, pieceNumber, overlap);

            List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();

            foreach (BitmapSlice item in pieces) {
                taskList.Add(new Task<Bitmap>(() => Utilities.Filter(item.Image, kernel)));
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

            return Slicer.PutTogetherFromList(resultList, bitmap.VerticalResolution, bitmap.HorizontalResolution); ;
        }

        public static Bitmap RunParallelBag(Bitmap bitmap, int kernelSize, int sigma, int pieceNumber, int taskNumber) {
            Kernel kernel = new Kernel();
            kernel.GenerateGaussianFilter(kernelSize, sigma);

            int overlap = Convert.ToInt32(Math.Floor(Convert.ToDouble(kernel.GetSize()) / 2));

            ConcurrentBag<BitmapSlice> slices = Slicer.SliceFramedWithOverlap(bitmap,pieceNumber, overlap);
            ConcurrentBag<BitmapSlice> filtered = new ConcurrentBag<BitmapSlice>();

            List<Task> taskList = new List<Task>();

            for (int i = 0; i < taskNumber; i++) {
                ConcurrentSliceProcessor processor = new ConcurrentSliceProcessor(kernel);

                taskList.Add(new Task(() => processor.Work(slices, filtered), TaskCreationOptions.LongRunning));
            }

            taskList.ForEach(task => task.Start());

            Task.WaitAll(taskList.ToArray());

            return Slicer.PutTogetherFromBag(filtered, bitmap.VerticalResolution, bitmap.HorizontalResolution);
        }
    }
}
