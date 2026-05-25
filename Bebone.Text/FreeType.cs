namespace Bebone.Text;

public sealed class FreeType : IDisposable
{
    private nint _handle;

    public FreeType()
    {
        var error = FreeTypeInternal.FT_Init_FreeType(out nint library);

        if (error != 0)
        {
            throw new InvalidOperationException($"Could not initialize FreeType. Error code: {error}");
        }

        _handle = library;
    }

    public FontFace CreateFontFace(string filePath)
    {
        var error = FreeTypeInternal.FT_New_Face(_handle, filePath, 0, out var face);

        if (error != 0)
        {
            throw new InvalidOperationException($"Could not load font face. Error code: {error}");
        }

        return new FontFace(face);
    }

    public void Dispose()
    {
        if (_handle == 0)
        {
            return;
        }

        var error = FreeTypeInternal.FT_Done_FreeType(_handle);
        _handle = 0;

        if (error != 0)
        {
            throw new InvalidOperationException($"Could not dispose FreeType. Error code: {error}");
        }
    }
}
