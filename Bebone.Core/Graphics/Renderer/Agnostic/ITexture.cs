namespace Bebone.Core.Graphics.Renderer.Agnostic;

public interface ITexture
{
    void ActivateBind(int slot);
    void Unbind();

    int GetWidth();
    int GetHeight();
}