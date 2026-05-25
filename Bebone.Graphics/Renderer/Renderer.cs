using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;
using System.Drawing;

namespace Bebone.Graphics.Renderer;

public class Renderer
{
    private const string DefaultVertexShader = """
        #version 330 core
        layout(location = 0) in vec3 aPosition;
        layout(location = 1) in vec3 aNormal;
        layout(location = 2) in vec2 aTexCoords;

        uniform mat4 cam;

        out vec2 texCoords;
        out vec3 normal;

        void main()
        {
            gl_Position = cam * vec4(aPosition, 1.0);
            texCoords = aTexCoords;
            normal = aNormal;
        }
        """;

    private const string DefaultFragmentShader = """
        #version 330 core
        in vec2 texCoords;
        in vec3 normal;

        out vec4 fragColor;

        void main()
        {
            fragColor = vec4(normal * 0.5 + 0.5, 1.0);
        }
        """;

    private readonly List<IDrawTask<int>> _mainPassTasks;
    private readonly List<IDrawTask<int>> _uiPassTasks;

    public RenderQueuePass MainPass { get; private set; }
    public RenderQueuePass UiPass { get; private set; }

    private readonly FrameGraph _frameGraph;
    private readonly IShaderProgram _shaderProgram;

    public Renderer(IGLContext context, IGraphicsFactory factory)
    {
        _mainPassTasks = [];
        _uiPassTasks = [];

        _shaderProgram = factory.CreateShader(DefaultVertexShader, DefaultFragmentShader);

        MainPass = new RenderQueuePass(context, _shaderProgram, _mainPassTasks, enableDepthTest: true);
        UiPass = new RenderQueuePass(context, _shaderProgram, _uiPassTasks, enableDepthTest: false);

        _frameGraph = new FrameGraph();
        _frameGraph.AddPass(CreateClearPass(context));
        _frameGraph.AddPass(MainPass);
        _frameGraph.AddPass(UiPass);
        _frameGraph.Compile();
    }

    private static RenderTask<int> CreateClearPass(IGLContext context)
    {
        return new RenderTask<int>(
        _ => 0,
        _ =>
        {
            context.ClearColor(Color.FromArgb(255, 135, 206, 235));
            context.ClearBuffers();
        });
    }

    public void Execute()
    {
        _frameGraph.Execute();

        _mainPassTasks.Clear();
        _uiPassTasks.Clear();
    }
}