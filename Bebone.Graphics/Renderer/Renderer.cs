using Bebone.Graphics.Abstractions;
using Bebone.Graphics.RenderGraph;
using System.Drawing;

namespace Bebone.Graphics.Renderer;

public class Renderer
{
    private readonly List<IDrawTask<int>> _mainPassTasks;
    private readonly List<IDrawTask<int>> _uiPassTasks;

    public RenderQueuePass MainPass { get; private set; }
    public RenderQueuePass UiPass { get; private set; }

    private readonly FrameGraph _frameGraph;
    private readonly IShaderProgram _shaderProgram;

    public Renderer(IGLContext context, IGraphicsFactory factory)
    {
        _mainPassTasks = [];
        _uiPassTasks = [];

        _shaderProgram = factory.CreateShader(DefaultShaderConstants.DefaultVertexShader, DefaultShaderConstants.DefaultFragmentShader);

        MainPass = new RenderQueuePass(context, _shaderProgram, _mainPassTasks, enableDepthTest: true);
        UiPass = new RenderQueuePass(context, _shaderProgram, _uiPassTasks, enableDepthTest: false);

        _frameGraph = new FrameGraph();
        _frameGraph.AddPass(CreateClearPass(context));
        _frameGraph.AddPass(MainPass);
        _frameGraph.AddPass(UiPass);
        _frameGraph.Compile();
    }

    private static RenderTask<int> CreateClearPass(IGLContext context)
    {
        return new RenderTask<int>(
        _ => 0,
        _ =>
        {
            context.ClearColor(Color.FromArgb(255, 135, 206, 235));
            context.ClearBuffers();
        });
    }

    public void Execute()
    {
        _frameGraph.Execute();

        _mainPassTasks.Clear();
        _uiPassTasks.Clear();
    }
}