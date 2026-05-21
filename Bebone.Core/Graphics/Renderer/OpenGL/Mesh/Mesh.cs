using Bebone.Core.Graphics.Renderer.Mesh;
using Bebone.Core.Graphics.Renderer.OpenGL.Buffers;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Mesh
{
    public class Mesh<T> : IMesh<T> where T : unmanaged, IVertex
    {
        private readonly VertexArrayObject _vao;
        private readonly VertexBufferObject _vbo;
        private readonly ElementBufferObject _ebo;

        private readonly uint _indicesCount;
        private int _vertexCount;
        private int _capacity;

        public unsafe Mesh(T[] vertices, uint[] indices)
        {
            _indicesCount = (uint)indices.Length;
            _vertexCount = vertices.Length;
            _capacity = vertices.Length;

            _vao = new VertexArrayObject();
            _vao.Bind();

            _ebo = new ElementBufferObject();
            _ebo.Bind();
            _ebo.BufferData(indices);

            _vbo = new VertexBufferObject();
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
            => OpenGL.Api.DrawElements(PrimitiveType.Triangles, _indicesCount, DrawElementsType.UnsignedInt, null);

        public void DrawLines()
            => OpenGL.Api.DrawArrays(PrimitiveType.Lines, 0, (uint)_vertexCount);

        public void DrawArrays()
            => OpenGL.Api.DrawArrays(GLEnum.Triangles, 0, (uint)_vertexCount);

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
}
