using Bebone.Graphics.Abstractions;
using Bebone.Graphics.RenderGraph;
using Bebone.Math;

namespace Bebone.Graphics.Renderer;

public class Renderer
{
    private readonly IGLContext _context;
    private readonly IGraphicsFactory _factory;
    public Vector2Int _targetViewportSize;

    private FrameGraph _frameGraph;

    public ClearPass ClearPass { get; private set; }
    public RenderQueuePass MainPass { get; private set; }
    public RenderQueuePass UiPass { get; private set; }

    public Renderer(Vector2Int targetViewportSize, IGLContext context, IGraphicsFactory factory)
    {
        _context = context;
        _factory = factory;
        _targetViewportSize = targetViewportSize;

        var shaderProgram = _factory.CreateShader(DefaultShaderConstants.DefaultVertexShader, DefaultShaderConstants.DefaultFragmentShader);

        ClearPass = new ClearPass(_context);
        MainPass = new RenderQueuePass(_context, shaderProgram, enableDepthTest: true);
        UiPass = new RenderQueuePass(_context, shaderProgram, enableDepthTest: false);

        _frameGraph = new FrameGraphBuilder()
            .AddPass(ClearPass)
            .AddPass(MainPass)
            .AddPass(UiPass)
            .Compile();
    }

    public void Render(float time = 0.0f)
    {
        var frameData = new FrameData((uint)_targetViewportSize.X, (uint)_targetViewportSize.Y, time);
        _frameGraph.Execute(frameData);
    }

    public void ResizeViewport(Vector2Int newSize)
    {
        _targetViewportSize = newSize;

        var shaderProgram = _factory.CreateShader(DefaultShaderConstants.DefaultVertexShader, DefaultShaderConstants.DefaultFragmentShader);

        ClearPass = new ClearPass(_context);
        MainPass = new RenderQueuePass(_context, shaderProgram, enableDepthTest: true);
        UiPass = new RenderQueuePass(_context, shaderProgram, enableDepthTest: false);

        _frameGraph = new FrameGraphBuilder()
            .AddPass(ClearPass)
            .AddPass(MainPass)
            .AddPass(UiPass)
            .Compile();
    }
}