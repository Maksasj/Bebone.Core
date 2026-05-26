namespace Bebone.Graphics.Abstractions;

public interface ITexture
{
    int Width { get; }
    int Height { get; }

    void Bind(uint slot);
    void Unbind();
}