using Bebone.Graphics.Abstractions;
using Bebone.Graphics.RenderGraph;

namespace Bebone.Graphics.Renderer;

public class Renderer
{
    public ClearPass ClearPass { get; init; }
    public RenderQueuePass MainPass { get; init; }
    public RenderQueuePass UiPass { get; init; }

    private readonly FrameGraph _frameGraph;

    public Renderer(IGLContext context, IGraphicsFactory factory)
    {
        var shaderProgram = factory.CreateShader(DefaultShaderConstants.DefaultVertexShader, DefaultShaderConstants.DefaultFragmentShader);

        ClearPass = new ClearPass(context);
        MainPass = new RenderQueuePass(context, shaderProgram, enableDepthTest: true);
        UiPass = new RenderQueuePass(context, shaderProgram, enableDepthTest: false);

        _frameGraph = new FrameGraphBuilder()
            .AddPass(ClearPass)
            .AddPass(MainPass)
            .AddPass(UiPass)
            .Compile();
    }

    public void Render()
    {
        var frameData = new FrameData(1920, 1080, 0);

        _frameGraph.Execute(frameData);
    }
}