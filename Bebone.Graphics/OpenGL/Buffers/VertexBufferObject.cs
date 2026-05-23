using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL.Buffers;

public class VertexBufferObject : IDisposable
{
    private readonly IGLContext _gl;
    private readonly uint _handle;
    private bool _disposed;
    public VertexBufferObject(IGLContext gl)
    {
        _gl = gl;

        _handle = _gl.GenBuffer();
        _disposed = false;
    }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _handle);
    }

    public void Unbind()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
    }

    public unsafe void BufferData<T>(T[] data) where T : unmanaged
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (data.Length == 0)
            throw new ArgumentException("Buffer data cannot be empty.", nameof(data));

        fixed (void* v = &data[0])
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(data.Length * sizeof(T)), v, BufferUsageARB.StaticDraw);
    }

    public unsafe void BufferSubData<T>(T[] data) where T : unmanaged
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (data.Length == 0)
            throw new ArgumentException("Buffer data cannot be empty.", nameof(data));

        fixed (void* v = &data[0])
            _gl.BufferSubData(BufferTargetARB.ArrayBuffer, 0, (nuint)(data.Length * sizeof(T)), v);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _gl.DeleteBuffer(_handle);
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}