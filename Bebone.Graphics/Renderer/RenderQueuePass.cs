using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;

namespace Bebone.Graphics.Renderer;

public class RenderQueuePass(IGLContext context, IShaderProgram shader, bool enableDepthTest) : IPass
{
    private readonly IGLContext _context = context;
    private readonly IShaderProgram _shader = shader;
    private readonly bool _enableDepthTest = enableDepthTest;

    public List<IDrawTask<IShaderProgram>> RenderQueue { get; init; } = [];
    public ICamera? Camera { get; set; } = null;

    public void Compile(IReadOnlyDictionary<string, object> resources)
    {

    }

    public void Execute(FrameData frameData)
    {
        if (Camera is null)
            throw new InvalidOperationException($"Camera is not set for {nameof(RenderQueuePass)}");

        _context.SetViewport(0, 0, frameData.Width, frameData.Height);

        if (_enableDepthTest)
            _context.EnableDepthTest();
        else
            _context.DisableDepthTest();

        _shader.Activate();
        _shader.SetUniform("cam", Camera.GetViewMatrix() * Camera.GetProjectionMatrix(frameData.AspectRatio));

        foreach (var task in RenderQueue)
            task.Execute(_shader);

        RenderQueue.Clear();
    }
}