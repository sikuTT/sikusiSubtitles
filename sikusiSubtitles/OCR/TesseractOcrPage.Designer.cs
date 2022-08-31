namespace sikusiSubtitles.OCR {
    partial class TesseractOcrPage {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ocrLangComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.translationEngineComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.translationLangComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 37);
            this.label1.TabIndex = 6;
            this.label1.Text = "OCR - Tesseract";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "読み取り元の言語";
            // 
            // ocrLangComboBox
            // 
            this.ocrLangComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrLangComboBox.FormattingEnabled = true;
            this.ocrLangComboBox.Location = new System.Drawing.Point(121, 36);
            this.ocrLangComboBox.Name = "ocrLangComboBox";
            this.ocrLangComboBox.Size = new System.Drawing.Size(474, 23);
            this.ocrLangComboBox.TabIndex = 1;
            this.ocrLangComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrLangComboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.translationEngineComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.translationLangComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ocrLangComboBox);
            this.groupBox1.Location = new System.Drawing.Point(17, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 160);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "デフォルト設定";
            // 
            // translationEngineComboBox
            // 
            this.translationEngineComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationEngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationEngineComboBox.FormattingEnabled = true;
            this.translationEngineComboBox.Location = new System.Drawing.Point(121, 76);
            this.translationEngineComboBox.Name = "translationEngineComboBox";
            this.translationEngineComboBox.Size = new System.Drawing.Size(474, 23);
            this.translationEngineComboBox.TabIndex = 2;
            this.translationEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.translationEngineComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "翻訳エンジン";
            // 
            // translationLangComboBox
            // 
            this.translationLangComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationLangComboBox.FormattingEnabled = true;
            this.translationLangComboBox.Location = new System.Drawing.Point(121, 112);
            this.translationLangComboBox.Name = "translationLangComboBox";
            this.translationLangComboBox.Size = new System.Drawing.Size(474, 23);
            this.translationLangComboBox.TabIndex = 3;
            this.translationLangComboBox.SelectedIndexChanged += new System.EventHandler(this.translationLangComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "翻訳先の言語";
            // 
            // TesseractOcrPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "TesseractOcrPage";
            this.Size = new System.Drawing.Size(634, 350);
            this.Load += new System.EventHandler(this.TesseractOcrPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox ocrLangComboBox;
        private GroupBox groupBox1;
        private ComboBox translationLangComboBox;
        private Label label3;
        private ComboBox translationEngineComboBox;
        private Label label4;
    }
}
