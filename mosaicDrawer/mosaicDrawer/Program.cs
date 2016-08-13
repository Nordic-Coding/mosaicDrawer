using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mosaicDrawer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Just for testing the mosaic, will be removed later on.
            int px = 100, py = 100;
            Mosaic mosaic = new Mosaic(px,py,500,500);
            Thread mosaicThread = new Thread(()=>mosaic.runLoop());
            byte[] rgb = new byte[px*py*3];
            mosaicThread.Start();
            Random random = new Random();
            
            while (1 > 0)
            {
                //Console.WriteLine("AAAAAAAAAAAAAAAA");
                gibBytes(ref random,ref rgb);
                mosaic.updateMosaic(ref rgb);
                Thread.Sleep(25);
            }
            //Thread workerThread = new Thread(mosaic.);
        }

        public static void gibBytes(ref Random ran, ref byte[] ret)
        {
            //Generate a random byte sequence.
            for(int i = 0; i < ret.Length; i++)
            {
                if(ret[i] > 200)
                {
                    ret[i] = (byte)(ret[i] / 10);
                }
                else
                {
                    ret[i] = (byte)(ret[i] + 1 + (byte)ran.Next()/10);
                }
                
            }
        }
    }
}
