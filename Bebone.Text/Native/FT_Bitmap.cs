using System.Runtime.InteropServices;

namespace Bebone.Text.Native;

[StructLayout(LayoutKind.Sequential)]
internal struct FT_Bitmap
{
    public uint rows;
    public uint width;
    public int pitch;
    public nint buffer;
    public ushort numGrays;
    public byte pixelMode;
    public byte paletteMode;
    public nint palette;
}
