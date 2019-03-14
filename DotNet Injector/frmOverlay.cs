using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotNet_Injector
{
    public partial class frmOverlay : Form
    {
        public frmOverlay()
        {
            InitializeComponent();
            btnLoader.Top = 0;
        }

        private Form oldForm = null;
        private void tActiveWindowChecker_Tick(object sender, EventArgs e)
        {
            if (Form.ActiveForm != null)
            {
                this.Visible = true;
                if (Form.ActiveForm != oldForm && Form.ActiveForm != this)
                {
                    if (oldForm != null)
                    {
                        oldForm.LocationChanged -= OldForm_LocationChanged;
                        oldForm.SizeChanged -= OldForm_SizeChanged;
                    }

                    oldForm = Form.ActiveForm;

                    oldForm.LocationChanged += OldForm_LocationChanged;
                    oldForm.SizeChanged += OldForm_SizeChanged;

                    OldForm_SizeChanged(null, null);
                    OldForm_LocationChanged(null, null);
                }
            }
            else
            {
                this.Visible = false;
            }
        }

        private void OldForm_SizeChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.Size = oldForm.Size;
                btnLoader.Left = this.Width / 4 * 3;
                
            }));
        }

        private void OldForm_LocationChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.Location = oldForm.Location;
            }));
        }

        private void btnLoader_Click(object sender, EventArgs e)
        {
            new frmLoader().Show();
        }

        NotifyIcon notifyIcon = new NotifyIcon();
        private void frmOverlay_Load(object sender, EventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add(new MenuItem("Unload injection", (a, b) => { Application.Exit(); }));
            cm.MenuItems.Add(new MenuItem($"Close {AppDomain.CurrentDomain.FriendlyName}", (a, b) => { Environment.Exit(-1); }));

            notifyIcon.Icon = Properties.Resources.Charge;
            notifyIcon.Visible = true;

            notifyIcon.BalloonTipTitle = "Successful";
            notifyIcon.BalloonTipText = "Injection completed!";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;

            notifyIcon.ContextMenu = cm;
            notifyIcon.ShowBalloonTip(5000);
//            notifyIcon.BalloonTipClosed += (a, b) => { notifyIcon.Visible = oldForm == null; };

        }
    }
}
