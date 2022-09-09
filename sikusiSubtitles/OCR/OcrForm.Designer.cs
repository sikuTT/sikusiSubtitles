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
            this.ocrTextBox = new System.Windows.Forms.TextBox();
            this.translatedTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.translateButton = new System.Windows.Forms.Button();
            this.ocrButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.windowNameTextBox = new System.Windows.Forms.TextBox();
            this.translationEngineComboBox = new System.Windows.Forms.ComboBox();
            this.translationLangComboBox = new System.Windows.Forms.ComboBox();
            this.honn = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.obsTextSourceComboBox = new System.Windows.Forms.ComboBox();
            this.obsTextSourceRefreshButton = new System.Windows.Forms.Button();
            this.ocrEngineComboBox = new System.Windows.Forms.ComboBox();
            this.ocrLanguageComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.obsClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ocrTextBox
            // 
            this.ocrTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrTextBox.Location = new System.Drawing.Point(15, 222);
            this.ocrTextBox.Multiline = true;
            this.ocrTextBox.Name = "ocrTextBox";
            this.ocrTextBox.Size = new System.Drawing.Size(724, 95);
            this.ocrTextBox.TabIndex = 9;
            // 
            // translatedTextBox
            // 
            this.translatedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translatedTextBox.Location = new System.Drawing.Point(14, 355);
            this.translatedTextBox.Multiline = true;
            this.translatedTextBox.Name = "translatedTextBox";
            this.translatedTextBox.ReadOnly = true;
            this.translatedTextBox.Size = new System.Drawing.Size(724, 105);
            this.translatedTextBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "テキスト";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "翻訳結果";
            // 
            // translateButton
            // 
            this.translateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translateButton.Location = new System.Drawing.Point(660, 102);
            this.translateButton.Name = "translateButton";
            this.translateButton.Size = new System.Drawing.Size(75, 30);
            this.translateButton.TabIndex = 4;
            this.translateButton.Text = "翻訳";
            this.translateButton.UseVisualStyleBackColor = true;
            this.translateButton.Click += new System.EventHandler(this.translateButton_Click);
            // 
            // ocrButton
            // 
            this.ocrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrButton.Location = new System.Drawing.Point(661, 41);
            this.ocrButton.Name = "ocrButton";
            this.ocrButton.Size = new System.Drawing.Size(75, 30);
            this.ocrButton.TabIndex = 1;
            this.ocrButton.Text = "文字認識";
            this.ocrButton.UseVisualStyleBackColor = true;
            this.ocrButton.Click += new System.EventHandler(this.ocrButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "キャプチャー対象";
            // 
            // windowNameTextBox
            // 
            this.windowNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.windowNameTextBox.Location = new System.Drawing.Point(99, 6);
            this.windowNameTextBox.Name = "windowNameTextBox";
            this.windowNameTextBox.ReadOnly = true;
            this.windowNameTextBox.Size = new System.Drawing.Size(637, 23);
            this.windowNameTextBox.TabIndex = 0;
            // 
            // translationEngineComboBox
            // 
            this.translationEngineComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationEngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationEngineComboBox.FormattingEnabled = true;
            this.translationEngineComboBox.Location = new System.Drawing.Point(111, 104);
            this.translationEngineComboBox.Name = "translationEngineComboBox";
            this.translationEngineComboBox.Size = new System.Drawing.Size(543, 23);
            this.translationEngineComboBox.TabIndex = 3;
            this.translationEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.translationEngineComboBox_SelectedIndexChanged);
            // 
            // translationLangComboBox
            // 
            this.translationLangComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationLangComboBox.FormattingEnabled = true;
            this.translationLangComboBox.Location = new System.Drawing.Point(111, 134);
            this.translationLangComboBox.Name = "translationLangComboBox";
            this.translationLangComboBox.Size = new System.Drawing.Size(543, 23);
            this.translationLangComboBox.TabIndex = 5;
            this.translationLangComboBox.SelectedIndexChanged += new System.EventHandler(this.translationLangComboBox_SelectedIndexChanged);
            // 
            // honn
            // 
            this.honn.AutoSize = true;
            this.honn.Location = new System.Drawing.Point(14, 109);
            this.honn.Name = "honn";
            this.honn.Size = new System.Drawing.Size(68, 15);
            this.honn.TabIndex = 6;
            this.honn.Text = "翻訳エンジン";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "翻訳先言語";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "OBSテキストソース";
            // 
            // obsTextSourceComboBox
            // 
            this.obsTextSourceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.obsTextSourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.obsTextSourceComboBox.FormattingEnabled = true;
            this.obsTextSourceComboBox.Location = new System.Drawing.Point(111, 166);
            this.obsTextSourceComboBox.Name = "obsTextSourceComboBox";
            this.obsTextSourceComboBox.Size = new System.Drawing.Size(462, 23);
            this.obsTextSourceComboBox.TabIndex = 6;
            // 
            // obsTextSourceRefreshButton
            // 
            this.obsTextSourceRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.obsTextSourceRefreshButton.Location = new System.Drawing.Point(579, 162);
            this.obsTextSourceRefreshButton.Name = "obsTextSourceRefreshButton";
            this.obsTextSourceRefreshButton.Size = new System.Drawing.Size(75, 30);
            this.obsTextSourceRefreshButton.TabIndex = 7;
            this.obsTextSourceRefreshButton.Text = "更新";
            this.obsTextSourceRefreshButton.UseVisualStyleBackColor = true;
            this.obsTextSourceRefreshButton.Click += new System.EventHandler(this.obsTextSourceRefreshButton_Click);
            // 
            // ocrEngineComboBox
            // 
            this.ocrEngineComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrEngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrEngineComboBox.FormattingEnabled = true;
            this.ocrEngineComboBox.Location = new System.Drawing.Point(112, 44);
            this.ocrEngineComboBox.Name = "ocrEngineComboBox";
            this.ocrEngineComboBox.Size = new System.Drawing.Size(543, 23);
            this.ocrEngineComboBox.TabIndex = 0;
            this.ocrEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrEngineComboBox_SelectedIndexChanged);
            // 
            // ocrLanguageComboBox
            // 
            this.ocrLanguageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrLanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrLanguageComboBox.FormattingEnabled = true;
            this.ocrLanguageComboBox.Location = new System.Drawing.Point(112, 74);
            this.ocrLanguageComboBox.Name = "ocrLanguageComboBox";
            this.ocrLanguageComboBox.Size = new System.Drawing.Size(543, 23);
            this.ocrLanguageComboBox.TabIndex = 2;
            this.ocrLanguageComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrLanguageComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "OCRエンジン";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "読み取り言語";
            // 
            // obsClearButton
            // 
            this.obsClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.obsClearButton.Location = new System.Drawing.Point(660, 162);
            this.obsClearButton.Name = "obsClearButton";
            this.obsClearButton.Size = new System.Drawing.Size(75, 30);
            this.obsClearButton.TabIndex = 8;
            this.obsClearButton.Text = "クリア";
            this.obsClearButton.UseVisualStyleBackColor = true;
            this.obsClearButton.Click += new System.EventHandler(this.obsClearButton_Click);
            // 
            // OcrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 472);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.honn);
            this.Controls.Add(this.ocrLanguageComboBox);
            this.Controls.Add(this.obsTextSourceComboBox);
            this.Controls.Add(this.ocrEngineComboBox);
            this.Controls.Add(this.translationLangComboBox);
            this.Controls.Add(this.translationEngineComboBox);
            this.Controls.Add(this.windowNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.obsClearButton);
            this.Controls.Add(this.obsTextSourceRefreshButton);
            this.Controls.Add(this.ocrButton);
            this.Controls.Add(this.translateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.translatedTextBox);
            this.Controls.Add(this.ocrTextBox);
            this.Name = "OcrForm";
            this.Text = "OcrForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OcrForm_FormClosed);
            this.Load += new System.EventHandler(this.OcrForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox ocrTextBox;
        private TextBox translatedTextBox;
        private Label label1;
        private Label label2;
        private Button translateButton;
        private Button ocrButton;
        private Label label3;
        private TextBox windowNameTextBox;
        private ComboBox translationEngineComboBox;
        private Label honn;
        private Label label4;
        private Label label5;
        private ComboBox obsTextSourceComboBox;
        private Button obsTextSourceRefreshButton;
        private ComboBox translationLangComboBox;
        private ComboBox ocrEngineComboBox;
        private ComboBox ocrLanguageComboBox;
        private Label label6;
        private Label label7;
        private Button obsClearButton;
    }
}