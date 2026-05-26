using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.OpenGL.Mesh;
using Bebone.Graphics.OpenGL.Shaders;
using System.Drawing;

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
    
    public ITexture2D CreateTexture(byte[] data, TextureConfiguration configuration) => new Texture2D(_gl, data, configuration);
    public ITexture2D CreateEmptyTexture(int width, int height) => new Texture2D(_gl, width, height);
    public ITexture2D CreateFlatColorTexture(Color color, TextureConfiguration configuration)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(configuration.Width, nameof(configuration.Width));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(configuration.Height, nameof(configuration.Height));

        var width = configuration.Width;
        var height = configuration.Height;

        byte[] data = new byte[width * height * 4];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = (y * width + x) * 4;
                data[index + 0] = color.R;
                data[index + 1] = color.G;
                data[index + 2] = color.B;
                data[index + 3] = color.A;
            }
        }

        return new Texture2D(_gl, data, new TextureConfiguration(width, height));
    }
}