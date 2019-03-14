using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AppConnector
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().Run(args);
        }

        public void Run(string[] args)
        {
            if (args.Length >= 1)
            {
                if (args[0].Equals("list"))
                {
                    var res = FindEveryDotNetProces();
                    res[0].ForEach(r => { Console.WriteLine($"{r.Id}|{r.ProcessName}|32|M"); });
                    res[1].ForEach(r => { Console.WriteLine($"{r.Id}|{r.ProcessName}|64|M"); });
                    res[2].ForEach(r => { Console.WriteLine($"{r.Id}|{r.ProcessName}|32|U"); });
                    res[3].ForEach(r => { Console.WriteLine($"{r.Id}|{r.ProcessName}|64|U"); });
                    res[4].ForEach(r => { Console.WriteLine($"{r.Id}|{r.ProcessName}|00|U"); });
                }
                else if (args[0].Equals("inject"))
                {
                    Boolean parsed = long.TryParse(args[1], out long lhwnd);

                    IntPtr hwnd = IntPtr.Zero;
                    if (parsed)
                        hwnd = new IntPtr(lhwnd);

                    var file = string.Empty;
                    if (IntPtr.Size == 8) { 
                        file = "ManagedInjector64.dll";
                    }
                    else
                    {
                        file = "ManagedInjector32.dll";
                    }

//                    String Assambly = args[2];

                    String[] newArgs = String.Join(" ", args).Split('|');


                    Assembly loadedAssembly = Assembly.Load(newArgs[1]);
                    if (loadedAssembly == null) parsed = false;

                    String methode = newArgs[2];
                    
                    
                    if (!parsed)
                    {
                        Console.WriteLine("inject <PID> |<assembly>|<fullname>|<methode>");
                        return;
                    }

                    String parsedFile;
                    if (!file.Contains(Path.DirectorySeparatorChar))
                    {
                        parsedFile = Path.Combine(Directory.GetCurrentDirectory(), file);
                    }
                    else
                    {
                        parsedFile = file;
                    }

                   
                    if (!File.Exists(parsedFile))
                    {
                        Console.WriteLine($"File not found: {parsedFile}");
                        return;
                    }
                    

                    try
                    {
                        Process processById = GetProcess(hwnd);
                        if (processById != null && IsManaged(processById))
                        {


                            var dll = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), parsedFile));

                            foreach (Type type in dll.GetExportedTypes())
                            {
                                var c = Activator.CreateInstance(type);
                                type.InvokeMember("Launch", BindingFlags.InvokeMethod, null, c,
                                    new object[] {hwnd, loadedAssembly, methode, "TakeTwo"});
                            }
                        }
                        else
                        {
                            Console.WriteLine("Process not found or process is not managed!");
                        }

                        //                        InjectorCore injector = new InjectorCore();
                        //                        if (!injector.DoInject(pid, parsedFile, startThread))
                        //                        {
                        //                            Console.WriteLine("Injection failed!");
                        //                        }
                        //                        else
                        //                        {
                        //                            Console.WriteLine("Injection successfull!");
                        //                        }
                    }
                    catch
                    {
                        Console.WriteLine("Process not found");
                    }
                }
            }
            else
            {
                String bitVersion;
                if (IntPtr.Size == 4)
                {
                    bitVersion = "32";
                }
                else if (IntPtr.Size == 8)
                {
                    bitVersion = "64";
                }
                else
                {
                    bitVersion = "[Unknown]";
                }

                Console.WriteLine($"App connector x{bitVersion}");
                Console.WriteLine("list - get list of processes");
                Console.WriteLine("inject - inject to process");
            }
        }

        public List<Process>[] FindEveryDotNetProces()
        {
            List<Process>[] array = new List<Process>[]
            {
                new List<Process>(), //Managed 32
                new List<Process>(), //Managed 64 bit
                new List<Process>(), //Unmanaged 32 bit
                new List<Process>(), //unmanaged 64 bit
                new List<Process>() // Unknown
            };
            int id = Process.GetCurrentProcess().Id;
            foreach (Process process in Process.GetProcesses())
            {
                if (process.Id != id && process.Id != 0)
                {
                    if (IsManaged(process)) //werkt niet :(
                    {
                        if (!IsWin32Process(process))
                        {
                            array[1].Add(process);
                        }
                        else
                        {
                            array[0].Add(process);
                        }
                    }
                    else
                    {
                        if (IsWin32Process(process))
                        {
                            array[2].Add(process);
                        }
                        else
                        {
                            array[3].Add(process);
                        }
                    }
                }
            }
            return array;
        }
        
        public static bool IsManaged(Process proc)
        {
            try
            {
                if (proc.Modules != null)
                {
                    foreach (object obj in proc.Modules)
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

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

        public bool IsWin32Process(Process process)
        {
            try
            {
                if (Environment.OSVersion.Version.Major > 5
                    || Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1)
                {
                    bool retVal;

                    return IsWow64Process(process.Handle, out retVal) && retVal;
                }

                return false; // not on 64-bit Windows Emulator
            }
            catch //Protected
            {
                return false;
            }
        }

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static Process GetProcess(IntPtr hWnd)
        {
            int procId;
            GetWindowThreadProcessId(hWnd, out procId);
            try
            {
                return Process.GetProcessById(procId);
            }
            catch
            {
                return null;
            }
        }
    }
}