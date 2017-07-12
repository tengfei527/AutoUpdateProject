namespace LY.Remote.Core
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class WinAPI
    {
        [DllImport("Gdi32.dll")]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, long lpInitData);
        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);
        [DllImport("user32")]
        public static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, CursorType cursor);
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, int data, UIntPtr extraInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr hCursor);
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern void ShowCursor(bool status);
    }
}

