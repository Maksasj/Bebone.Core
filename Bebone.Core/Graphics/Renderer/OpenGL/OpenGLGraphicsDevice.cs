using Silk.NET.OpenGL;
using System.Drawing;

namespace Bebone.Core.Graphics.Renderer.OpenGL;

public class OpenGLGraphicsDevice
{
    private const float _maxByteColorValue = 255.0f;
    private readonly GL _gl;

    public OpenGLGraphicsDevice(Func<string, IntPtr> procAddressLoader)
    {
        _gl = GL.GetApi(procAddressLoader);

        Api.Enable(EnableCap.DepthTest);
        Api.DepthFunc(GLEnum.Less);

        Api.Enable(EnableCap.Blend);
        Api.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    public void EnableDepthTest() => Api.Enable(EnableCap.DepthTest);
    public void DisableDepthTest() => Api.Disable(EnableCap.DepthTest);
    public void SetViewport(int x, int y, int width, int height) => Api.Viewport(x, y, (uint)width, (uint)height);
    public void ClearBuffers() => Api.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    public void ClearColor(Color color) => Api.ClearColor(color.R / _maxByteColorValue, color.G / _maxByteColorValue, color.B / _maxByteColorValue, 1.0f);

    public GL Api
    {
        get
        {
            return _gl;
        }
    }
}