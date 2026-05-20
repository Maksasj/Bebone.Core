using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL
{
    public class Framebuffer : IFramebuffer
    {
        private readonly uint framebuffer;

        public Framebuffer()
        {
            OpenGL.Api.GenFramebuffers(1, out framebuffer);
        }

        public void Bind() => OpenGL.Api.BindFramebuffer(GLEnum.Framebuffer, framebuffer);
        public void Unbind() => OpenGL.Api.BindFramebuffer(GLEnum.Framebuffer, 0);

        public void AttachTexture(IColorAttachment attachment, ColorAttachmentSlot colorAttachment)
        {
            Bind();

            OpenGL.Api.FramebufferTexture2D(GLEnum.Framebuffer, GLEnum.ColorAttachment0 + (int)colorAttachment, TextureTarget.Texture2D, attachment.GetHandle(), 0);

            if (OpenGL.Api.CheckFramebufferStatus(GLEnum.Framebuffer) != GLEnum.FramebufferComplete)
                throw new InvalidOperationException("ERROR::FRAMEBUFFER:: Framebuffer is not complete!");

            Unbind();
        }
        public void AttachRenderbuffer(IRenderBuffer renderBuffer)
        {
            Bind();

            OpenGL.Api.FramebufferRenderbuffer(GLEnum.Framebuffer, GLEnum.DepthStencilAttachment, GLEnum.Renderbuffer, renderBuffer.GetHandle());

            if (OpenGL.Api.CheckFramebufferStatus(GLEnum.Framebuffer) != GLEnum.FramebufferComplete)
                throw new InvalidOperationException("ERROR::FRAMEBUFFER:: Framebuffer is not complete!");

            Unbind();
        }

        public void DrawBuffers(ColorAttachmentSlot[] buffers)
        {
            var attachments = buffers.Select(b => GLEnum.ColorAttachment0 + (int)b).ToArray();
            OpenGL.Api.DrawBuffers((uint)attachments.Length, attachments);
        }
    }
}
