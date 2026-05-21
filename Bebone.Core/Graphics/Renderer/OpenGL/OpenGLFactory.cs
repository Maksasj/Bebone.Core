using Bebone.Core.Graphics.Renderer.Agnostic;
using Bebone.Core.Graphics.Renderer.OpenGL.Shaders;

namespace Bebone.Core.Graphics.Renderer.OpenGL;

public class OpenGLFactory(OpenGLGraphicsDevice device) : IGraphicsFactory
{
    private readonly OpenGLGraphicsDevice _device = device;

    public IShaderProgram CreateShader(string vertexShaderSource, string fragmentShaderSource) => new ShaderProgram(_device, vertexShaderSource, fragmentShaderSource);
    public ITexture CreateTexture(int width, int height, byte[] data) => new Texture2D(_device, width, height, data);
}