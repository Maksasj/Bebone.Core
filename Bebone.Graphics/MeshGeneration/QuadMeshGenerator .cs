using Bebone.Graphics.Abstractions.Mesh;
using System.Numerics;

namespace Bebone.Graphics.MeshGeneration;

public class QuadMeshGenerator : IMeshGenerator<Vertex>
{
    private readonly float _size;

    public QuadMeshGenerator(float size = 1.0f)
    {
        if (size <= 0.0f)
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be a positive value.");

        _size = size;
    }

    public IMesh<Vertex> GenerateMesh(IMeshBuilder<Vertex> builder)
    {
        var haflSize = _size * 0.5f;

        var v00 = new Vertex(new Vector3(-haflSize, haflSize, 0.0f), Vector3.One, new Vector2(0.0f, 1.0f));
        var v01 = new Vertex(new Vector3(-haflSize, -haflSize, 0.0f), Vector3.One, new Vector2(0.0f, 0.0f));
        var v02 = new Vertex(new Vector3(haflSize, -haflSize, 0.0f), Vector3.One, new Vector2(1.0f, 0.0f));

        var v10 = new Vertex(new Vector3(-haflSize, haflSize, 0.0f), Vector3.One, new Vector2(0.0f, 1.0f));
        var v11 = new Vertex(new Vector3(haflSize, -haflSize, 0.0f), Vector3.One, new Vector2(1.0f, 0.0f));
        var v12 = new Vertex(new Vector3(haflSize, haflSize, 0.0f), Vector3.One, new Vector2(1.0f, 1.0f));

        builder.AddTriangle(v00, v01, v02);
        builder.AddTriangle(v10, v11, v12);

        return builder.Build();
    }
}