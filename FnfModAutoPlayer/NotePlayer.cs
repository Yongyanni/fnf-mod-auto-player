using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FnfModAutoPlayer
{
    public static class NotePlayer
    {
        // SendInput 结构体
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)]
            public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        const uint INPUT_KEYBOARD = 1;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_SCANCODE = 0x0008;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        // ⭐ Psych Engine 方向键必须带扩展位（E0）
        private static ushort MapDirectionToScanCode(int dir)
        {
            return dir switch
            {
                0 => 0x4B, // Left Arrow
                1 => 0x50, // Down Arrow
                2 => 0x48, // Up Arrow
                3 => 0x4D, // Right Arrow

                // WASD（非扩展键）
                4 => 0x1E, // A
                5 => 0x1F, // S
                6 => 0x11, // W
                7 => 0x20, // D

                _ => 0
            };
        }

        private static bool IsArrowKey(int dir)
        {
            return dir >= 0 && dir <= 3;
        }

        private static void PressKey(ushort scanCode, bool extended)
        {
            uint flags = KEYEVENTF_SCANCODE;
            if (extended) flags |= KEYEVENTF_EXTENDEDKEY;

            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wScan = scanCode,
                            dwFlags = flags
                        }
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        private static void ReleaseKey(ushort scanCode, bool extended)
        {
            uint flags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP;
            if (extended) flags |= KEYEVENTF_EXTENDEDKEY;

            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wScan = scanCode,
                            dwFlags = flags
                        }
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        // ⭐ 最终稳定版播放逻辑
        public static async Task PlayAsync(List<NoteInfo> notes)
        {
            if (notes == null || notes.Count == 0)
                return;

            double startTime = notes[0].Time;
            var sw = Stopwatch.StartNew();

            foreach (var n in notes)
            {
                ushort scanCode = MapDirectionToScanCode(n.Direction);
                if (scanCode == 0)
                    continue;

                bool extended = IsArrowKey(n.Direction);

                double target = n.Time - startTime;
                if (target < 0) target = 0;

                double now = sw.Elapsed.TotalMilliseconds;
                double wait = target - now;

                if (wait > 0)
                    await Task.Delay((int)wait);

                // Hold note
                if (n.Length > 0)
                {
                    PressKey(scanCode, extended);
                    await Task.Delay((int)n.Length);
                    ReleaseKey(scanCode, extended);
                }
                else
                {
                    // Tap note
                    PressKey(scanCode, extended);
                    await Task.Delay(50);
                    ReleaseKey(scanCode, extended);
                }
            }
        }
    }
}
