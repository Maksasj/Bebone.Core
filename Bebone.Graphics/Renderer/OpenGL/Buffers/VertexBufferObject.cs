using Silk.NET.OpenGL;

namespace Bebone.Graphics.Renderer.OpenGL.Buffers;

public class VertexBufferObject : IDisposable
{
    private readonly GL _gl;
    private readonly uint _handle;

    public VertexBufferObject(GL gl)
    {
        _gl = gl;

        _handle = _gl.GenBuffer();
    }

    public void Bind() => _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _handle);

    public void Unbind() => _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);

    public unsafe void BufferData<T>(T[] data) where T : unmanaged
    {
        fixed (void* v = &data[0])
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(data.Length * sizeof(T)), v, BufferUsageARB.StaticDraw);
    }

    public unsafe void BufferSubData<T>(T[] data) where T : unmanaged
    {
        fixed (void* v = &data[0])
            _gl.BufferSubData(BufferTargetARB.ArrayBuffer, 0, (nuint)(data.Length * sizeof(T)), v);
    }

    public void Dispose()
    {
        _gl.DeleteBuffer(_handle);
    }
}