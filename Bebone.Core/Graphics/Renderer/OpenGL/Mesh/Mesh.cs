using Bebone.Core.Graphics.Renderer.Mesh;
using Bebone.Core.Graphics.Renderer.OpenGL.Buffers;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Mesh
{
    public class Mesh<T> : IMesh<T> where T : IVertex
    {
        private readonly VertexArrayObject vao;
        private readonly VertexBufferObject vbo;
        private readonly ElementBufferObject ebo;

        private readonly uint indicesCount;
        private int vertexCount;
        private int capacity;

        public unsafe Mesh(T[] vertices, uint[] indices)
        {
            indicesCount = (uint)indices.Length;
            vertexCount = vertices.Length;
            capacity = vertices.Length;

            vao = new VertexArrayObject();
            vao.Bind();

            ebo = new ElementBufferObject();
            ebo.Bind();
            ebo.BufferData(indices);

            vbo = new VertexBufferObject();
            vbo.Bind();
            vbo.BufferData(vertices);

            var stride = sizeof(T);

            foreach (var bind in T.GetAttributes())
                vao.LinkAttribute(bind.Index, bind.Size, bind.Type.ToOpenGL(), stride, (void*)bind.Offset);

            vao.Unbind();
            vbo.Unbind();
            ebo.Unbind();
        }

        public void Bind() => vao.Bind();

        public unsafe void DrawTriangles()
            => OpenGL.Api.DrawElements(PrimitiveType.Triangles, indicesCount, DrawElementsType.UnsignedInt, null);

        public void DrawLines()
            => OpenGL.Api.DrawArrays(PrimitiveType.Lines, 0, (uint)vertexCount);

        public void DrawArrays()
            => OpenGL.Api.DrawArrays(GLEnum.Triangles, 0, (uint)vertexCount);

        public void Dispose()
        {
            vbo.Dispose();
            ebo.Dispose();
            vao.Dispose();
        }

        // TODO: NOTE EBO IS NOT UPDATED HERE IF THE INDICES CHANGE
        public void UpdateVertices(T[] verticies)
        {
            if (verticies == null || verticies.Length == 0) return;

            vbo.Bind();

            if (verticies.Length <= capacity)
                vbo.BufferSubData(verticies);
            else
                vbo.BufferData(verticies);
            capacity = verticies.Length;

            vertexCount = verticies.Length;

            vbo.Unbind();
        }
    }
}
