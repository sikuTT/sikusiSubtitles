using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class ScreenCapture {
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public RECT() { }

            public int left = 0;
            public int top = 0;
            public int right = 0;
            public int bottom = 0;
        }

        [DllImport("user32.dll")]
        public static extern uint GetDpiForWindow(IntPtr hwnd);

        delegate bool delegateEnumThreadWndProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(int dwThreadId, delegateEnumThreadWndProc lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern IntPtr GetParent(IntPtr hWnd);

        static public bool GetRect(int processId, IntPtr hwnd, out RECT rect) {
            bool result = false;
            int width = 0, height = 0;
            if (GetWindowRect(hwnd, out rect)) {
                width = rect.right - rect.left;
                height = rect.bottom - rect.top;
            }

            if (width == 0 || height == 0) {
                // メインウィンドウのサイズが取得できない場合、プロセスの全ウィンドウをチェックする
                Process process = Process.GetProcessById(processId);
                RECT r = new RECT();
                foreach (ProcessThread thread in process.Threads) {
                    EnumThreadWindows(
                        thread.Id,
                        (hWnd, lParam) => {
                            IntPtr parent = GetParent(hWnd);
                            if (parent == IntPtr.Zero) {
                                if (GetWindowRect(hWnd, out r)) {
                                    width = r.right - r.left;
                                    height = r.bottom - r.top;
                                    if (width != 0 && height != 0) {
                                        result = true;
                                        return false;
                                    }
                                }
                            }
                            return true;
                        },
                        IntPtr.Zero
                    );
                    if (result == true) {
                        rect = r;
                        break;
                    }
                }
            } else {
                result = true;
            }

            return result;
        }
    }
}