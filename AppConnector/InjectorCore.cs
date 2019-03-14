using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AppConnector
{
    class InjectorCore
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        


        private static void LoadPayload(Process targetProcessIN, string DllPathIN, bool StartThread)
        {
            Form form = new Form();
            form.ShowInTaskbar = false;
            form.Opacity = 0.01;
            form.Size = new Size(2, 2);
            form.Show();
            form.Location = new Point(-20, -20);
            form.FormBorderStyle = FormBorderStyle.None;
            form.Opacity = 0.0;
            form.Invoke(new MethodInvoker(delegate ()
            {
                ThreadPool.QueueUserWorkItem((a) =>
                {
                    if (DllPathIN.Length * 2 > 1000)
                    {
                        throw new Exception("path is to long");
                    }
                    IntPtr intPtr = OpenProcess(1082u, 1, (uint)targetProcessIN.Id);
                    if (intPtr == (IntPtr)0)
                    {
                        Console.WriteLine("Fail on - attatch to process With (admin nedded??) code# - " + Marshal.GetLastWin32Error());
                        throw new Exception("Fail on - attatch to process With (admin nedded??) code# - " + Marshal.GetLastWin32Error());
                    }
                    IntPtr procAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    if (procAddress == (IntPtr)0)
                    {
                        Console.WriteLine("Fail on - get address of (LoadLibraryA) code# - " + Marshal.GetLastWin32Error());
                        throw new Exception("Fail on - get address of (LoadLibraryA) code# - " + Marshal.GetLastWin32Error());
                    }
                    IntPtr intPtr2 = VirtualAllocEx(intPtr, (IntPtr)null, (IntPtr)DllPathIN.Length, 12288u, 64u);
                    if (intPtr2 == (IntPtr)0 && intPtr2 == (IntPtr)0)
                    {
                        Console.WriteLine("Fail on - allocate memory in target process code# - " + Marshal.GetLastWin32Error());
                        throw new Exception("Fail on - allocate memory in target process code# - " + Marshal.GetLastWin32Error());
                    }
                    byte[] bytes = Encoding.ASCII.GetBytes(DllPathIN);
                    IntPtr zero = IntPtr.Zero;
                    WriteProcessMemory(intPtr, intPtr2, bytes, (uint)bytes.Length, out zero);
                    if (Marshal.GetLastWin32Error() != 0)
                    {
                        Console.WriteLine("Fail on - write memory in target process code# - " + Marshal.GetLastWin32Error());
                        throw new Exception("Fail on - write memory in target process code# - " + Marshal.GetLastWin32Error());
                    }

                    if (StartThread)
                    {
                        IntPtr value = CreateRemoteThread(intPtr, (IntPtr)null, (IntPtr)0, procAddress, intPtr2, 0u, (IntPtr)null);
                        if (value == (IntPtr)0)
                        {
                            Console.WriteLine("Fail on - load dll in target process code# - " + Marshal.GetLastWin32Error());
                            throw new Exception("Fail on - load dll in target process code# - " + Marshal.GetLastWin32Error());
                        }
                    }
                }, null);
            }));
            
            Thread.Sleep(300);
        }

        public bool DoInject(string nameOfAppIN, string nameOfDllIN, bool StartThread)
        {
            int pid_IN = -1;
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == nameOfAppIN)
                {
                    pid_IN = process.Id;
                    break;
                }
            }
            return DoInject(pid_IN, nameOfDllIN, StartThread);
        }

       public bool DoInject(int PID_IN, string nameOfDllIN, bool StartThread)
        {
            Console.WriteLine(string.Concat(new string[]
            {
                PID_IN.ToString(),
                " : ",
                nameOfDllIN,
                " : ",
                StartThread.ToString()
            }));
            int num = PID_IN;
            if (num == -1)
            {
                Console.WriteLine("Process Not Found");
                return false;
            }
            if (num == 0)
            {
                Console.WriteLine("don't inject PID-0");
                return false;
            }
            Process processById = Process.GetProcessById(num);
            try
            {
                InjectorCore.LoadPayload(processById, nameOfDllIN, StartThread);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DoInject(Process targetProcess, string nameOfDllIN, bool StartThread)
        {
            bool result;
            try
            {
                InjectorCore.LoadPayload(targetProcess, nameOfDllIN, StartThread);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
