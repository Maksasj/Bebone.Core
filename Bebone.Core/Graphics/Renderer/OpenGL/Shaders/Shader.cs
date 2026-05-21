using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Shaders
{
    public class Shader : IDisposable
    {
        public uint _handle { get; init; }

        public Shader(string shaderSource, ShaderType shaderType)
        {
            _handle = OpenGL.Api.CreateShader(shaderType);
            OpenGL.Api.ShaderSource(_handle, shaderSource);
            OpenGL.Api.CompileShader(_handle);

            string infoLog = OpenGL.Api.GetShaderInfoLog(_handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
                throw new Exception($"Error compiling shader {infoLog}");
        }

        public void Dispose() => OpenGL.Api.DeleteShader(_handle);
    }
}
