namespace Bebone.Core.Graphics.Renderer.Agnostic
{
    public interface ITexture
    {
        void ActivateBind(ColorAttachmentSlot slot);
        void Unbind();

        int GetWidth();
        int GetHeight();
        int GetDepth();
    }
}
