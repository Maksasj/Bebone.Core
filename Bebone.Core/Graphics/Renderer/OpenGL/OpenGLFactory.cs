using Bebone.Core.Graphics.Renderer.Agnostic;
using Bebone.Core.Graphics.Renderer.OpenGL.Shaders;
using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL;

public class OpenGLFactory : IGraphicsFactory
{
    private readonly OpenGLGraphicsDevice _device;

    public OpenGLFactory(OpenGLGraphicsDevice device)
    {
        _device = device;


        /*
        OpenGLGraphicsDevice.Initialize(GL.GetApi(glfw.GetProcAddress));
        */

        _device.Api.Enable(EnableCap.DepthTest);
        _device.Api.DepthFunc(GLEnum.Less);

        _device.Api.Enable(EnableCap.Blend);
        _device.Api.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    public IShaderProgram CreateShader(string vertexShaderPath, string fragmentShaderPath) => new ShaderProgram(_device, File.ReadAllText(vertexShaderPath), File.ReadAllText(fragmentShaderPath));
    public ITexture CreateTexture(int width, int height, byte[] data) => new Texture2D(_device, width, height, data);
}