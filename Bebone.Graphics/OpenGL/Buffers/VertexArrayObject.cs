using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL.Buffers;

public class VertexArrayObject : IDisposable
{
    private readonly IGLContext _gl;
    private readonly uint _handle;
    private bool _disposed;

    public VertexArrayObject(IGLContext gl)
    {
        _gl = gl;

        _handle = _gl.GenVertexArray();
        _disposed = false;
    }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _gl.BindVertexArray(_handle);
    }
    public void Unbind()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _gl.BindVertexArray(0);
    }

    public unsafe void LinkAttribute(int index, int componentCount, VertexAttribPointerType type, int strideSize, void* offset)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _gl.VertexAttribPointer((uint)index, componentCount, type, false, (uint)strideSize, offset);
        _gl.EnableVertexAttribArray((uint)index);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!_disposed)
            {
                _gl.DeleteVertexArray(_handle);
                _disposed = true;
            }
        }
    }
}