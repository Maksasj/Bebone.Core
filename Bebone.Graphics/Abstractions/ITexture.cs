namespace Bebone.Graphics.Abstractions;

public interface ITexture
{
    void ActivateBind(int slot);
    void Unbind();

    int GetWidth();
    int GetHeight();
}