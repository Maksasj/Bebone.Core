using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.OpenGL.Buffers;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL.Mesh;

public class Mesh<T> : IMesh<T> where T : unmanaged, IVertex
{
    private readonly IGLContext _gl;

    private readonly VertexArrayObject _vao;
    private readonly VertexBufferObject _vbo;
    private readonly ElementBufferObject _ebo;

    private readonly uint _indicesCount;
    private int _vertexCount;
    private int _capacity;

    public unsafe Mesh(IGLContext gl, T[] vertices, uint[] indices)
    {
        _gl = gl;

        _indicesCount = (uint)indices.Length;
        _vertexCount = vertices.Length;
        _capacity = vertices.Length;

        _vao = new VertexArrayObject(gl);
        _vao.Bind();
        _ebo = new ElementBufferObject(gl);
        _ebo.Bind();
        _ebo.BufferData(indices);
        _vbo = new VertexBufferObject(gl);
        _vbo.Bind();
        _vbo.BufferData(vertices);

        var stride = sizeof(T);

        foreach (var bind in T.GetAttributes())
            _vao.LinkAttribute(bind.Index, bind.Size, bind.Type.ToOpenGL(), stride, (void*)bind.Offset);

        _vao.Unbind();
        _vbo.Unbind();
        _ebo.Unbind();
    }

    public void Bind() => _vao.Bind();

    public unsafe void DrawTriangles()
        => _gl.DrawElements(PrimitiveType.Triangles, _indicesCount, DrawElementsType.UnsignedInt, null);

    public void DrawLines()
        => _gl.DrawArrays(PrimitiveType.Lines, 0, (uint)_vertexCount);

    public void DrawArrays()
        => _gl.DrawArrays(GLEnum.Triangles, 0, (uint)_vertexCount);

    public void Dispose()
    {
        _vbo.Dispose();
        _ebo.Dispose();
        _vao.Dispose();
    }

    // TODO: NOTE EBO IS NOT UPDATED HERE IF THE INDICES CHANGE
    public void UpdateVertices(T[] verticies)
    {
        if (verticies == null || verticies.Length == 0) return;

        _vbo.Bind();

        if (verticies.Length <= _capacity)
            _vbo.BufferSubData(verticies);
        else
            _vbo.BufferData(verticies);
        _capacity = verticies.Length;

        _vertexCount = verticies.Length;

        _vbo.Unbind();
    }
}
