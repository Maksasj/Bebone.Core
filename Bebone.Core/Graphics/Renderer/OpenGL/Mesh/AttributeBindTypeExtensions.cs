using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Mesh
{
    public static class AttributeBindTypeExtensions
    {
        public static VertexAttribPointerType ToOpenGL(this AttributeBindType type) =>
            type switch
            {
                AttributeBindType.Float => VertexAttribPointerType.Float,
                _ => throw new ArgumentException("Invalid enum value for command", nameof(type)),
            };
    }
}
