using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers
{
    public class ElementBufferObject
    {
        private readonly uint _handle;

        public ElementBufferObject()
        {
            Handle = OpenGL.Api.GenBuffer();
        }

        public void Bind()
        {
            OpenGL.Api.BindBuffer(BufferTargetARB.ElementArrayBuffer, Handle);
        }

        public void Unbind()
        {
            OpenGL.Api.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }

        public unsafe void BufferData<T>(T[] data) where T : unmanaged
        {
            fixed (void* i = &data[0])
                OpenGL.Api.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(data.Length * sizeof(T)), i, BufferUsageARB.StaticDraw);
        }

        public void Dispose()
        {
            OpenGL.Api.DeleteBuffer(Handle);
        }
    }
}
