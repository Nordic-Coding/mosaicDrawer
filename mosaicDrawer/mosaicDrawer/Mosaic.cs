using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        int px, py, lx, ly, texId;
        int counter;
        //byte[] RGB;
        public bool drawing;

        public Mosaic(int px, int py, int lx, int ly)
        {
            this.px = px;
            this.py = py;
            this.lx = lx;
            this.ly = ly;
            counter = 0;
            //RGB = new byte[3 * px * py];
            drawing = false;

        }

        public void runLoop(byte[] RGB)
        {

            mainWindow = new GameWindow(lx, ly, GraphicsMode.Default, "MosaicDrawer " + lx.ToString() + "x" + ly.ToString() + " Window");

            using (mainWindow)
            {
                //Enable vsync as standard.
                mainWindow.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    //mainWindow.VSync = VSyncMode.On;
                    GL.Enable(EnableCap.Texture2D);
                    //GL.Enable(EnableCap.Blend);
                    texId = GL.GenTexture();
                    GL.BindTexture(TextureTarget.Texture2D, texId);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
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
                    drawing = true;
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.MatrixMode(MatrixMode.Modelview);
                    GL.LoadIdentity();
                    GL.BindTexture(TextureTarget.Texture2D, texId);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, px, py, 0,
                        OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.Byte, RGB);
                    //Draw the mosaic
                    GL.Begin(PrimitiveType.Quads);
                        GL.Color3(1f,1f,1f);
                        GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1f, -1f);
                        GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1f, -1f);
                        GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1f, 1f);
                        GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1f, 1f);
                    GL.End();
                    counter++;
                    mainWindow.Title = "MosaicDrawer " + lx.ToString() + "x" + ly.ToString() + " Window" + "(" + counter.ToString() + ")";
                    mainWindow.SwapBuffers();
                    drawing = false;
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
        /*
        public void updateMosaic(ref byte[] RGB)
        {
            //Bind the referenced RGB to the texture.
            this.RGB = RGB;
        }*/

    }
}
