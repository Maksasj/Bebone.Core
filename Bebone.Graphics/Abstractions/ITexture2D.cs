namespace Bebone.Graphics.Abstractions;

public interface ITexture2D
{
    void ActivateBind(int slot);
    void Unbind();

    int GetWidth();
    int GetHeight();
}