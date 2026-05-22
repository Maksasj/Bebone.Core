using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL.Buffers;

public class ElementBufferObject : IDisposable
{
    private readonly IGLContext _gl;
    private readonly uint _handle;
    private bool _disposed;

    public ElementBufferObject(IGLContext gl)
    {
        _gl = gl;

        _handle = _gl.GenBuffer();
        _disposed = false;
    }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _handle);
    }

    public void Unbind()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
    }

    public unsafe void BufferData<T>(T[] data) where T : unmanaged
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        fixed (void* i = &data[0])
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(data.Length * sizeof(T)), i, BufferUsageARB.StaticDraw);
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