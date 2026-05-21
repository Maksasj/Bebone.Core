namespace Bebone.Core.Graphics.Renderer.Agnostic
{
    public interface IFramebuffer
    {
        void Bind();

        void Unbind();

        void AttachTexture(IColorAttachment attachment, int colorAttachmentSlot);

        void AttachRenderbuffer(IRenderBuffer renderBuffer);

        void DrawBuffers(int[] slots);
    }
}
