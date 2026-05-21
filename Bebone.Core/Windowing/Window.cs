using Bebone.Core.Graphics.Renderer.Agnostic;
using System.Numerics;

namespace Bebone.Core.Windowing
{
    public abstract class Window
    {
        public abstract IGraphicsApiContext CreateApiContext();

        public abstract int GetWidth();
        public abstract int GetHeight();

        public abstract void HideCursor();
        public abstract void ShowCursor();

        public abstract float GetTime();

        public abstract void SwapBuffers();
        public abstract void PollEvents();
        public abstract bool WindowShouldClose();
        public abstract void Destroy();

        public event EventHandler<Vector2> OnCursorPositionChange = delegate { };
        public event EventHandler<Vector2> OnInputChange = delegate { };
        public event EventHandler<Vector2> OnMouseButtonChange = delegate { };
        public event EventHandler<Vector2> OnMouseScrollChange = delegate { };
    }
}
