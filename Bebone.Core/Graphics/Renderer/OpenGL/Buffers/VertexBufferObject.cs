using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Buffers
{
    public class VertexBufferObject : IDisposable
    {
        private readonly OpenGLGraphicsDevice _device;
        private readonly uint _handle;

        public VertexBufferObject(OpenGLGraphicsDevice device)
        {
            _device = device;

            _handle = _device.Api.GenBuffer();
        }

        public void Bind()
        {
            _device.Api.BindBuffer(BufferTargetARB.ArrayBuffer, _handle);
        }

        public void Unbind()
        {
            _device.Api.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        }

        public unsafe void BufferData<T>(T[] data) where T : unmanaged
        {
            fixed (void* v = &data[0])
                _device.Api.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(data.Length * sizeof(T)), v, BufferUsageARB.StaticDraw);
        }

        public unsafe void BufferSubData<T>(T[] data) where T : unmanaged
        {
            fixed (void* v = &data[0])
                _device.Api.BufferSubData(BufferTargetARB.ArrayBuffer, 0, (nuint)(data.Length * sizeof(T)), v);
        }

        public void Dispose()
        {
            _device.Api.DeleteBuffer(_handle);
        }
    }
}
