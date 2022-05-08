using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;

namespace ParallelConvolution {
    public static class Slicer {

        public static ConcurrentBag<BitmapSlice> SliceFramedWithOverlap(Bitmap image, int pieces, int overlap) {
            ConcurrentBag<BitmapSlice> result = new ConcurrentBag<BitmapSlice>();

            int innerPieceSize = (image.Width - overlap * 2) / pieces;
            int cloneX = overlap;
            int cloneWidth = innerPieceSize + 2 * overlap;

            for (int i = 1; i <= pieces; i++) {
                if (i == pieces) {
                    cloneWidth = image.Width - ((i-1) * innerPieceSize);
                }

                Rectangle rect = new Rectangle(cloneX - overlap, 0, cloneWidth, image.Height);

                Bitmap slice = image.Clone(rect, image.PixelFormat);

                BitmapSlice sliceWithOffset = new BitmapSlice(slice, cloneX - overlap);

                result.Add(sliceWithOffset);

                cloneX += innerPieceSize;
            }

            return result;
        }

        public static List<Bitmap> SliceFramedWithOverlapIntoList(Bitmap image, int pieces, int overlap) {
            List<Bitmap> result = new List<Bitmap>();

            int innerPieceSize = (image.Width - overlap * 2) / pieces;
            int cloneX = overlap;
            int cloneWidth = innerPieceSize + 2 * overlap;

            for (int i = 1; i <= pieces; i++) {
                if (i == pieces) {
                    cloneWidth = image.Width - ((i - 1) * innerPieceSize);
                }

                Rectangle rect = new Rectangle(cloneX - overlap, 0, cloneWidth, image.Height);

                Bitmap slice = image.Clone(rect, image.PixelFormat);

                result.Add(slice);

                cloneX += innerPieceSize;
            }

            return result;
        }

        private static int calculateWidthList(List<Bitmap> imagePieces) {
            int width = 0;

            imagePieces.ForEach(imagePiece => width += imagePiece.Width);

            return width;
        }

        public static Bitmap PutTogetherFromList(List<Bitmap> imagePieces, float verticalResolution, float horizontalResolution) {
            int width = calculateWidthList(imagePieces);
            int height = imagePieces[0].Height;
            int stepSize = imagePieces[0].Width;

            Bitmap result = new Bitmap(width, height);
            result.SetResolution(horizontalResolution, verticalResolution);

            using (Graphics g = Graphics.FromImage(result)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                for (int i = 0; i < imagePieces.Count; i++) {
                    Rectangle r = new Rectangle(i * stepSize, 0, imagePieces[i].Width, imagePieces[i].Height);
                    g.DrawImage(imagePieces[i], r);
                }
            }

            return result;
        }

        private static int calculateWidthBag(ConcurrentBag<BitmapSlice> imagePieces) {
            int width = 0;

            foreach (BitmapSlice piece in imagePieces) {
                width += piece.Image.Width;
            }

            return width;
        }

        public static Bitmap PutTogetherFromBag(ConcurrentBag<BitmapSlice> slices, float verticalResolution, float horizontalResolution) {
            int width = calculateWidthBag(slices);
            int height = -1;

            BitmapSlice randomSlice;
            if (slices.TryPeek(out randomSlice)) {
                height = randomSlice.Image.Height;
            } else {
                return null;
            }

            Bitmap result = new Bitmap(width, height);
            result.SetResolution(horizontalResolution, verticalResolution);

            using (Graphics g = Graphics.FromImage(result)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                foreach (BitmapSlice slice in slices) {
                    Rectangle r = new Rectangle(slice.Offset, 0, slice.Image.Width, slice.Image.Height);
                    g.DrawImage(slice.Image, r);
                }
            }

            return result;
        }
    }
}
