namespace Bebone.Text;

public sealed record FontAtlas(byte[] Pixels, int Width, int Height, IReadOnlyDictionary<char, AtlasGlyph> Glyphs);