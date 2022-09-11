using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Shortcut {
    public partial class ShortcutPage : SettingPage {
        ShortcutService service;

        List<int> keys = new List<int>();

        public override void Unload() {
            this.service.End();
        }

        public ShortcutPage(ServiceManager serviceManager, ShortcutService service) : base(serviceManager) {
            this.service = service;
            service.Start();

            InitializeComponent();
        }

        private void ShortcutPage_Load(object sender, EventArgs e) {
            foreach (var shortcut in service.Shortcuts) {
                ListViewItem item = new ListViewItem(new String[] { shortcut.Command, shortcut.ShortcutKey, shortcut.Source });
                this.shortcutListView.Items.Add(item);
            }
        }

        private void shortcutListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.IsSelected) {
                this.shortcutTextBox.Text = "";
                this.shortcutTextBox.Enabled = true;
                this.setShortcutButton.Enabled = true;
            } else {
                this.shortcutTextBox.Text = "";
                this.shortcutTextBox.Enabled = false;
                this.setShortcutButton.Enabled = false;
            }
        }

        private void shortcutTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (this.keys.Contains(e.KeyValue) == false) {
                this.keys.Add(e.KeyValue);
                this.shortcutTextBox.Text = service.CreateShortcutText(this.keys);
            }
        }

        private void shortcutTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (this.keys.Contains(e.KeyValue)) {
                this.keys.Remove(e.KeyValue);
            }
        }

        private void setShortcutButton_Click(object sender, EventArgs e) {
            // ショートカットをリストビューに反映
            ListViewItem item = this.shortcutListView.SelectedItems[0];
            item.SubItems[1].Text = this.shortcutTextBox.Text;

            // ショートカットをショートカットサービスに反映
            if (this.shortcutListView.SelectedIndices.Count > 0) {
                var i = this.shortcutListView.SelectedIndices[0];
                service.Shortcuts[i].ShortcutKey = this.shortcutTextBox.Text;
            }
        }
    }
}
