using Silk.NET.OpenGL;
using System.Numerics;

namespace Bebone.Graphics.Abstractions
{
    public interface IGLContext
    {
        // Shader
        uint CreateShader(ShaderType shaderType);
        void ShaderSource(uint handle, string shaderSource);
        void CompileShader(uint handle);
        string? GetShaderInfoLog(uint handle);
        void AttachShader(uint handle, uint _);
        void DetachShader(uint target, uint shader);
        void DeleteShader(uint handle);

        // Program
        uint CreateProgram();
        void LinkProgram(uint handle);
        void GetProgram(uint program, GLEnum link, out int status);
        string GetProgramInfoLog(uint program);
        void UseProgram(uint handle);
        void DeleteProgram(uint handle);

        // Buffer
        uint GenBuffer();
        void BindBuffer(BufferTargetARB arrayBuffer, uint handle);
        unsafe void BufferData(BufferTargetARB target, nuint size, void* pointer, BufferUsageARB usage);
        unsafe void BufferSubData(BufferTargetARB target, int v, nuint size, void* pointer);
        void DeleteBuffer(uint handle);

        // Vertex array
        uint GenVertexArray();
        void BindVertexArray(uint handle);
        void EnableVertexAttribArray(uint index);
        unsafe void VertexAttribPointer(uint index, int componentCount, VertexAttribPointerType type, bool v, uint strideSize, void* offset);
        void DeleteVertexArray(uint handle);

        // Texture
        uint GenTexture();
        void BindTexture(TextureTarget texture2D, uint created);
        void ActiveTexture(TextureUnit texture0);
        unsafe void TexImage2D(TextureTarget a, int v, InternalFormat b, uint a1, uint v1, int v2, PixelFormat b1, PixelType a2, byte* b2);
        void TextureParameter(uint target, TextureParameterName parameter, int value);
        void GenerateMipmap(TextureTarget texture2D);

        // Uniforms
        int GetUniformLocation(uint handle, string name);
        void Uniform1(int v, int value);
        void Uniform1(int v, float value);
        void Uniform2(int v, Vector2 value);
        void Uniform3(int v, Vector3 value);
        void Uniform4(int location, Vector4 x);
        unsafe void UniformMatrix4(int location, int count, bool transpose, float* value);

        // Draw
        void DrawArrays(GLEnum type, uint offset, uint count);
        void DrawArrays(PrimitiveType type, uint offset, uint count);
        void DrawElements(PrimitiveType type, uint count, DrawElementsType elementsType, int? _);
    }
}
