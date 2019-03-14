using DotNet_Injector.Targeter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace DotNet_Injector
{
    public partial class frmMain : Form
    {
        //        public Dictionary<int, ProcessLink> processLinks = new Dictionary<int, ProcessLink>();
        //        private BackgroundWorker processUpdater = new BackgroundWorker();

        private string payloadLoc;

        private ObservableCollection<WindowInfo> windows = new ObservableCollection<WindowInfo>();

        private TargetResult lastTarget = null;

        public frmMain() {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e) {
            this.windowTarget1.onWindowHandleChanged += targetResult => {
                lastTarget = targetResult;

                if (targetResult.Handle != IntPtr.Zero) {
                    payloadLoc = $"ManagedInjector{(targetResult.IsWin32Process() ? "32" : "64")}.dll";

                    this.txtPlatform.Text = $"{(targetResult.IsWin32Process() ? "32" : "64")}bit";
                    this.txtIsManaged.Text = targetResult.IsManaged() ? "Yes" : "No";
                    this.txtProcess.Text = targetResult.getApplicationFilename();
                    this.txtText.Text = targetResult.WindowText;
                    this.txtClass.Text = targetResult.WindowClass;
                    this.lPayload.Text = $"Payload: {payloadLoc}";
//                    }
                }
            };
        }

        private void InjectMenuClick(object sender, EventArgs e) {
            MessageBox.Show("sup");
        }

        private void ProcessUpdater_DoWork(object sender, DoWorkEventArgs e) {
            this.Invoke(new MethodInvoker(() => { this.Text = "DotNet Injector - scanning"; }));

            foreach (IntPtr windowHandle in Win32.ToplevelWindows) {
                WindowInfo window = new WindowInfo(windowHandle);
                if (window.IsValidProcess && !this.HasProcess(window.OwningProcess))
                    this.windows.Add(window);
            }

            this.Invoke(new MethodInvoker(() => { this.Text = "DotNet Injector"; }));
        }


        private void btnInject_Click(object sender, EventArgs e) {
            Inject(lastTarget.Handle);
        }

        public void Inject(IntPtr hWnd) {
            if (hWnd == IntPtr.Zero) return;
            //            test();

//            var result = this.windowTarget1.Handle;

            var connector = new Process() {
                StartInfo = {
                    FileName = $"AppConnector{(Win32.IsWin32Process(hWnd) ? "32" : "64")}.exe",
                    Arguments = $"inject {hWnd} |{typeof(Program).Assembly}|{typeof(Program).FullName}|TakeTwo"
                }
            };

            connector.Start();


//            result.Handle


//            Launch(this.windowTarget1.result.Handle, typeof(Program).Assembly, typeof(Program).FullName,
//                "TakeTwo");        
        }

        private bool HasProcess(Process process) {
            foreach (WindowInfo window in this.windows)
                if (window.OwningProcess.Id == process.Id)
                    return true;
            return false;
        }
    }

    public class WindowInfo
    {
        private static Dictionary<int, bool> processIDToValidityMap = new Dictionary<int, bool>();

        private IntPtr hwnd;

        public WindowInfo(IntPtr hwnd) {
            this.hwnd = hwnd;
        }

        public static void ClearCachedProcessInfo() {
            WindowInfo.processIDToValidityMap.Clear();
        }

        public bool IsValidProcess {
            get {
                bool isValid = false;
                try {
                    if (this.hwnd == IntPtr.Zero)
                        return false;

                    Process process = this.OwningProcess;
                    if (process == null)
                        return false;

                    if (WindowInfo.processIDToValidityMap.TryGetValue(process.Id, out isValid))
                        return isValid;

                    if (process.Id == Process.GetCurrentProcess().Id)
                        isValid = false;
                    else if (process.MainWindowHandle == IntPtr.Zero)
                        isValid = false;
                    // Remove VS, since it loads the WPF assemblies but we know that it doesn't work.
                    else if (process.ProcessName.Contains("devenv"))
                        isValid = false;
                    else {
                        foreach (ProcessModule module in process.Modules) {
                            //check if is managed
                            if (module.ModuleName.StartsWith("mscorlib", StringComparison.InvariantCultureIgnoreCase) ||
                                module.ModuleName.Contains("PresentationFramework.")) {
                                isValid = true;
                                break;
                            }
                        }
                    }

                    WindowInfo.processIDToValidityMap[process.Id] = isValid;
                }
                catch (Exception) { }

                return isValid;
            }
        }

        public Process OwningProcess {
            get { return Win32.GetProcess(this.hwnd); }
        }
    }
}