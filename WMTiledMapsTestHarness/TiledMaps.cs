using System; // © 2008 Koushik Dutta - www.koushikdutta.com
using TiledMaps;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WMTiledMapsTestHarness
{
    public partial class TiledMaps : Form
    {
        public TiledMaps()
        {
            InitializeComponent();

            mySession.RefreshBitmap = myRenderer.LoadBitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("WMTiledMapsTestHarness.Refresh.png"));
            IMapDrawable marker = myRenderer.LoadBitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("WMTiledMapsTestHarness.Marker.png"));
            MapOverlay overlay = new MapOverlay(marker, new Geocode(47.6141229683726, -122.346501168284), new Point(0, -marker.Height / 2));
            mySession.Overlays.Add(overlay);
            RefreshBitmap();
        }

        Bitmap myBitmap;
        GraphicsRenderer myRenderer = new GraphicsRenderer();
        GoogleMapSession mySession = new GoogleMapSession();

        private void myZoomInMenuItem_Click(object sender, EventArgs e)
        {
            mySession.ZoomIn();
            RefreshBitmap();
        }

        private void myZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            mySession.ZoomOut();
            RefreshBitmap();
        }

        void RefreshBitmap()
        {
            // clear out tiles that haven't been used in 10 seconds, just to keep from running out of memory.
            mySession.ClearAgedTiles(10000);

            if (myBitmap == null || myBitmap.Width != myPictureBox.ClientSize.Width || myBitmap.Height != myPictureBox.ClientSize.Height)
            {
                myBitmap = new Bitmap(myPictureBox.ClientSize.Width, myPictureBox.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
                myRenderer.Graphics = Graphics.FromImage(myBitmap);
                myPictureBox.Image = myBitmap;
            }
            mySession.DrawMap(myRenderer, 0, 0, myBitmap.Width, myBitmap.Height, (o) =>
            {
                Invoke(new EventHandler((sender, args) =>
                {
                    RefreshBitmap();
                }));
            }, null);
            myPictureBox.Refresh();
        }

        Point myLastPos = Point.Empty;
        private void myPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mySession.Pan(MousePosition.X - myLastPos.X, MousePosition.Y - myLastPos.Y);
            myLastPos = MousePosition;
            RefreshBitmap();
        }

        private void myPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            myLastPos = MousePosition;
        }
    }
}