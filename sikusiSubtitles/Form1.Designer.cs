namespace sikusiSubtitles {
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Chrome");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Azure Cognitive Services");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("AmiVoice");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("音声認識", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("字幕");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("OBS", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Google Cloud Translation - Basic");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Google Apps Script");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Azure Cognitive Services");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("DeepL");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("翻訳", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Google Cloud Vision AI");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Azure Cognitive Search");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("OCR", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.speechRecognitionCheckBox = new System.Windows.Forms.CheckBox();
            this.obsCheckBox = new System.Windows.Forms.CheckBox();
            this.recognitionResultTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.menuView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(950, 481);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 2;
            // 
            // menuView
            // 
            this.menuView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuView.Font = new System.Drawing.Font("Yu Gothic UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuView.Location = new System.Drawing.Point(0, 0);
            this.menuView.Name = "menuView";
            treeNode1.Name = "chromeSpeechRecognitionPage";
            treeNode1.Text = "Chrome";
            treeNode2.Name = "azureSpeechRecognitionPage";
            treeNode2.Text = "Azure Cognitive Services";
            treeNode3.Name = "amiVoiceSpeechRecognitionPage";
            treeNode3.Text = "AmiVoice";
            treeNode4.Name = "speechRecognitionPage";
            treeNode4.Text = "音声認識";
            treeNode5.Name = "subtitlesPage";
            treeNode5.Text = "字幕";
            treeNode6.Name = "obsPage";
            treeNode6.Text = "OBS";
            treeNode7.Name = "googleBasicTranslationPage";
            treeNode7.Text = "Google Cloud Translation - Basic";
            treeNode8.Name = "googleAppsScriptTranslationPage";
            treeNode8.Text = "Google Apps Script";
            treeNode9.Name = "azureTranslationPage";
            treeNode9.Text = "Azure Cognitive Services";
            treeNode10.Name = "deeplTranslationPage";
            treeNode10.Text = "DeepL";
            treeNode11.Name = "translationPage";
            treeNode11.Text = "翻訳";
            treeNode12.Name = "googleVisionOcrPage";
            treeNode12.Text = "Google Cloud Vision AI";
            treeNode13.Name = "azureOcrPage";
            treeNode13.Text = "Azure Cognitive Search";
            treeNode14.Name = "ocrPage";
            treeNode14.Text = "OCR";
            this.menuView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode6,
            treeNode11,
            treeNode14});
            this.menuView.Size = new System.Drawing.Size(316, 481);
            this.menuView.TabIndex = 1;
            this.menuView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.menuView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 481);
            this.panel1.TabIndex = 2;
            // 
            // speechRecognitionCheckBox
            // 
            this.speechRecognitionCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.speechRecognitionCheckBox.AutoSize = true;
            this.speechRecognitionCheckBox.Location = new System.Drawing.Point(12, 12);
            this.speechRecognitionCheckBox.Name = "speechRecognitionCheckBox";
            this.speechRecognitionCheckBox.Size = new System.Drawing.Size(65, 25);
            this.speechRecognitionCheckBox.TabIndex = 3;
            this.speechRecognitionCheckBox.Text = "音声認識";
            this.speechRecognitionCheckBox.UseVisualStyleBackColor = false;
            this.speechRecognitionCheckBox.CheckedChanged += new System.EventHandler(this.speechRecognitionCheckBox_CheckedChanged);
            // 
            // obsCheckBox
            // 
            this.obsCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.obsCheckBox.AutoSize = true;
            this.obsCheckBox.Location = new System.Drawing.Point(83, 12);
            this.obsCheckBox.Name = "obsCheckBox";
            this.obsCheckBox.Size = new System.Drawing.Size(63, 25);
            this.obsCheckBox.TabIndex = 4;
            this.obsCheckBox.Text = "OBS接続";
            this.obsCheckBox.UseVisualStyleBackColor = false;
            this.obsCheckBox.CheckedChanged += new System.EventHandler(this.obsCheckBox_CheckedChanged);
            // 
            // recognitionResultTextBox
            // 
            this.recognitionResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recognitionResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.recognitionResultTextBox.Location = new System.Drawing.Point(12, 534);
            this.recognitionResultTextBox.Multiline = true;
            this.recognitionResultTextBox.Name = "recognitionResultTextBox";
            this.recognitionResultTextBox.ReadOnly = true;
            this.recognitionResultTextBox.Size = new System.Drawing.Size(950, 52);
            this.recognitionResultTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(912, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ver. 0.1.4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 598);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recognitionResultTextBox);
            this.Controls.Add(this.obsCheckBox);
            this.Controls.Add(this.speechRecognitionCheckBox);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "sikusiSubtitles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CheckBox speechRecognitionCheckBox;
        private CheckBox obsCheckBox;
        private SplitContainer splitContainer1;
        private TreeView menuView;
        private Panel panel1;
        private TextBox recognitionResultTextBox;
        private Label label1;
    }
}