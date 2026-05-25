using System.Runtime.InteropServices;

namespace Bebone.Text.Native;

[StructLayout(LayoutKind.Sequential)]
internal struct FT_BBox
{
    public int xMin;
    public int yMin;
    public int xMax;
    public int yMax;
}
