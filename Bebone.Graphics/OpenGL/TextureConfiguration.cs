namespace Bebone.Graphics.OpenGL;

public record TextureConfiguration(
    int Width,
    int Height,
    TextureMinFilter MinFilter = TextureMinFilter.Linear,
    TextureMagFilter MagFilter = TextureMagFilter.Linear,
    TextureWrapMode STextureWrap = TextureWrapMode.Repeat,
    TextureWrapMode TTextureWrap = TextureWrapMode.Repeat,
    bool CreateMipmap = true);
