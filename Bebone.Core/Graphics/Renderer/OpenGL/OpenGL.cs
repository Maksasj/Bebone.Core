using Silk.NET.OpenGL;

namespace Bebone.Core.Graphics.Renderer.OpenGL
{
    public static class OpenGL
    {
        private static GL? api;

        public static void Initialize(GL gl) => api = gl;

        public static GL Api
        {
            get
            {
                if (api == null)
                    throw new InvalidOperationException("OpenGL API not initialized. Call OpenGL.Initialize(GL gl) before using any OpenGL functionality.");

                return api;
            }
        }
    }
}
