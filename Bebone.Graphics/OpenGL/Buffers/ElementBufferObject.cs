using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL.Buffers;

public class ElementBufferObject : IDisposable
{
    private readonly GL _gl;
    private readonly uint _handle;

    public ElementBufferObject(GL gl)
    {
        _gl = gl;

        _handle = _gl.GenBuffer();
    }

    public void Bind() => _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _handle);

    public void Unbind() => _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

    public void BufferData<T>(T[] data) where T : unmanaged
    {
        fixed (void* i = &data[0])
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(data.Length * sizeof(T)), i, BufferUsageARB.StaticDraw);
    }

    public void Dispose()
    {
        _gl.DeleteBuffer(_handle);
    }
}