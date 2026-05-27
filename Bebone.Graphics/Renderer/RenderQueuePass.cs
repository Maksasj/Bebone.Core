using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;

namespace Bebone.Graphics.Renderer;

public class RenderQueuePass(IGLContext context, IShaderProgram shader, bool enableDepthTest) : IPass
{
    private readonly IGLContext _context = context;
    private readonly IShaderProgram _shader = shader;
    private readonly bool _enableDepthTest = enableDepthTest;

    private readonly List<IDrawTask<IShaderProgram>> _renderQueue = [];
    private ICamera? _camera = null;

    public void Compile(IReadOnlyDictionary<string, object> resources)
    {

    }

    public void Execute(FrameData frameData)
    {
        if (_camera is null)
            throw new InvalidOperationException($"Camera is not set for {nameof(RenderQueuePass)}");

        _context.SetViewport(0, 0, frameData.Width, frameData.Height);

        if (_enableDepthTest)
            _context.EnableDepthTest();
        else
            _context.DisableDepthTest();

        _shader.Activate();
        _shader.SetUniform("cam", _camera.GetViewMatrix() * _camera.GetProjectionMatrix(frameData.AspectRatio));

        foreach (var task in _renderQueue)
            task.Execute(_shader);

        _renderQueue.Clear();
    }

    public void SubmitTask(IDrawTask<IShaderProgram> drawTask) => _renderQueue.Add(drawTask);

    public void SetCamera(ICamera camera) => _camera = camera;
}