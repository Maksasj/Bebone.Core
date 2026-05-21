using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Shaders
{
    public class Shader : IDisposable
    {
        private readonly OpenGLGraphicsDevice _device;
        public uint Handle { get; init; }

        public Shader(OpenGLGraphicsDevice device, string shaderSource, ShaderType shaderType)
        {
            _device = device;

            Handle = _device.Api.CreateShader(shaderType);
            _device.Api.ShaderSource(Handle, shaderSource);
            _device.Api.CompileShader(Handle);

            var infoLog = _device.Api.GetShaderInfoLog(Handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
                throw new Exception($"Error compiling shader {infoLog}");
        }

        public void Dispose() => _device.Api.DeleteShader(Handle);
    }
}
