using Bebone.Core.Graphics.Renderer.Mesh;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers
{
    public class VertexBufferObject : IDisposable
    {
        private readonly uint _handle;

        public VertexBufferObject()
        {
            _handle = OpenGL.Api.GenBuffer();

        }

        public void Bind()
        {
            OpenGL.Api.BindBuffer(BufferTargetARB.ArrayBuffer, _handle);
        }

        public void Unbind()
        {
            OpenGL.Api.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        }

        public unsafe void BufferData<T>(T[] data) where T : unmanaged
        {
            fixed (void* v = &data[0])
                OpenGL.Api.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(data.Length * sizeof(T)), v, BufferUsageARB.StaticDraw);
        }

        public unsafe void BufferSubData<T>(T[] data) where T : unmanaged
        {
            fixed (void* v = &data[0])
                OpenGL.Api.BufferSubData(BufferTargetARB.ArrayBuffer, 0, (nuint)(data.Length * sizeof(T)), v);
        }

        public void Dispose()
        {
            OpenGL.Api.DeleteBuffer(_handle);
        }
    }
}
