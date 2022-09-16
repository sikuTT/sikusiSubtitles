namespace sikusiSubtitles.Shortcut {
    partial class ShortcutPage {
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
            this.shortcutListView = new System.Windows.Forms.ListView();
            this.commandColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.keyColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.sourceColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.shortcutTextBox = new System.Windows.Forms.TextBox();
            this.setShortcutButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 37);
            this.label1.TabIndex = 6;
            this.label1.Text = "ショートカット";
            // 
            // shortcutListView
            // 
            this.shortcutListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.commandColumnHeader,
            this.keyColumnHeader,
            this.sourceColumnHeader});
            this.shortcutListView.FullRowSelect = true;
            this.shortcutListView.GridLines = true;
            this.shortcutListView.Location = new System.Drawing.Point(3, 58);
            this.shortcutListView.MultiSelect = false;
            this.shortcutListView.Name = "shortcutListView";
            this.shortcutListView.Size = new System.Drawing.Size(537, 232);
            this.shortcutListView.TabIndex = 7;
            this.shortcutListView.UseCompatibleStateImageBehavior = false;
            this.shortcutListView.View = System.Windows.Forms.View.Details;
            this.shortcutListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.shortcutListView_ItemSelectionChanged);
            // 
            // commandColumnHeader
            // 
            this.commandColumnHeader.Text = "機能";
            this.commandColumnHeader.Width = 200;
            // 
            // keyColumnHeader
            // 
            this.keyColumnHeader.Text = "キー";
            this.keyColumnHeader.Width = 200;
            // 
            // sourceColumnHeader
            // 
            this.sourceColumnHeader.Text = "ソース";
            this.sourceColumnHeader.Width = 100;
            // 
            // shortcutTextBox
            // 
            this.shortcutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.shortcutTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shortcutTextBox.Enabled = false;
            this.shortcutTextBox.Location = new System.Drawing.Point(3, 296);
            this.shortcutTextBox.Name = "shortcutTextBox";
            this.shortcutTextBox.ReadOnly = true;
            this.shortcutTextBox.Size = new System.Drawing.Size(422, 23);
            this.shortcutTextBox.TabIndex = 8;
            this.shortcutTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.shortcutTextBox_KeyDown);
            this.shortcutTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.shortcutTextBox_KeyUp);
            // 
            // setShortcutButton
            // 
            this.setShortcutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.setShortcutButton.Enabled = false;
            this.setShortcutButton.Location = new System.Drawing.Point(431, 296);
            this.setShortcutButton.Name = "setShortcutButton";
            this.setShortcutButton.Size = new System.Drawing.Size(109, 23);
            this.setShortcutButton.TabIndex = 9;
            this.setShortcutButton.Text = "ショートカット設定";
            this.setShortcutButton.UseVisualStyleBackColor = true;
            this.setShortcutButton.Click += new System.EventHandler(this.setShortcutButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(238, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(302, 16);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ShortcutPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.setShortcutButton);
            this.Controls.Add(this.shortcutTextBox);
            this.Controls.Add(this.shortcutListView);
            this.Controls.Add(this.label1);
            this.Name = "ShortcutPage";
            this.Size = new System.Drawing.Size(543, 331);
            this.Load += new System.EventHandler(this.ShortcutPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private ListView shortcutListView;
        private ColumnHeader commandColumnHeader;
        private ColumnHeader keyColumnHeader;
        private TextBox shortcutTextBox;
        private Button setShortcutButton;
        private ColumnHeader sourceColumnHeader;
        private TextBox textBox1;
    }
}
