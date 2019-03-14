namespace DotNet_Injector
{
    partial class frmLoader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInject = new System.Windows.Forms.Button();
            this.txtExe = new System.Windows.Forms.TextBox();
            this.lbDllFiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddDll = new System.Windows.Forms.Button();
            this.btnRemoveDll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInject
            // 
            this.btnInject.Location = new System.Drawing.Point(246, 139);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new System.Drawing.Size(75, 23);
            this.btnInject.TabIndex = 0;
            this.btnInject.Text = "Run code";
            this.btnInject.UseVisualStyleBackColor = true;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // txtExe
            // 
            this.txtExe.AllowDrop = true;
            this.txtExe.Location = new System.Drawing.Point(81, 12);
            this.txtExe.Name = "txtExe";
            this.txtExe.Size = new System.Drawing.Size(242, 20);
            this.txtExe.TabIndex = 1;
            this.txtExe.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtExe_DragDrop);
            // 
            // lbDllFiles
            // 
            this.lbDllFiles.AllowDrop = true;
            this.lbDllFiles.FormattingEnabled = true;
            this.lbDllFiles.Location = new System.Drawing.Point(81, 38);
            this.lbDllFiles.Name = "lbDllFiles";
            this.lbDllFiles.Size = new System.Drawing.Size(242, 95);
            this.lbDllFiles.TabIndex = 2;
            this.lbDllFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbDllFiles_DragDrop);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Executable:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "DLL files:";
            // 
            // btnAddDll
            // 
            this.btnAddDll.Location = new System.Drawing.Point(52, 81);
            this.btnAddDll.Name = "btnAddDll";
            this.btnAddDll.Size = new System.Drawing.Size(23, 23);
            this.btnAddDll.TabIndex = 4;
            this.btnAddDll.Text = "+";
            this.btnAddDll.UseVisualStyleBackColor = true;
            this.btnAddDll.Click += new System.EventHandler(this.btnAddDll_Click);
            // 
            // btnRemoveDll
            // 
            this.btnRemoveDll.Location = new System.Drawing.Point(52, 110);
            this.btnRemoveDll.Name = "btnRemoveDll";
            this.btnRemoveDll.Size = new System.Drawing.Size(23, 23);
            this.btnRemoveDll.TabIndex = 4;
            this.btnRemoveDll.Text = "-";
            this.btnRemoveDll.UseVisualStyleBackColor = true;
            this.btnRemoveDll.Click += new System.EventHandler(this.btnRemoveDll_Click);
            // 
            // frmLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 175);
            this.Controls.Add(this.btnRemoveDll);
            this.Controls.Add(this.btnAddDll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbDllFiles);
            this.Controls.Add(this.txtExe);
            this.Controls.Add(this.btnInject);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoader";
            this.Text = "frmLoader";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLoader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInject;
        private System.Windows.Forms.TextBox txtExe;
        private System.Windows.Forms.ListBox lbDllFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddDll;
        private System.Windows.Forms.Button btnRemoveDll;
    }
}