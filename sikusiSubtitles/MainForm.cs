using System.Diagnostics;
using System.Reflection;

namespace sikusiSubtitles {
    public partial class MainForm : Form {
        ServiceManager serviceManager = new ServiceManager();

        public MainForm() {
            InitializeComponent();

            LoadPlugins();

            // Init all services
            this.serviceManager.Init();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.serviceManager.Managers.ForEach(service => {
                var page = service.GetSettingPage();
                // �T�[�r�X�ɐݒ�y�[�W�����݂���ꍇ�A�t�H�[���ɐݒ�y�[�W��ǉ�����
                // �c���[�r���[�ɐݒ�y�[�W��\�����郁�j���[���쐬
                var node = new TreeNode(service.DisplayName) { Name = service.Name };
                this.menuView.Nodes.Add(node);

                if (page != null) {
                    // �ݒ�y�[�W���쐬����
                    page.Name = service.Name;
                    page.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(page);
                }
            });

            this.serviceManager.Services.ForEach(service => {
                var page = service.GetSettingPage();
                // �T�[�r�X�ɐݒ�y�[�W�����݂���ꍇ�A�t�H�[���ɐݒ�y�[�W��ǉ�����
                if (page != null) {
                    // �e�̃��j���[���擾
                    var parentNodes = menuView.Nodes.Find(service.ServiceName, false);
                    if (parentNodes.Length > 0 && parentNodes[0].Name != service.Name) {
                        var node = new TreeNode(service.DisplayName) { Name = service.Name };
                        parentNodes[0].Nodes.Add(node);
                    }

                    // �ݒ�y�[�W���쐬����
                    page.Name = service.Name;
                    page.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(page);
                }
            });

            this.menuView.ExpandAll();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            this.serviceManager.Finish();
            Properties.Settings.Default.Save();
        }

        private void menuView_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node == null)
                return;

            if (SelectNode(e.Node) == false) {
                if (e.Node.Nodes.Count > 0) {
                    SelectNode(e.Node.Nodes[0]);
                }
            }
        }

        private bool SelectNode(TreeNode node) {
            if (node != null) {
                var controls = this.splitContainer1.Panel2.Controls.Find(node.Name, false);
                var page = controls.Length > 0 ? controls[0] as UserControl : null;
                if (page != null) {
                    this.ShowChildPage(page);
                    return true;
                }
            }
            return false;
        }

        private void ShowChildPage(UserControl view) {
            foreach (var control in this.splitContainer1.Panel2.Controls) {
                var page = control as UserControl;
                if (page != null) {
                    page.Visible = view == control;
                }
            }
        }

        private string GetPluginPath() {
            return System.Windows.Forms.Application.StartupPath + "Plug-ins";
        }

        private void LoadPlugins() {
            List<Type> typeList = new List<Type>();
            var path = GetPluginPath();
            foreach (var file in Directory.EnumerateFiles(path, "*.dll")) {
                try {
                    var asm = Assembly.LoadFrom(file);
                    foreach (Type type in asm.GetTypes()) {
                        var baseType = type.BaseType;
                        while (baseType != null) {
                            if (baseType == typeof(Service)) {
                                typeList.Add(type);
                            }
                            baseType = baseType.BaseType;
                        }
                    }
                } catch (Exception ex) {
                    Debug.WriteLine("MainForm: Load DLL failed: " + ex.Message);
                }
            }

            int typeCount;
            do {
                typeCount = typeList.Count;
                for (var i = 0; i < typeList.Count; i++) {
                    try {
                        var type = typeList[i];
                        var service = Activator.CreateInstance(type, new object[] { serviceManager }) as Service;
                        if (service != null) {
                            typeList.RemoveAt(i--);
                        }
                    } catch (Exception) {
                    }
                }
            } while (typeCount != typeList.Count);
        }
    }
}