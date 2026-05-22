using Silk.NET.OpenGL;

namespace Bebone.Graphics.OpenGL.Shaders;

public class Shader : IDisposable
{
    private readonly GL _gl;
    public uint Handle { get; init; }

    public Shader(GL gl, string shaderSource, ShaderType shaderType)
    {
        _gl = gl;

        Handle = _gl.CreateShader(shaderType);
        _gl.ShaderSource(Handle, shaderSource);
        _gl.CompileShader(Handle);

        var infoLog = _gl.GetShaderInfoLog(Handle);

        if (!string.IsNullOrWhiteSpace(infoLog))
            throw new Exception($"Error compiling shader {infoLog}");
    }

    public void Dispose() => _gl.DeleteShader(Handle);
}