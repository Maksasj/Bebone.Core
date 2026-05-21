using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;
using System.Drawing;
using System.Numerics;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Shaders;

public class ShaderProgram : IShaderProgram, IDisposable
{
    private readonly OpenGLGraphicsDevice _device;
    private readonly uint _handle;
    private const float _maxByteColorValue = 255.0f;

    public ShaderProgram(OpenGLGraphicsDevice device, string vertexShaderSource, string fragmentShaderSource)
    {
        _device = device;
        var vertexShader = new Shader(device, vertexShaderSource, ShaderType.VertexShader);
        var fragmentShader = new Shader(device, fragmentShaderSource, ShaderType.FragmentShader);

        _handle = _device.Api.CreateProgram();
        _device.Api.AttachShader(_handle, vertexShader.Handle);
        _device.Api.AttachShader(_handle, fragmentShader.Handle);
        _device.Api.LinkProgram(_handle);

        _device.Api.GetProgram(_handle, GLEnum.LinkStatus, out var status);

        if (status == 0)
            throw new Exception($"Error linking shader {_device.Api.GetProgramInfoLog(_handle)}");

        _device.Api.DetachShader(_handle, vertexShader.Handle);
        _device.Api.DetachShader(_handle, fragmentShader.Handle);

        vertexShader.Dispose();
        fragmentShader.Dispose();
    }

    public void Activate() => _device.Api.UseProgram(_handle);
    public void Dispose() => _device.Api.DeleteProgram(_handle);

    public void SetUniform(string name, int value) => _device.Api.Uniform1(GetUniformLocation(name), value);
    public void SetUniform(string name, float value) => _device.Api.Uniform1(GetUniformLocation(name), value);
    public void SetUniform(string name, Vector2 value) => _device.Api.Uniform2(GetUniformLocation(name), value);
    public void SetUniform(string name, Vector3 value) => _device.Api.Uniform3(GetUniformLocation(name), value);
    public void SetUniform(string name, Color value) => _device.Api.Uniform4(GetUniformLocation(name), value.R / _maxByteColorValue, value.G / _maxByteColorValue, value.B / _maxByteColorValue, value.A / _maxByteColorValue);
    public unsafe void SetUniform(string name, Matrix4x4 value) => _device.Api.UniformMatrix4(GetUniformLocation(name), count: 1, transpose: false, (float*)&value);
    private int GetUniformLocation(string name) => _device.Api.GetUniformLocation(_handle, name);
}