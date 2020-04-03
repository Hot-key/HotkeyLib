using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace HotkeyLib
{
    public static class Window
    {
        [DllExport("HotkeyLib_Window_FindWindow", CallingConvention.StdCall)]
        public static IntPtr FindWindow(string lpClassName = null, string lpWindowName = null)
        {
            return Win32.FindWindow(lpClassName, lpWindowName);
        }

        [DllExport("HotkeyLib_Window_FindWindowEx", CallingConvention.StdCall)]
        public static IntPtr FindWindowEx(IntPtr hWnd1, string lpClassName = null, string lpWindowName = null)
        {
            return Win32.FindWindowEx(hWnd1, IntPtr.Zero, lpClassName, lpWindowName);
        }
    }

    public static class Input
    {

        [DllExport("HotkeyLib_Input_Click", CallingConvention.StdCall)]
        public static void Click(IntPtr hWnd, int x, int y, int delay = 0)
        {
            Win32.PostMessage(hWnd, (int)Win32.WMessages.WM_LBUTTONDOWN, 1, new IntPtr(y * 0x10000 + x));
            Thread.Sleep(delay);
            Win32.PostMessage(hWnd, (int)Win32.WMessages.WM_LBUTTONUP, 0, new IntPtr(y * 0x10000 + x));
        }
    }
}