namespace Bebone.Text;

public readonly record struct GlyphData(byte[] Bitmap, int Width, int Height, int BearingX, int BearingY, int Advance);
