namespace Bebone.Text;

public static class FontAtlasBuilder
{
    public static FontAtlas Build(FontFace face, uint pixelSize)
    {
        face.SetPixelSize(pixelSize);

        const int atlasWidth = 512;
        const int atlasHeight = 512;
        const int padding = 1;

        var atlasPixels = new byte[atlasWidth * atlasHeight];
        Dictionary<char, AtlasGlyph> glyphs = [];

        var x = padding;
        var y = padding;
        var rowHeight = 0;

        for (char c = (char)32; c < 127; c++)
        {
            var glyph = face.LoadGlyph(c);

            if (x + glyph.Width + padding > atlasWidth)
            {
                x = padding;
                y += rowHeight + padding;
                rowHeight = 0;
            }

            if (y + glyph.Height + padding > atlasHeight)
            {
                throw new InvalidOperationException("Font atlas is too small.");
            }

            CopyGlyphToAtlas(glyph.Bitmap, glyph.Width, glyph.Height, atlasPixels, atlasWidth, x, y);

            glyphs[c] = new AtlasGlyph
            {
                U0 = x / (float)atlasWidth,
                V0 = y / (float)atlasHeight,
                U1 = (x + glyph.Width) / (float)atlasWidth,
                V1 = (y + glyph.Height) / (float)atlasHeight,

                Width = glyph.Width,
                Height = glyph.Height,
                BearingX = glyph.BearingX,
                BearingY = glyph.BearingY,
                Advance = glyph.Advance
            };

            x += glyph.Width + padding;
            rowHeight = Math.Max(rowHeight, glyph.Height);
        }

        return new FontAtlas(atlasPixels, atlasWidth, atlasHeight, glyphs);
    }

    private static void CopyGlyphToAtlas(byte[] glyphPixels, int glyphWidth, int glyphHeight, byte[] atlasPixels, int atlasWidth, int atlasX, int atlasY)
    {
        for (var row = 0; row < glyphHeight; row++)
        {
            var sourceOffset = row * glyphWidth;
            var destinationOffset = (atlasY + row) * atlasWidth + atlasX;

            Array.Copy(glyphPixels, sourceOffset, atlasPixels, destinationOffset, glyphWidth);
        }
    }
}