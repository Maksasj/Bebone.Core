using Silk.NET.OpenGL;
using System.Numerics;

namespace Bebone.Graphics.Abstractions
{
    public interface IGLContext
    {
        // Shader
        uint CreateShader(ShaderType type);
        void ShaderSource(uint shader, string @string);
        void CompileShader(uint shader);
        string GetShaderInfoLog(uint shader);
        void AttachShader(uint program, uint shader);
        void DetachShader(uint program, uint shader);
        void DeleteShader(uint shader);

        // Program
        uint CreateProgram();
        void LinkProgram(uint program);
        unsafe void GetProgram(uint program, GLEnum pname, out int @params);
        string GetProgramInfoLog(uint program);
        void UseProgram(uint program);
        void DeleteProgram(uint program);

        // Buffer
        uint GenBuffer();
        void BindBuffer(BufferTargetARB target, uint buffer);
        unsafe void BufferData(BufferTargetARB target, nuint size, void* data, BufferUsageARB usage);
        unsafe void BufferSubData(BufferTargetARB target, nint offset, nuint size, void* data);
        void DeleteBuffer(uint buffer);

        // Vertex array
        uint GenVertexArray();
        void BindVertexArray(uint array);
        void EnableVertexAttribArray(uint index);
        unsafe void VertexAttribPointer(uint index, int size, VertexAttribPointerType type, bool normalized, uint stride, void* pointer);
        void DeleteVertexArray(uint arrays);

        // Texture
        uint GenTexture();
        void BindTexture(TextureTarget target, uint texture);
        void ActiveTexture(TextureUnit texture);
        unsafe void TexImage2D(TextureTarget target, int level, InternalFormat internalFormat, uint width, uint height, int border, PixelFormat format, PixelType type, byte* pixels);
        void TextureParameter(uint texture, TextureParameterName pname, int param);
        void GenerateMipmap(TextureTarget target);

        // Uniforms
        unsafe int GetUniformLocation(uint program, string name);
        void Uniform1(int location, int x);
        void Uniform1(int location, float x);
        void Uniform2(int location, Vector2 x);
        void Uniform3(int location, Vector3 x);
        void Uniform4(int location, Vector4 x);
        unsafe void UniformMatrix4(int location, uint count, bool transpose, float* value);

        // Draw
        void DrawArrays(PrimitiveType mode, int first, uint count);
        unsafe void DrawElements(PrimitiveType mode, uint count, DrawElementsType type, void* indices);
    }
}
