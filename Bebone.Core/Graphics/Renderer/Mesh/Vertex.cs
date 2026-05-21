using System.Numerics;

namespace Bebone.Core.Graphics.Renderer.Mesh
{
    public struct Vertex : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoordinates;

        public Vertex(Vector3 position) : this(position, Vector3.Zero, Vector2.Zero) { }

        public Vertex(Vector3 position, Vector3 normal) : this(position, normal, Vector2.Zero) { }

        public Vertex(Vector3 position, Vector3 normal, Vector2 textureCoordinates)
        {
            Position = position;
            Normal = normal;
            TextureCoordinates = textureCoordinates;
        }

        public static IEnumerable<AttributeBind> GetAttributes()
        {
            var positionBind = new AttributeBind(index: 0, size: 3, AttributeBindType.Float, offset: 0);
            var normalBind = new AttributeBind(index: 1, size: 3, AttributeBindType.Float, offset: 3 * sizeof(float));
            var textureCoordinatesBind = new AttributeBind(index: 2, size: 2, AttributeBindType.Float, offset: 6 * sizeof(float));
            
            return [positionBind, normalBind, textureCoordinatesBind];
        }

        public static Vertex ProjectToSphere(Vertex vertex, float radius) => new(Vector3.Normalize(vertex.Position) * radius, vertex.Normal, vertex.TextureCoordinates);
    }
}
