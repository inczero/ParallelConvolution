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

            Bitmap bitmap = new Bitmap(pictureBox.Image);
            Bitmap filtered;

            int kernelSize = Int32.Parse(textBoxKernelSize.Text);
            double sigma = Double.Parse(textBoxSigma.Text);

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

        private bool areInputsValid() {
            if (pictureBox.Image == null) {
                MessageBox.Show("Please select an image before starting!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int kernelSize;
            if (Int32.TryParse(textBoxKernelSize.Text, out kernelSize)) {
                if (kernelSize % 2 == 0) {
                    MessageBox.Show("Kernel size must be an odd number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else {
                if (textBoxKernelSize.Text.Length > 0) {
                    MessageBox.Show("Kernel size must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else {
                    MessageBox.Show("Kernel size not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            double sigma;
            if (!Double.TryParse(textBoxSigma.Text, out sigma)) {
                if (textBoxSigma.Text.Length > 0) {
                    MessageBox.Show("Sigma must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else {
                    MessageBox.Show("Sigma not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (radioButtonParallelEqual.Checked || radioButtonParallelBag.Checked) {
                int pieceNumber;
                if (!Int32.TryParse(textBoxPieceNumber.Text, out pieceNumber)) {
                    if (textBoxPieceNumber.Text.Length > 0) {
                        MessageBox.Show("Piece number must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else {
                        MessageBox.Show("Piece number not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (pictureBox.Image.Width / pieceNumber < 1) {
                    string message = "Piece number is too big! The maximum value is " + pictureBox.Image.Width + ".";
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (radioButtonParallelBag.Checked) {
                    int taskNumber;
                    if (!Int32.TryParse(textBoxTaskNumber.Text, out taskNumber)) {
                        if (textBoxTaskNumber.Text.Length > 0) {
                            MessageBox.Show("Task number must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else {
                            MessageBox.Show("Task number not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void buttonConvolute_Click(object sender, EventArgs e) {
            if (!areInputsValid()) {
                return;
            }

            timer1.Start();
            backgroundWorker1.RunWorkerAsync();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            textBoxExecutionTime.Text = stopwatch.Elapsed.ToString();
            pictureBox.Refresh(); 
        }

        private void handleInputKey(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void textBoxKernelSize_KeyPress(object sender, KeyPressEventArgs e) {
            handleInputKey(sender, e);
        }

        private void textBoxSigma_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') != -1)) {
                e.Handled = true;
            }
        }

        private void textBoxPieceNumber_KeyPress(object sender, KeyPressEventArgs e) {
            handleInputKey(sender, e);
        }

        private void textBoxTaskNumber_KeyPress(object sender, KeyPressEventArgs e) {
            handleInputKey(sender, e);
        }
    }
}
