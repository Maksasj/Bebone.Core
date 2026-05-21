namespace Bebone.Core.Graphics.Renderer.RenderGraph
{
    public class RenderTask<PassData>(Func<Dictionary<string, object>, PassData> compile, Action<PassData> execute) : IPass
    {
        public PassData? data = default;

        public Func<Dictionary<string, object>, PassData> compile = compile;
        public Action<PassData> execute = execute;

        public void Compile(Dictionary<string, object> resources) => data = compile(resources);
        public void Execute() => execute(data!);
    }
}
