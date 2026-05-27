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

            foreach (var tri in triangles)
            {
                var res = tri.Subdivide();

                foreach (var r in res)
                    tmp.Add(VertexTriangle.ProjectToUnitSphere(r, _radius));
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

        foreach (var tri in triangles)
            builder.AddTriangle(tri.V1, tri.V2, tri.V3);

        return builder.Build();
    }
}
