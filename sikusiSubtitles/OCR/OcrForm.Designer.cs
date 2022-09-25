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
            this.components = new System.ComponentModel.Container();
            this.ocrTextBox = new System.Windows.Forms.TextBox();
            this.ocrTextEditContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchByWeblio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.label5 = new System.Windows.Forms.Label();
            this.obsTextSourceComboBox = new System.Windows.Forms.ComboBox();
            this.obsTextSourceRefreshButton = new System.Windows.Forms.Button();
            this.ocrEngineComboBox = new System.Windows.Forms.ComboBox();
            this.ocrLanguageComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.obsClearButton = new System.Windows.Forms.Button();
            this.captureAreaButton = new System.Windows.Forms.Button();
            this.ocrSpeechEngineComboBox = new System.Windows.Forms.ComboBox();
            this.ocrSpeechVoiceComboBox = new System.Windows.Forms.ComboBox();
            this.speechOcrStopButton = new System.Windows.Forms.Button();
            this.speechOcrButton = new System.Windows.Forms.Button();
            this.ocrTextEditContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ocrTextBox
            // 
            this.ocrTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrTextBox.ContextMenuStrip = this.ocrTextEditContextMenuStrip;
            this.ocrTextBox.HideSelection = false;
            this.ocrTextBox.Location = new System.Drawing.Point(14, 193);
            this.ocrTextBox.Multiline = true;
            this.ocrTextBox.Name = "ocrTextBox";
            this.ocrTextBox.Size = new System.Drawing.Size(912, 109);
            this.ocrTextBox.TabIndex = 14;
            this.ocrTextBox.TextChanged += new System.EventHandler(this.ocrTextBox_TextChanged);
            // 
            // ocrTextEditContextMenuStrip
            // 
            this.ocrTextEditContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchByWeblio,
            this.toolStripMenuItem1,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.ocrTextEditContextMenuStrip.Name = "ocrTextEditContextMenuStrip";
            this.ocrTextEditContextMenuStrip.Size = new System.Drawing.Size(163, 98);
            // 
            // searchByWeblio
            // 
            this.searchByWeblio.Name = "searchByWeblio";
            this.searchByWeblio.Size = new System.Drawing.Size(162, 22);
            this.searchByWeblio.Text = "weblioで検索する";
            this.searchByWeblio.Click += new System.EventHandler(this.searchByWeblio_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.cutToolStripMenuItem.Text = "切り取り";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.copyToolStripMenuItem.Text = "コピー";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.pasteToolStripMenuItem.Text = "貼り付け";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // translatedTextBox
            // 
            this.translatedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translatedTextBox.Location = new System.Drawing.Point(14, 323);
            this.translatedTextBox.Multiline = true;
            this.translatedTextBox.Name = "translatedTextBox";
            this.translatedTextBox.ReadOnly = true;
            this.translatedTextBox.Size = new System.Drawing.Size(912, 105);
            this.translatedTextBox.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "テキスト";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "翻訳結果";
            // 
            // translateButton
            // 
            this.translateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translateButton.Location = new System.Drawing.Point(848, 80);
            this.translateButton.Name = "translateButton";
            this.translateButton.Size = new System.Drawing.Size(75, 30);
            this.translateButton.TabIndex = 7;
            this.translateButton.Text = "翻訳";
            this.translateButton.UseVisualStyleBackColor = true;
            this.translateButton.Click += new System.EventHandler(this.translateButton_Click);
            // 
            // ocrButton
            // 
            this.ocrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrButton.Enabled = false;
            this.ocrButton.Location = new System.Drawing.Point(849, 41);
            this.ocrButton.Name = "ocrButton";
            this.ocrButton.Size = new System.Drawing.Size(75, 30);
            this.ocrButton.TabIndex = 4;
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
            this.windowNameTextBox.Size = new System.Drawing.Size(707, 23);
            this.windowNameTextBox.TabIndex = 0;
            // 
            // translationEngineComboBox
            // 
            this.translationEngineComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationEngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationEngineComboBox.FormattingEnabled = true;
            this.translationEngineComboBox.Location = new System.Drawing.Point(111, 82);
            this.translationEngineComboBox.Name = "translationEngineComboBox";
            this.translationEngineComboBox.Size = new System.Drawing.Size(425, 23);
            this.translationEngineComboBox.TabIndex = 5;
            this.translationEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.translationEngineComboBox_SelectedIndexChanged);
            // 
            // translationLangComboBox
            // 
            this.translationLangComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translationLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationLangComboBox.FormattingEnabled = true;
            this.translationLangComboBox.Location = new System.Drawing.Point(542, 82);
            this.translationLangComboBox.Name = "translationLangComboBox";
            this.translationLangComboBox.Size = new System.Drawing.Size(300, 23);
            this.translationLangComboBox.TabIndex = 6;
            this.translationLangComboBox.SelectedIndexChanged += new System.EventHandler(this.translationLangComboBox_SelectedIndexChanged);
            // 
            // honn
            // 
            this.honn.AutoSize = true;
            this.honn.Location = new System.Drawing.Point(14, 87);
            this.honn.Name = "honn";
            this.honn.Size = new System.Drawing.Size(68, 15);
            this.honn.TabIndex = 6;
            this.honn.Text = "翻訳エンジン";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 123);
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
            this.obsTextSourceComboBox.Location = new System.Drawing.Point(111, 120);
            this.obsTextSourceComboBox.Name = "obsTextSourceComboBox";
            this.obsTextSourceComboBox.Size = new System.Drawing.Size(650, 23);
            this.obsTextSourceComboBox.TabIndex = 8;
            // 
            // obsTextSourceRefreshButton
            // 
            this.obsTextSourceRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.obsTextSourceRefreshButton.Location = new System.Drawing.Point(767, 117);
            this.obsTextSourceRefreshButton.Name = "obsTextSourceRefreshButton";
            this.obsTextSourceRefreshButton.Size = new System.Drawing.Size(75, 30);
            this.obsTextSourceRefreshButton.TabIndex = 9;
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
            this.ocrEngineComboBox.Size = new System.Drawing.Size(424, 23);
            this.ocrEngineComboBox.TabIndex = 2;
            this.ocrEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrEngineComboBox_SelectedIndexChanged);
            // 
            // ocrLanguageComboBox
            // 
            this.ocrLanguageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrLanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrLanguageComboBox.FormattingEnabled = true;
            this.ocrLanguageComboBox.Location = new System.Drawing.Point(542, 44);
            this.ocrLanguageComboBox.Name = "ocrLanguageComboBox";
            this.ocrLanguageComboBox.Size = new System.Drawing.Size(300, 23);
            this.ocrLanguageComboBox.TabIndex = 3;
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
            // obsClearButton
            // 
            this.obsClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.obsClearButton.Location = new System.Drawing.Point(848, 117);
            this.obsClearButton.Name = "obsClearButton";
            this.obsClearButton.Size = new System.Drawing.Size(75, 30);
            this.obsClearButton.TabIndex = 10;
            this.obsClearButton.Text = "クリア";
            this.obsClearButton.UseVisualStyleBackColor = true;
            this.obsClearButton.Click += new System.EventHandler(this.obsClearButton_Click);
            // 
            // captureAreaButton
            // 
            this.captureAreaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.captureAreaButton.Location = new System.Drawing.Point(814, 5);
            this.captureAreaButton.Name = "captureAreaButton";
            this.captureAreaButton.Size = new System.Drawing.Size(112, 30);
            this.captureAreaButton.TabIndex = 1;
            this.captureAreaButton.Text = "読み取り先を設定";
            this.captureAreaButton.UseVisualStyleBackColor = true;
            this.captureAreaButton.Click += new System.EventHandler(this.captureAreaButton_Click);
            // 
            // ocrSpeechEngineComboBox
            // 
            this.ocrSpeechEngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrSpeechEngineComboBox.FormattingEnabled = true;
            this.ocrSpeechEngineComboBox.Location = new System.Drawing.Point(111, 162);
            this.ocrSpeechEngineComboBox.Name = "ocrSpeechEngineComboBox";
            this.ocrSpeechEngineComboBox.Size = new System.Drawing.Size(297, 23);
            this.ocrSpeechEngineComboBox.TabIndex = 11;
            this.ocrSpeechEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrSpeechEngineComboBox_SelectedIndexChanged);
            // 
            // ocrSpeechVoiceComboBox
            // 
            this.ocrSpeechVoiceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrSpeechVoiceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrSpeechVoiceComboBox.FormattingEnabled = true;
            this.ocrSpeechVoiceComboBox.Location = new System.Drawing.Point(414, 162);
            this.ocrSpeechVoiceComboBox.Name = "ocrSpeechVoiceComboBox";
            this.ocrSpeechVoiceComboBox.Size = new System.Drawing.Size(428, 23);
            this.ocrSpeechVoiceComboBox.TabIndex = 12;
            this.ocrSpeechVoiceComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrSpeechVoiceComboBox_SelectedIndexChanged);
            // 
            // speechOcrStopButton
            // 
            this.speechOcrStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speechOcrStopButton.Location = new System.Drawing.Point(850, 159);
            this.speechOcrStopButton.Name = "speechOcrStopButton";
            this.speechOcrStopButton.Size = new System.Drawing.Size(75, 30);
            this.speechOcrStopButton.TabIndex = 13;
            this.speechOcrStopButton.Text = "キャンセル";
            this.speechOcrStopButton.UseVisualStyleBackColor = true;
            this.speechOcrStopButton.Visible = false;
            this.speechOcrStopButton.Click += new System.EventHandler(this.speechOcrStopButton_Click);
            // 
            // speechOcrButton
            // 
            this.speechOcrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speechOcrButton.Enabled = false;
            this.speechOcrButton.Location = new System.Drawing.Point(850, 158);
            this.speechOcrButton.Name = "speechOcrButton";
            this.speechOcrButton.Size = new System.Drawing.Size(75, 30);
            this.speechOcrButton.TabIndex = 19;
            this.speechOcrButton.Text = "読み上げる";
            this.speechOcrButton.UseVisualStyleBackColor = true;
            this.speechOcrButton.Click += new System.EventHandler(this.speechOcrButton_Click);
            // 
            // OcrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 440);
            this.Controls.Add(this.speechOcrButton);
            this.Controls.Add(this.speechOcrStopButton);
            this.Controls.Add(this.ocrSpeechVoiceComboBox);
            this.Controls.Add(this.ocrSpeechEngineComboBox);
            this.Controls.Add(this.captureAreaButton);
            this.Controls.Add(this.label5);
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
            this.ocrTextEditContextMenuStrip.ResumeLayout(false);
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
        private Label label5;
        private ComboBox obsTextSourceComboBox;
        private Button obsTextSourceRefreshButton;
        private ComboBox translationLangComboBox;
        private ComboBox ocrEngineComboBox;
        private ComboBox ocrLanguageComboBox;
        private Label label6;
        private Button obsClearButton;
        private Button captureAreaButton;
        private ComboBox ocrSpeechEngineComboBox;
        private ComboBox ocrSpeechVoiceComboBox;
        private Button speechOcrStopButton;
        private Button speechOcrButton;
        private ContextMenuStrip ocrTextEditContextMenuStrip;
        private ToolStripMenuItem weblioで検索するToolStripMenuItem;
        private ToolStripMenuItem searchByWeblio;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
    }
}