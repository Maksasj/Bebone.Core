using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Shaders
{
    public class Shader : IDisposable
    {
        public uint Handle { get; init; }

        public Shader(string shaderSource, ShaderType shaderType)
        {
            Handle = OpenGL.Api.CreateShader(shaderType);
            OpenGL.Api.ShaderSource(Handle, shaderSource);
            OpenGL.Api.CompileShader(Handle);

            string infoLog = OpenGL.Api.GetShaderInfoLog(Handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
                throw new Exception($"Error compiling shader {infoLog}");
        }

        public void Dispose() => OpenGL.Api.DeleteShader(Handle);
    }
}
