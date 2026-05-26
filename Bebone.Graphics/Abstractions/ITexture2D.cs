namespace Bebone.Graphics.Abstractions;

public interface ITexture2D
{
    int Width { get; }
    int Height { get; }

    void ActiveBind(uint slot);
    void Unbind();
}