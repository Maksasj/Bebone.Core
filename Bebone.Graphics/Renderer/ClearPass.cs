using Bebone.Graphics.Abstractions;
using Bebone.Graphics.RenderGraph;
using System.Drawing;

namespace Bebone.Graphics.Renderer;

public class ClearPass(IGLContext context, Color clearColor) : IPass
{
    private readonly IGLContext _context = context;
    private Color _clearColor = clearColor;

    public ClearPass(IGLContext context) : this(context, Color.Black) { }

    public void Compile(IReadOnlyDictionary<string, object> resources)
    {

    }

    public void Execute(FrameData frameData)
    {
        _context.ClearColor(_clearColor);
        _context.ClearBuffers();
    }

    public void SetClearColor(Color clearColor) => _clearColor = clearColor;
}