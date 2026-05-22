using Bebone.Graphics.Renderer.Agnostic;
using Bebone.Graphics.Renderer.OpenGL.Shaders;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.Renderer.OpenGL;

public class OpenGLFactory(GL gl) : IGraphicsFactory
{
    private readonly GL _gl = gl;

    public IShaderProgram CreateShader(string vertexShaderSource, string fragmentShaderSource) => new ShaderProgram(_gl, vertexShaderSource, fragmentShaderSource);
    public ITexture CreateTexture(int width, int height, byte[] data) => new Texture2D(_gl, width, height, data);
}