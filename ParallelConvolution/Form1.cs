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

        private readonly Stopwatch stopwatch = new Stopwatch();

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

        private void buttonSave_Click(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image Files|*.jpg;*.jpeg";
            sfd.Title = "Save image here...";

            if (sfd.ShowDialog() == DialogResult.OK) {
                string filepath = sfd.FileName;
                try {
                    Bitmap bmp = new Bitmap(pictureBox.Image);

                    bmp.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception) {
                    throw;
                }
            }
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            stopwatch.Restart();

            // check if not null
            Bitmap bitmap = new Bitmap(pictureBox.Image);
            Bitmap filtered;

            // use TryParse
            int kernelSize = Int32.Parse(textBoxKernelSize.Text);
            double sigma = Double.Parse(textBoxSigma.Text);

            // add input validation!!!
            if (radioButtonSequential.Checked) {

                filtered = Modes.RunSequential(bitmap, kernelSize, sigma);

                stopwatch.Stop();

                pictureBox.Image = filtered;
            }
            else if (radioButtonParallelEqual.Checked) {

                int pieceNumber = Int32.Parse(textBoxPieceNumber.Text);

                filtered = Modes.RunParallelEqual(bitmap, kernelSize, sigma, pieceNumber);

                stopwatch.Stop();

                pictureBox.Image = filtered;

            }
            else if (radioButtonParallelBag.Checked) {

                int pieceNumber = Int32.Parse(textBoxPieceNumber.Text);
                int taskNumber = Int32.Parse(textBoxTaskNumber.Text);

                filtered = Modes.RunParallelBag(bitmap, kernelSize, sigma, pieceNumber, taskNumber);

                stopwatch.Stop();

                pictureBox.Image = filtered;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            timer1.Stop();
        }

        private void buttonConvolute_Click(object sender, EventArgs e) {
            timer1.Start();

            backgroundWorker1.RunWorkerAsync();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            textBoxExecutionTime.Text = stopwatch.Elapsed.ToString();
            pictureBox.Refresh();
        }
    }
}
