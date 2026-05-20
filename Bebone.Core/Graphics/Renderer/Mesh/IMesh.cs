namespace Bebone.Core.Graphics
{
    public interface IMesh<T> where T : IVertex
    {
        void Bind();
        void DrawTriangles();
        void DrawLines();
        void DrawArrays();
        void UpdateVertices(T[] verticies);
    }
}
