using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL;

public class OpenGLGraphicsDevice
{
    private readonly GLContext _context;

    public OpenGLGraphicsDevice(Func<string, IntPtr> procAddressLoader)
    {
        var gl = GL.GetApi(procAddressLoader);
        _context = new GLContext(gl);

        _context.Enable(EnableCap.DepthTest);
        _context.DepthFunc(GLEnum.Less);

        _context.Enable(EnableCap.Blend);
        _context.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    public IGLContext GetContext() => _context;

    public IGraphicsFactory CreateFactory() => new OpenGLFactory(_context);
}