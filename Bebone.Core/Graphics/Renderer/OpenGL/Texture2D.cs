using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL;

public class Texture2D : ITexture
{
    private readonly OpenGLGraphicsDevice _device;
    private readonly uint _handle;

    private readonly int _width;
    private readonly int _height;

    public unsafe Texture2D(OpenGLGraphicsDevice device, int width, int height, byte[] data)
    {
        _device = device;
        _width = width;
        _height = height;

        _handle = CreateTexture(false, TextureMinFilterType.Linear, TextureMagFilterType.Linear);
        ActivateBind(slot: 0);

        fixed (byte* ptr = data)
        {
            _device.Api.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width,
                (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }

        _device.Api.GenerateMipmap(TextureTarget.Texture2D);

        Unbind();
    }

    public void ActivateBind(int slot)
    {
        _device.Api.ActiveTexture(TextureUnit.Texture0 + (int)slot);
        _device.Api.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Unbind() => _device.Api.BindTexture(TextureTarget.Texture2D, 0);

    public int GetWidth() => _width;
    public int GetHeight() => _height;

    private uint CreateTexture(bool isFboAttachment = false, TextureMinFilterType minFilter = TextureMinFilterType.Linear, TextureMagFilterType magFilter = TextureMagFilterType.Linear)
    {
        var created = _device.Api.GenTexture();

        _device.Api.ActiveTexture(TextureUnit.Texture0);
        _device.Api.BindTexture(TextureTarget.Texture2D, created);

        _device.Api.TextureParameter(created, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        _device.Api.TextureParameter(created, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        if (isFboAttachment)
        {
            _device.Api.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            _device.Api.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
        else
        {
            _device.Api.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)minFilter);
            _device.Api.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)magFilter);
        }

        _device.Api.BindTexture(TextureTarget.Texture2D, 0);

        return created;
    }
}
