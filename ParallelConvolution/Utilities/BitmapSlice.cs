using System.Drawing;

namespace ParallelConvolution {
    public class BitmapSlice {
        private Bitmap image;
        private int offset = -1;

        public BitmapSlice(Bitmap image, int offset) {
            this.image = image;
            this.offset = offset;
        }

        public Bitmap Image {
            get { return image; }
            set { image = value; }
        }
        public int Offset {
            get { return offset; }
        }
    }
}
