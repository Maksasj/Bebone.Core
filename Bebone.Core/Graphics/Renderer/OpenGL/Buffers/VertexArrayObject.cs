using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers;

public class VertexArrayObject : IDisposable
{
    private readonly OpenGLGraphicsDevice _device;
    private readonly uint _handle;

    public VertexArrayObject(OpenGLGraphicsDevice device)
    {
        _device = device;

        _handle = _device.Api.GenVertexArray();
    }

    public void Bind() => _device.Api.BindVertexArray(_handle);

    public void Unbind() => _device.Api.BindVertexArray(0);

    public unsafe void LinkAttribute(int index, int componentCount, VertexAttribPointerType type, int strideSize, void* offset)
    {
        _device.Api.VertexAttribPointer((uint)index, componentCount, type, false, (uint)strideSize, offset);
        _device.Api.EnableVertexAttribArray((uint)index);
    }

    public void Dispose()
    {
        _device.Api.DeleteVertexArray(_handle);
    }
}