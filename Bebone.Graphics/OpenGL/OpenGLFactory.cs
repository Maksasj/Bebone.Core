using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.OpenGL.Mesh;
using Bebone.Graphics.OpenGL.Shaders;

namespace Bebone.Graphics.OpenGL;

public class OpenGLFactory(IGLContext gl) : IGraphicsFactory
{
    private readonly IGLContext _gl = gl;
    public IMeshBuilder<T> CreateMeshBuilder<T>() where T : unmanaged, IVertex
    {
        return new MeshBuilder<T>(_gl);
    }

    public IMesh<T> CreateMesh<T>(T[] vertices, uint[] indices) where T : unmanaged, IVertex
    {
        return new Mesh<T>(_gl, vertices, indices);
    }

    public IShaderProgram CreateShader(string vertexShaderSource, string fragmentShaderSource) => new ShaderProgram(_gl, vertexShaderSource, fragmentShaderSource);
    public ITexture CreateTexture(byte[] data, TextureConfiguration textureConfiguration) => new Texture2D(_gl, data, textureConfiguration);
    public ITexture CreateEmptyTexture(int width, int height) => new Texture2D(_gl, width, height);
}