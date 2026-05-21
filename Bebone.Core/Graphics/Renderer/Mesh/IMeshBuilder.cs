namespace Bebone.Core.Graphics.Renderer.Mesh;

public interface IMeshBuilder<T> where T : IVertex
{
    public void AddTriangle(T v1, T v2, T v3);
    public void AddQuad(T v1, T v2, T v3, T v4);

    public IMesh<T> Build();
}