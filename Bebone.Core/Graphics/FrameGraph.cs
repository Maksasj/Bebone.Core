namespace Bebone.Core.Graphics
{
    public class FrameGraph : IFrameGraph
    {
        private readonly List<IPass> passes;
        private readonly Dictionary<string, object> resources;

        public FrameGraph()
        {
            passes = [];
            resources = [];
        }

        public IPass AddPass(IPass pass)
        {
            passes.Add(pass);
            return pass;
        }

        public void Compile()
        {
            foreach (var pass in passes)
                pass.Compile(resources);
        }

        public void Execute()
        {
            foreach (var pass in passes)
                pass.Execute();
        }

        public void AddResource(string name, object resource) => resources[name] = resource;
    }
}
