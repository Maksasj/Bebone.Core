namespace Bebone.Graphics.Renderer.Mesh;

public interface IMesh<T> where T : IVertex
{
    void Bind();

    void DrawTriangles();
    void DrawLines();
    void DrawArrays();

    void UpdateVertices(T[] verticies);
}