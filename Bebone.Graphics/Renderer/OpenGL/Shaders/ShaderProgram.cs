using Bebone.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;
using System.Drawing;
using System.Numerics;

namespace Bebone.Graphics.Renderer.OpenGL.Shaders;

public class ShaderProgram : IShaderProgram, IDisposable
{
    private readonly GL _gl;
    private readonly uint _handle;
    private const float _maxByteColorValue = 255.0f;

    public ShaderProgram(GL gl, string vertexShaderSource, string fragmentShaderSource)
    {
        _gl = gl;
        var vertexShader = new Shader(gl, vertexShaderSource, ShaderType.VertexShader);
        var fragmentShader = new Shader(gl, fragmentShaderSource, ShaderType.FragmentShader);

        _handle = _gl.CreateProgram();
        _gl.AttachShader(_handle, vertexShader.Handle);
        _gl.AttachShader(_handle, fragmentShader.Handle);
        _gl.LinkProgram(_handle);

        _gl.GetProgram(_handle, GLEnum.LinkStatus, out var status);

        if (status == 0)
            throw new Exception($"Error linking shader {_gl.GetProgramInfoLog(_handle)}");

        _gl.DetachShader(_handle, vertexShader.Handle);
        _gl.DetachShader(_handle, fragmentShader.Handle);

        vertexShader.Dispose();
        fragmentShader.Dispose();
    }

    public void Activate() => _gl.UseProgram(_handle);
    public void Dispose() => _gl.DeleteProgram(_handle);

    public void SetUniform(string name, int value) => _gl.Uniform1(GetUniformLocation(name), value);
    public void SetUniform(string name, float value) => _gl.Uniform1(GetUniformLocation(name), value);
    public void SetUniform(string name, Vector2 value) => _gl.Uniform2(GetUniformLocation(name), value);
    public void SetUniform(string name, Vector3 value) => _gl.Uniform3(GetUniformLocation(name), value);
    public void SetUniform(string name, Color value) => _gl.Uniform4(GetUniformLocation(name), value.R / _maxByteColorValue, value.G / _maxByteColorValue, value.B / _maxByteColorValue, value.A / _maxByteColorValue);
    public unsafe void SetUniform(string name, Matrix4x4 value) => _gl.UniformMatrix4(GetUniformLocation(name), count: 1, transpose: false, (float*)&value);
    private int GetUniformLocation(string name) => _gl.GetUniformLocation(_handle, name);
}