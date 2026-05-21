namespace Bebone.Core.Graphics.Renderer.RenderGraph
{
    public interface IFrameGraph
    {
        public IPass AddPass(IPass pass);
        public void AddResource(string name, object resource);

        public void Compile();
        public void Execute();
    }
}
