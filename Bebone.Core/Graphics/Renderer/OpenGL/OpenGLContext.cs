using Bebone.Core.Graphics.Renderer.Agnostic;
using Bebone.Core.Graphics.Renderer.OpenGL.Shaders;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using System.Drawing;

namespace Bebone.Core.Graphics.Renderer.OpenGL
{
    public class OpenGLContext : IGraphicsApiContext
    {
        private readonly Glfw _glfw;

        public OpenGLContext(Glfw glfw)
        {
            _glfw = glfw;

            OpenGL.Initialize(GL.GetApi(glfw.GetProcAddress));

            OpenGL.Api.Enable(EnableCap.DepthTest);
            OpenGL.Api.DepthFunc(GLEnum.Less);

            OpenGL.Api.Enable(EnableCap.Blend);
            OpenGL.Api.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void EnableDepthTest() => OpenGL.Api.Enable(EnableCap.DepthTest);
        public void DisableDepthTest() => OpenGL.Api.Disable(EnableCap.DepthTest);
        public void SetViewport(int x, int y, int width, int height) => OpenGL.Api.Viewport(x, y, (uint)width, (uint)height);
        public void ClearBuffers() => OpenGL.Api.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        public void ClearColor(Color color) => OpenGL.Api.ClearColor(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 1.0f);

        public IShaderProgram CreateShader(string vertexShaderPath, string fragmentShaderPath) => new ShaderProgram(File.ReadAllText(vertexShaderPath), File.ReadAllText(fragmentShaderPath));
        public ITexture CreateTexture(string filePath) => new Texture2D(filePath);
        public IRenderBuffer CreateRenderBuffer(int width, int height) => new RenderBuffer(width, height);
        public IFramebuffer CreateFramebuffer() => new Framebuffer();

        public float GetTime() => (float)_glfw.GetTime();
    }
}
