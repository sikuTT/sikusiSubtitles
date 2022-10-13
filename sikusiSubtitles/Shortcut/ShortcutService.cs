using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace sikusiSubtitles.Shortcut {
    public class ShortcutService : sikusiSubtitles.Service {
        private struct KBDLLHOOKSTRUCT {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        delegate IntPtr delegateHookCallback(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        delegateHookCallback handler;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, delegateHookCallback lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern short GetKeyState(int nVirtKey);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern short GetAsyncKeyState(int nVirtKey);

        IntPtr hookPtr = IntPtr.Zero;

        // ショートカットに使用できるキーの一覧とキーの名前
        string?[] keyNames = new string[256];

        // 現在同時に入力されているキーの一覧
        List<int> keys = new List<int>();

        // ショートカット一覧
        public List<Shortcut> Shortcuts { get; set; }

        public event EventHandler<Shortcut>? ShortcutRun;

        // 
        DateTime dateTime = DateTime.Now;

        public ShortcutService(ServiceManager serviceManager) : base(serviceManager, ShortcutServiceManager.ServiceName, "Shortcut", "ショートカット", 500) {
            handler = HookCallback;
            Shortcuts = new List<Shortcut>();

            CreateKeyNames();

            // 設定ページ
            settingsPage = new ShortcutPage(ServiceManager, this);
        }

        public override void Init() {
            Start();
        }

        public void Start() {
            const int WH_KEYBOARD_LL = 13;

            using (var process = Process.GetCurrentProcess())
            using (var module = process.MainModule) {
                // フックを行う
                // 第1引数   フックするイベントの種類
                //   13はキーボードフックを表す
                // 第2引数 フック時のメソッドのアドレス
                //   フックメソッドを登録する
                // 第3引数   インスタンスハンドル
                //   現在実行中のハンドルを渡す
                // 第4引数   スレッドID
                //   0を指定すると、すべてのスレッドでフックされる
                var moduleName = module?.ModuleName;
                if (moduleName != null) {
                    hookPtr = SetWindowsHookEx(
                        WH_KEYBOARD_LL,
                        handler,
                        GetModuleHandle(moduleName),
                        0
                    );
                }
            }
        }

        public override void Finish() {
            UnhookWindowsHookEx(hookPtr);
            hookPtr = IntPtr.Zero;
        }

        public string CreateShortcutText(List<int> keys) {
            StringBuilder text = new StringBuilder();
            foreach (var key in keys) {
                if (text.Length > 0) {
                    text.Append(" + ");
                }
                text.Append(keyNames[key]);
            }
            return text.ToString();
        }

        private IntPtr HookCallback(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam) {
            if (nCode < 0) {
                return CallNextHookEx(hookPtr, nCode, wParam, ref lParam);
            }

            try {
                // キーの入力判定が残るときがあるので、一定時間入力がなければクリアしてごまかす
                TimeSpan span = DateTime.Now - dateTime;
                if (span.TotalMilliseconds > 1500) keys.Clear();
                dateTime = DateTime.Now;


                var push = ((int)wParam & 0x00000001) == 0;
                var keyCode = lParam.vkCode;
                if (keyCode < keyNames.Length) {
                    var keyName = keyNames[keyCode];
                    if (keyName != null) {
                        if (push) {
                            if (!this.keys.Contains(keyCode)) {
                                this.keys.Add(keyCode);

                                bool shortcutInvoked = false;
                                string text = CreateShortcutText(keys);
                                foreach (var shortcut in Shortcuts) {
                                    if (text == shortcut.ShortcutKey) {
                                        this.ShortcutRun?.Invoke(this, shortcut);
                                        shortcutInvoked = true;
                                        break;
                                    }
                                }
                                if (shortcutInvoked == false) {
                                    this.ShortcutRun?.Invoke(this, new Shortcut("", "", "", text));
                                }
                            }
                        } else {
                            if (this.keys.Contains(keyCode)) {
                                this.keys.Remove(keyCode);
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("ShortcutService.HookCallback: ", ex.Message);
            }

            return CallNextHookEx(hookPtr, nCode, wParam, ref lParam);
        }

        void CreateKeyNames() {
            for (int i = (int)Keys.D0; i <= (int)Keys.D9; i++) {
                keyNames[i] = (i - (int)Keys.D0).ToString();
            }

            for (int i = (int)Keys.A; i <= (int)Keys.Z; i++) {
                keyNames[i] = ((Keys)i).ToString();
            }

            for (int i = (int)Keys.F1; i <= (int)Keys.F24; i++) {
                keyNames[i] = ((Keys)i).ToString();
            }

            for (int i = (int)Keys.Space; i <= (int)Keys.Help; i++) {
                keyNames[i] = ((Keys)i).ToString();
            }

            keyNames[(int)Keys.ShiftKey] = "SHIFT";
            keyNames[(int)Keys.LShiftKey] = "SHIFT";
            keyNames[(int)Keys.RShiftKey] = "SHIFT";
            keyNames[(int)Keys.ControlKey] = "CTRL";
            keyNames[(int)Keys.LControlKey] = "CTRL";
            keyNames[(int)Keys.RControlKey] = "CTRL";
            keyNames[(int)Keys.Menu] = "ALT";
            keyNames[(int)Keys.LMenu] = "ALT";
            keyNames[(int)Keys.RMenu] = "ALT";
            keyNames[(int)Keys.LWin] = "WIN";
            keyNames[(int)Keys.RWin] = "WIN";
            keyNames[(int)Keys.RWin] = "WIN";
            keyNames[(int)Keys.OemMinus] = "-";
            keyNames[(int)Keys.Oem7] = "^";
            keyNames[(int)Keys.Oem5] = "\\";
            keyNames[(int)Keys.Oem3] = "@";
            keyNames[(int)Keys.Oem4] = "[";
            keyNames[(int)Keys.Oemplus] = ";";
            keyNames[(int)Keys.Oem1] = ":";
            keyNames[(int)Keys.Oem6] = "]";
            keyNames[(int)Keys.Oemcomma] = ",";
            keyNames[(int)Keys.OemPeriod] = ".";
            keyNames[(int)Keys.OemQuestion] = "/";
            keyNames[(int)Keys.Oem102] = "\\";

            keyNames[(int)Keys.Back] = "Backspace";
            keyNames[(int)Keys.Enter] = "Enter";
            keyNames[(int)Keys.Tab] = "Tab";
            keyNames[(int)Keys.Escape] = "Escape";

            for (int i = (int)Keys.NumPad0; i <= (int)Keys.NumPad9; i++) {
                keyNames[i] = ((Keys)i).ToString();
            }
            keyNames[(int)Keys.Multiply] = "NumPad*";
            keyNames[(int)Keys.Add] = "NumPad+";
            keyNames[(int)Keys.Subtract] = "NumPad-";
            keyNames[(int)Keys.Decimal] = "NumPad.";
            keyNames[(int)Keys.Divide] = "NumPad/";
        }
    }

    public class Shortcut {
        public string Name { get; set; } = "";
        public string Source { get; set; } = "";
        public string Command { get; set; } = "";
        public string ShortcutKey { get; set; } = "";

        public Shortcut() {}
 
        public Shortcut(string name, string source, string command, string shortcutKey) {
            Name = name;
            Source = source;
            Command = command;
            ShortcutKey = shortcutKey;
        }
    }
}