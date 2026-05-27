using Bebone.Graphics.Abstractions.Mesh;
using System.Numerics;

namespace Bebone.Graphics.MeshGeneration;

public class IcosphereMeshGenerator(float radius, int lod = 3) : IcosahedronMeshGenerator
{
    private readonly float _radius = radius;
    private readonly float _lod = lod;

    protected List<VertexTriangle> GenerateIcosphereTriangles()
    {
        var triangles = GenerateIcosahedronTriangles();

        for (int l = 0; l < _lod; ++l)
        {
            var tmp = new List<VertexTriangle>();

            foreach (var triangle in triangles)
            {
                var newTriangles = triangle.Subdivide();

                foreach (var newTriangle in newTriangles)
                    tmp.Add(VertexTriangle.ProjectToUnitSphere(newTriangle, _radius));
            }

            triangles = tmp;
        }

        for (int i = 0; i < triangles.Count; ++i)
        {
            var tri = triangles[i];

            tri.V1.Normal = Vector3.Normalize(tri.V1.Position);
            tri.V2.Normal = Vector3.Normalize(tri.V2.Position);
            tri.V3.Normal = Vector3.Normalize(tri.V3.Position);

            triangles[i] = tri;
        }

        return triangles;
    }

    public new IMesh<Vertex> GenerateMesh(IMeshBuilder<Vertex> builder)
    {
        var triangles = GenerateIcosphereTriangles();

        foreach (var triangle in triangles)
            builder.AddTriangle(triangle.V1, triangle.V2, triangle.V3);

        return builder.Build();
    }
}
