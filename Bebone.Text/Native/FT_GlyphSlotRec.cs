using System.Runtime.InteropServices;

namespace Bebone.Text.Native;

[StructLayout(LayoutKind.Sequential)]
internal struct FT_GlyphSlotRec
{
    public nint library;
    public nint face;
    public nint next;
    public uint glyphIndex;
    public FT_Generic generic;

    public FT_Glyph_Metrics metrics;
    public int linearHoriAdvance;
    public int linearVertAdvance;
    public FT_Vector advance;

    public uint format;

    public FT_Bitmap bitmap;
    public int bitmapLeft;
    public int bitmapTop;
}
