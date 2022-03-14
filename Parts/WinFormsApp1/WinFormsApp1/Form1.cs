using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg,*.png";
            ofd.Title = "Select an image to mess up...";

            Bitmap bmp;

            if (ofd.ShowDialog() == DialogResult.OK) {
                string filepath = ofd.FileName;
                try {
                    bmp = new Bitmap(filepath);
                    pictureBox1.Image = bmp;
                }
                catch (Exception) {
                    throw;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            for (int i = 0; i < bmp.Width; i++) {
                for (int j = 0; j < bmp.Height; j++) {
                    Color oldPixelColor = bmp.GetPixel(i, j);
                    Color newPixelColor = Color.FromArgb(oldPixelColor.B, oldPixelColor.R, oldPixelColor.G);
                    bmp.SetPixel(i, j, newPixelColor);
                }
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
            }
        }
    }
}
