using Silk.NET.OpenGL;
using System.Numerics;

namespace Bebone.Graphics.Abstractions
{
    public interface IGLContext
    {
        void ActiveTexture(TextureUnit texture0);
        void AttachShader(uint handle, uint _);
        void BindBuffer(BufferTargetARB arrayBuffer, uint handle);
        void BindTexture(TextureTarget texture2D, uint created);
        void BindVertexArray(uint handle);
        void CompileShader(uint handle);
        uint CreateProgram();
        uint CreateShader(ShaderType shaderType);
        void DeleteBuffer(uint handle);
        void DeleteProgram(uint handle);
        void DeleteShader(uint handle);
        void DeleteVertexArray(uint handle);
        void EnableVertexAttribArray(uint index);
        uint GenBuffer();
        void GenerateMipmap(TextureTarget texture2D);
        uint GenTexture();
        uint GenVertexArray();
        string? GetShaderInfoLog(uint handle);
        void LinkProgram(uint handle);
        void ShaderSource(uint handle, string shaderSource);
        void Uniform1(int v, int value);
        void Uniform1(int v, float value);
        void Uniform2(int v, Vector2 value);
        void Uniform3(int v, Vector3 value);
        void Uniform4(int location, Vector4 x);
        void UseProgram(uint handle);
        unsafe void VertexAttribPointer(uint index, int componentCount, VertexAttribPointerType type, bool v, uint strideSize, void* offset);

        void GetProgram(uint program, GLEnum link, out int status);
        string GetProgramInfoLog(uint program);
        void DetachShader(uint target, uint shader);
        unsafe void UniformMatrix4(int location, int count, bool transpose, float* value);
        int GetUniformLocation(uint handle, string name);
        void TextureParameter(uint target, TextureParameterName parameter, int value);
        unsafe void TexImage2D(TextureTarget a, int v, InternalFormat b, uint a1, uint v1, int v2, PixelFormat b1, PixelType a2, byte* b2);

        unsafe void BufferData(BufferTargetARB target, nuint size, void* pointer, BufferUsageARB usage);
        unsafe void BufferSubData(BufferTargetARB target, int v, nuint size, void* pointer);

        void DrawElements(PrimitiveType type, uint count, DrawElementsType elementsType, int? _);
        void DrawArrays(GLEnum type, uint offset, uint count);
        void DrawArrays(PrimitiveType type, uint offset, uint count);
    }
}
