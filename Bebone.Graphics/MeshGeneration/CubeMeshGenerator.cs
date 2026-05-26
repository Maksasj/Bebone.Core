using Bebone.Graphics.Abstractions.Mesh;
using System.Numerics;

namespace Bebone.Graphics.MeshGeneration;

public class CubeMeshGenerator : IMeshGenerator<Vertex>
{
    private readonly float _size;

    public CubeMeshGenerator(float size = 1.0f)
    {
        if (size <= 0.0f)
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be a positive value.");

        _size = size;
    }

    public IMesh<Vertex> GenerateMesh(IMeshBuilder<Vertex> builder)
    {
        var haflSize = _size * 0.5f;

        // Front Side
        var a0 = new Vertex(new Vector3(-haflSize, -haflSize, -haflSize), new Vector3(0f, 0f, 1f), Vector2.Zero);
        var a1 = new Vertex(new Vector3(haflSize, -haflSize, -haflSize), new Vector3(0f, 0f, 1f), Vector2.Zero);
        var a2 = new Vertex(new Vector3(-haflSize, haflSize, -haflSize), new Vector3(0f, 0f, 1f), Vector2.Zero);
        var a3 = new Vertex(new Vector3(haflSize, haflSize, -haflSize), new Vector3(0f, 0f, 1f), Vector2.Zero);
        builder.AddQuad(a0, a1, a2, a3);

        // Right Side
        var b0 = new Vertex(new Vector3(haflSize, -haflSize, -haflSize), new Vector3(1f, 0f, 0f), Vector2.Zero);
        var b1 = new Vertex(new Vector3(haflSize, -haflSize, haflSize), new Vector3(1f, 0f, 0f), Vector2.Zero);
        var b2 = new Vertex(new Vector3(haflSize, haflSize, -haflSize), new Vector3(1f, 0f, 0f), Vector2.Zero);
        var b3 = new Vertex(new Vector3(haflSize, haflSize, haflSize), new Vector3(1f, 0f, 0f), Vector2.Zero);
        builder.AddQuad(b0, b1, b2, b3);

        // Back Side
        var c0 = new Vertex(new Vector3(haflSize, -haflSize, haflSize), new Vector3(0f, 0f, -1f), Vector2.Zero);
        var c1 = new Vertex(new Vector3(-haflSize, -haflSize, haflSize), new Vector3(0f, 0f, -1f), Vector2.Zero);
        var c2 = new Vertex(new Vector3(haflSize, haflSize, haflSize), new Vector3(0f, 0f, -1f), Vector2.Zero);
        var c3 = new Vertex(new Vector3(-haflSize, haflSize, haflSize), new Vector3(0f, 0f, -1f), Vector2.Zero);
        builder.AddQuad(c0, c1, c2, c3);

        // Left Side
        var d0 = new Vertex(new Vector3(-haflSize, haflSize, -haflSize), new Vector3(-1f, 0f, 0f), Vector2.Zero);
        var d1 = new Vertex(new Vector3(-haflSize, haflSize, haflSize), new Vector3(-1f, 0f, 0f), Vector2.Zero);
        var d2 = new Vertex(new Vector3(-haflSize, -haflSize, -haflSize), new Vector3(-1f, 0f, 0f), Vector2.Zero);
        var d3 = new Vertex(new Vector3(-haflSize, -haflSize, haflSize), new Vector3(-1f, 0f, 0f), Vector2.Zero);
        builder.AddQuad(d0, d1, d2, d3);

        // Top Side
        var e0 = new Vertex(new Vector3(-haflSize, haflSize, -haflSize), new Vector3(0f, 1f, 0f), Vector2.Zero);
        var e1 = new Vertex(new Vector3(haflSize, haflSize, -haflSize), new Vector3(0f, 1f, 0f), Vector2.Zero);
        var e2 = new Vertex(new Vector3(-haflSize, haflSize, haflSize), new Vector3(0f, 1f, 0f), Vector2.Zero);
        var e3 = new Vertex(new Vector3(haflSize, haflSize, haflSize), new Vector3(0f, 1f, 0f), Vector2.Zero);
        builder.AddQuad(e0, e1, e2, e3);

        // Bottom Side
        var f0 = new Vertex(new Vector3(haflSize, -haflSize, -haflSize), new Vector3(0f, -1f, 0f), Vector2.Zero);
        var f1 = new Vertex(new Vector3(-haflSize, -haflSize, -haflSize), new Vector3(0f, -1f, 0f), Vector2.Zero);
        var f2 = new Vertex(new Vector3(haflSize, -haflSize, haflSize), new Vector3(0f, -1f, 0f), Vector2.Zero);
        var f3 = new Vertex(new Vector3(-haflSize, -haflSize, haflSize), new Vector3(0f, -1f, 0f), Vector2.Zero);
        builder.AddQuad(f0, f1, f2, f3);

        return builder.Build();
    }
}