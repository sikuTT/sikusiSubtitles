namespace sikusiSubtitles.OCR {
    partial class OcrPage {
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.updateWidowListButton = new System.Windows.Forms.Button();
            this.windowListView = new System.Windows.Forms.ListView();
            this.titleColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.processNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.setAreaButton = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ocrButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.translationLangComboBox = new System.Windows.Forms.ComboBox();
            this.translationEngineComboBox = new System.Windows.Forms.ComboBox();
            this.ocrLangComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ocrComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "OCR";
            // 
            // updateWidowListButton
            // 
            this.updateWidowListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateWidowListButton.Location = new System.Drawing.Point(590, 398);
            this.updateWidowListButton.Name = "updateWidowListButton";
            this.updateWidowListButton.Size = new System.Drawing.Size(107, 35);
            this.updateWidowListButton.TabIndex = 8;
            this.updateWidowListButton.Text = "リストを更新";
            this.updateWidowListButton.UseVisualStyleBackColor = true;
            this.updateWidowListButton.Click += new System.EventHandler(this.updateWidowListButton_Click);
            // 
            // windowListView
            // 
            this.windowListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.windowListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumnHeader,
            this.processNameColumnHeader});
            this.windowListView.FullRowSelect = true;
            this.windowListView.Location = new System.Drawing.Point(18, 242);
            this.windowListView.MultiSelect = false;
            this.windowListView.Name = "windowListView";
            this.windowListView.Size = new System.Drawing.Size(679, 150);
            this.windowListView.TabIndex = 5;
            this.windowListView.UseCompatibleStateImageBehavior = false;
            this.windowListView.View = System.Windows.Forms.View.Details;
            this.windowListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.windowListView_ItemSelectionChanged);
            // 
            // titleColumnHeader
            // 
            this.titleColumnHeader.Text = "タイトル";
            this.titleColumnHeader.Width = 200;
            // 
            // processNameColumnHeader
            // 
            this.processNameColumnHeader.Text = "プロセス名";
            this.processNameColumnHeader.Width = 300;
            // 
            // setAreaButton
            // 
            this.setAreaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.setAreaButton.Enabled = false;
            this.setAreaButton.Location = new System.Drawing.Point(468, 398);
            this.setAreaButton.Name = "setAreaButton";
            this.setAreaButton.Size = new System.Drawing.Size(116, 35);
            this.setAreaButton.TabIndex = 7;
            this.setAreaButton.Text = "読み取り位置を設定";
            this.setAreaButton.UseVisualStyleBackColor = true;
            this.setAreaButton.Click += new System.EventHandler(this.setAreaButton_Click);
            // 
            // ocrButton
            // 
            this.ocrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ocrButton.Enabled = false;
            this.ocrButton.Location = new System.Drawing.Point(18, 398);
            this.ocrButton.Name = "ocrButton";
            this.ocrButton.Size = new System.Drawing.Size(110, 35);
            this.ocrButton.TabIndex = 6;
            this.ocrButton.Text = "読み取りウィンドウ";
            this.ocrButton.UseVisualStyleBackColor = true;
            this.ocrButton.Click += new System.EventHandler(this.ocrButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.translationLangComboBox);
            this.groupBox1.Controls.Add(this.translationEngineComboBox);
            this.groupBox1.Controls.Add(this.ocrLangComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ocrComboBox);
            this.groupBox1.Location = new System.Drawing.Point(18, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(679, 183);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OCR設定";
            // 
            // translationLangComboBox
            // 
            this.translationLangComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationLangComboBox.FormattingEnabled = true;
            this.translationLangComboBox.Location = new System.Drawing.Point(130, 140);
            this.translationLangComboBox.Name = "translationLangComboBox";
            this.translationLangComboBox.Size = new System.Drawing.Size(528, 23);
            this.translationLangComboBox.TabIndex = 4;
            this.translationLangComboBox.SelectedIndexChanged += new System.EventHandler(this.translationLangComboBox_SelectedIndexChanged);
            // 
            // translationEngineComboBox
            // 
            this.translationEngineComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationEngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translationEngineComboBox.FormattingEnabled = true;
            this.translationEngineComboBox.Location = new System.Drawing.Point(130, 103);
            this.translationEngineComboBox.Name = "translationEngineComboBox";
            this.translationEngineComboBox.Size = new System.Drawing.Size(528, 23);
            this.translationEngineComboBox.TabIndex = 3;
            this.translationEngineComboBox.SelectedIndexChanged += new System.EventHandler(this.translationEngineComboBox_SelectedIndexChanged);
            // 
            // ocrLangComboBox
            // 
            this.ocrLangComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrLangComboBox.FormattingEnabled = true;
            this.ocrLangComboBox.Location = new System.Drawing.Point(130, 66);
            this.ocrLangComboBox.Name = "ocrLangComboBox";
            this.ocrLangComboBox.Size = new System.Drawing.Size(528, 23);
            this.ocrLangComboBox.TabIndex = 2;
            this.ocrLangComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrLangComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "OCRエンジン";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "翻訳エンジン";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "翻訳先の言語";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "読み取り元の言語";
            // 
            // ocrComboBox
            // 
            this.ocrComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocrComboBox.FormattingEnabled = true;
            this.ocrComboBox.Location = new System.Drawing.Point(130, 29);
            this.ocrComboBox.Name = "ocrComboBox";
            this.ocrComboBox.Size = new System.Drawing.Size(528, 23);
            this.ocrComboBox.TabIndex = 0;
            this.ocrComboBox.SelectedIndexChanged += new System.EventHandler(this.ocrComboBox_SelectedIndexChanged);
            // 
            // OcrPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ocrButton);
            this.Controls.Add(this.setAreaButton);
            this.Controls.Add(this.windowListView);
            this.Controls.Add(this.updateWidowListButton);
            this.Controls.Add(this.label1);
            this.Name = "OcrPage";
            this.Size = new System.Drawing.Size(700, 436);
            this.Load += new System.EventHandler(this.OcrPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button updateWidowListButton;
        private ListView windowListView;
        private ColumnHeader titleColumnHeader;
        private ColumnHeader processNameColumnHeader;
        private Button setAreaButton;
        private BindingSource bindingSource1;
        private Button ocrButton;
        private GroupBox groupBox1;
        private ComboBox ocrComboBox;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox ocrLangComboBox;
        private ComboBox translationEngineComboBox;
        private ComboBox translationLangComboBox;
    }
}
