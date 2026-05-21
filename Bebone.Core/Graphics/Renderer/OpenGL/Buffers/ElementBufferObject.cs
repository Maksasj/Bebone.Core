using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers;

public class ElementBufferObject : IDisposable
{
    private readonly OpenGLGraphicsDevice _device;
    private readonly uint _handle;

    public ElementBufferObject(OpenGLGraphicsDevice device)
    {
        _device = device;

        _handle = _device.Api.GenBuffer();
    }

    public void Bind() => _device.Api.BindBuffer(BufferTargetARB.ElementArrayBuffer, _handle);

    public void Unbind() => _device.Api.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

    public unsafe void BufferData<T>(T[] data) where T : unmanaged
    {
        fixed (void* i = &data[0])
            _device.Api.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(data.Length * sizeof(T)), i, BufferUsageARB.StaticDraw);
    }

    public void Dispose()
    {
        _device.Api.DeleteBuffer(_handle);
    }
}