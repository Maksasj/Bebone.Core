using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL
{
    public class RenderBuffer : IRenderBuffer
    {
        private readonly uint _nativeHandle;

        public RenderBuffer(int width, int height)
        {
            OpenGL.Api.GenRenderbuffers(1, out nativeHandle);

            OpenGL.Api.BindRenderbuffer(GLEnum.Renderbuffer, nativeHandle);
            OpenGL.Api.RenderbufferStorage(GLEnum.Renderbuffer, InternalFormat.Depth24Stencil8, (uint)width, (uint)height);
        }

        public void Bind() => OpenGL.Api.BindRenderbuffer(GLEnum.Renderbuffer, nativeHandle);
        public void Unbind() => OpenGL.Api.BindRenderbuffer(GLEnum.Renderbuffer, 0);

        public uint GetHandle() => nativeHandle;
    }
}
