using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelConvolution {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void buttonSelectImage_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg";
            ofd.Title = "Select an image...";

            Bitmap bmp;

            if (ofd.ShowDialog() == DialogResult.OK) {
                string filepath = ofd.FileName;
                try {
                    bmp = new Bitmap(filepath);
                    pictureBox.Image = bmp;

                    long pixels = bmp.Height * bmp.Width;

                    textBoxDimensions.Text = bmp.Width.ToString() + "x" + bmp.Height.ToString() + " (" + pixels.ToString() + " pixels)";
                }
                catch (Exception) {
                    throw;
                }
            }
        }

        private void buttonConvolute_Click(object sender, EventArgs e) {
            this.backgroundWorker.RunWorkerAsync();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.backgroundWorker.CancelAsync();
        }

        private void radioButtonSequential_CheckedChanged(object sender, EventArgs e) {
            textBoxPieceNumber.Enabled = false;
            textBoxTaskNumber.Enabled = false;
        }

        private void radioButtonParallelEqual_CheckedChanged(object sender, EventArgs e) {
            textBoxPieceNumber.Enabled = true;
            textBoxTaskNumber.Enabled = false;
        }

        private void radioButtonParallelBag_CheckedChanged(object sender, EventArgs e) {
            textBoxPieceNumber.Enabled = true;
            textBoxTaskNumber.Enabled = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // check if not null
            Bitmap bitmap = new Bitmap(pictureBox.Image);
            Bitmap filtered = new Bitmap(pictureBox.Image);

            // use TryParse
            int kernelSize = Int32.Parse(textBoxKernelSize.Text);
            int sigma = Int32.Parse(textBoxSigma.Text);

            // add input validation!!!
            if (radioButtonSequential.Checked) {

                filtered = Modes.RunSequential(bitmap, kernelSize, sigma);

            } else if (radioButtonParallelEqual.Checked) {

                int pieceNumber = Int32.Parse(textBoxPieceNumber.Text);

                filtered = Modes.RunParallelEqual(bitmap, kernelSize, sigma, pieceNumber);

            } else if (radioButtonParallelBag.Checked) {

                int pieceNumber = Int32.Parse(textBoxPieceNumber.Text);
                int taskNumber = Int32.Parse(textBoxTaskNumber.Text);

                filtered = Modes.RunParallelBag(bitmap, kernelSize, sigma, pieceNumber, taskNumber);

            }

            stopwatch.Stop();

            pictureBox.Image = filtered;
            textBoxExecutionTime.Text = stopwatch.Elapsed.ToString();
        }
    }
}
