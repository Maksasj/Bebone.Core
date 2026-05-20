using Bebone.Core.Graphics;
using System.Numerics;

namespace tpi.Graphics.Mesh
{
    public struct Vertex : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoordinates;

        public Vertex(Vector3 position)
        {
            Position = position;
            Normal = Vector3.Zero;
            TextureCoordinates = Vector2.Zero;
        }

        public Vertex(Vector3 position, Vector3 normal)
        {
            Position = position;
            Normal = normal;
            TextureCoordinates = Vector2.Zero;
        }

        public Vertex(Vector3 position, Vector3 normal, Vector2 textureCoordinates)
        {
            Position = position;
            Normal = normal;
            TextureCoordinates = textureCoordinates;
        }

        public static List<AttributeBind> GetAttributes()
            => [
                new AttributeBind(0, 3, AttributeBindType.Float, 0),
                new AttributeBind(1, 3, AttributeBindType.Float, 3 * sizeof(float)),
                new AttributeBind(2, 2, AttributeBindType.Float, 6 * sizeof(float))
            ];

        public static Vertex ProjectToSphere(Vertex vertex, float radius) => new(Vector3.Normalize(vertex.Position) * radius, vertex.Normal, vertex.TextureCoordinates);
    }
}
