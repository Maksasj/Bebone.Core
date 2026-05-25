using System.Runtime.InteropServices;

namespace Bebone.Text.Native;

[StructLayout(LayoutKind.Sequential)]
internal struct FT_Glyph_Metrics
{
    public int width;
    public int height;

    public int horiBearingX;
    public int horiBearingY;
    public int horiAdvance;

    public int vertBearingX;
    public int vertBearingY;
    public int vertAdvance;
}
