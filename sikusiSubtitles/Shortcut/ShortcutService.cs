using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Shortcut {
    public class ShortcutService : sikusiSubtitles.Service {
        delegate IntPtr delegateHookCallback(int nCode, IntPtr wParam, IntPtr lParam);

        delegateHookCallback handler;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, delegateHookCallback lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern short GetKeyState(int nVirtKey);

        IntPtr hookPtr = IntPtr.Zero;

        // 現在同時に入力されているキーの一覧
        List<int> keys = new List<int>();

        // ショートカット一覧
        public List<Shortcut> Shortcuts { get; set; }

        public event EventHandler<Shortcut>? ShortcutRun;

        public ShortcutService(ServiceManager serviceManager) : base(serviceManager, ShortcutServiceManager.ServiceName, "Shortcut", "ショートカット", 500) {
            SettingPage = new ShortcutPage(serviceManager, this);
            handler = HookCallback;
            Shortcuts = new List<Shortcut>();
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
            string text = "";
            foreach (var key in keys) {
                if (text.Length > 0) {
                    text += " + ";
                }
                if (key == 16) {
                    text += "SHIFT";
                } else if (key == 17) {
                    text += "CTRL";
                } else if (key == 18) {
                    text += "ALT";
                } else if ((key >= 48 && key <= 57)) {
                    text += (char)key;
                } else {
                    text += (Keys)key;
                }
            }
            return text;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
            if (nCode < 0) {
                return CallNextHookEx(hookPtr, nCode, wParam, lParam);
            }

            try {
                var push = ((int)wParam & 0x00000001) == 0;
                var keyCode = (short)Marshal.ReadInt32(lParam);
                if (keyCode >= 240) {
                    // キーを話した時のイベントがこないので処理しない
                    return CallNextHookEx(hookPtr, nCode, wParam, lParam);
                } else if (keyCode == 160 || keyCode == 161) {
                    // LShiftKey, RShiftKeyはShiftKeyにする
                    keyCode = 16;
                } else if (keyCode == 162 || keyCode == 163) {
                    // LControlKey, RControlKeyはControlKeyにする。
                    keyCode = 17;
                } else if (keyCode == 164 || keyCode == 165) {
                    // LMenu, RMenuはMenuにする。
                    keyCode = 18;
                }
                if (push) {
                    if (!this.keys.Contains(keyCode)) {
                        this.keys.Add(keyCode);

                        string text = CreateShortcutText(keys);
                        this.ShortcutRun?.Invoke(this, new Shortcut("", "", "", text));
                    }
                } else {
                    if (this.keys.Contains(keyCode)) {
                        this.keys.Remove(keyCode);
                    }
                }

                if (this.keys.Count > 0) {
                    string text = CreateShortcutText(keys);
                    foreach (var shortcut in Shortcuts) {
                        if (text == shortcut.ShortcutKey) {
                            this.ShortcutRun?.Invoke(this, shortcut);
                            this.keys.Clear();
                            break;
                        }
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("ShortcutService.HookCallback: ", ex.Message);
            }

            return CallNextHookEx(hookPtr, nCode, wParam, lParam);
        }
    }

    public class Shortcut {
        public string Name { get; set; }
        public string Source { get; set; }
        public string Command { get; set; }
        public string ShortcutKey { get; set; }

        public Shortcut(string name, string source, string command, string shortcutKey) {
            Name = name;
            Source = source;
            Command = command;
            ShortcutKey = shortcutKey;
        }
    }
}
