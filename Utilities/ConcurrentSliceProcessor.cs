using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelConvolution {
    internal class ConcurrentSliceProcessor {
        private Kernel kernel;

        public ConcurrentSliceProcessor(Kernel kernel) { this.kernel = kernel; }

        public void Work(ConcurrentBag<BitmapSlice> slices, ConcurrentBag<BitmapSlice> filtered) {
            // will run until every slice from slices is processed
            while (!slices.IsEmpty) {
                BitmapSlice slice;

                if (slices.TryTake(out slice)) {
                    Bitmap filteredSlice = FilterUtils.Filter(slice.Image, kernel);

                    slice.Image = filteredSlice;

                    filtered.Add(slice);
                }
            }
        }
    }
}
