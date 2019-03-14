using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace DotNet_Injector.Targeter
{
    public partial class WindowTarget : UserControl
    {
        private bool _dragging = false;
        private Cursor crossCursor;

        /// <summary>
        /// Called when the WindowHandle property is changed.
        /// </summary>
        public Action<TargetResult> onWindowHandleChanged;
        

        private IntPtr _hWndCurrent = IntPtr.Zero;

        public WindowTarget()
        {
            InitializeComponent();

            this.MouseDown += WindowTarget_MouseDown;
            this.MouseMove += WindowTarget_MouseMove;
            this.MouseUp += WindowTarget_MouseUp;

            using (MemoryStream ms = new MemoryStream(Properties.Resources.cross))
            {
                crossCursor = new Cursor(ms);
            }
        }

        private void WindowTarget_MouseUp(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                _dragging = false;
                this.Cursor = Cursors.Default;
                if (_hWndCurrent != IntPtr.Zero)
                {
                    _hWndCurrent = IntPtr.Zero;
                }
                this.BackgroundImage = Properties.Resources.app_cross;

                Win32.SetCapture(IntPtr.Zero);
            }
        }

        private void WindowTarget_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                IntPtr hWnd = Win32.WindowFromPoint(MousePosition);
                if (hWnd == this.Handle)
                {
                    // Drawing a border around the dragPictureBox (where we start
                    // dragging) doesn't look nice, so we ignore this window
                    hWnd = IntPtr.Zero;
                }

                if (hWnd != _hWndCurrent) {
                    _hWndCurrent = hWnd;
                    onWindowHandleChanged?.Invoke(new TargetResult(hWnd));
                }

                
            }
        }

        private void WindowTarget_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                this.Cursor = crossCursor;
                this.BackgroundImage = Properties.Resources.app_no_cross;

                Win32.SetCapture(Handle);
//                targetWindow = IntPtr.Zero;

                onWindowHandleChanged?.Invoke(new TargetResult(Handle));
            }
        }
    }
}
