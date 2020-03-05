using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentPipelineLibrary
{
    public class RectangleSet
    {
        public Rectangle[] Rectangles;
        public int Width;
        public int Height;
        public RectangleSet(Rectangle[] rs, int w, int h)
        {
            this.Rectangles = rs;
            this.Width = w;
            this.Height = h;
        }
    }
}
