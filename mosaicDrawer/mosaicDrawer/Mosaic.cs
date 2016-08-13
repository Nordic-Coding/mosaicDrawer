using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace mosaicDrawer
{

    public class Mosaic
    {
        GameWindow mainWindow;
        int px, py, lx, ly;
        byte[] RGB;

        public Mosaic(int px, int py, int lx, int ly)
        {
            this.px = px;
            this.py = py;
            this.lx = lx;
            this.ly = ly;

            mainWindow = new GameWindow(lx,ly,GraphicsMode.Default,"MosaicDrawer " + lx.ToString()+"x" + ly.ToString() + " Window");
            RGB = new byte[3 * px * py];
        }

        void loop()
        {

        }
    }
}
