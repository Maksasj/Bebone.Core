using Bebone.Graphics.Abstractions.Mesh;
using System.Numerics;

namespace Bebone.Graphics.Utils;

public class QuadMeshGenerator : IMeshGenerator<Vertex>
{
    public IMesh<Vertex> GenerateMesh(IMeshBuilder<Vertex> builder)
    {
        var v00 = new Vertex(new Vector3(-0.5f, 0.5f, 0.0f), Vector3.One, new Vector2(0.0f, 1.0f));
        var v01 = new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), Vector3.One, new Vector2(0.0f, 0.0f));
        var v02 = new Vertex(new Vector3(0.5f, -0.5f, 0.0f), Vector3.One, new Vector2(1.0f, 0.0f));

        var v10 = new Vertex(new Vector3(-0.5f, 0.5f, 0.0f), Vector3.One, new Vector2(0.0f, 1.0f));
        var v11 = new Vertex(new Vector3(0.5f, -0.5f, 0.0f), Vector3.One, new Vector2(1.0f, 0.0f));
        var v12 = new Vertex(new Vector3(0.5f, 0.5f, 0.0f), Vector3.One, new Vector2(1.0f, 1.0f));

        builder.AddTriangle(v00, v01, v02);
        builder.AddTriangle(v10, v11, v12);

        return builder.Build();
    }
}