using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL.Buffers;

public class VertexArrayObject : IDisposable
{
    private readonly IGLContext _gl;
    private readonly uint _handle;

    public VertexArrayObject(IGLContext gl)
    {
        _gl = gl;

        _handle = _gl.GenVertexArray();
    }

    public void Bind() => _gl.BindVertexArray(_handle);

    public void Unbind() => _gl.BindVertexArray(0);

    public unsafe void LinkAttribute(int index, int componentCount, VertexAttribPointerType type, int strideSize, void* offset)
    {
        _gl.VertexAttribPointer((uint)index, componentCount, type, false, (uint)strideSize, offset);
        _gl.EnableVertexAttribArray((uint)index);
    }

    public void Dispose()
    {
        _gl.DeleteVertexArray(_handle);
    }
}