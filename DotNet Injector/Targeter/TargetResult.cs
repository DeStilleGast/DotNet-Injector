using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotNet_Injector.Targeter
{
    public class TargetResult
    {
        public IntPtr Handle;
        public string WindowText;
        public string Application;
        public string WindowClass;

        public TargetResult(IntPtr handle) {
            if ((Win32.IsWindow(handle) == 0) || (Win32.IsRelativeWindow(handle, this.Handle, true))) { }
            else {

                Handle = handle;

//            windowHandle = handle;
//            windowHandleText = Convert.ToString(handle.ToInt32(), 16).ToUpper().PadLeft(8, '0');
                WindowClass = Win32.GetClassName(handle);
                WindowText = Win32.GetWindowText(handle);
            }
        }
        
        public void Reset()
        {
            this.Handle = IntPtr.Zero;
            this.WindowText = null;
            this.Application = null;
        }
        
        public bool IsWin32Process()
        {
            return Win32.IsWin32Process(Handle);
        }

        public bool IsManaged()
        {
            try {
                var process = Win32.GetProcess(Handle);
                if (process.Modules != null)
                {
                    foreach (object obj in process.Modules)
                    {
                        ProcessModule processModule = (ProcessModule)obj;
                        if (processModule.ModuleName.StartsWith("mscorlib", StringComparison.InvariantCultureIgnoreCase) || processModule.ModuleName.Equals("mscorlib.ni.dll", StringComparison.InvariantCultureIgnoreCase))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch
            {
            }

            return false;
        }

        public Process GetProcess() {
            return Win32.GetProcess(Handle);
        }

        public string getApplicationFilename() {
            return Win32.GetApplicationName(Handle);
        }
    }
}
