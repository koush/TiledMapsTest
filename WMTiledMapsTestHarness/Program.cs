using System; // © 2008 Koushik Dutta - www.koushikdutta.com

using System.Collections.Generic;
using System.Windows.Forms;

namespace WMTiledMapsTestHarness
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new TiledMaps());
        }
    }
}