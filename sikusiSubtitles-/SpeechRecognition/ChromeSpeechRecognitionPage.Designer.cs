namespace sikusiSubtitles.SpeechRecognition {
    partial class ChromeSpeechRecognitionPage {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.webSocketServerPortUpDown = new System.Windows.Forms.NumericUpDown();
            this.httpServerPortUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webSocketServerPortUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.httpServerPortUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "音声認識 - Chrome";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.webSocketServerPortUpDown);
            this.groupBox1.Controls.Add(this.httpServerPortUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 101);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chromeからの接続に使用するポート番号を設定します";
            // 
            // webSocketServerPortUpDown
            // 
            this.webSocketServerPortUpDown.Location = new System.Drawing.Point(93, 59);
            this.webSocketServerPortUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.webSocketServerPortUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.webSocketServerPortUpDown.Name = "webSocketServerPortUpDown";
            this.webSocketServerPortUpDown.Size = new System.Drawing.Size(120, 23);
            this.webSocketServerPortUpDown.TabIndex = 1;
            this.webSocketServerPortUpDown.Value = new decimal(new int[] {
            14950,
            0,
            0,
            0});
            this.webSocketServerPortUpDown.ValueChanged += new System.EventHandler(this.webSocketServerPortUpDown_ValueChanged);
            // 
            // httpServerPortUpDown
            // 
            this.httpServerPortUpDown.Location = new System.Drawing.Point(93, 28);
            this.httpServerPortUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.httpServerPortUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.httpServerPortUpDown.Name = "httpServerPortUpDown";
            this.httpServerPortUpDown.Size = new System.Drawing.Size(120, 23);
            this.httpServerPortUpDown.TabIndex = 1;
            this.httpServerPortUpDown.Value = new decimal(new int[] {
            14949,
            0,
            0,
            0});
            this.httpServerPortUpDown.ValueChanged += new System.EventHandler(this.httpServerPortUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "ポート";
            // 
            // ChromeSpeechRecognitionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "ChromeSpeechRecognitionPage";
            this.Size = new System.Drawing.Size(639, 378);
            this.Load += new System.EventHandler(this.ChromeSpeechRecognitionPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webSocketServerPortUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.httpServerPortUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
        private NumericUpDown httpServerPortUpDown;
        private NumericUpDown webSocketServerPortUpDown;
    }
}
