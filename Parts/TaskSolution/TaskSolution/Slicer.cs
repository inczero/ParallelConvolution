using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;

namespace TaskSolution {
    public static class Slicer {
        public static List<Bitmap> RipApart(Bitmap image, int pieces) {
            List<Bitmap> result = new List<Bitmap>();

            int pieceSize = image.Width / pieces;

            int pieceIndex = 0;

            for (int i = 1; i <= pieces; i++) {
                if (i == pieces) {
                    pieceSize = image.Width - (i - 1) * pieceSize;
                }

                Rectangle rect = new Rectangle(pieceIndex, 0, pieceSize, image.Height);

                Bitmap slice = image.Clone(rect, image.PixelFormat);

                result.Add(slice);

                pieceIndex += pieceSize;
            }

            return result;
        }

        public static ConcurrentBag<BitmapSlice> RipApartFramedWithOverlap(Bitmap image, int pieces, int overlap) {
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

        private static int calculateWidth(List<Bitmap> imagePieces) {
            int width = 0;

            imagePieces.ForEach(imagePiece => width += imagePiece.Width);

            return width;
        }

        public static Bitmap PutTogether(List<Bitmap> imagePieces, float verticalResolution, float horizontalResolution) {
            int width = calculateWidth(imagePieces);
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
    }
}
