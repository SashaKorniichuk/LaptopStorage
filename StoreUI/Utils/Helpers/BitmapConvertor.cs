using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace StoreUI.Utils.Helpers
{
    public static class BitmapConvertor
    {
        public static Bitmap Convert(Stream stream,int width,int height)
        {
            var bitmap = new Bitmap(width, height);

            using(var graphics=Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(Image.FromStream(stream), 0, 0, width, height);
            }
            return bitmap;
        }
    }
}