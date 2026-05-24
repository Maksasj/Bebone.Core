using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;

namespace Bebone.Graphics.Renderer
{
    public class RenderQueuePass(IGLContext context, ICamera camera, IShaderProgram shader, List<IDrawTask<int>> renderQueue, bool enableDepthTest) : IPass
    {
        private readonly IGLContext _context = context;
        private readonly ICamera _camera = camera;
        private readonly IShaderProgram _shader = shader;
        private readonly List<IDrawTask<int>> _renderQueue = renderQueue;
        private readonly bool _enableDepthTest = enableDepthTest;

        public void Compile(IReadOnlyDictionary<string, object> resources)
        {

        }

        public void Execute()
        {
            _context.SetViewport(0, 0, 1920, 1080);

            if (_enableDepthTest)
                _context.EnableDepthTest();
            else
                _context.DisableDepthTest();

            _shader.Activate();
            _shader.SetUniform("cam", _camera.GetViewMatrix() * _camera.GetProjectionMatrix((float)1920 / (float)1080));

            foreach (var task in _renderQueue)
                task.Execute(0);
        }
    }
}
