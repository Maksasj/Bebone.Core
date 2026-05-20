using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;
using System.Drawing;
using System.Numerics;

namespace Bebone.Core.Graphics.Renderer.OpenGL.Shaders
{
    public class ShaderProgram : IShaderProgram, IDisposable
    {
        private readonly uint nativeHandle;

        public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
        {
            var vertexShader = new Shader(vertexShaderSource, ShaderType.VertexShader);
            var fragmentShader = new Shader(fragmentShaderSource, ShaderType.FragmentShader);

            nativeHandle = OpenGL.Api.CreateProgram();
            OpenGL.Api.AttachShader(nativeHandle, vertexShader.Handle);
            OpenGL.Api.AttachShader(nativeHandle, fragmentShader.Handle);
            OpenGL.Api.LinkProgram(nativeHandle);

            OpenGL.Api.GetProgram(nativeHandle, GLEnum.LinkStatus, out var status);

            if (status == 0)
                throw new Exception($"Error linking shader {OpenGL.Api.GetProgramInfoLog(nativeHandle)}");

            OpenGL.Api.DetachShader(nativeHandle, vertexShader.Handle);
            OpenGL.Api.DetachShader(nativeHandle, fragmentShader.Handle);

            vertexShader.Dispose();
            fragmentShader.Dispose();
        }

        public void Activate() => OpenGL.Api.UseProgram(nativeHandle);
        public void Dispose() => OpenGL.Api.DeleteProgram(nativeHandle);

        public void SetUniform(string name, int value) => OpenGL.Api.Uniform1(GetUniformLocation(name), value);
        public void SetUniform(string name, float value) => OpenGL.Api.Uniform1(GetUniformLocation(name), value);
        public void SetUniform(string name, Vector2 value) => OpenGL.Api.Uniform2(GetUniformLocation(name), value);
        public void SetUniform(string name, Vector3 value) => OpenGL.Api.Uniform3(GetUniformLocation(name), value);
        public void SetUniform(string name, Color value) => OpenGL.Api.Uniform4(GetUniformLocation(name), value.R / 255.0f, value.G / 255.0f, value.B / 255.0f, value.A / 255.0f);
        public unsafe void SetUniform(string name, Matrix4x4 value) => OpenGL.Api.UniformMatrix4(GetUniformLocation(name), 1, false, (float*)&value);

        private int GetUniformLocation(string name)
        {
            int location = OpenGL.Api.GetUniformLocation(nativeHandle, name);

            // if (location == -1)
            // throw new Exception($"'{name}' uniform not found in shader.");

            return location;
        }
    }
}
