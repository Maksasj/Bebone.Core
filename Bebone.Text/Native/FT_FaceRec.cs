using System.Runtime.InteropServices;

namespace Bebone.Text.Native;

[StructLayout(LayoutKind.Sequential)]
internal struct FT_FaceRec
{
    public int numberOfFaces;
    public int faceIndex;

    public int faceFlags;
    public int styleFlags;

    public int numberOfGlyphs;

    public nint familyName;
    public nint styleName;

    public int numberOfFixedSizes;
    public nint availableSizes;

    public int numberOfCharmaps;
    public nint charmaps;

    public FT_Generic generic;

    public FT_BBox bbox;

    public ushort unitsPerEm;
    public short ascender;
    public short descender;
    public short height;

    public short maxAdvanceWidth;
    public short maxAdvanceHeight;

    public short underlinePosition;
    public short underlineThickness;

    public nint glyph;
}
