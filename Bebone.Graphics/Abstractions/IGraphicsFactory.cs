using Bebone.Graphics.Abstractions.Mesh;

namespace Bebone.Graphics.Abstractions;

public interface IGraphicsFactory
{
    public IMeshBuilder<T> CreateMeshBuilder<T>() where T : unmanaged, IVertex;

    public IMesh<T> CreateMesh<T>(T[] vertices, uint[] indices) where T : unmanaged, IVertex;

    public IShaderProgram CreateShader(string vertexShaderSource, string fragmentShaderSource);

    public ITexture CreateTexture(int width, int height, byte[] data);
}
