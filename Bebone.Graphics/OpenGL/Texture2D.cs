using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL;

public class Texture2D : ITexture
{
    private readonly IGLContext _gl;
    private readonly uint _handle;

    private readonly int _width;
    private readonly int _height;

    private const int _maxTextureSlots = 16; // Todo: This can be more depending on device, 16 is minimum

    public unsafe Texture2D(IGLContext gl, int width, int height, byte[] data)
    {
        _gl = gl;
        _width = width;
        _height = height;

        _handle = CreateTexture(false, TextureMinFilterType.Linear, TextureMagFilterType.Linear);
        ActivateBind(slot: 0);

        fixed (byte* ptr = data)
        {
            _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width,
                (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }

        _gl.GenerateMipmap(TextureTarget.Texture2D);

        Unbind();
    }

    public void ActivateBind(int slot)
    {
        if (slot < 0 || slot >= _maxTextureSlots)
            throw new ArgumentOutOfRangeException(nameof(slot), $"Texture slot {slot} is out of bounds. Valid range is 0 to {_maxTextureSlots - 1}.");

        _gl.ActiveTexture(TextureUnit.Texture0 + slot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Unbind() => _gl.BindTexture(TextureTarget.Texture2D, 0);

    public int GetWidth() => _width;
    public int GetHeight() => _height;

    private uint CreateTexture(bool isFboAttachment = false, TextureMinFilterType minFilter = TextureMinFilterType.Linear, TextureMagFilterType magFilter = TextureMagFilterType.Linear)
    {
        var created = _gl.GenTexture();

        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, created);

        _gl.TextureParameter(created, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        _gl.TextureParameter(created, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        if (isFboAttachment)
        {
            _gl.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            _gl.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
        else
        {
            _gl.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)minFilter);
            _gl.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)magFilter);
        }

        _gl.BindTexture(TextureTarget.Texture2D, 0);

        return created;
    }
}
