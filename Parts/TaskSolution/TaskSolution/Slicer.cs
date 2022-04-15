using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TaskSolution {
    public static class Slicer {
        public static List<Bitmap> RipApart(Bitmap image, int pieces) {
            List<Bitmap> result = new List<Bitmap>();

            int pieceSize = image.Width / pieces;

            int pieceIndex = 0;

            for (int i = 1; i <= pieces; i++) {
                if (i == pieces) {
                    pieceSize = image.Width - (i-1) * pieceSize;
                }

                Rectangle rect = new Rectangle(pieceIndex, 0, pieceSize, image.Height);

                Bitmap slice = image.Clone(rect, image.PixelFormat);

                result.Add(slice);

                pieceIndex += pieceSize;
            }

            return result;
        }
    }
}
