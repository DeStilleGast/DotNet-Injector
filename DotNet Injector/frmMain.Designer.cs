namespace DotNet_Injector
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lBit = new System.Windows.Forms.Label();
            this.lProcess = new System.Windows.Forms.Label();
            this.lManaged = new System.Windows.Forms.Label();
            this.btnInject = new System.Windows.Forms.Button();
            this.btnSelectProcess = new System.Windows.Forms.Button();
            this.lPayload = new System.Windows.Forms.Label();
            this.windowTarget1 = new DotNet_Injector.Targeter.WindowTarget();
            this.lWindowClass = new System.Windows.Forms.Label();
            this.txtPlatform = new System.Windows.Forms.TextBox();
            this.txtIsManaged = new System.Windows.Forms.TextBox();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.lText = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lBit
            // 
            this.lBit.AutoSize = true;
            this.lBit.Location = new System.Drawing.Point(10, 9);
            this.lBit.Name = "lBit";
            this.lBit.Size = new System.Drawing.Size(48, 13);
            this.lBit.TabIndex = 1;
            this.lBit.Text = "Platform:";
            // 
            // lProcess
            // 
            this.lProcess.AutoSize = true;
            this.lProcess.Location = new System.Drawing.Point(10, 85);
            this.lProcess.Name = "lProcess";
            this.lProcess.Size = new System.Drawing.Size(48, 13);
            this.lProcess.TabIndex = 1;
            this.lProcess.Text = "Process:";
            // 
            // lManaged
            // 
            this.lManaged.AutoSize = true;
            this.lManaged.Location = new System.Drawing.Point(3, 35);
            this.lManaged.Name = "lManaged";
            this.lManaged.Size = new System.Drawing.Size(55, 13);
            this.lManaged.TabIndex = 1;
            this.lManaged.Text = "Managed:";
            // 
            // btnInject
            // 
            this.btnInject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInject.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInject.Location = new System.Drawing.Point(212, 134);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new System.Drawing.Size(75, 23);
            this.btnInject.TabIndex = 2;
            this.btnInject.Text = "Inject";
            this.btnInject.UseVisualStyleBackColor = true;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // btnSelectProcess
            // 
            this.btnSelectProcess.Location = new System.Drawing.Point(114, 134);
            this.btnSelectProcess.Name = "btnSelectProcess";
            this.btnSelectProcess.Size = new System.Drawing.Size(92, 23);
            this.btnSelectProcess.TabIndex = 2;
            this.btnSelectProcess.Text = "Select process";
            this.btnSelectProcess.UseVisualStyleBackColor = true;
            this.btnSelectProcess.Visible = false;
            // 
            // lPayload
            // 
            this.lPayload.AutoSize = true;
            this.lPayload.Location = new System.Drawing.Point(10, 137);
            this.lPayload.Name = "lPayload";
            this.lPayload.Size = new System.Drawing.Size(48, 13);
            this.lPayload.TabIndex = 1;
            this.lPayload.Text = "Payload:";
            // 
            // windowTarget1
            // 
            this.windowTarget1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.windowTarget1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("windowTarget1.BackgroundImage")));
            this.windowTarget1.Location = new System.Drawing.Point(252, 9);
            this.windowTarget1.Name = "windowTarget1";
            this.windowTarget1.Size = new System.Drawing.Size(31, 28);
            this.windowTarget1.TabIndex = 0;
            // 
            // lWindowClass
            // 
            this.lWindowClass.AutoSize = true;
            this.lWindowClass.Location = new System.Drawing.Point(23, 111);
            this.lWindowClass.Name = "lWindowClass";
            this.lWindowClass.Size = new System.Drawing.Size(35, 13);
            this.lWindowClass.TabIndex = 1;
            this.lWindowClass.Text = "Class:";
            // 
            // txtPlatform
            // 
            this.txtPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlatform.Location = new System.Drawing.Point(64, 6);
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.ReadOnly = true;
            this.txtPlatform.Size = new System.Drawing.Size(178, 20);
            this.txtPlatform.TabIndex = 3;
            // 
            // txtIsManaged
            // 
            this.txtIsManaged.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIsManaged.Location = new System.Drawing.Point(64, 32);
            this.txtIsManaged.Name = "txtIsManaged";
            this.txtIsManaged.ReadOnly = true;
            this.txtIsManaged.Size = new System.Drawing.Size(178, 20);
            this.txtIsManaged.TabIndex = 3;
            // 
            // txtProcess
            // 
            this.txtProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcess.Location = new System.Drawing.Point(64, 82);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(223, 20);
            this.txtProcess.TabIndex = 3;
            // 
            // txtClass
            // 
            this.txtClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClass.Location = new System.Drawing.Point(64, 108);
            this.txtClass.Name = "txtClass";
            this.txtClass.ReadOnly = true;
            this.txtClass.Size = new System.Drawing.Size(223, 20);
            this.txtClass.TabIndex = 3;
            // 
            // lText
            // 
            this.lText.AutoSize = true;
            this.lText.Location = new System.Drawing.Point(27, 61);
            this.lText.Name = "lText";
            this.lText.Size = new System.Drawing.Size(31, 13);
            this.lText.TabIndex = 1;
            this.lText.Text = "Text:";
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.Location = new System.Drawing.Point(64, 58);
            this.txtText.Name = "txtText";
            this.txtText.ReadOnly = true;
            this.txtText.Size = new System.Drawing.Size(223, 20);
            this.txtText.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 169);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.txtIsManaged);
            this.Controls.Add(this.txtPlatform);
            this.Controls.Add(this.btnSelectProcess);
            this.Controls.Add(this.btnInject);
            this.Controls.Add(this.lWindowClass);
            this.Controls.Add(this.lPayload);
            this.Controls.Add(this.lText);
            this.Controls.Add(this.lManaged);
            this.Controls.Add(this.lProcess);
            this.Controls.Add(this.lBit);
            this.Controls.Add(this.windowTarget1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "DotNet Injector";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Targeter.WindowTarget windowTarget1;
        private System.Windows.Forms.Label lBit;
        private System.Windows.Forms.Label lProcess;
        private System.Windows.Forms.Label lManaged;
        private System.Windows.Forms.Button btnInject;
        private System.Windows.Forms.Button btnSelectProcess;
        private System.Windows.Forms.Label lPayload;
        private System.Windows.Forms.Label lWindowClass;
        private System.Windows.Forms.TextBox txtPlatform;
        private System.Windows.Forms.TextBox txtIsManaged;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label lText;
        private System.Windows.Forms.TextBox txtText;
    }
}

