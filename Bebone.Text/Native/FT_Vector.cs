using System.Runtime.InteropServices;

namespace Bebone.Text.Native;

[StructLayout(LayoutKind.Sequential)]
internal struct FT_Vector
{
    public int x;
    public int y;
}
