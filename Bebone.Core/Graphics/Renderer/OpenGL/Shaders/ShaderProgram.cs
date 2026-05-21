using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;
using System.Drawing;
using System.Numerics;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Shaders
{
    public class ShaderProgram : IShaderProgram, IDisposable
    {
        private readonly uint _handle;

        public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
        {
            var vertexShader = new Shader(vertexShaderSource, ShaderType.VertexShader);
            var fragmentShader = new Shader(fragmentShaderSource, ShaderType.FragmentShader);

            _handle = OpenGL.Api.CreateProgram();
            OpenGL.Api.AttachShader(_handle, vertexShader._handle);
            OpenGL.Api.AttachShader(_handle, fragmentShader._handle);
            OpenGL.Api.LinkProgram(_handle);

            OpenGL.Api.GetProgram(_handle, GLEnum.LinkStatus, out var status);

            if (status == 0)
                throw new Exception($"Error linking shader {OpenGL.Api.GetProgramInfoLog(_handle)}");

            OpenGL.Api.DetachShader(_handle, vertexShader._handle);
            OpenGL.Api.DetachShader(_handle, fragmentShader._handle);

            vertexShader.Dispose();
            fragmentShader.Dispose();
        }

        public void Activate() => OpenGL.Api.UseProgram(_handle);
        public void Dispose() => OpenGL.Api.DeleteProgram(_handle);

        public void SetUniform(string name, int value) => OpenGL.Api.Uniform1(GetUniformLocation(name), value);
        public void SetUniform(string name, float value) => OpenGL.Api.Uniform1(GetUniformLocation(name), value);
        public void SetUniform(string name, Vector2 value) => OpenGL.Api.Uniform2(GetUniformLocation(name), value);
        public void SetUniform(string name, Vector3 value) => OpenGL.Api.Uniform3(GetUniformLocation(name), value);
        public void SetUniform(string name, Color value) => OpenGL.Api.Uniform4(GetUniformLocation(name), value.R / 255.0f, value.G / 255.0f, value.B / 255.0f, value.A / 255.0f);
        public unsafe void SetUniform(string name, Matrix4x4 value) => OpenGL.Api.UniformMatrix4(GetUniformLocation(name), count: 1, transpose: false, (float*)&value);

        private int GetUniformLocation(string name)
        {
            int location = OpenGL.Api.GetUniformLocation(_handle, name);

            // if (location == -1)
            // throw new Exception($"'{name}' uniform not found in shader.");

            return location;
        }
    }
}
