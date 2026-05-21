using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL
{
    public class RenderBuffer : IRenderBuffer
    {
        private readonly uint _handle;

        public RenderBuffer(int width, int height)
        {
            OpenGL.Api.GenRenderbuffers(1, out _handle);

            OpenGL.Api.BindRenderbuffer(GLEnum.Renderbuffer, _handle);
            OpenGL.Api.RenderbufferStorage(GLEnum.Renderbuffer, InternalFormat.Depth24Stencil8, (uint)width, (uint)height);
        }

        public void Bind() => OpenGL.Api.BindRenderbuffer(GLEnum.Renderbuffer, _handle);
        public void Unbind() => OpenGL.Api.BindRenderbuffer(GLEnum.Renderbuffer, 0);

        public uint GetHandle() => _handle;
    }
}
