using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSolution {
    internal class ImageProcessor {
        private Kernel kernel;

        public ImageProcessor(Kernel kernel) { this.kernel = kernel; }

        public void Work(ConcurrentBag<BitmapSlice> slices, ConcurrentBag<BitmapSlice> filtered) {
            while (!slices.IsEmpty) {
                BitmapSlice slice;

                if (slices.TryTake(out slice)) {
                    Bitmap filteredSlice = Utilities.FilterExtendedImage(slice.image, kernel);

                    slice.image = filteredSlice;

                    filtered.Add(slice);
                }
            }
        }
    }
}
