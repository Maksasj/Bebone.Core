using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers
{
    public class VertexBufferObject : IDisposable
    {
        private readonly uint _handle;

        public VertexBufferObject()
        {
            Handle = OpenGL.Api.GenBuffer();

        }

        public void Bind()
        {
            OpenGL.Api.BindBuffer(BufferTargetARB.ArrayBuffer, Handle);
        }

        public void Unbind()
        {
            OpenGL.Api.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        }

        public unsafe void BufferData<T>(T[] data)
        {
            fixed (void* v = &data[0])
                OpenGL.Api.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(data.Length * sizeof(T)), v, BufferUsageARB.StaticDraw);
        }

        public unsafe void BufferSubData<T>(T[] data)
        {
            fixed (void* v = &data[0])
                OpenGL.Api.BufferSubData(BufferTargetARB.ArrayBuffer, 0, (nuint)(data.Length * sizeof(T)), v);
        }

        public void Dispose()
        {
            OpenGL.Api.DeleteBuffer(Handle);
        }
    }
}
