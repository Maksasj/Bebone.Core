using Bebone.Text.Native;
using System.Runtime.InteropServices;

namespace Bebone.Text;

public sealed class FontFace(nint faceHandle) : IDisposable
{
    private nint _handle = faceHandle;

    // Automatically converts glyph to bitmap after the glyph loading
    // https://freetype.org/freetype2/docs/reference/ft2-glyph_retrieval.html#ft_load_xxx
    private const int _renderLoad = 0x4;

    public void SetPixelSize(uint size)
    {
        SetPixelSize(0, size);
    }

    public void SetPixelSize(uint width, uint height)
    {
        ThrowIfDisposed();

        var error = FreeTypeInternal.FT_Set_Pixel_Sizes(_handle, width, height);

        if (error != 0)
        {
            throw new InvalidOperationException($"Could not set font pixel size. Error code: {error}");
        }
    }

    public GlyphData LoadGlyph(char character)
    {
        ThrowIfDisposed();

        var error = FreeTypeInternal.FT_Load_Char(_handle, character, _renderLoad);

        if (error != 0)
        {
            throw new InvalidOperationException($"Could not load glyph '{character}'. Error code: {error}");
        }

        var face = Marshal.PtrToStructure<FT_FaceRec>(_handle);
        var glyph = Marshal.PtrToStructure<FT_GlyphSlotRec>(face.glyph);

        var rawBitmap = glyph.bitmap;
        var bitmap = new byte[rawBitmap.width * rawBitmap.rows];

        if (rawBitmap.buffer != 0 && bitmap.Length > 0)
        {
            Marshal.Copy(rawBitmap.buffer, bitmap, startIndex: 0, bitmap.Length);
        }

        var advance = FreeTypeHelper.From26Dot6((int)glyph.advance.x);
        return new GlyphData(bitmap, (int)rawBitmap.width, (int)rawBitmap.rows, glyph.bitmapLeft, glyph.bitmapTop, advance);
    }

    public void Dispose()
    {
        if (_handle == 0)
        {
            return;
        }

        var error = FreeTypeInternal.FT_Done_Face(_handle);
        _handle = 0;

        if (error != 0)
        {
            throw new InvalidOperationException($"Could not dispose font face. Error code: {error}");
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == 0)
        {
            throw new ObjectDisposedException(nameof(FontFace));
        }
    }
}
