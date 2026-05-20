using Bebone.Core.Windowing;
using System.Numerics;

namespace Bebone.Core.Graphics.Renderer.Agnostic
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

        public Action<Vector2> OnCursorPositionChange = (_) => { };
        public Action<Key, bool> OnInputChange = (_, _) => { };
        public Action<MouseButton, bool> OnMouseButtonChange = (_, _) => { };
        public Action<Vector2> OnMouseScrollChange = (_) => { };
    }
}
