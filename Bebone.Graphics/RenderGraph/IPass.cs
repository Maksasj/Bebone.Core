namespace Bebone.Graphics.RenderGraph;

public interface IPass
{
    void Compile(Dictionary<string, object> resources);

    void Execute();
}