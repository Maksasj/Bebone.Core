using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.MeshGeneration;
using Bebone.Math;

namespace Bebone.Graphics.Renderer;

public class RendererDispatcher(Renderer renderer, IGraphicsFactory factory)
{
    private readonly Renderer _renderer = renderer;

    // Pregenerate often used resources
    private readonly IMesh<Vertex> _cubeMesh = new CubeMeshGenerator().GenerateMesh(factory.CreateMeshBuilder<Vertex>());

    public void DrawCube()
    {
        _renderer.MainPass.SubmitTask(new MeshDrawTask()
        {
            Mesh = _cubeMesh,
            Transform = Transform.Identity
        });
    }
}