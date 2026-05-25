namespace Bebone.Text;

public static class FontAtlasBuilder
{
    public static FontAtlas BuildAscii(FontFace face, uint pixelSize, int width, int height, int padding)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width));
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height));
        }

        if (padding < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(padding));
        }

        face.SetPixelSize(pixelSize);

        var atlasPixels = new byte[width * height];
        Dictionary<char, AtlasGlyph> glyphs = [];

        var x = padding;
        var y = padding;
        var rowHeight = 0;

        const int asciiStart = 32;
        const int asciiEnd = 127;

        for (char c = (char)asciiStart; c < asciiEnd; c++)
        {
            var glyph = face.LoadGlyph(c);

            if (x + glyph.Width + padding > width)
            {
                x = padding;
                y += rowHeight + padding;
                rowHeight = 0;
            }

            if (y + glyph.Height + padding > height)
            {
                throw new InvalidOperationException("Font atlas is too small.");
            }

            CopyGlyphToAtlas(glyph.Bitmap, glyph.Width, glyph.Height, atlasPixels, width, x, y);

            glyphs[c] = new AtlasGlyph
            {
                U0 = x / (float)width,
                V0 = y / (float)height,
                U1 = (x + glyph.Width) / (float)width,
                V1 = (y + glyph.Height) / (float)height,

                Width = glyph.Width,
                Height = glyph.Height,
                BearingX = glyph.BearingX,
                BearingY = glyph.BearingY,
                Advance = glyph.Advance
            };

            x += glyph.Width + padding;
            rowHeight = Math.Max(rowHeight, glyph.Height);
        }

        return new FontAtlas(atlasPixels, width, height, glyphs);
    }

    private static void CopyGlyphToAtlas(byte[] glyphPixels, int glyphWidth, int glyphHeight, byte[] atlasPixels, int atlasWidth, int atlasX, int atlasY)
    {
        if (glyphWidth == 0 || glyphHeight == 0)
        {
            return;
        }

        for (var row = 0; row < glyphHeight; row++)
        {
            var sourceOffset = row * glyphWidth;
            var destinationOffset = (atlasY + row) * atlasWidth + atlasX;

            Array.Copy(glyphPixels, sourceOffset, atlasPixels, destinationOffset, glyphWidth);
        }
    }
}