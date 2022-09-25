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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ocrButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
            this.windowListView.Location = new System.Drawing.Point(18, 55);
            this.windowListView.MultiSelect = false;
            this.windowListView.Name = "windowListView";
            this.windowListView.Size = new System.Drawing.Size(679, 337);
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
            // ocrButton
            // 
            this.ocrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ocrButton.Enabled = false;
            this.ocrButton.Location = new System.Drawing.Point(474, 398);
            this.ocrButton.Name = "ocrButton";
            this.ocrButton.Size = new System.Drawing.Size(110, 35);
            this.ocrButton.TabIndex = 6;
            this.ocrButton.Text = "読み取りウィンドウ";
            this.ocrButton.UseVisualStyleBackColor = true;
            this.ocrButton.Click += new System.EventHandler(this.ocrButton_Click);
            // 
            // OcrPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.ocrButton);
            this.Controls.Add(this.windowListView);
            this.Controls.Add(this.updateWidowListButton);
            this.Controls.Add(this.label1);
            this.Name = "OcrPage";
            this.Size = new System.Drawing.Size(700, 436);
            this.Load += new System.EventHandler(this.OcrPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button updateWidowListButton;
        private ListView windowListView;
        private ColumnHeader titleColumnHeader;
        private ColumnHeader processNameColumnHeader;
        private BindingSource bindingSource1;
        private Button ocrButton;
    }
}
