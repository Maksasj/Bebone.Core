namespace Bebone.Core.Graphics.Renderer.Agnostic
{
    public interface IFramebuffer
    {
        void Bind();

        void Unbind();

        void AttachTexture(IColorAttachment attachment, ColorAttachmentSlot colorAttachment);

        void AttachRenderbuffer(IRenderBuffer renderBuffer);

        void DrawBuffers(ColorAttachmentSlot[] buffers);
    }
}
