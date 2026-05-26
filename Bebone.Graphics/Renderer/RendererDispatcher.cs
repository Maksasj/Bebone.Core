using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.MeshGeneration;
using Bebone.Graphics.OpenGL;
using Bebone.Math;
using System.Numerics;

namespace Bebone.Graphics.Renderer;

public class RendererDispatcher(Renderer renderer, IGraphicsFactory factory)
{
    private readonly Renderer _renderer = renderer;

    // Pregenerate often used resources
    private readonly IMesh<Vertex> _cubeMesh = new CubeMeshGenerator().GenerateMesh(factory.CreateMeshBuilder<Vertex>());
    private readonly IMesh<Vertex> _quadMesh = new QuadMeshGenerator().GenerateMesh(factory.CreateMeshBuilder<Vertex>());
    private readonly ITexture2D _whiteTexture = factory.CreateTexture([], new TextureConfiguration(Width: 16, Height: 16));

    public void DrawQuad(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        _renderer.MainPass.SubmitTask(new MeshDrawTask()
        {
            Mesh = _quadMesh,
            Transform = new Transform(position, rotation, scale)
        });
    }

    public void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        _renderer.MainPass.SubmitTask(new MeshDrawTask()
        {
            Mesh = _cubeMesh,
            Transform = new Transform(position, rotation, scale)
        });
    }
}