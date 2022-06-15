namespace sikusiSubtitles.SpeechRecognition {
    partial class SpeechRecognitionPage {
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
            this.micComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.azureSpeechRecognitionRadioButton = new System.Windows.Forms.RadioButton();
            this.chromeSpeechRecognitionRadioButton = new System.Windows.Forms.RadioButton();
            this.amiVoiceSpeechRecognitionRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "音声認識";
            // 
            // micComboBox
            // 
            this.micComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.micComboBox.FormattingEnabled = true;
            this.micComboBox.Location = new System.Drawing.Point(112, 27);
            this.micComboBox.Name = "micComboBox";
            this.micComboBox.Size = new System.Drawing.Size(482, 23);
            this.micComboBox.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.micComboBox);
            this.groupBox1.Location = new System.Drawing.Point(26, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 85);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "使用するマイク";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(488, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "※Chrome使用時はここではマイク設定は出来ません。Chrome上で使用するマイクを設定してください。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "マイク";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.amiVoiceSpeechRecognitionRadioButton);
            this.groupBox2.Controls.Add(this.azureSpeechRecognitionRadioButton);
            this.groupBox2.Controls.Add(this.chromeSpeechRecognitionRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(26, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(687, 104);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "使用する音声認識サービス";
            // 
            // azureSpeechRecognitionRadioButton
            // 
            this.azureSpeechRecognitionRadioButton.AutoSize = true;
            this.azureSpeechRecognitionRadioButton.Location = new System.Drawing.Point(26, 47);
            this.azureSpeechRecognitionRadioButton.Name = "azureSpeechRecognitionRadioButton";
            this.azureSpeechRecognitionRadioButton.Size = new System.Drawing.Size(153, 19);
            this.azureSpeechRecognitionRadioButton.TabIndex = 1;
            this.azureSpeechRecognitionRadioButton.Text = "Azure Cognitive Services";
            this.azureSpeechRecognitionRadioButton.UseVisualStyleBackColor = true;
            // 
            // chromeSpeechRecognitionRadioButton
            // 
            this.chromeSpeechRecognitionRadioButton.AutoSize = true;
            this.chromeSpeechRecognitionRadioButton.Checked = true;
            this.chromeSpeechRecognitionRadioButton.Location = new System.Drawing.Point(26, 22);
            this.chromeSpeechRecognitionRadioButton.Name = "chromeSpeechRecognitionRadioButton";
            this.chromeSpeechRecognitionRadioButton.Size = new System.Drawing.Size(66, 19);
            this.chromeSpeechRecognitionRadioButton.TabIndex = 0;
            this.chromeSpeechRecognitionRadioButton.TabStop = true;
            this.chromeSpeechRecognitionRadioButton.Text = "Chrome";
            this.chromeSpeechRecognitionRadioButton.UseVisualStyleBackColor = true;
            // 
            // amiVoiceSpeechRecognitionRadioButton
            // 
            this.amiVoiceSpeechRecognitionRadioButton.AutoSize = true;
            this.amiVoiceSpeechRecognitionRadioButton.Location = new System.Drawing.Point(26, 72);
            this.amiVoiceSpeechRecognitionRadioButton.Name = "amiVoiceSpeechRecognitionRadioButton";
            this.amiVoiceSpeechRecognitionRadioButton.Size = new System.Drawing.Size(74, 19);
            this.amiVoiceSpeechRecognitionRadioButton.TabIndex = 2;
            this.amiVoiceSpeechRecognitionRadioButton.Text = "AmiVoice";
            this.amiVoiceSpeechRecognitionRadioButton.UseVisualStyleBackColor = true;
            // 
            // SpeechRecognitionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "SpeechRecognitionPage";
            this.Size = new System.Drawing.Size(737, 528);
            this.Load += new System.EventHandler(this.SpeechRecognitionPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private ComboBox micComboBox;
        private GroupBox groupBox1;
        private Label label4;
        private GroupBox groupBox2;
        private RadioButton azureSpeechRecognitionRadioButton;
        private RadioButton chromeSpeechRecognitionRadioButton;
        private Label label2;
        private RadioButton amiVoiceSpeechRecognitionRadioButton;
    }
}
