using System.Runtime.InteropServices;

namespace Bebone.Text.Native;

[StructLayout(LayoutKind.Sequential)]
internal struct FT_Generic
{
    public nint data;
    public nint finalizer;
}
