using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Text;
using TWrite = ContentPipelineLibrary.RectangleSet;

namespace ContentPipelineLibrary
{
    [ContentTypeWriter]
    public class Writer : ContentTypeWriter<TWrite>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "MonoGameWindowsStarter.Reader, MonoGameWindowsStarter";
        }

        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.Rectangles.Count);
            output.Write(value.Width);
            output.Write(value.Height);
            foreach (Rectangle r in value.Rectangles)
                output.WriteObject<Rectangle>(r);
        }
    }
}
