namespace Bebone.Graphics.Abstractions;

public interface ITexture
{
    int Width { get; }
    int Height { get; }

    void ActivateBind(int slot);
    void Unbind();
}