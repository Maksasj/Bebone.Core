using System.Drawing;

namespace Bebone.Core.Graphics.Renderer.Agnostic
{
    public interface IGraphicsApiContext
    {
        public IShaderProgram CreateShader(string vertexShaderPath, string fragmentShaderPath);
        public IFramebuffer CreateFramebuffer();
        public ITexture CreateTexture(string filePath);
        public IRenderBuffer CreateRenderBuffer(int width, int height);

        public void SetViewport(int x, int y, int width, int height);

        void ClearColor(Color color);
        void EnableDepthTest();
        void DisableDepthTest();
        float GetTime();
        void ClearBuffers();
    }
}
