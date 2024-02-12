using System;
using System.Runtime.InteropServices;

namespace MineMinderX.Process
{
    public class ProcessData
    {
        public string Name { get; }
        public IntPtr Hwnd { get; }
        public Rect WindowRect { get; }

        public Rect BoardRect =>
            new()
            {
                Left = WindowRect.Left + 15,
                Right = WindowRect.Right - 11,
                Top = WindowRect.Top + 100,
                Bottom = WindowRect.Bottom - 12
            };

        public ProcessData(string name, IntPtr hWnd, Rect windowRect) {
            Name = name;
            Hwnd = hWnd;
            WindowRect = windowRect;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }
    }
}