using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers
{
    public class ElementBufferObject
    {
        public readonly uint Handle;

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

        public unsafe void BufferData<T>(T[] data)
        {
            fixed (void* i = &data[0])
                OpenGL.Api.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(data.Length * sizeof(T)), i, BufferUsageARB.StaticDraw); //Setting buffer data.
        }

        public void Dispose()
        {
            OpenGL.Api.DeleteBuffer(Handle);
        }
    }
}
