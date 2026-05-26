using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.Utils;
using Bebone.Math;

namespace Bebone.Graphics.Renderer;

public class RendererDispatcher(Renderer renderer, IGraphicsFactory factory)
{
    private readonly Renderer _renderer = renderer;

    // Pregenerate often used geometry
    private readonly IMesh<Vertex> _cubeMesh = new CubeMeshGenerator().GenerateMesh(factory.CreateMeshBuilder<Vertex>());

    public void DrawCube()
    {
        renderer.MainPass.RenderQueue.Add(new MeshDrawTask()
        {
            Mesh = cubeMesh,
            Transform = Transform.Identity
        });
    }
}