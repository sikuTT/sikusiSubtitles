namespace sikusiSubtitles.Translation {
    partial class TranslationPage {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.googleBasicTranslationRadioButton = new System.Windows.Forms.RadioButton();
            this.azureTranslationRadioButton = new System.Windows.Forms.RadioButton();
            this.noTranslationRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.googleAppsScriptTranslationRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.googleAppsScriptTranslationRadioButton);
            this.groupBox1.Controls.Add(this.googleBasicTranslationRadioButton);
            this.groupBox1.Controls.Add(this.azureTranslationRadioButton);
            this.groupBox1.Controls.Add(this.noTranslationRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(26, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 139);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "使用するサービス";
            // 
            // googleBasicTranslationRadioButton
            // 
            this.googleBasicTranslationRadioButton.AutoSize = true;
            this.googleBasicTranslationRadioButton.Location = new System.Drawing.Point(26, 56);
            this.googleBasicTranslationRadioButton.Name = "googleBasicTranslationRadioButton";
            this.googleBasicTranslationRadioButton.Size = new System.Drawing.Size(195, 19);
            this.googleBasicTranslationRadioButton.TabIndex = 4;
            this.googleBasicTranslationRadioButton.Text = "Google Cloud Translation - Basic";
            this.googleBasicTranslationRadioButton.UseVisualStyleBackColor = true;
            // 
            // azureTranslationRadioButton
            // 
            this.azureTranslationRadioButton.AutoSize = true;
            this.azureTranslationRadioButton.Location = new System.Drawing.Point(26, 106);
            this.azureTranslationRadioButton.Name = "azureTranslationRadioButton";
            this.azureTranslationRadioButton.Size = new System.Drawing.Size(207, 19);
            this.azureTranslationRadioButton.TabIndex = 5;
            this.azureTranslationRadioButton.Text = "Azure Cognitive Services Translator";
            this.azureTranslationRadioButton.UseVisualStyleBackColor = true;
            // 
            // noTranslationRadioButton
            // 
            this.noTranslationRadioButton.AutoSize = true;
            this.noTranslationRadioButton.Checked = true;
            this.noTranslationRadioButton.Location = new System.Drawing.Point(26, 31);
            this.noTranslationRadioButton.Name = "noTranslationRadioButton";
            this.noTranslationRadioButton.Size = new System.Drawing.Size(77, 19);
            this.noTranslationRadioButton.TabIndex = 3;
            this.noTranslationRadioButton.TabStop = true;
            this.noTranslationRadioButton.Text = "翻訳しない";
            this.noTranslationRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "翻訳";
            // 
            // googleAppsScriptTranslationRadioButton
            // 
            this.googleAppsScriptTranslationRadioButton.AutoSize = true;
            this.googleAppsScriptTranslationRadioButton.Location = new System.Drawing.Point(26, 81);
            this.googleAppsScriptTranslationRadioButton.Name = "googleAppsScriptTranslationRadioButton";
            this.googleAppsScriptTranslationRadioButton.Size = new System.Drawing.Size(126, 19);
            this.googleAppsScriptTranslationRadioButton.TabIndex = 4;
            this.googleAppsScriptTranslationRadioButton.Text = "Google Apps Script";
            this.googleAppsScriptTranslationRadioButton.UseVisualStyleBackColor = true;
            // 
            // TranslationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "TranslationPage";
            this.Size = new System.Drawing.Size(618, 464);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private RadioButton noTranslationRadioButton;
        private RadioButton azureTranslationRadioButton;
        private RadioButton googleBasicTranslationRadioButton;
        private RadioButton googleAppsScriptTranslationRadioButton;
    }
}
