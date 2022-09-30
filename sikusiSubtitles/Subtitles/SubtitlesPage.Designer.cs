namespace sikusiSubtitles.Subtitles {
    partial class SubtitlesPage {
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.translateTargetRemove = new System.Windows.Forms.Button();
            this.translateTargetAddButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.translationToGridView = new System.Windows.Forms.DataGridView();
            this.TranslationToLanguage = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.translationFromComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.translationEngineComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.additionalTrackBar = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.clearIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.additionalCheckBox = new System.Windows.Forms.CheckBox();
            this.clearCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.translationToGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additionalTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearIntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "字幕";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.translateTargetRemove);
            this.groupBox3.Controls.Add(this.translateTargetAddButton);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.translationToGridView);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.translationFromComboBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.translationEngineComboBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(720, 317);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "翻訳";
            // 
            // translateTargetRemove
            // 
            this.translateTargetRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translateTargetRemove.Location = new System.Drawing.Point(626, 281);
            this.translateTargetRemove.Name = "translateTargetRemove";
            this.translateTargetRemove.Size = new System.Drawing.Size(75, 23);
            this.translateTargetRemove.TabIndex = 12;
            this.translateTargetRemove.Text = "削除";
            this.translateTargetRemove.UseVisualStyleBackColor = true;
            this.translateTargetRemove.Click += new System.EventHandler(this.translateTargetRemove_Click);
            // 
            // translateTargetAddButton
            // 
            this.translateTargetAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.translateTargetAddButton.Location = new System.Drawing.Point(545, 281);
            this.translateTargetAddButton.Name = "translateTargetAddButton";
            this.translateTargetAddButton.Size = new System.Drawing.Size(75, 23);
            this.translateTargetAddButton.TabIndex = 11;
            this.translateTargetAddButton.Text = "追加";
            this.translateTargetAddButton.UseVisualStyleBackColor = true;
            this.translateTargetAddButton.Click += new System.EventHandler(this.translateTargetAddButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 15);
            this.label10.TabIndex = 7;
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
            this.TranslationToLanguage});
            this.translationToGridView.Location = new System.Drawing.Point(105, 111);
            this.translationToGridView.MultiSelect = false;
            this.translationToGridView.Name = "translationToGridView";
            this.translationToGridView.RowTemplate.Height = 25;
            this.translationToGridView.Size = new System.Drawing.Size(596, 164);
            this.translationToGridView.TabIndex = 8;
            this.translationToGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.translationToGridView_CellValueChanged);
            // 
            // TranslationToLanguage
            // 
            this.TranslationToLanguage.HeaderText = "翻訳先言語";
            this.TranslationToLanguage.Name = "TranslationToLanguage";
            this.TranslationToLanguage.Width = 180;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "翻訳元言語";
            // 
            // translationFromComboBox
            // 
            this.translationFromComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationFromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationFromComboBox.FormattingEnabled = true;
            this.translationFromComboBox.Location = new System.Drawing.Point(105, 71);
            this.translationFromComboBox.Name = "translationFromComboBox";
            this.translationFromComboBox.Size = new System.Drawing.Size(596, 23);
            this.translationFromComboBox.TabIndex = 2;
            this.translationFromComboBox.SelectedIndexChanged += new System.EventHandler(this.translationFromComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "翻訳サービス";
            // 
            // translationEngineComboBox
            // 
            this.translationEngineComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationEngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationEngineComboBox.FormattingEnabled = true;
            this.translationEngineComboBox.Items.AddRange(new object[] {
            "翻訳しない"});
            this.translationEngineComboBox.Location = new System.Drawing.Point(105, 32);
            this.translationEngineComboBox.Name = "translationEngineComboBox";
            this.translationEngineComboBox.Size = new System.Drawing.Size(596, 23);
            this.translationEngineComboBox.TabIndex = 0;
            this.translationEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.translationEngineComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.additionalTrackBar);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.clearIntervalNumericUpDown);
            this.groupBox2.Controls.Add(this.additionalCheckBox);
            this.groupBox2.Controls.Add(this.clearCheckBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(3, 395);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(720, 191);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表示時間";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(239, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "（右にするほど消えるまでの時間が長くなります）";
            // 
            // additionalTrackBar
            // 
            this.additionalTrackBar.LargeChange = 2;
            this.additionalTrackBar.Location = new System.Drawing.Point(23, 143);
            this.additionalTrackBar.Minimum = 1;
            this.additionalTrackBar.Name = "additionalTrackBar";
            this.additionalTrackBar.Size = new System.Drawing.Size(355, 45);
            this.additionalTrackBar.TabIndex = 13;
            this.additionalTrackBar.Value = 1;
            this.additionalTrackBar.Scroll += new System.EventHandler(this.additionalTrackBar_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(206, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "秒";
            // 
            // clearIntervalNumericUpDown
            // 
            this.clearIntervalNumericUpDown.Location = new System.Drawing.Point(116, 59);
            this.clearIntervalNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.clearIntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.clearIntervalNumericUpDown.Name = "clearIntervalNumericUpDown";
            this.clearIntervalNumericUpDown.Size = new System.Drawing.Size(84, 23);
            this.clearIntervalNumericUpDown.TabIndex = 11;
            this.clearIntervalNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.clearIntervalNumericUpDown.ValueChanged += new System.EventHandler(this.clearIntervalNumericUpDown_ValueChanged);
            // 
            // additionalCheckBox
            // 
            this.additionalCheckBox.AutoSize = true;
            this.additionalCheckBox.Location = new System.Drawing.Point(23, 102);
            this.additionalCheckBox.Name = "additionalCheckBox";
            this.additionalCheckBox.Size = new System.Drawing.Size(244, 19);
            this.additionalCheckBox.TabIndex = 12;
            this.additionalCheckBox.Text = "文字が長い場合、消すまでの時間を長くする。";
            this.additionalCheckBox.UseVisualStyleBackColor = true;
            this.additionalCheckBox.CheckedChanged += new System.EventHandler(this.additionalCheckBox_CheckedChanged);
            // 
            // clearCheckBox
            // 
            this.clearCheckBox.AutoSize = true;
            this.clearCheckBox.Location = new System.Drawing.Point(23, 32);
            this.clearCheckBox.Name = "clearCheckBox";
            this.clearCheckBox.Size = new System.Drawing.Size(139, 19);
            this.clearCheckBox.TabIndex = 10;
            this.clearCheckBox.Text = "一定時間で字幕を消す";
            this.clearCheckBox.UseVisualStyleBackColor = true;
            this.clearCheckBox.CheckedChanged += new System.EventHandler(this.clearCheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "消すまでの時間";
            // 
            // SubtitlesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Name = "SubtitlesPage";
            this.Size = new System.Drawing.Size(726, 713);
            this.Load += new System.EventHandler(this.SubtitlesPage_Load);
            this.VisibleChanged += new System.EventHandler(this.SubtitlesPage_VisibleChanged);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.translationToGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additionalTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearIntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private GroupBox groupBox3;
        private Label label9;
        private ComboBox translationFromComboBox;
        private Label label8;
        private ComboBox translationEngineComboBox;
        private Label label10;
        private DataGridView translationToGridView;
        private Button translateTargetRemove;
        private Button translateTargetAddButton;
        private DataGridViewComboBoxColumn TranslationToLanguage;
        private GroupBox groupBox2;
        private Label label5;
        private TrackBar additionalTrackBar;
        private Label label7;
        private NumericUpDown clearIntervalNumericUpDown;
        private CheckBox additionalCheckBox;
        private CheckBox clearCheckBox;
        private Label label6;
    }
}
