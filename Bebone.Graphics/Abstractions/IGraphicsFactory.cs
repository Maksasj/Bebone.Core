using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.OpenGL;
using System.Drawing;

namespace Bebone.Graphics.Abstractions;

public interface IGraphicsFactory
{
    public IMeshBuilder<T> CreateMeshBuilder<T>() where T : unmanaged, IVertex;

    public IMesh<T> CreateMesh<T>(T[] vertices, uint[] indices) where T : unmanaged, IVertex;

    public IShaderProgram CreateShader(string vertexShaderSource, string fragmentShaderSource);

    public ITexture2D CreateTexture(byte[] data, TextureConfiguration textureConfiguration);
    public ITexture2D CreateEmptyTexture(int width, int height);
    public ITexture2D CreateFlatColorTexture(Color color, TextureConfiguration configuration);
}
