namespace sikusiSubtitles.OBS {
    partial class ObsSubtitlesPage {
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
            this.translateTargetRemove = new System.Windows.Forms.Button();
            this.voiceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.translateTargetAddButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.translationToGridView = new System.Windows.Forms.DataGridView();
            this.TranslationToLanguage = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TranslationToTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.translationToGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.translateTargetRemove);
            this.groupBox1.Controls.Add(this.voiceTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.translateTargetAddButton);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.translationToGridView);
            this.groupBox1.Location = new System.Drawing.Point(10, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 277);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字幕表示先";
            // 
            // translateTargetRemove
            // 
            this.translateTargetRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translateTargetRemove.Location = new System.Drawing.Point(574, 244);
            this.translateTargetRemove.Name = "translateTargetRemove";
            this.translateTargetRemove.Size = new System.Drawing.Size(75, 23);
            this.translateTargetRemove.TabIndex = 10;
            this.translateTargetRemove.Text = "削除";
            this.translateTargetRemove.UseVisualStyleBackColor = true;
            // 
            // voiceTextBox
            // 
            this.voiceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.voiceTextBox.Location = new System.Drawing.Point(105, 27);
            this.voiceTextBox.Name = "voiceTextBox";
            this.voiceTextBox.Size = new System.Drawing.Size(544, 23);
            this.voiceTextBox.TabIndex = 7;
            this.voiceTextBox.TextChanged += new System.EventHandler(this.voiceTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "音声";
            // 
            // translateTargetAddButton
            // 
            this.translateTargetAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translateTargetAddButton.Location = new System.Drawing.Point(493, 244);
            this.translateTargetAddButton.Name = "translateTargetAddButton";
            this.translateTargetAddButton.Size = new System.Drawing.Size(75, 23);
            this.translateTargetAddButton.TabIndex = 9;
            this.translateTargetAddButton.Text = "追加";
            this.translateTargetAddButton.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "翻訳結果";
            // 
            // translationToGridView
            // 
            this.translationToGridView.AllowUserToAddRows = false;
            this.translationToGridView.AllowUserToDeleteRows = false;
            this.translationToGridView.AllowUserToResizeRows = false;
            this.translationToGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationToGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.translationToGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.translationToGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TranslationToLanguage,
            this.TranslationToTarget});
            this.translationToGridView.Location = new System.Drawing.Point(105, 74);
            this.translationToGridView.MultiSelect = false;
            this.translationToGridView.Name = "translationToGridView";
            this.translationToGridView.RowTemplate.Height = 25;
            this.translationToGridView.Size = new System.Drawing.Size(544, 164);
            this.translationToGridView.TabIndex = 6;
            this.translationToGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.translationToDataGridView_CellValueChanged);
            // 
            // TranslationToLanguage
            // 
            this.TranslationToLanguage.HeaderText = "翻訳先言語";
            this.TranslationToLanguage.Name = "TranslationToLanguage";
            this.TranslationToLanguage.Width = 180;
            // 
            // TranslationToTarget
            // 
            this.TranslationToTarget.HeaderText = "翻訳結果の表示先";
            this.TranslationToTarget.Name = "TranslationToTarget";
            this.TranslationToTarget.Width = 180;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 37);
            this.label1.TabIndex = 6;
            this.label1.Text = "OBS - 字幕";
            // 
            // ObsSubtitlesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "ObsSubtitlesPage";
            this.Size = new System.Drawing.Size(668, 375);
            this.Load += new System.EventHandler(this.ObsSubtitlesPage_Load);
            this.VisibleChanged += new System.EventHandler(this.ObsSubtitlesPage_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.translationToGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private TextBox voiceTextBox;
        private Label label2;
        private Label label1;
        private Label label10;
        private DataGridView translationToGridView;
        private DataGridViewComboBoxColumn TranslationToLanguage;
        private DataGridViewTextBoxColumn TranslationToTarget;
        private Button translateTargetRemove;
        private Button translateTargetAddButton;
    }
}
