using Bebone.Core.Graphics.Renderer.Mesh;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Mesh
{
    public class MeshBuilder<T> : IMeshBuilder<T> where T : IVertex
    {
        private readonly List<T> vertices;
        private readonly List<uint> indices;
        private uint currentIndex = 0;

        public MeshBuilder()
        {
            vertices = [];
            indices = [];
        }

        public void AddQuad(T v1, T v2, T v3, T v4)
        {
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);

            var triangles = new uint[]
            {
                currentIndex, currentIndex + 1, currentIndex + 2,
                currentIndex + 3, currentIndex + 2, currentIndex + 1
            };

            indices.AddRange(triangles);
            currentIndex += 4;
        }

        public void AddTriangle(T v1, T v2, T v3)
        {
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            var triangles = new uint[]
            {
                currentIndex, currentIndex + 1, currentIndex + 2
            };

            indices.AddRange(triangles);

            currentIndex += 3;
        }

        public IMesh<T> Build() => new Mesh<T>([.. vertices], [.. indices]);
    }
}
