using System.Drawing;

namespace TaskSolution {
    public class BitmapSlice {
        public Bitmap image;
        public int offset = -1;

        public BitmapSlice(Bitmap image, int offset) {
            this.image = image;
            this.offset = offset;
        }
    }
}
