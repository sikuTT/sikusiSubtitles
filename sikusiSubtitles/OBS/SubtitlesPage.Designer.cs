namespace sikusiSubtitles.OBS {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.translation2TextBox = new System.Windows.Forms.TextBox();
            this.translation1TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.voiceTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.additionalTrackBar = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.clearIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.additionalCheckBox = new System.Windows.Forms.CheckBox();
            this.clearCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additionalTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearIntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.translation2TextBox);
            this.groupBox1.Controls.Add(this.translation1TextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.voiceTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 152);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接続情報";
            // 
            // translation2TextBox
            // 
            this.translation2TextBox.Location = new System.Drawing.Point(116, 111);
            this.translation2TextBox.Name = "translation2TextBox";
            this.translation2TextBox.Size = new System.Drawing.Size(353, 23);
            this.translation2TextBox.TabIndex = 8;
            this.translation2TextBox.TextChanged += new System.EventHandler(this.translation2TextBox_TextChanged);
            // 
            // translation1TextBox
            // 
            this.translation1TextBox.Location = new System.Drawing.Point(116, 69);
            this.translation1TextBox.Name = "translation1TextBox";
            this.translation1TextBox.Size = new System.Drawing.Size(353, 23);
            this.translation1TextBox.TabIndex = 7;
            this.translation1TextBox.TextChanged += new System.EventHandler(this.translation1TextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "翻訳２";
            // 
            // voiceTextBox
            // 
            this.voiceTextBox.Location = new System.Drawing.Point(116, 27);
            this.voiceTextBox.Name = "voiceTextBox";
            this.voiceTextBox.Size = new System.Drawing.Size(353, 23);
            this.voiceTextBox.TabIndex = 2;
            this.voiceTextBox.TextChanged += new System.EventHandler(this.voiceTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "翻訳１";
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
            this.groupBox2.Location = new System.Drawing.Point(10, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 191);
            this.groupBox2.TabIndex = 7;
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
            this.additionalTrackBar.Location = new System.Drawing.Point(23, 143);
            this.additionalTrackBar.Minimum = 1;
            this.additionalTrackBar.Name = "additionalTrackBar";
            this.additionalTrackBar.Size = new System.Drawing.Size(355, 45);
            this.additionalTrackBar.TabIndex = 11;
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
            this.clearIntervalNumericUpDown.TabIndex = 9;
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
            this.additionalCheckBox.TabIndex = 8;
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
            this.clearCheckBox.TabIndex = 8;
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
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "SubtitlesPage";
            this.Size = new System.Drawing.Size(741, 518);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additionalTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearIntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Label label4;
        private TextBox voiceTextBox;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox translation2TextBox;
        private TextBox translation1TextBox;
        private GroupBox groupBox2;
        private TrackBar additionalTrackBar;
        private Label label7;
        private NumericUpDown clearIntervalNumericUpDown;
        private CheckBox additionalCheckBox;
        private CheckBox clearCheckBox;
        private Label label6;
        private Label label5;
    }
}
