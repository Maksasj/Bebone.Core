using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL;

public sealed class Texture2D : ITexture, IDisposable
{
    private readonly IGLContext _gl;
    private readonly uint _handle;

    private const int _maxTextureSlots = 16; // Todo: This can be more depending on device, 16 is minimum

    public int Width { get; }
    public int Height { get; }

    public Texture2D(IGLContext gl, byte[] data, TextureConfiguration configuration)
    {
        _gl = gl;
        Width = configuration.Width;
        Height = configuration.Height;

        _handle = CreateTexture(configuration, data);
    }

    public void Bind(uint slot)
    {
        if (slot >= _maxTextureSlots)
            throw new ArgumentOutOfRangeException(nameof(slot), $"Texture slot {slot} is out of bounds. Valid range is 0 to {_maxTextureSlots - 1}.");

        _gl.ActiveTexture(TextureUnit.Texture0 + (int)slot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Unbind() => _gl.BindTexture(TextureTarget.Texture2D, 0);

    private unsafe uint CreateTexture(TextureConfiguration configuration, byte[] data, bool isFboAttachment = false)
    {
        var created = _gl.GenTexture();

        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, created);

        _gl.TextureParameter(created, TextureParameterName.TextureWrapS, (int)configuration.STextureWrap);
        _gl.TextureParameter(created, TextureParameterName.TextureWrapT, (int)configuration.TTextureWrap);

        if (isFboAttachment)
        {
            _gl.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            _gl.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
        else
        {
            _gl.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)configuration.MinFilter);
            _gl.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)configuration.MagFilter);
        }

        fixed (byte* ptr = data)
        {
            _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)Width,
                (uint)Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }

        var isMipmapFilter = configuration.MinFilter == TextureMinFilter.LinearMipmapLinear
            || configuration.MinFilter == TextureMinFilter.NearestMipmapLinear
            || configuration.MinFilter == TextureMinFilter.LinearMipmapNearest
            || configuration.MinFilter == TextureMinFilter.NearestMipmapNearest;
        
        if (isMipmapFilter)
        {
            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }

        Unbind();

        return created;
    }

    public void Dispose()
    {
        _gl.DeleteTexture(_handle);
    }
}
