namespace Prototype
{
    partial class frmAbout
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
            this.lblTunnelSnakes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTunnelSnakes
            // 
            this.lblTunnelSnakes.AutoSize = true;
            this.lblTunnelSnakes.Location = new System.Drawing.Point(12, 20);
            this.lblTunnelSnakes.Name = "lblTunnelSnakes";
            this.lblTunnelSnakes.Size = new System.Drawing.Size(104, 13);
            this.lblTunnelSnakes.TabIndex = 0;
            this.lblTunnelSnakes.Text = "Tunnel Snakes Rule";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(128, 54);
            this.Controls.Add(this.lblTunnelSnakes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTunnelSnakes;
    }
}