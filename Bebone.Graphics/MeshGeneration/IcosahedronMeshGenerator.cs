using Bebone.Graphics.Abstractions.Mesh;
using System.Numerics;

namespace Bebone.Graphics.MeshGeneration;

public class IcosahedronMeshGenerator : IMeshGenerator<Vertex>
{
    protected static List<VertexTriangle> GenerateIcosahedronTriangles()
    {
        float phi = (1.0f + MathF.Sqrt(5.0f)) * 0.5f;
        float a = 1.0f;
        float b = 1.0f / phi;

        Vector3[] positions =
        [
            new Vector3(0, b, -a),
            new Vector3(b, a, 0),
            new Vector3(-b, a, 0),
            new Vector3(0, b, a),
            new Vector3(0, -b, a),
            new Vector3(-a, 0, b),
            new Vector3(0, -b, -a),
            new Vector3(a, 0, -b),
            new Vector3(a, 0, b),
            new Vector3(-a, 0, -b),
            new Vector3(b, -a, 0),
            new Vector3(-b, -a, 0)
        ];

        int[][] triangleIndices =
        [
            [2, 1, 0],
            [1, 2, 3],
            [5, 4, 3],
            [4, 8, 3],
            [7, 6, 0],
            [6, 9, 0],
            [11, 10, 4],
            [10, 11, 6],
            [9, 5, 2],
            [5, 9, 11],
            [8, 7, 1],
            [7, 8, 10],
            [2, 5, 3],
            [8, 1, 3],
            [9, 2, 0],
            [1, 7, 0],
            [11, 9, 6],
            [7, 10, 6],
            [5, 11, 4],
            [10, 8, 4]
        ];

        var triangles = new List<VertexTriangle>();

        foreach (var triangle in triangleIndices)
        {
            var p0 = positions[tri[0]];
            var p1 = positions[tri[1]];
            var p2 = positions[tri[2]];

            var n0 = Vector3.Normalize(p0);
            var n1 = Vector3.Normalize(p1);
            var n2 = Vector3.Normalize(p2);

            var v0 = new Vertex(p0, n0, new Vector2(0.0f, 0.0f));
            var v1 = new Vertex(p1, n1, new Vector2(1.0f, 0.0f));
            var v2 = new Vertex(p2, n2, new Vector2(0.0f, 1.0f));

            triangles.Add(new VertexTriangle(v0, v1, v2));
        }

        return triangles;
    }

    public IMesh<Vertex> GenerateMesh(IMeshBuilder<Vertex> builder)
    {
        var triangles = GenerateIcosahedronTriangles();

        foreach (var tri in triangles)
            builder.AddTriangle(tri.V1, tri.V2, tri.V3);

        return builder.Build();
    }
}
