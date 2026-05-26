namespace Bebone.Graphics.Abstractions;

public interface ITexture
{
    int Width { get; }
    int Height { get; }

    void ActiveBind(uint slot);
    void Unbind();
}