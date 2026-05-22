namespace Bebone.Graphics.Renderer.RenderGraph;

public interface IPass
{
    void Compile(Dictionary<string, object> resources);

    void Execute();
}