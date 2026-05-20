using Bebone.Core.Graphics.Renderer.Agnostic;
using Silk.NET.OpenGL;
using StbImageSharp;
using System.Drawing;

namespace Bebone.Core.Graphics.Renderer.OpenGL
{
    public class Texture2D : ITexture, IColorAttachment
    {
        private readonly uint texture;

        private readonly int width;
        private readonly int height;

        public unsafe Texture2D(int width, int height)
        {
            this.width = width;
            this.height = height;

            texture = CreateTexture(isFboAttachment: true);
            ActivateBind(ColorAttachmentSlot.ColorAttachemnt0);

            OpenGL.Api.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width, (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);

            Unbind();
        }

        public unsafe Texture2D(int width, int height, byte[] data)
        {
            this.width = width;
            this.height = height;

            texture = CreateTexture(false, TextureMinFilterType.Linear, TextureMagFilterType.Linear);
            ActivateBind(ColorAttachmentSlot.ColorAttachemnt0);

            fixed (byte* ptr = data)
            {
                OpenGL.Api.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width,
                    (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
            }

            OpenGL.Api.GenerateMipmap(TextureTarget.Texture2D);

            Unbind();
        }

        public unsafe Texture2D(int width, int height, Color color)
        {
            this.width = width;
            this.height = height;

            texture = CreateTexture();
            ActivateBind(ColorAttachmentSlot.ColorAttachemnt0);

            byte[] data = new byte[width * height * 4];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = (y * width + x) * 4;
                    data[index + 0] = color.R;
                    data[index + 1] = color.G;
                    data[index + 2] = color.B;
                    data[index + 3] = color.A;
                }
            }


            fixed (byte* ptr = data)
            {
                OpenGL.Api.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width,
                    (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
            }

            OpenGL.Api.GenerateMipmap(TextureTarget.Texture2D);

            Unbind();
        }

        public unsafe Texture2D(string filePath, TextureMinFilterType minFilter = TextureMinFilterType.Linear, TextureMagFilterType magFilter = TextureMagFilterType.Linear)
        {
            ImageResult result = ImageResult.FromMemory(File.ReadAllBytes(filePath), ColorComponents.RedGreenBlueAlpha);

            width = result.Width;
            height = result.Height;

            texture = CreateTexture(false, minFilter, magFilter);
            ActivateBind(ColorAttachmentSlot.ColorAttachemnt0);

            fixed (byte* ptr = result.Data)
            {
                OpenGL.Api.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width,
                    (uint)result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
            }

            OpenGL.Api.GenerateMipmap(TextureTarget.Texture2D);

            Unbind();
        }

        public void ActivateBind(ColorAttachmentSlot slot)
        {
            OpenGL.Api.ActiveTexture(TextureUnit.Texture0 + (int)slot);
            OpenGL.Api.BindTexture(TextureTarget.Texture2D, texture);
        }

        public void Unbind() => OpenGL.Api.BindTexture(TextureTarget.Texture2D, 0);

        public int GetWidth() => width;
        public int GetHeight() => height;
        public int GetDepth() => 0;

        public uint GetHandle() => texture;

        private static uint CreateTexture(bool isFboAttachment = false, TextureMinFilterType minFilter = TextureMinFilterType.Linear, TextureMagFilterType magFilter = TextureMagFilterType.Linear)
        {
            var created = OpenGL.Api.GenTexture();

            OpenGL.Api.ActiveTexture(TextureUnit.Texture0);
            OpenGL.Api.BindTexture(TextureTarget.Texture2D, created);

            OpenGL.Api.TextureParameter(created, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            OpenGL.Api.TextureParameter(created, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            if (isFboAttachment)
            {
                OpenGL.Api.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                OpenGL.Api.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
            else
            {
                OpenGL.Api.TextureParameter(created, TextureParameterName.TextureMinFilter, (int)minFilter);
                OpenGL.Api.TextureParameter(created, TextureParameterName.TextureMagFilter, (int)magFilter);
            }

            OpenGL.Api.BindTexture(TextureTarget.Texture2D, 0);

            return created;
        }
    }
}
