using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using TInput = ContentPipelineLibrary.RectangleSet;
using TOutput = ContentPipelineLibrary.RectangleSet;

namespace ContentPipelineLibrary
{
    [ContentProcessor(DisplayName = "TextProcessor")]
    public class Processor : ContentProcessor<TInput, TOutput>
    {
        public override TInput Process(TInput input, ContentProcessorContext context)
        {
            return input;
        }
    }
}
