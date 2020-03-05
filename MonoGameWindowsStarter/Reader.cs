using ContentPipelineLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRead = ContentPipelineLibrary.RectangleSet;

namespace MonoGameWindowsStarter
{
    public class Reader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            Rectangle[] rectangles = new Rectangle[input.ReadInt32()];
            var width = input.ReadInt32();
            var height = input.ReadInt32();
            for (var i = 0; i < rectangles.Length; i++)
                rectangles[i] = input.ReadObject<Rectangle>();

            return new RectangleSet(rectangles, width, height);
        }
    }
}
