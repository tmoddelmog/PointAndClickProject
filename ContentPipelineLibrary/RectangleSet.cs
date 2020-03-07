using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentPipelineLibrary
{
    public class RectangleSet
    {
        public List<Rectangle> Rectangles;
        public int Width;
        public int Height;
        public RectangleSet(Rectangle[] rs, int w, int h)
        {
            this.Rectangles = new List<Rectangle>();
            foreach (Rectangle r in rs) this.Rectangles.Add(r);
            this.Width = w;
            this.Height = h;
        }
    }
}
