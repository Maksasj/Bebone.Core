using Bebone.Graphics.Abstractions;
using Silk.NET.OpenGL;
using System.Numerics;

namespace Bebone.Graphics.OpenGL;

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

    public void DetachShader(uint program, uint shader)
    {
        _gl.DetachShader(program, shader);
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

    public void GetProgram(uint program, GLEnum pname, out int @params)
    {
        _gl.GetProgram(program, pname, out @params);
    }

    public string GetProgramInfoLog(uint program)
    {
        return _gl.GetProgramInfoLog(program);
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

    public unsafe void BufferData(BufferTargetARB target, nuint size, void* data, BufferUsageARB usage)
    {
        _gl.BufferData(target, size, data, usage);
    }

    public unsafe void BufferSubData(BufferTargetARB target, nint offset, nuint size, void* data)
    {
        _gl.BufferSubData(target, offset, size, data);
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

    public unsafe void TexImage2D(TextureTarget target, int level, InternalFormat internalFormat, uint width, uint height, int border, PixelFormat format, PixelType type, byte* pixels)
    {
        _gl.TexImage2D(target, level, internalFormat, width, height, border, format, type, pixels);
    }

    public void TextureParameter(uint texture, TextureParameterName pname, int param)
    {
        _gl.TextureParameter(texture, pname, param);
    }

    public void GenerateMipmap(TextureTarget target)
    {
        _gl.GenerateMipmap(target);
    }

    // Uniforms

    public unsafe int GetUniformLocation(uint program, string name)
    {
        return _gl.GetUniformLocation(program, name);
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

    public unsafe void UniformMatrix4(int location, uint count, bool transpose, float* value)
    {
        _gl.UniformMatrix4(location, count, transpose, value);
    }

    // Draw

    public void DrawArrays(PrimitiveType mode, int first, uint count)
    {
        _gl.DrawArrays(mode, first, count);
    }

    public unsafe void DrawElements(PrimitiveType mode, uint count, DrawElementsType type, void* indices)
    {
        _gl.DrawElements(mode, count, type, indices);
    }
}
