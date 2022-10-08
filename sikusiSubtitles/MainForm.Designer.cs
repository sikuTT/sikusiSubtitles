namespace sikusiSubtitles {
    partial class MainForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuView = new System.Windows.Forms.TreeView();
            this.versionLabel = new System.Windows.Forms.Label();
            this.topLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.recognitionResultTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.menuView);
            this.splitContainer1.Size = new System.Drawing.Size(950, 481);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.TabStop = false;
            // 
            // menuView
            // 
            this.menuView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuView.Font = new System.Drawing.Font("Yu Gothic UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuView.Location = new System.Drawing.Point(0, 0);
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(316, 481);
            this.menuView.TabIndex = 1;
            this.menuView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.menuView_AfterSelect);
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(912, 12);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(52, 15);
            this.versionLabel.TabIndex = 6;
            this.versionLabel.Text = "Ver. 0.4.4";
            // 
            // topLayoutPanel
            // 
            this.topLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topLayoutPanel.Location = new System.Drawing.Point(12, 4);
            this.topLayoutPanel.Name = "topLayoutPanel";
            this.topLayoutPanel.Size = new System.Drawing.Size(894, 37);
            this.topLayoutPanel.TabIndex = 0;
            // 
            // recognitionResultTextBox
            // 
            this.recognitionResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recognitionResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.recognitionResultTextBox.Location = new System.Drawing.Point(12, 534);
            this.recognitionResultTextBox.Multiline = true;
            this.recognitionResultTextBox.Name = "recognitionResultTextBox";
            this.recognitionResultTextBox.ReadOnly = true;
            this.recognitionResultTextBox.Size = new System.Drawing.Size(950, 52);
            this.recognitionResultTextBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 598);
            this.Controls.Add(this.topLayoutPanel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.recognitionResultTextBox);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "sikusiSubtitles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private SplitContainer splitContainer1;
        private TreeView menuView;
        private Label versionLabel;
        private FlowLayoutPanel topLayoutPanel;
        private TextBox recognitionResultTextBox;
    }
}