using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;

namespace Bebone.Graphics.Renderer;

public class RenderQueuePass(IGLContext context, IShaderProgram shader, List<IDrawTask<IShaderProgram>> renderQueue, bool enableDepthTest) : IPass
{
    private readonly IGLContext _context = context;
    private readonly IShaderProgram _shader = shader;
    private readonly List<IDrawTask<IShaderProgram>> _renderQueue = renderQueue;
    private readonly bool _enableDepthTest = enableDepthTest;

    public ICamera? Camera { get; set; } = null;

    public void Compile(IReadOnlyDictionary<string, object> resources)
    {

    }

    public void Execute()
    {
        if (Camera is null)
            throw new InvalidOperationException($"Camera is not set for {nameof(RenderQueuePass)}");

        _context.SetViewport(0, 0, 1920, 1080);

        if (_enableDepthTest)
            _context.EnableDepthTest();
        else
            _context.DisableDepthTest();

        _shader.Activate();
        _shader.SetUniform("cam", Camera.GetViewMatrix() * Camera.GetProjectionMatrix((float)1920 / (float)1080));

        foreach (var task in _renderQueue)
            task.Execute(_shader);

        _renderQueue.Clear();
    }
}