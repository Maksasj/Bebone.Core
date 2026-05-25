namespace Bebone.Graphics.Abstractions;

public interface IGraphicsFactory
{
    public IShaderProgram CreateShader(string vertexShaderSource, string fragmentShaderSource);
    public ITexture CreateTexture(int width, int height, byte[] data);
}
