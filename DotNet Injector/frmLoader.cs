using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DotNet_Injector
{
    public partial class frmLoader : Form
    {
        private List<string> filesToLoad = new List<string>();

        public frmLoader()
        {
            InitializeComponent();
        }

        private void frmLoader_Load(object sender, EventArgs e)
        {
            this.Text = AppDomain.CurrentDomain.FriendlyName;

            this.txtExe.DragEnter += Global_DragEnter;
            this.lbDllFiles.DragEnter += Global_DragEnter;
        }

        private void Global_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void doAsmBuildEvent()
        {
            #region System.AppDomain.CurrentDomain.AssemblyResolve event
            // This is fired when the CLR tries to bind to the assembly and fails
            ResolveEventHandler Eve = new ResolveEventHandler((object sender, ResolveEventArgs args) =>
            {
                string nameOfFile = args.Name.Substring(0, args.Name.IndexOf(","));
                Assembly am = null;
                foreach (string supportFile in filesToLoad)
                {
                    int start = supportFile.LastIndexOf("\\") + 1;
                    int end = supportFile.LastIndexOf(".") - start;
                    if (supportFile.Substring(start, end) == nameOfFile) {
                        var supportFileBytes = File.ReadAllBytes(supportFile);
                        am = Assembly.Load(supportFileBytes);
                    }
                }
                return am;
            });
            AppDomain.CurrentDomain.AssemblyResolve += Eve;

            #endregion ---System.AppDomain.CurrentDomain.AssemblyResolve event---

            this.FormClosing += (a, b) =>
            {
                AppDomain.CurrentDomain.AssemblyResolve -= Eve;
            };
        }

        private void loadPayload()
        {
            doAsmBuildEvent();

            if (lbDllFiles.Items.Count == 0 && txtExe.Text == string.Empty)
            {
                MessageBox.Show("select file to load");
                return;
            }

            foreach (string s in lbDllFiles.Items)
            {
                if (!filesToLoad.Contains(s))
                    filesToLoad.Add(s);
            }

            var exeFileBytes = File.ReadAllBytes(txtExe.Text);
            Assembly am = Assembly.Load(exeFileBytes);

            MethodInfo m = am.EntryPoint;

            int numParams = m.GetParameters().Length;
            object[] paramList = new object[numParams];

            int i = 0;
            foreach (ParameterInfo p in m.GetParameters())
            {
                if (p.ParameterType.IsArray)
                {
                    switch (p.ParameterType.ToString())
                    {
                        case "System.String[]":
                            paramList[i] = new string[0];
                            break;
                        case "System.Int[]":
                            paramList[i] = new int[0];
                            break;
                    }
                }
                else
                    paramList[i] = p.DefaultValue;
            }

            if (paramList.Length == 0)
                paramList = null;

            Semaphore mu = new Semaphore(0, 1);

            ThreadPool.QueueUserWorkItem(delegate
            {
                Thread t = new Thread(new ThreadStart(
                    delegate
                    {
                        m.Invoke(null, paramList);
                    }));

                t.Priority = ThreadPriority.AboveNormal;
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
                Thread.Sleep(200);
                mu.Release();
                Thread.Sleep(200);

            }, null);

            mu.WaitOne();
            
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            loadPayload();
        }

        private void btnAddDll_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "dll files (*.dll)|*.dll|exe files (*.exe)|*.exe|All files (*.*)|*.*";

            ofd.Title = "Select a dll/exe code base";

            //Present to the user
            if (ofd.ShowDialog() == DialogResult.OK)
                foreach (string name in ofd.FileNames)
                    lbDllFiles.Items.Add(name);
        }

        private void btnRemoveDll_Click(object sender, EventArgs e)
        {
            foreach (var selected in lbDllFiles.Items)
            {
                lbDllFiles.Items.Remove(selected);
            }
        }

        private void txtExe_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            txtExe.Text = files.FirstOrDefault();
        }

        private void lbDllFiles_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            files.ToList().ForEach(f => { lbDllFiles.Items.Add(f); });
        }


        
    }
}
