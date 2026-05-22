namespace Bebone.Graphics.RenderGraph;

public class FrameGraph : IFrameGraph
{
    private readonly List<IPass> _passes;
    private readonly Dictionary<string, object> _resources;

    public FrameGraph()
    {
        _passes = [];
        _resources = [];
    }

    public IPass AddPass(IPass pass)
    {
        _passes.Add(pass);
        return pass;
    }

    public void Compile()
    {
        foreach (var pass in _passes)
            pass.Compile(_resources);
    }

    public void Execute()
    {
        foreach (var pass in _passes)
            pass.Execute();
    }

    public void AddResource(string name, object resource) => _resources[name] = resource;
}