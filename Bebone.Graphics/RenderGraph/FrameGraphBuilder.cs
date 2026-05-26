namespace Bebone.Graphics.RenderGraph;

public class FrameGraphBuilder
{
    private readonly FrameGraph _frameGraph = new();

    public FrameGraphBuilder AddPass(IPass pass)
    {
        _frameGraph.AddPass(pass);

        return this;
    }

    public FrameGraph Compile()
    {
        _frameGraph.Compile();

        return _frameGraph;
    }
}