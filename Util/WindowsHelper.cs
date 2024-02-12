using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MineMinderX.Process;

namespace MineMinderX.Util
{
    public static class WindowsHelper
    {
        public static Bitmap PrintWindow(IntPtr hWnd) {
            var rect = new ProcessData.Rect();
            GetWindowRect(hWnd, ref rect);
            var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            
            using var gfxBmp = Graphics.FromImage(bmp);        
            var hdcBitmap = gfxBmp.GetHdc();
            
            PrintWindow(hWnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            return bmp;
        }
        
        public static Bitmap PrintClientWindow(IntPtr hWnd) {
            var windowRect = new ProcessData.Rect();
            var clientRect = new ProcessData.Rect();
            GetWindowRect(hWnd, ref windowRect);
            GetClientRect(hWnd, ref clientRect);
            
            var bmp = PrintWindow(hWnd);
            
            var borderSize = (windowRect.Width - clientRect.Width) / 2;
            var titleBarSize = (windowRect.Height - clientRect.Height) - borderSize;
            var width = bmp.Width - 2 * borderSize;
            var height = bmp.Height - titleBarSize - borderSize;
            var clientArea = new Rectangle(borderSize, titleBarSize, width, height);

            return bmp.Clone(clientArea, PixelFormat.Format32bppRgb);
        }
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref ProcessData.Rect rect);
        [DllImport("user32.dll")]
        public static extern IntPtr GetClientRect(IntPtr hWnd, ref ProcessData.Rect rect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}