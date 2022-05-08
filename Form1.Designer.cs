namespace ParallelConvolution {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonSequential = new System.Windows.Forms.RadioButton();
            this.radioButtonParallelEqual = new System.Windows.Forms.RadioButton();
            this.radioButtonParallelBag = new System.Windows.Forms.RadioButton();
            this.buttonConvolute = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSelectImage = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxKernelSize = new System.Windows.Forms.TextBox();
            this.textBoxSigma = new System.Windows.Forms.TextBox();
            this.textBoxPieceNumber = new System.Windows.Forms.TextBox();
            this.textBoxTaskNumber = new System.Windows.Forms.TextBox();
            this.textBoxDimensions = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBoxExecutionTime = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox.Location = new System.Drawing.Point(15, 15);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(600, 600);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(640, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kernel size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(640, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sigma";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(640, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Piece number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(640, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Task number";
            // 
            // radioButtonSequential
            // 
            this.radioButtonSequential.AutoSize = true;
            this.radioButtonSequential.Checked = true;
            this.radioButtonSequential.Location = new System.Drawing.Point(645, 325);
            this.radioButtonSequential.Name = "radioButtonSequential";
            this.radioButtonSequential.Size = new System.Drawing.Size(100, 24);
            this.radioButtonSequential.TabIndex = 5;
            this.radioButtonSequential.TabStop = true;
            this.radioButtonSequential.Text = "Sequential";
            this.radioButtonSequential.UseVisualStyleBackColor = true;
            this.radioButtonSequential.CheckedChanged += new System.EventHandler(this.radioButtonSequential_CheckedChanged);
            // 
            // radioButtonParallelEqual
            // 
            this.radioButtonParallelEqual.AutoSize = true;
            this.radioButtonParallelEqual.Location = new System.Drawing.Point(645, 355);
            this.radioButtonParallelEqual.Name = "radioButtonParallelEqual";
            this.radioButtonParallelEqual.Size = new System.Drawing.Size(129, 24);
            this.radioButtonParallelEqual.TabIndex = 6;
            this.radioButtonParallelEqual.Text = "Parallel (equal)";
            this.radioButtonParallelEqual.UseVisualStyleBackColor = true;
            this.radioButtonParallelEqual.CheckedChanged += new System.EventHandler(this.radioButtonParallelEqual_CheckedChanged);
            // 
            // radioButtonParallelBag
            // 
            this.radioButtonParallelBag.AutoSize = true;
            this.radioButtonParallelBag.Location = new System.Drawing.Point(645, 385);
            this.radioButtonParallelBag.Name = "radioButtonParallelBag";
            this.radioButtonParallelBag.Size = new System.Drawing.Size(118, 24);
            this.radioButtonParallelBag.TabIndex = 7;
            this.radioButtonParallelBag.Text = "Parallel (bag)";
            this.radioButtonParallelBag.UseVisualStyleBackColor = true;
            this.radioButtonParallelBag.CheckedChanged += new System.EventHandler(this.radioButtonParallelBag_CheckedChanged);
            // 
            // buttonConvolute
            // 
            this.buttonConvolute.Location = new System.Drawing.Point(640, 471);
            this.buttonConvolute.Name = "buttonConvolute";
            this.buttonConvolute.Size = new System.Drawing.Size(110, 50);
            this.buttonConvolute.TabIndex = 8;
            this.buttonConvolute.Text = "Convolute";
            this.buttonConvolute.UseVisualStyleBackColor = true;
            this.buttonConvolute.Click += new System.EventHandler(this.buttonConvolute_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(775, 471);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(110, 50);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save image";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSelectImage
            // 
            this.buttonSelectImage.Location = new System.Drawing.Point(491, 636);
            this.buttonSelectImage.Name = "buttonSelectImage";
            this.buttonSelectImage.Size = new System.Drawing.Size(124, 29);
            this.buttonSelectImage.TabIndex = 10;
            this.buttonSelectImage.Text = "Select image...";
            this.buttonSelectImage.UseVisualStyleBackColor = true;
            this.buttonSelectImage.Click += new System.EventHandler(this.buttonSelectImage_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 641);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Dimensions";
            // 
            // textBoxKernelSize
            // 
            this.textBoxKernelSize.Location = new System.Drawing.Point(760, 32);
            this.textBoxKernelSize.Name = "textBoxKernelSize";
            this.textBoxKernelSize.Size = new System.Drawing.Size(125, 27);
            this.textBoxKernelSize.TabIndex = 12;
            this.textBoxKernelSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxKernelSize_KeyPress);
            // 
            // textBoxSigma
            // 
            this.textBoxSigma.Location = new System.Drawing.Point(760, 92);
            this.textBoxSigma.Name = "textBoxSigma";
            this.textBoxSigma.Size = new System.Drawing.Size(125, 27);
            this.textBoxSigma.TabIndex = 13;
            this.textBoxSigma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSigma_KeyPress);
            // 
            // textBoxPieceNumber
            // 
            this.textBoxPieceNumber.Enabled = false;
            this.textBoxPieceNumber.Location = new System.Drawing.Point(760, 152);
            this.textBoxPieceNumber.Name = "textBoxPieceNumber";
            this.textBoxPieceNumber.Size = new System.Drawing.Size(125, 27);
            this.textBoxPieceNumber.TabIndex = 14;
            this.textBoxPieceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPieceNumber_KeyPress);
            // 
            // textBoxTaskNumber
            // 
            this.textBoxTaskNumber.Enabled = false;
            this.textBoxTaskNumber.Location = new System.Drawing.Point(760, 212);
            this.textBoxTaskNumber.Name = "textBoxTaskNumber";
            this.textBoxTaskNumber.Size = new System.Drawing.Size(125, 27);
            this.textBoxTaskNumber.TabIndex = 15;
            this.textBoxTaskNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxTaskNumber_KeyPress);
            // 
            // textBoxDimensions
            // 
            this.textBoxDimensions.Location = new System.Drawing.Point(107, 637);
            this.textBoxDimensions.Name = "textBoxDimensions";
            this.textBoxDimensions.ReadOnly = true;
            this.textBoxDimensions.Size = new System.Drawing.Size(360, 27);
            this.textBoxDimensions.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(640, 595);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Execution time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(640, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Select mode:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // textBoxExecutionTime
            // 
            this.textBoxExecutionTime.Location = new System.Drawing.Point(760, 592);
            this.textBoxExecutionTime.Name = "textBoxExecutionTime";
            this.textBoxExecutionTime.ReadOnly = true;
            this.textBoxExecutionTime.Size = new System.Drawing.Size(125, 27);
            this.textBoxExecutionTime.TabIndex = 20;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 683);
            this.Controls.Add(this.textBoxExecutionTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxDimensions);
            this.Controls.Add(this.textBoxTaskNumber);
            this.Controls.Add(this.textBoxPieceNumber);
            this.Controls.Add(this.textBoxSigma);
            this.Controls.Add(this.textBoxKernelSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonSelectImage);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonConvolute);
            this.Controls.Add(this.radioButtonParallelBag);
            this.Controls.Add(this.radioButtonParallelEqual);
            this.Controls.Add(this.radioButtonSequential);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Parallel Convolution (Gaussian Blur)";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonSequential;
        private System.Windows.Forms.RadioButton radioButtonParallelEqual;
        private System.Windows.Forms.RadioButton radioButtonParallelBag;
        private System.Windows.Forms.Button buttonConvolute;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSelectImage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxKernelSize;
        private System.Windows.Forms.TextBox textBoxSigma;
        private System.Windows.Forms.TextBox textBoxPieceNumber;
        private System.Windows.Forms.TextBox textBoxTaskNumber;
        private System.Windows.Forms.TextBox textBoxDimensions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBoxExecutionTime;
        private System.Windows.Forms.Timer timer1;
    }
}
