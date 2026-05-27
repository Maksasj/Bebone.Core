using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Math;
using System.Drawing;

namespace Bebone.Graphics.Renderer;

public readonly record struct MeshDrawTask : IDrawTask<IShaderProgram>
{
    public IMesh<Vertex> Mesh { get; init; }
    public Transform Transform { get; init; }
    public ITexture2D Texture { get; init; }
    public Color Tint { get; init; }

    public void Execute(IShaderProgram shader)
    {
        Texture.ActiveBind(0);
        shader.SetUniform("albedo", 0);
        shader.SetUniform("tint", Tint);
        shader.SetUniform("model", Transform.ToMatrix());
        Mesh.Bind();
        Mesh.DrawTriangles();
    }
}