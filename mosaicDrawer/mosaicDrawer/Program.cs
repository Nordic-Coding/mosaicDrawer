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
            Mosaic mosaic = new Mosaic(10,10,500,500);
            Thread mosaicThread = new Thread(()=>mosaic.runLoop());
            byte[] rgb = new byte[10*10*3];
            mosaicThread.Start();
            Random random = new Random();
            
            while (1 > 0)
            {
                //Console.WriteLine("AAAAAAAAAAAAAAAA");
                gibBytes(ref random,ref rgb);
                mosaic.updateMosaic(rgb);
                Thread.Sleep(20);
            }
            //Thread workerThread = new Thread(mosaic.);
        }

        public static void gibBytes(ref Random ran, ref byte[] ret)
        {
            //Generate a random byte sequence.
            ran.NextBytes(ret);
        }
    }
}
