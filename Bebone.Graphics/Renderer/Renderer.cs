using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.OpenGL.Shaders;
using Bebone.Graphics.RenderGraph;
using System.Drawing;
using System.Numerics;

namespace Bebone.Graphics.Renderer;

public record struct Transform
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public Transform(Vector3 position)
    {
        Position = position;
        Rotation = Quaternion.Identity;
        Scale = Vector3.One;
    }

    public Transform(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
        Scale = Vector3.One;
    }

    public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public static Transform Identity => new()
    {
        Position = Vector3.Zero,
        Rotation = Quaternion.Identity,
        Scale = new Vector3(1.0f, 1.0f, 1.0f)
    };

    public readonly Matrix4x4 ToMatrix()
    {
        var translationMatrix = Matrix4x4.CreateTranslation(Position);
        var rotationMatrix = Matrix4x4.CreateFromQuaternion(Rotation);
        var scaleMatrix = Matrix4x4.CreateScale(Scale);

        return scaleMatrix * rotationMatrix * translationMatrix;
    }
}

public readonly record struct MeshDrawTask : IDrawTask<IShaderProgram>
{
    public readonly IMesh<Vertex> Mesh { get; init; }
    public readonly Transform Transform { get; init; }

    public void Execute(IShaderProgram shader)
    {
        shader.SetUniform("model", Transform.ToMatrix());
        Mesh.Bind();
        Mesh.DrawTriangles();
    }
}

public class Renderer
{
    public List<IDrawTask<IShaderProgram>> _mainPassTasks;
    public List<IDrawTask<IShaderProgram>> _uiPassTasks;

    public RenderQueuePass MainPass { get; private set; }
    public RenderQueuePass UiPass { get; private set; }

    private readonly FrameGraph _frameGraph;

    public Renderer(IGLContext context, IGraphicsFactory factory)
    {
        _mainPassTasks = [];
        _uiPassTasks = [];

        var shaderProgram = factory.CreateShader(DefaultShaderConstants.DefaultVertexShader, DefaultShaderConstants.DefaultFragmentShader);
        MainPass = new RenderQueuePass(context, shaderProgram, _mainPassTasks, enableDepthTest: true);
        UiPass = new RenderQueuePass(context, shaderProgram, _uiPassTasks, enableDepthTest: false);

        _frameGraph = new FrameGraph();
        _frameGraph.AddPass(CreateClearPass(context));
        _frameGraph.AddPass(MainPass);
        _frameGraph.AddPass(UiPass);
        _frameGraph.Compile();
    }

    public void Execute()
    {
        _frameGraph.Execute();
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
}