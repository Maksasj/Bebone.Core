using Bebone.Graphics.Abstractions.Mesh;
using System.Numerics;

namespace Bebone.Graphics.MeshGeneration;

public struct VertexTriangle(Vertex v1, Vertex v2, Vertex v3)
{
    public Vertex V1 = v1;
    public Vertex V2 = v2;
    public Vertex V3 = v3;

    public readonly List<VertexTriangle> Subdivide()
    {
        var ab = new Vertex((V1.Position + V2.Position) / 2.0f, Vector3.Zero,
            (V1.TextureCoordinates + V2.TextureCoordinates) * 0.5f
        );

        var bc = new Vertex((V2.Position + V3.Position) / 2.0f, Vector3.Zero,
            (V2.TextureCoordinates + V3.TextureCoordinates) * 0.5f
        );

        var ac = new Vertex((V1.Position + V3.Position) / 2.0f, Vector3.Zero,
            (V1.TextureCoordinates + V3.TextureCoordinates) * 0.5f
        );

        return [
            new VertexTriangle(V1, ab, ac),
            new VertexTriangle(ab, V2, bc),
            new VertexTriangle(ab, bc, ac),
            new VertexTriangle(ac, bc, V3)
        ];
    }

    public static VertexTriangle ProjectToUnitSphere(VertexTriangle triangle, float radius)
        => new(Vertex.ProjectToSphere(triangle.V1, radius), Vertex.ProjectToSphere(triangle.V2, radius), Vertex.ProjectToSphere(triangle.V3, radius));
}
