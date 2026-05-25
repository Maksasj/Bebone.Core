using System.Runtime.InteropServices;

namespace Bebone.Text;

internal static class FreeTypeInternal
{
    private const string Library = "freetype";

    [DllImport(Library)]
    public static extern int FT_Init_FreeType(out nint library);

    [DllImport(Library)]
    public static extern int FT_Done_FreeType(nint library);

    [DllImport(Library, CharSet = CharSet.Ansi)]
    public static extern int FT_New_Face(nint library, string filePathName, int faceIndex, out nint face);

    [DllImport(Library)]
    public static extern int FT_Done_Face(nint face);

    [DllImport(Library)]
    public static extern int FT_Set_Pixel_Sizes(nint face, uint pixelWidth, uint pixelHeight);

    [DllImport(Library)]
    public static extern int FT_Load_Char(nint face, uint charCode, int loadFlags);
}
