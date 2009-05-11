using System; // © 2008 Koushik Dutta - www.koushikdutta.com
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;
using System.Threading;
using TiledMaps;

namespace TiledMapsTestHarness
{
    public partial class TiledMaps : Form
    {
        public TiledMaps()
        {
            InitializeComponent();
        }

        private void VirtualEarth_Load(object sender, EventArgs e)
        {
            myVEBitmap = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            myVEGraphics = Graphics.FromImage(myVEBitmap);
            myGMBitmap = new Bitmap(pictureBox2.ClientSize.Width, pictureBox1.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            myGMGraphics = Graphics.FromImage(myGMBitmap);

            myGMSession.RefreshBitmap = myRenderer.LoadBitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("TiledMapsTestHarness.Refresh.png"));
            myGMSession.SetSessions(new VirtualEarthSatelliteSession(), new GoogleRoadsSession());
            // enable roads.
            myGMSession[1] = true;

            Geocode seattle = new Geocode(47.6141229683726, -122.346501168284);

            IMapDrawable marker = myRenderer.LoadBitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("TiledMapsTestHarness.Marker.png"));
            MapOverlay overlay = new MapOverlay(marker, seattle, new Point(0, -marker.Height / 2));
            myGMSession.Overlays.Add(overlay);
            myVESession.Overlays.Add(overlay);

            TextMapDrawable markerText = new TextMapDrawable();
            markerText.Brush = new SolidBrush(Color.Red);
            markerText.Font = new Font(FontFamily.GenericSerif, 26, FontStyle.Bold);
            markerText.Text = "Seattle";
            MapOverlay markerTextOverlay = new MapOverlay(markerText, seattle, new Point(0, -marker.Height - markerText.Height / 2));
            myGMSession.Overlays.Add(markerTextOverlay);
            myVESession.Overlays.Add(markerTextOverlay); 
            
            pictureBox1.Image = myVEBitmap;
            pictureBox2.Image = myGMBitmap;
            RefreshVEBox(null, null);
            RefreshGMBox(null, null);
        }

        VirtualEarthMapSession myVESession = new VirtualEarthMapSession();
        CompositeMapSession myGMSession = new CompositeMapSession();
        Bitmap myVEBitmap;
        Graphics myVEGraphics;
        Bitmap myGMBitmap;
        Graphics myGMGraphics;
        GraphicsRenderer myRenderer = new GraphicsRenderer();

        void RefreshVEBox(object sender, EventArgs e)
        {
            myRenderer.Graphics = myVEGraphics;
            myVESession.DrawMap(myRenderer, 0, 0, myVEBitmap.Width, myVEBitmap.Height, new WaitCallback(VECallback), null);
            pictureBox1.Refresh();
        }
        void RefreshGMBox(object sender, EventArgs e)
        {
            myRenderer.Graphics = myGMGraphics;
            myGMSession.DrawMap(myRenderer, 0, 0, myGMBitmap.Width, myGMBitmap.Height, new WaitCallback(GMCallback), null);
            pictureBox2.Refresh();
        }


        void VECallback(object state)
        {
            Invoke(new EventHandler(RefreshVEBox));
        }

        void GMCallback(object state)
        {
            Invoke(new EventHandler(RefreshGMBox));
        }

        Point lastMovePos = Point.Empty;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xDif = e.X - lastMovePos.X;
                int yDif = e.Y - lastMovePos.Y;
                myVESession.Pan(xDif, yDif);
                myGMSession.Pan(xDif, yDif);
                RefreshVEBox(null, null);
                RefreshGMBox(null, null);
                lastMovePos = new Point(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastMovePos = new Point(e.X, e.Y);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            myVESession.ZoomIn();
            myGMSession.ZoomIn();
            RefreshVEBox(null, null);
            RefreshGMBox(null, null);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            myVESession.ZoomOut();
            myGMSession.ZoomOut();
            RefreshVEBox(null, null);
            RefreshGMBox(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //myVESession.FitPOIToDimensions(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, 15, new Geocode(double.Parse(textBox1.Text), double.Parse(textBox2.Text)));
            //myGMSession.FitPOIToDimensions(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, 15, new Geocode(double.Parse(textBox1.Text), double.Parse(textBox2.Text)));
            myVESession.FitPOIToDimensions(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, 15, new Geocode(double.Parse(textBox1.Text), double.Parse(textBox2.Text)));
            myGMSession.FitPOIToDimensions(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, 15, new Geocode(double.Parse(textBox1.Text), double.Parse(textBox2.Text)));
            RefreshVEBox(null, null);
            RefreshGMBox(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myGMSession.Routes.Clear();
            myVESession.Routes.Clear();
            var dirs = GoogleServices.GetDirections<Directions>(textBox3.Text, textBox4.Text);
            //myGMSession.Directions = VirtualEarthServices.GetDirections(new Geocode(47.6141229683726, -122.346501168284), new Geocode(47.6201369499368, -122.204050742954));
            //Directions dirs = GoogleServices.GetDirections("Chicago", "Seattle");
            myGMSession.Routes.Add(dirs);
            myVESession.Routes.Add(dirs);

            myGMSession.FitPOIToDimensions(myGMBitmap.Width, myGMBitmap.Height, 15, dirs.PolyLine[0], dirs.PolyLine[dirs.PolyLine.Length - 1]);
            myVESession.FitPOIToDimensions(myVEBitmap.Width, myVEBitmap.Height, 15, dirs.PolyLine[0], dirs.PolyLine[dirs.PolyLine.Length - 1]);
            RefreshVEBox(null, null);
            RefreshGMBox(null, null);
        }

        private void myToggleRoads_Click(object sender, EventArgs e)
        {
            myGMSession[1] = !myGMSession[1];
            RefreshGMBox(null, null);
        }
    }
}