namespace DotNet_Injector
{
    partial class frmOverlay
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
            this.components = new System.ComponentModel.Container();
            this.btnLoader = new System.Windows.Forms.Button();
            this.tActiveWindowChecker = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnLoader
            // 
            this.btnLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoader.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoader.Location = new System.Drawing.Point(235, 3);
            this.btnLoader.Name = "btnLoader";
            this.btnLoader.Size = new System.Drawing.Size(23, 23);
            this.btnLoader.TabIndex = 0;
            this.btnLoader.Text = "*";
            this.btnLoader.UseVisualStyleBackColor = false;
            this.btnLoader.Click += new System.EventHandler(this.btnLoader_Click);
            // 
            // tActiveWindowChecker
            // 
            this.tActiveWindowChecker.Enabled = true;
            this.tActiveWindowChecker.Tick += new System.EventHandler(this.tActiveWindowChecker_Tick);
            // 
            // frmOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(304, 57);
            this.Controls.Add(this.btnLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOverlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmOverlay";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.frmOverlay_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoader;
        private System.Windows.Forms.Timer tActiveWindowChecker;
    }
}