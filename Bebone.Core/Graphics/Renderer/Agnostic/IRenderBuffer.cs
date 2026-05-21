namespace Bebone.Core.Graphics.Renderer.Agnostic
{
    public interface IRenderBuffer
    {
        void Bind();
        void Unbind();

        uint GetHandle();
    }
}
