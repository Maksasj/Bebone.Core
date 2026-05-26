using Bebone.Graphics.Abstractions.Mesh;

namespace Bebone.Graphics.MeshGeneration;

public interface IMeshGenerator<T> where T : IVertex
{
    public IMesh<T> GenerateMesh(IMeshBuilder<T> builder);
}