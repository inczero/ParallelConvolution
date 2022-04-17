using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace TaskSolution {
    internal class Program {
        static void Main(string[] args) {
            Bitmap bitmap = new Bitmap("C:\\Users\\incze\\Desktop\\workdir\\mercedes-small.jpg");

            Kernel kernel = new Kernel();

            kernel.GenerateGaussianFilter(11, 7);

            //kernel.PrintWeights();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Bitmap filtered = Utilities.Filter(bitmap, kernel);
            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            //filtered.Save("C:\\Users\\incze\\Desktop\\workdir\\filtered.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);



            //List<Bitmap> pieces = Slicer.RipApart(bitmap, 4);

            //List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();

            //foreach (Bitmap item in pieces) {
            //    //Kernel kernel = new Kernel();

            //    //kernel.GenerateGaussianFilter(3, 1.2);

            //    taskList.Add(new Task<Bitmap>(() => Utilities.Filter(item, kernel)));
            //}

            //Stopwatch sw1 = new Stopwatch();
            //sw1.Start();

            //taskList.ForEach(task => task.Start());

            //Task.WaitAll(taskList.ToArray());

            //sw1.Stop();
            //Console.WriteLine(sw1.Elapsed);





            // Draw border around image:
            //int borderSize = 120;
            //int widthWithBorder = bitmap.Width + 2 * borderSize;
            //int heightWithBorder = bitmap.Height + 2 * borderSize;
            //Bitmap bmpWithBorder = new Bitmap(widthWithBorder, heightWithBorder);
            //Color borderColor = Color.FromArgb(125, 125, 125);
            //Rectangle background = new Rectangle(0, 0, widthWithBorder, heightWithBorder);
            //Rectangle foregroundImage = new Rectangle(borderSize, borderSize, bitmap.Width, bitmap.Height);
            //using (Graphics g = Graphics.FromImage(bmpWithBorder)) {
            //    using (Brush brush = new SolidBrush(borderColor)) {
            //        g.FillRectangle(brush, background);
            //    }
            //    g.DrawImage(bitmap, foregroundImage);
            //}
            //bmpWithBorder.Save("C:\\Users\\incze\\Desktop\\workdir\\framed.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            // Image sticking:
            //Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            //result.SetResolution(90, 90);
            //using (Graphics g = Graphics.FromImage(result)) {
            //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            //    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //    g.DrawImage(pieces[0], 0, 0, pieces[0].Width, pieces[0].Height);
            //    g.DrawImage(pieces[1], 2335, 0, pieces[1].Width, pieces[1].Height);
            //    // use rectangle instead of individual values
            //}
            //result.Save("C:\\Users\\incze\\Desktop\\workdir\\merged.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //int fileIndex = 1;
            //foreach (Bitmap piece in pieces) {
            //    string filepath = "C:\\Users\\incze\\Desktop\\workdir\\sli ced_" + fileIndex.ToString() + ".jpg";
            //    piece.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    fileIndex++;
            //}

            //Kernel kernel = new Kernel();

            //kernel.GenerateGaussianFilter(23, 1.5);
            //kernel.PrintWeights();

            //Convolution convolution = new Convolution(pieces[1]);

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Bitmap filteredImage = convolution.Filter(kernel);
            //sw.Stop();

            //Console.WriteLine(sw.Elapsed);

            //filteredImage.Save("C:\\Users\\incze\\Desktop\\workdir\\filtered.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //  -> cut to pieces with a boundary + piece length -> 
            // create filtered image -> the size will be framed image width/height - boundary -> assemble filtered image parts

        }
    }
}
