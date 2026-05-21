using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers
{
    public class VertexArrayObject : IDisposable
    {
        private readonly uint _handle;

        public VertexArrayObject()
        {
            _handle = OpenGL.Api.GenVertexArray();
        }

        public void Bind()
        {
            OpenGL.Api.BindVertexArray(_handle);
        }

        public void Unbind()
        {
            OpenGL.Api.BindVertexArray(0);
        }

        public unsafe void LinkAttribute(int index, int componentCount, VertexAttribPointerType type, int strideSize, void* offset)
        {
            OpenGL.Api.VertexAttribPointer((uint)index, componentCount, type, false, (uint)strideSize, offset);
            OpenGL.Api.EnableVertexAttribArray((uint)index);
        }

        public void Dispose()
        {
            OpenGL.Api.DeleteVertexArray(_handle);
        }
    }
}
