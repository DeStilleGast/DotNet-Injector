using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNet_Injector.Targeter
{
    public class Win32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            #region Helper methods

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }

            #endregion
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        public static string GetWindowText(IntPtr hWnd)
        {
            StringBuilder text = new StringBuilder(256);
            if (GetWindowText(hWnd.ToInt32(), text, text.Capacity) > 0)
            {
                return text.ToString();
            }

            return String.Empty;
        }


        [DllImport("user32", EntryPoint = "GetWindowThreadProcessId", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);

        public static string GetApplicationName(IntPtr hWnd)
        {
            Process proc = GetProcess(hWnd);
            try
            {
                if (proc == null) return "{UNKNOWN}";
                return proc.MainModule.ModuleName;
            }
            catch
            {
                return "{Protected ?}";
            }
        }

        public static Process GetProcess(IntPtr hWnd)
        {
            int procId = new int();
            GetWindowThreadProcessId(hWnd, ref procId);
            try
            {
                return Process.GetProcessById(procId);
            }
            catch
            {
                return null;
            }
        }





        private delegate bool EnumWindowsCallBackDelegate(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.Dll")]
        private static extern int EnumWindows(EnumWindowsCallBackDelegate callback, IntPtr lParam);



        public static IntPtr[] ToplevelWindows
        {
            get
            {
                List<IntPtr> windowList = new List<IntPtr>();
                GCHandle handle = GCHandle.Alloc(windowList);
                try
                {
                    EnumWindows(EnumWindowsCallback, (IntPtr)handle);
                }
                finally
                {
                    handle.Free();
                }

                return windowList.ToArray();

            }
        }

        private static bool EnumWindowsCallback(IntPtr hwnd, IntPtr lParam)
        {
            ((List<IntPtr>)((GCHandle)lParam).Target).Add(hwnd);
            return true;
        }

        //
        //        const int DSTINVERT = 0x00550009;
        //
        //        [DllImport("gdi32.dll")]
        //        public static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, uint dwRop);
        //
        //
        //        [StructLayout(LayoutKind.Sequential)]
        //        public struct RECT
        //        {
        //            public int Left;
        //            public int Top;
        //            public int Right;
        //            public int Bottom;
        //        }
        //
        //        [DllImport("user32.dll")]
        //        static extern IntPtr GetWindowDC(IntPtr hWnd);
        //
        //        [DllImport("user32.dll")]
        //        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        //
        //        [DllImport("user32.dll")]
        //        static extern bool OffsetRect(ref RECT lprc, int dx, int dy);
        //
        //        public static void DrawRevFrame(IntPtr hWnd)
        //        {
        //            if (hWnd == IntPtr.Zero)
        //                return;
        //
        //            IntPtr hdc = GetWindowDC(hWnd);
        //            RECT rect;
        //            GetWindowRect(hWnd, out rect);
        //            OffsetRect(ref rect, -rect.Left, -rect.Top);
        //
        //            const int frameWidth = 3;
        //
        //            PatBlt(hdc, rect.Left, rect.Top, rect.Right - rect.Left, frameWidth, DSTINVERT);
        //            PatBlt(hdc, rect.Left, rect.Bottom - frameWidth, frameWidth,
        //                -(rect.Bottom - rect.Top - 2 * frameWidth), DSTINVERT);
        //            PatBlt(hdc, rect.Right - frameWidth, rect.Top + frameWidth, frameWidth,
        //                rect.Bottom - rect.Top - 2 * frameWidth, DSTINVERT);
        //            PatBlt(hdc, rect.Right, rect.Bottom - frameWidth, -(rect.Right - rect.Left),
        //                frameWidth, DSTINVERT);
        //        }


        [DllImport("user32", EntryPoint = "SetCapture", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        public static string GetClassName(IntPtr hWnd)
        {
            StringBuilder className = new StringBuilder(100);
            if (GetClassName(hWnd, className, className.Capacity) > 0)
            {
                return className.ToString();
            }

            return string.Empty;
        }

        [DllImport("user32", EntryPoint = "IsWindow", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int IsWindow(IntPtr hWnd);

        /// <summary>
        /// Determines whether the two windows are related.
        /// </summary>
        internal static bool IsRelativeWindow(IntPtr hWnd, IntPtr hRelativeWindow, bool bProcessAncestor)
        {
            int dwProcess = new int(), dwProcessOwner = new int();
            int dwThread = new int(), dwThreadOwner = new int();
            ;

            // Failsafe
            if (hWnd == IntPtr.Zero)
                return false;
            if (hRelativeWindow == IntPtr.Zero)
                return false;
            if (hWnd == hRelativeWindow)
                return true;

            // Get processes and threads
            dwThread = GetWindowThreadProcessId(hWnd, ref dwProcess);
            dwThreadOwner = GetWindowThreadProcessId(hRelativeWindow, ref dwProcessOwner);

            // Get relative info
            if (bProcessAncestor)
                return (dwProcess == dwProcessOwner);
            return (dwThread == dwThreadOwner);
        }



        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

        public static bool IsWin32Process(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
            {
                if ((Environment.OSVersion.Version.Major > 5)
                    || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
                {
                    bool retVal;
                    try
                    {
                        return IsWow64Process(GetProcess(handle).Handle, out retVal) && retVal;
                    }
                    catch
                    {

                    }
                }

                return false; // not on 64-bit Windows Emulator
            }
            return false;
        }

//        public Process GetProcess(IntPtr handle)
//        {
//            Win32.GetWindowThreadProcessId(handle, ref procId = new int());
//            return Process.GetProcessById(procId);
//        }
    }
}
