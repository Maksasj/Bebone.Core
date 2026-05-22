using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;

namespace Bebone.Graphics.OpenGL.Mesh;

public class MeshBuilder<T>(IGLContext gl) : IMeshBuilder<T> where T : unmanaged, IVertex
{
    private readonly IGLContext _gl = gl;

    private readonly List<T> _vertices = [];
    private readonly List<uint> _indices = [];
    private uint _currentIndex = 0;

    public void AddQuad(T v1, T v2, T v3, T v4)
    {
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);
        _vertices.Add(v4);

        var triangles = new uint[]
        {
            _currentIndex, _currentIndex + 1, _currentIndex + 2,
            _currentIndex + 3, _currentIndex + 2, _currentIndex + 1
        };

        _indices.AddRange(triangles);
        _currentIndex += 4;
    }

    public void AddTriangle(T v1, T v2, T v3)
    {
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);

        var triangles = new uint[]
        {
            _currentIndex, _currentIndex + 1, _currentIndex + 2
        };

        _indices.AddRange(triangles);

        _currentIndex += 3;
    }

    public IMesh<T> Build() => new Mesh<T>(_gl, [.. _vertices], [.. _indices]);
}
