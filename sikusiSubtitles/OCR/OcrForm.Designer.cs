namespace sikusiSubtitles.OCR {
    partial class OcrForm {
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
            this.originalTextBox = new System.Windows.Forms.TextBox();
            this.translatedTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.translateButton = new System.Windows.Forms.Button();
            this.ocrButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // originalTextBox
            // 
            this.originalTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalTextBox.Location = new System.Drawing.Point(12, 27);
            this.originalTextBox.Multiline = true;
            this.originalTextBox.Name = "originalTextBox";
            this.originalTextBox.Size = new System.Drawing.Size(776, 97);
            this.originalTextBox.TabIndex = 0;
            // 
            // translatedTextBox
            // 
            this.translatedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translatedTextBox.Location = new System.Drawing.Point(12, 176);
            this.translatedTextBox.Multiline = true;
            this.translatedTextBox.Name = "translatedTextBox";
            this.translatedTextBox.ReadOnly = true;
            this.translatedTextBox.Size = new System.Drawing.Size(776, 204);
            this.translatedTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "オリジナル";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "翻訳結果";
            // 
            // translateButton
            // 
            this.translateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translateButton.Location = new System.Drawing.Point(713, 130);
            this.translateButton.Name = "translateButton";
            this.translateButton.Size = new System.Drawing.Size(75, 30);
            this.translateButton.TabIndex = 2;
            this.translateButton.Text = "翻訳";
            this.translateButton.UseVisualStyleBackColor = true;
            this.translateButton.Click += new System.EventHandler(this.translateButton_Click);
            // 
            // ocrButton
            // 
            this.ocrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrButton.Location = new System.Drawing.Point(632, 130);
            this.ocrButton.Name = "ocrButton";
            this.ocrButton.Size = new System.Drawing.Size(75, 30);
            this.ocrButton.TabIndex = 1;
            this.ocrButton.Text = "文字認識";
            this.ocrButton.UseVisualStyleBackColor = true;
            this.ocrButton.Click += new System.EventHandler(this.ocrButton_Click);
            // 
            // OcrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 392);
            this.Controls.Add(this.ocrButton);
            this.Controls.Add(this.translateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.translatedTextBox);
            this.Controls.Add(this.originalTextBox);
            this.Name = "OcrForm";
            this.Text = "OcrForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox originalTextBox;
        private TextBox translatedTextBox;
        private Label label1;
        private Label label2;
        private Button translateButton;
        private Button ocrButton;
    }
}