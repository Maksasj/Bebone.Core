using Silk.NET.OpenGL;
using System.Drawing;

namespace Bebone.Graphics.OpenGL;

public class OpenGLGraphicsDevice
{
    private const float _maxByteColorValue = 255.0f;
    private readonly GL _gl;

    public OpenGLGraphicsDevice(Func<string, IntPtr> procAddressLoader)
    {
        _gl = GL.GetApi(procAddressLoader);

        _gl.Enable(EnableCap.DepthTest);
        _gl.DepthFunc(GLEnum.Less);

        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    public OpenGLFactory CreateFactory() => new(new GLContext(_gl));

    public void EnableDepthTest() => _gl.Enable(EnableCap.DepthTest);
    public void DisableDepthTest() => _gl.Disable(EnableCap.DepthTest);
    public void SetViewport(int x, int y, int width, int height) => _gl.Viewport(x, y, (uint)width, (uint)height);
    public void ClearBuffers() => _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    public void ClearColor(Color color) => _gl.ClearColor(color.R / _maxByteColorValue, color.G / _maxByteColorValue, color.B / _maxByteColorValue, 1.0f);
}