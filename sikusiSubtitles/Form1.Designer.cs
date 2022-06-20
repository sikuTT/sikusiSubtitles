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
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Chrome");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Azure Cognitive Services");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("AmiVoice");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("音声認識", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("字幕");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("OBS", new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Google Cloud Translation - Basic");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Google Apps Script");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Azure Cognitive Services");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("翻訳", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.amiVoiceSpeechRecognitionPage = new sikusiSubtitles.SpeechRecognition.AmiVoiceSpeechRecognitionPage();
            this.googleBasicTranslationPage = new sikusiSubtitles.Translation.GoogleBasicTranslationPage();
            this.azureTranslationPage = new sikusiSubtitles.Translation.AzureTranslationPage();
            this.translationPage = new sikusiSubtitles.Translation.TranslationPage();
            this.subtitlesPage = new sikusiSubtitles.OBS.SubtitlesPage();
            this.obsPage = new sikusiSubtitles.ObsPage();
            this.azureSpeechRecognitionPage = new sikusiSubtitles.SpeechRecognition.AzureSpeechRecognitionPage();
            this.chromeSpeechRecognitionPage = new sikusiSubtitles.SpeechRecognition.ChromeSpeechRecognitionPage();
            this.speechRecognitionPage = new sikusiSubtitles.SpeechRecognition.SpeechRecognitionPage();
            this.speechRecognitionCheckBox = new System.Windows.Forms.CheckBox();
            this.obsCheckBox = new System.Windows.Forms.CheckBox();
            this.recognitionResultTextBox = new System.Windows.Forms.TextBox();
            this.googleAppsScriptTranslationPage = new sikusiSubtitles.Translation.GoogleAppsScriptTranslationPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(995, 471);
            this.splitContainer1.SplitterDistance = 331;
            this.splitContainer1.TabIndex = 2;
            // 
            // menuView
            // 
            this.menuView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuView.Font = new System.Drawing.Font("Yu Gothic UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuView.Location = new System.Drawing.Point(0, 0);
            this.menuView.Name = "menuView";
            treeNode11.Name = "chromeSpeechRecognitionPage";
            treeNode11.Text = "Chrome";
            treeNode12.Name = "azureSpeechRecognitionPage";
            treeNode12.Text = "Azure Cognitive Services";
            treeNode13.Name = "amiVoiceSpeechRecognitionPage";
            treeNode13.Text = "AmiVoice";
            treeNode14.Name = "speechRecognitionPage";
            treeNode14.Text = "音声認識";
            treeNode15.Name = "subtitlesPage";
            treeNode15.Text = "字幕";
            treeNode16.Name = "obsPage";
            treeNode16.Text = "OBS";
            treeNode17.Name = "googleBasicTranslationPage";
            treeNode17.Text = "Google Cloud Translation - Basic";
            treeNode18.Name = "googleAppsScriptTranslationPage";
            treeNode18.Text = "Google Apps Script";
            treeNode19.Name = "azureTranslationPage";
            treeNode19.Text = "Azure Cognitive Services";
            treeNode20.Name = "translationPage";
            treeNode20.Text = "翻訳";
            this.menuView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode16,
            treeNode20});
            this.menuView.Size = new System.Drawing.Size(331, 471);
            this.menuView.TabIndex = 1;
            this.menuView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.menuView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.googleAppsScriptTranslationPage);
            this.panel1.Controls.Add(this.amiVoiceSpeechRecognitionPage);
            this.panel1.Controls.Add(this.googleBasicTranslationPage);
            this.panel1.Controls.Add(this.azureTranslationPage);
            this.panel1.Controls.Add(this.translationPage);
            this.panel1.Controls.Add(this.subtitlesPage);
            this.panel1.Controls.Add(this.obsPage);
            this.panel1.Controls.Add(this.azureSpeechRecognitionPage);
            this.panel1.Controls.Add(this.chromeSpeechRecognitionPage);
            this.panel1.Controls.Add(this.speechRecognitionPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 471);
            this.panel1.TabIndex = 2;
            // 
            // amiVoiceSpeechRecognitionPage
            // 
            this.amiVoiceSpeechRecognitionPage.AutoScroll = true;
            this.amiVoiceSpeechRecognitionPage.Location = new System.Drawing.Point(146, 99);
            this.amiVoiceSpeechRecognitionPage.Name = "amiVoiceSpeechRecognitionPage";
            this.amiVoiceSpeechRecognitionPage.Size = new System.Drawing.Size(379, 72);
            this.amiVoiceSpeechRecognitionPage.TabIndex = 11;
            // 
            // googleBasicTranslationPage
            // 
            this.googleBasicTranslationPage.AutoScroll = true;
            this.googleBasicTranslationPage.Location = new System.Drawing.Point(110, 310);
            this.googleBasicTranslationPage.Name = "googleBasicTranslationPage";
            this.googleBasicTranslationPage.Size = new System.Drawing.Size(440, 65);
            this.googleBasicTranslationPage.TabIndex = 10;
            // 
            // azureTranslationPage
            // 
            this.azureTranslationPage.AutoScroll = true;
            this.azureTranslationPage.Location = new System.Drawing.Point(51, 283);
            this.azureTranslationPage.Name = "azureTranslationPage";
            this.azureTranslationPage.Size = new System.Drawing.Size(571, 63);
            this.azureTranslationPage.TabIndex = 8;
            // 
            // translationPage
            // 
            this.translationPage.AutoScroll = true;
            this.translationPage.Location = new System.Drawing.Point(3, 244);
            this.translationPage.Name = "translationPage";
            this.translationPage.Size = new System.Drawing.Size(273, 60);
            this.translationPage.TabIndex = 7;
            // 
            // subtitlesPage
            // 
            this.subtitlesPage.AutoScroll = true;
            this.subtitlesPage.Location = new System.Drawing.Point(51, 177);
            this.subtitlesPage.Name = "subtitlesPage";
            this.subtitlesPage.Size = new System.Drawing.Size(172, 68);
            this.subtitlesPage.TabIndex = 6;
            // 
            // obsPage
            // 
            this.obsPage.AutoScroll = true;
            this.obsPage.Location = new System.Drawing.Point(16, 138);
            this.obsPage.Name = "obsPage";
            this.obsPage.Size = new System.Drawing.Size(124, 63);
            this.obsPage.TabIndex = 5;
            // 
            // azureSpeechRecognitionPage
            // 
            this.azureSpeechRecognitionPage.AutoScroll = true;
            this.azureSpeechRecognitionPage.Location = new System.Drawing.Point(94, 74);
            this.azureSpeechRecognitionPage.Name = "azureSpeechRecognitionPage";
            this.azureSpeechRecognitionPage.Size = new System.Drawing.Size(480, 73);
            this.azureSpeechRecognitionPage.TabIndex = 4;
            // 
            // chromeSpeechRecognitionPage
            // 
            this.chromeSpeechRecognitionPage.AutoScroll = true;
            this.chromeSpeechRecognitionPage.Location = new System.Drawing.Point(51, 38);
            this.chromeSpeechRecognitionPage.Name = "chromeSpeechRecognitionPage";
            this.chromeSpeechRecognitionPage.Size = new System.Drawing.Size(288, 55);
            this.chromeSpeechRecognitionPage.TabIndex = 9;
            // 
            // speechRecognitionPage
            // 
            this.speechRecognitionPage.AutoScroll = true;
            this.speechRecognitionPage.Location = new System.Drawing.Point(3, 3);
            this.speechRecognitionPage.Name = "speechRecognitionPage";
            this.speechRecognitionPage.Size = new System.Drawing.Size(185, 65);
            this.speechRecognitionPage.TabIndex = 0;
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
            this.recognitionResultTextBox.Location = new System.Drawing.Point(12, 524);
            this.recognitionResultTextBox.Multiline = true;
            this.recognitionResultTextBox.Name = "recognitionResultTextBox";
            this.recognitionResultTextBox.ReadOnly = true;
            this.recognitionResultTextBox.Size = new System.Drawing.Size(995, 52);
            this.recognitionResultTextBox.TabIndex = 5;
            // 
            // googleAppsScriptTranslationPage
            // 
            this.googleAppsScriptTranslationPage.AutoScroll = true;
            this.googleAppsScriptTranslationPage.Location = new System.Drawing.Point(176, 339);
            this.googleAppsScriptTranslationPage.Name = "googleAppsScriptTranslationPage";
            this.googleAppsScriptTranslationPage.Size = new System.Drawing.Size(364, 66);
            this.googleAppsScriptTranslationPage.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 588);
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
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CheckBox speechRecognitionCheckBox;
        private CheckBox obsCheckBox;
        private SplitContainer splitContainer1;
        private TreeView menuView;
        private Panel panel1;
        private SpeechRecognition.SpeechRecognitionPage speechRecognitionPage;
        private SpeechRecognition.ChromeSpeechRecognitionPage chromeSpeechRecognitionPage;
        private SpeechRecognition.AzureSpeechRecognitionPage azureSpeechRecognitionPage;
        private ObsPage obsPage;
        private OBS.SubtitlesPage subtitlesPage;
        private Translation.TranslationPage translationPage;
        private Translation.AzureTranslationPage azureTranslationPage;
        private TextBox recognitionResultTextBox;
        private Translation.GoogleBasicTranslationPage googleBasicTranslationPage;
        private SpeechRecognition.AmiVoiceSpeechRecognitionPage amiVoiceSpeechRecognitionPage;
        private Translation.GoogleAppsScriptTranslationPage googleAppsScriptTranslationPage;
    }
}