using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;
using System.Numerics;

namespace Bebone.Graphics.OpenGL
{
    public class GLContext(GL gl) : IGLContext
    {
        private readonly GL _gl = gl;

        // Shader

        public uint CreateShader(ShaderType type)
        {
            return _gl.CreateShader(type);
        }

        public void ShaderSource(uint shader, string @string)
        {
            _gl.ShaderSource(shader, @string);
        }

        public void CompileShader(uint shader)
        {
            _gl.CompileShader(shader);
        }

        public string GetShaderInfoLog(uint shader)
        {
            return _gl.GetShaderInfoLog(shader);
        }

        public void AttachShader(uint program, uint shader)
        {
            _gl.AttachShader(program, shader);
        }

        public void DetachShader(uint target, uint shader)
        {
            throw new NotImplementedException();
        }

        public void DeleteShader(uint shader)
        {
            _gl.DeleteShader(shader);
        }

        // Program

        public uint CreateProgram()
        {
            return _gl.CreateProgram();
        }

        public void LinkProgram(uint program)
        {
            _gl.LinkProgram(program);
        }

        public void GetProgram(uint program, GLEnum link, out int status)
        {
            throw new NotImplementedException();
        }

        public string GetProgramInfoLog(uint program)
        {
            throw new NotImplementedException();
        }

        public void UseProgram(uint program)
        {
            _gl.UseProgram(program);
        }

        public void DeleteProgram(uint program)
        {
            _gl.DeleteProgram(program);
        }

        // Buffer

        public uint GenBuffer()
        {
            return _gl.GenBuffer();
        }

        public void BindBuffer(BufferTargetARB target, uint buffer)
        {
            _gl.BindBuffer(target, buffer);
        }

        public unsafe void BufferData(BufferTargetARB target, nuint size, void* pointer, BufferUsageARB usage)
        {
            throw new NotImplementedException();
        }

        public unsafe void BufferSubData(BufferTargetARB target, int v, nuint size, void* pointer)
        {
            throw new NotImplementedException();
        }

        public void DeleteBuffer(uint buffer)
        {
            _gl.DeleteBuffer(buffer);
        }

        // Vertex array

        public uint GenVertexArray()
        {
            return _gl.GenVertexArray();
        }

        public void BindVertexArray(uint array)
        {
            _gl.BindVertexArray(array);
        }

        public void EnableVertexAttribArray(uint index)
        {
            _gl.EnableVertexAttribArray(index);
        }

        public unsafe void VertexAttribPointer(uint index, int size, VertexAttribPointerType type, bool normalized, uint stride, void* pointer)
        {
            _gl.VertexAttribPointer(index, size, type, normalized, stride, pointer);
        }

        public void DeleteVertexArray(uint arrays)
        {
            _gl.DeleteVertexArray(arrays);
        }

        // Texture

        public uint GenTexture()
        {
            return _gl.GenTexture();
        }

        public void BindTexture(TextureTarget target, uint texture)
        {
            _gl.BindTexture(target, texture);
        }

        public void ActiveTexture(TextureUnit texture)
        {
            _gl.ActiveTexture(texture);
        }

        public unsafe void TexImage2D(TextureTarget a, int v, InternalFormat b, uint a1, uint v1, int v2, PixelFormat b1, PixelType a2, byte* b2)
        {
            throw new NotImplementedException();
        }

        public void TextureParameter(uint target, TextureParameterName parameter, int value)
        {
            throw new NotImplementedException();
        }

        public void GenerateMipmap(TextureTarget target)
        {
            _gl.GenerateMipmap(target);
        }

        // Uniforms

        public int GetUniformLocation(uint handle, string name)
        {
            throw new NotImplementedException();
        }

        public void Uniform1(int location, int x)
        {
            _gl.Uniform1(location, x);
        }

        public void Uniform1(int location, float x)
        {
            _gl.Uniform1(location, x);
        }

        public void Uniform2(int location, Vector2 x)
        {
            _gl.Uniform2(location, x);
        }

        public void Uniform3(int location, Vector3 x)
        {
            _gl.Uniform3(location, x);
        }

        public void Uniform4(int location, Vector4 x)
        {
            _gl.Uniform4(location, x);
        }

        public unsafe void UniformMatrix4(int location, int count, bool transpose, float* value)
        {
            throw new NotImplementedException();
        }

        // Draw

        public void DrawArrays(GLEnum type, uint offset, uint count)
        {
            throw new NotImplementedException();
        }

        public void DrawArrays(PrimitiveType type, uint offset, uint count)
        {
            throw new NotImplementedException();
        }

        public void DrawElements(PrimitiveType type, uint count, DrawElementsType elementsType, int? _)
        {
            throw new NotImplementedException();
        }
    }
}
