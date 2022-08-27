namespace sikusiSubtitles.Translation {
    partial class AzureTranslationPage {
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
            this.regionTextBox = new System.Windows.Forms.TextBox();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.to2CheckBox = new System.Windows.Forms.CheckBox();
            this.to1CheckBox = new System.Windows.Forms.CheckBox();
            this.to2ComboBox = new System.Windows.Forms.ComboBox();
            this.to1ComboBox = new System.Windows.Forms.ComboBox();
            this.fromComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.regionTextBox);
            this.groupBox1.Controls.Add(this.keyTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(26, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "認証情報";
            // 
            // regionTextBox
            // 
            this.regionTextBox.Location = new System.Drawing.Point(119, 69);
            this.regionTextBox.Name = "regionTextBox";
            this.regionTextBox.Size = new System.Drawing.Size(353, 23);
            this.regionTextBox.TabIndex = 5;
            this.regionTextBox.TextChanged += new System.EventHandler(this.regionTextBox_TextChanged);
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(119, 27);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.PasswordChar = '*';
            this.keyTextBox.Size = new System.Drawing.Size(353, 23);
            this.keyTextBox.TabIndex = 3;
            this.keyTextBox.TextChanged += new System.EventHandler(this.keyTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "場所/地域";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "キー";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(525, 37);
            this.label1.TabIndex = 8;
            this.label1.Text = "翻訳 - Azure Cognitive Services Translator";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.to2CheckBox);
            this.groupBox2.Controls.Add(this.to1CheckBox);
            this.groupBox2.Controls.Add(this.to2ComboBox);
            this.groupBox2.Controls.Add(this.to1ComboBox);
            this.groupBox2.Controls.Add(this.fromComboBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(26, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(585, 217);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "翻訳";
            // 
            // to2CheckBox
            // 
            this.to2CheckBox.AutoSize = true;
            this.to2CheckBox.Location = new System.Drawing.Point(26, 150);
            this.to2CheckBox.Name = "to2CheckBox";
            this.to2CheckBox.Size = new System.Drawing.Size(69, 19);
            this.to2CheckBox.TabIndex = 12;
            this.to2CheckBox.Text = "翻訳する";
            this.to2CheckBox.UseVisualStyleBackColor = true;
            this.to2CheckBox.CheckedChanged += new System.EventHandler(this.to2CheckBox_CheckedChanged);
            // 
            // to1CheckBox
            // 
            this.to1CheckBox.AutoSize = true;
            this.to1CheckBox.Location = new System.Drawing.Point(26, 74);
            this.to1CheckBox.Name = "to1CheckBox";
            this.to1CheckBox.Size = new System.Drawing.Size(69, 19);
            this.to1CheckBox.TabIndex = 9;
            this.to1CheckBox.Text = "翻訳する";
            this.to1CheckBox.UseVisualStyleBackColor = true;
            this.to1CheckBox.CheckedChanged += new System.EventHandler(this.to1CheckBox_CheckedChanged);
            // 
            // to2ComboBox
            // 
            this.to2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.to2ComboBox.FormattingEnabled = true;
            this.to2ComboBox.Location = new System.Drawing.Point(119, 173);
            this.to2ComboBox.Name = "to2ComboBox";
            this.to2ComboBox.Size = new System.Drawing.Size(353, 23);
            this.to2ComboBox.TabIndex = 14;
            this.to2ComboBox.SelectedIndexChanged += new System.EventHandler(this.to2ComboBox_SelectedIndexChanged);
            // 
            // to1ComboBox
            // 
            this.to1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.to1ComboBox.FormattingEnabled = true;
            this.to1ComboBox.Location = new System.Drawing.Point(119, 100);
            this.to1ComboBox.Name = "to1ComboBox";
            this.to1ComboBox.Size = new System.Drawing.Size(353, 23);
            this.to1ComboBox.TabIndex = 11;
            this.to1ComboBox.SelectedIndexChanged += new System.EventHandler(this.to1ComboBox_SelectedIndexChanged);
            // 
            // fromComboBox
            // 
            this.fromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromComboBox.FormattingEnabled = true;
            this.fromComboBox.Location = new System.Drawing.Point(119, 27);
            this.fromComboBox.Name = "fromComboBox";
            this.fromComboBox.Size = new System.Drawing.Size(353, 23);
            this.fromComboBox.TabIndex = 8;
            this.fromComboBox.SelectedIndexChanged += new System.EventHandler(this.fromComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "翻訳先２";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "翻訳先１";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "翻訳元";
            // 
            // AzureTranslationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "AzureTranslationPage";
            this.Size = new System.Drawing.Size(629, 498);
            this.Load += new System.EventHandler(this.AzureTranslationPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private TextBox regionTextBox;
        private TextBox keyTextBox;
        private Label label3;
        private Label label2;
        private GroupBox groupBox2;
        private CheckBox to2CheckBox;
        private CheckBox to1CheckBox;
        private ComboBox to2ComboBox;
        private ComboBox to1ComboBox;
        private ComboBox fromComboBox;
        private Label label6;
        private Label label4;
        private Label label5;
    }
}
