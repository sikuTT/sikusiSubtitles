﻿namespace sikusiSubtitles.OCR {
    partial class CaptureForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.Name = "CaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CaptureForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CaptureForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CaptureForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CaptureForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}