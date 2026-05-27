using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;
using Bebone.Math;
using System.Drawing;

namespace Bebone.Graphics.Renderer;

public class Renderer
{
    private readonly IGLContext _context;
    private readonly IGraphicsFactory _factory;
    private readonly IShaderProgram _shaderProgram;

    private Vector2Int _targetViewportSize;

    private FrameGraph _frameGraph;
    private readonly ClearPass _clearPass;
    private readonly RenderQueuePass _mainPass;
    private readonly RenderQueuePass _uiPass;

    public Renderer(Vector2Int targetViewportSize, IGLContext context, IGraphicsFactory factory)
    {
        _context = context;
        _factory = factory;
        _targetViewportSize = targetViewportSize;

        _shaderProgram = _factory.CreateShader(DefaultShaderConstants.DefaultVertexShader, DefaultShaderConstants.DefaultFragmentShader);

        _clearPass = new ClearPass(_context);
        _mainPass = new RenderQueuePass(_context, _shaderProgram, enableDepthTest: true);
        _uiPass = new RenderQueuePass(_context, _shaderProgram, enableDepthTest: false);

        _frameGraph = BuildFrameGraph();
    }

    public void Render(float time = 0.0f)
    {
        var frameData = new FrameData((uint)_targetViewportSize.X, (uint)_targetViewportSize.Y, time);
        _frameGraph.Execute(frameData);
    }

    public void ResizeViewport(Vector2Int newSize)
    {
        _targetViewportSize = newSize;
        _frameGraph = BuildFrameGraph();
    }

    public void SubmitMain(IDrawTask<IShaderProgram> task) => _mainPass.SubmitTask(task);
    public void SubmitUi(IDrawTask<IShaderProgram> task) => _uiPass.SubmitTask(task);

    public void SetMainCamera(ICamera camera) => _mainPass.SetCamera(camera);
    public void SetUiCamera(ICamera camera) => _uiPass.SetCamera(camera);

    public void SetClearColor(Color color) => _clearPass.SetClearColor(color);

    private FrameGraph BuildFrameGraph() => new FrameGraphBuilder()
        .AddPass(_clearPass)
        .AddPass(_mainPass)
        .AddPass(_uiPass)
        .Compile();
}