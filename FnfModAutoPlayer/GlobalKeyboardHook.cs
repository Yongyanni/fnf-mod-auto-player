using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FnfModAutoPlayer
{
    public class GlobalKeyboardHook : IMessageFilter
    {
        public event KeyEventHandler KeyDownEvent;

        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID = 0x1234;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public GlobalKeyboardHook()
        {
            // 注册全局热键：空格键
            RegisterHotKey(IntPtr.Zero, HOTKEY_ID, 0, Keys.Space);

            // 让 WinForms 处理消息
            Application.AddMessageFilter(this);
        }

        ~GlobalKeyboardHook()
        {
            UnregisterHotKey(IntPtr.Zero, HOTKEY_ID);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                KeyDownEvent?.Invoke(this, new KeyEventArgs(Keys.Space));
                return true;
            }

            return false;
        }
    }
}

