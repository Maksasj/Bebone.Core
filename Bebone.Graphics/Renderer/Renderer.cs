using Bebone.Graphics.Abstractions;
using Bebone.Graphics.RenderGraph;
using System.Drawing;

namespace Bebone.Graphics.Renderer;

public class Renderer
{
    public RenderQueuePass MainPass { get; private set; }
    public RenderQueuePass UiPass { get; private set; }

    private readonly FrameGraph _frameGraph;

    public Renderer(IGLContext context, IGraphicsFactory factory)
    {
        var shaderProgram = factory.CreateShader(DefaultShaderConstants.DefaultVertexShader, DefaultShaderConstants.DefaultFragmentShader);
        MainPass = new RenderQueuePass(context, shaderProgram, enableDepthTest: true);
        UiPass = new RenderQueuePass(context, shaderProgram, enableDepthTest: false);

        _frameGraph = new FrameGraph();
        _frameGraph.AddPass(CreateClearPass(context));
        _frameGraph.AddPass(MainPass);
        _frameGraph.AddPass(UiPass);
        _frameGraph.Compile();
    }

    public void Execute()
    {
        _frameGraph.Execute();
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
}