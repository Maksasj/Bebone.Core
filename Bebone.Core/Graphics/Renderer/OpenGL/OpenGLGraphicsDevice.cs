using Silk.NET.OpenGL;
using System.Drawing;

namespace Bebone.Core.Graphics.Renderer.OpenGL
{
    public class OpenGLGraphicsDevice(GL gl)
    {
        private const float _maxByteColorValue = 255.0f;
        private readonly GL _api = gl;

        public GL Api
        {
            get
            {
                if (_api == null)
                    throw new InvalidOperationException("OpenGL API not initialized. Call OpenGL.Initialize(GL gl) before using any OpenGL functionality.");

                return _api;
            }
        }

        public void EnableDepthTest() => Api.Enable(EnableCap.DepthTest);
        public void DisableDepthTest() => Api.Disable(EnableCap.DepthTest);
        public void SetViewport(int x, int y, int width, int height) => Api.Viewport(x, y, (uint)width, (uint)height);
        public void ClearBuffers() => Api.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        public void ClearColor(Color color) => Api.ClearColor(color.R / _maxByteColorValue, color.G / _maxByteColorValue, color.B / _maxByteColorValue, 1.0f);

    }
}
