namespace sikusiSubtitles {
    partial class MainForm {
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuView = new System.Windows.Forms.TreeView();
            this.speechRecognitionCheckBox = new System.Windows.Forms.CheckBox();
            this.obsCheckBox = new System.Windows.Forms.CheckBox();
            this.recognitionResultTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(950, 481);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.TabStop = false;
            // 
            // menuView
            // 
            this.menuView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuView.Font = new System.Drawing.Font("Yu Gothic UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuView.Location = new System.Drawing.Point(0, 0);
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(316, 481);
            this.menuView.TabIndex = 10;
            this.menuView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.menuView_AfterSelect);
            // 
            // speechRecognitionCheckBox
            // 
            this.speechRecognitionCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.speechRecognitionCheckBox.AutoSize = true;
            this.speechRecognitionCheckBox.Location = new System.Drawing.Point(12, 12);
            this.speechRecognitionCheckBox.Name = "speechRecognitionCheckBox";
            this.speechRecognitionCheckBox.Size = new System.Drawing.Size(65, 25);
            this.speechRecognitionCheckBox.TabIndex = 1;
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
            this.obsCheckBox.TabIndex = 2;
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
            this.recognitionResultTextBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(912, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ver. 0.3.4";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 598);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recognitionResultTextBox);
            this.Controls.Add(this.obsCheckBox);
            this.Controls.Add(this.speechRecognitionCheckBox);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "sikusiSubtitles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
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
        private TextBox recognitionResultTextBox;
        private Label label1;
    }
}