namespace WMTiledMapsTestHarness
{
    partial class TiledMaps
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.myZoomInMenuItem = new System.Windows.Forms.MenuItem();
            this.myZoomOutMenuItem = new System.Windows.Forms.MenuItem();
            this.myPictureBox = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.myZoomInMenuItem);
            this.mainMenu1.MenuItems.Add(this.myZoomOutMenuItem);
            // 
            // myZoomInMenuItem
            // 
            this.myZoomInMenuItem.Text = "Zoom In";
            this.myZoomInMenuItem.Click += new System.EventHandler(this.myZoomInMenuItem_Click);
            // 
            // myZoomOutMenuItem
            // 
            this.myZoomOutMenuItem.Text = "Zoom Out";
            this.myZoomOutMenuItem.Click += new System.EventHandler(this.myZoomOutMenuItem_Click);
            // 
            // myPictureBox
            // 
            this.myPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPictureBox.Location = new System.Drawing.Point(0, 0);
            this.myPictureBox.Name = "myPictureBox";
            this.myPictureBox.Size = new System.Drawing.Size(240, 268);
            this.myPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.myPictureBox_MouseMove);
            this.myPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.myPictureBox_MouseDown);
            // 
            // TiledMaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.myPictureBox);
            this.Menu = this.mainMenu1;
            this.Name = "TiledMaps";
            this.Text = "Tiled Maps";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem myZoomInMenuItem;
        private System.Windows.Forms.MenuItem myZoomOutMenuItem;
        private System.Windows.Forms.PictureBox myPictureBox;
    }
}

