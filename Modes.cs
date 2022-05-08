using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelConvolution {
    public static class Modes {

        public static Bitmap RunSequential(Bitmap bitmap, int kernelSize, double sigma) {
            Kernel kernel = new Kernel();
            kernel.GenerateGaussianFilter(kernelSize, sigma);

            int overlap = Convert.ToInt32(Math.Floor(Convert.ToDouble(kernel.GetSize()) / 2));

            bitmap = FilterUtils.DrawBorder(bitmap, overlap, Color.Gray);

            return FilterUtils.Filter(bitmap, kernel);
        }

        public static Bitmap RunParallelEqual(Bitmap bitmap, int kernelSize, double sigma, int pieceNumber) {
            
            int overlap = Convert.ToInt32(Math.Floor(Convert.ToDouble(kernelSize) / 2));

            bitmap = FilterUtils.DrawBorder(bitmap, overlap, Color.Gray);

            List<Bitmap> pieces = Slicer.SliceFramedWithOverlapIntoList(bitmap, pieceNumber, overlap);

            List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();

            foreach (Bitmap slice in pieces) {
                Kernel kernel = new Kernel();
                kernel.GenerateGaussianFilter(kernelSize, sigma);

                taskList.Add(new Task<Bitmap>(() => FilterUtils.Filter(slice, kernel)));
            }

            taskList.ForEach(task => task.Start());

            Task.WaitAll(taskList.ToArray());

            List<Bitmap> resultList = new List<Bitmap>();

            foreach (Task<Bitmap> task in taskList) {
                resultList.Add(task.Result);
            }

            return Slicer.PutTogetherFromList(resultList, bitmap.VerticalResolution, bitmap.HorizontalResolution); ;
        }

        public static Bitmap RunParallelBag(Bitmap bitmap, int kernelSize, double sigma, int pieceNumber, int taskNumber) {

            int overlap = Convert.ToInt32(Math.Floor(Convert.ToDouble(kernelSize) / 2));

            bitmap = FilterUtils.DrawBorder(bitmap, overlap, Color.Gray);

            ConcurrentBag<BitmapSlice> slices = Slicer.SliceFramedWithOverlap(bitmap,pieceNumber, overlap);
            ConcurrentBag<BitmapSlice> filtered = new ConcurrentBag<BitmapSlice>();

            List<Task> taskList = new List<Task>();

            for (int i = 0; i < taskNumber; i++) {
                Kernel kernel = new Kernel();
                kernel.GenerateGaussianFilter(kernelSize, sigma);

                ConcurrentSliceProcessor processor = new ConcurrentSliceProcessor(kernel);

                taskList.Add(new Task(() => processor.Work(slices, filtered), TaskCreationOptions.LongRunning));
            }

            taskList.ForEach(task => task.Start());

            Task.WaitAll(taskList.ToArray());

            return Slicer.PutTogetherFromBag(filtered, bitmap.VerticalResolution, bitmap.HorizontalResolution);
        }
    }
}
