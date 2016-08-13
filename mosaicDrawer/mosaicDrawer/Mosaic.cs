using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

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

            RGB = new byte[3 * px * py];

            //runLoop(mainWindow);
        }

        public void runLoop()
        {
            mainWindow = new GameWindow(lx, ly, GraphicsMode.Default, "MosaicDrawer " + lx.ToString() + "x" + ly.ToString() + " Window");

            using (mainWindow)
            {
                //Enable vsync as standard.
                mainWindow.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    mainWindow.VSync = VSyncMode.On;
                };

                //Resize handler
                mainWindow.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, mainWindow.Width, mainWindow.Height);
                };

                //The rendercalls, here the map should call its renderfunction.
                mainWindow.RenderFrame += (sender, e) =>
                {
                    // render graphics

                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    //Draw the mosaic
                    GL.Begin(BeginMode.Quads);
                    for (int i = 0; i < px; i++)
                    {
                        for(int j = 0; j < py; j++)
                        {
                            GL.Color3(RGB[3 * px * i + j * 3], RGB[3 * px * i + j * 3 + 1], RGB[3 * px * i + j * 3 + 2]);
                            GL.Vertex2((double)((lx / px) * i)/(double)lx, (double)((ly / py) * i) / (double)ly);
                            GL.Vertex2((double)((lx / px) * i-lx/(px)) / (double)lx, (double)((ly / py) * i) / (double)ly);
                            GL.Vertex2((double)((lx / px) * i - lx/px) / (double)lx, (double)((ly / py) * i-ly/py) / (double)ly);
                            GL.Vertex2((double)((lx / px) * i) / (double)lx, (double)((ly / py) * i - ly / py) / (double)ly);
                        }
                    }
                    GL.End();

                    mainWindow.SwapBuffers();
                };

                mainWindow.UpdateFrame += (sender, e) =>
                {

                    // add mainWindow logic, input handling here.
                    if (mainWindow.Keyboard[Key.Escape])
                    {
                        mainWindow.Exit();
                    }
                };

                // Run the mainWindow at 144 updates per second
                mainWindow.Run(144.0);
            }
        }

        public void updateMosaic(byte[] RGB)
        {
            this.RGB = RGB;
        }
    }
}
