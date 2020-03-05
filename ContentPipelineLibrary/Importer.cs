using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.IO;
using TInput = ContentPipelineLibrary.RectangleSet;

namespace ContentPipelineLibrary
{
    [ContentImporter(".txt", DisplayName = "TXT Importer", DefaultProcessor = "TextProcessor")]
    public class Importer : ContentImporter<TInput>
    {
        public override TInput Import(string filename, ContentImporterContext context)
        {
            String[] lines = File.ReadAllLines(filename);
            var pointCount = int.Parse(lines[0]);
            var size = int.Parse(lines[1]);
            Rectangle[] rectangles = new Rectangle[pointCount];

            char[] seperator = { ',', ' ' };
            for (var i = 2; i < pointCount + 2; i++)
            {
                var tokens = lines[i].Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                rectangles[i - 2] = new Rectangle(
                    int.Parse(tokens[0]),   // X
                    int.Parse(tokens[1]),   // Y
                    size,                   // width
                    size                    // height
                    );
            }

            return new RectangleSet(rectangles, size, size);
        }
    }
}
