using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;

namespace Bebone.Graphics.Renderer;

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