namespace Bebone.Graphics.Abstractions;

public interface IGraphicsFactory
{
    public IShaderProgram CreateShader(string vertexShaderPath, string fragmentShaderPath);
    public ITexture CreateTexture(int width, int height, byte[] data);
}
