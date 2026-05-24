namespace Bebone.Graphics.RenderGraph;

public interface IPass
{
    void Compile(IReadOnlyDictionary<string, object> resources);

    void Execute();
}