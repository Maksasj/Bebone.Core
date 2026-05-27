using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL;

public sealed class Texture2D : ITexture2D, IDisposable
{
    private readonly IGLContext _gl;
    private readonly uint _handle;

    private const int _maxTextureSlots = 16; // Todo: This can be more depending on device, 16 is minimum

    public int Width { get; }
    public int Height { get; }

    private bool _disposed;

    public Texture2D(IGLContext gl, byte[] data, TextureConfiguration configuration)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(configuration.Width, nameof(configuration.Width));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(configuration.Height, nameof(configuration.Height));

        _gl = gl;
        Width = configuration.Width;
        Height = configuration.Height;

        _handle = CreateTexture(configuration, data);
    }

    public Texture2D(IGLContext gl, int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width, nameof(width));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height, nameof(height));

        _gl = gl;
        Width = width;
        Height = height;

        _handle = CreateEmptyTexture(width, height);
    }

    public void ActiveBind(uint slot)
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(Texture2D));

        if (slot >= _maxTextureSlots)
            throw new ArgumentOutOfRangeException(nameof(slot), $"Texture slot {slot} is out of bounds. Valid range is 0 to {_maxTextureSlots - 1}.");

        _gl.ActiveTexture(TextureUnit.Texture0 + (int)slot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Unbind()
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(Texture2D));

        _gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    private unsafe uint CreateEmptyTexture(int width, int height)
    {
        var created = _gl.GenTexture();

        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, created);

        _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width, (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);

        _gl.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        _gl.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        Unbind();

        return created;
    }

    private unsafe uint CreateTexture(TextureConfiguration configuration, byte[] data)
    {
        var created = _gl.GenTexture();

        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, created);

        _gl.TextureParameter(created, TextureParameterName.TextureWrapS, (int)configuration.STextureWrap);
        _gl.TextureParameter(created, TextureParameterName.TextureWrapT, (int)configuration.TTextureWrap);

        _gl.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)configuration.MinFilter);
        _gl.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)configuration.MagFilter);

        fixed (byte* ptr = data)
        {
            _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)Width,
                (uint)Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }

        var isMipmapFilter = configuration.MinFilter == TextureMinFilter.LinearMipmapLinear
            || configuration.MinFilter == TextureMinFilter.NearestMipmapLinear
            || configuration.MinFilter == TextureMinFilter.LinearMipmapNearest
            || configuration.MinFilter == TextureMinFilter.NearestMipmapNearest;

        if (isMipmapFilter && configuration.CreateMipmap)
        {
            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }

        Unbind();

        return created;
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _gl.DeleteTexture(_handle);

        _disposed = true;
    }
}
