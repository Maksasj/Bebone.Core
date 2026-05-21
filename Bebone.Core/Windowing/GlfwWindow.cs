using Bebone.Core.Graphics.Renderer.Agnostic;
using Bebone.Core.Graphics.Renderer.OpenGL;
using Silk.NET.GLFW;
using System.Numerics;

namespace Bebone.Core.Windowing
{
    public class GlfwWindow : Window
    {
        private readonly Glfw _glfw;
        private readonly IntPtr _windowHandle;

        public GlfwWindow(string title, int width, int height)
        {
            _baseTitle = title;
            _glfw = Glfw.GetApi();

            if (!_glfw.Init())
                throw new Exception("GLFW initialization failed.");

            _glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGL);
            _glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
            _glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
            _glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
            _glfw.WindowHint(WindowHintBool.Resizable, false);

            unsafe
            {
                var windowPtr = _glfw.CreateWindow(width, height, title, null, null);

                if (windowPtr is null)
                {
                    _glfw.Terminate();
                    throw new InvalidOperationException("Failed to create GLFW window.");
                }

                _windowHandle = (IntPtr)windowPtr;

                _glfw.MakeContextCurrent((WindowHandle*)_windowHandle.ToPointer());
            }

            unsafe
            {
                _glfw.SetKeyCallback((WindowHandle*)_windowHandle, KeyInputCallback);
                _glfw.SetCursorPosCallback((WindowHandle*)_windowHandle, CursorPosInputCallback);
                _glfw.SetMouseButtonCallback((WindowHandle*)_windowHandle, MouseButtonCallback);
                _glfw.SetScrollCallback((WindowHandle*)_windowHandle, ScrollCallback);

                _glfw.SwapInterval(0);
            }
        }

        public override void SwapBuffers()
        {
            unsafe
            {
                _glfw.SwapBuffers((WindowHandle*)_windowHandle);
            }
        }

        public override void PollEvents() => _glfw.PollEvents();

        public override bool WindowShouldClose()
        {
            unsafe
            {
                return _glfw.WindowShouldClose((WindowHandle*)_windowHandle);
            }
        }

        public override void Destroy()
        {
            unsafe
            {
                _glfw.DestroyWindow((WindowHandle*)_windowHandle);
            }
        }

        public override IGraphicsApiContext CreateApiContext() => new OpenGLContext(_glfw);

        public override unsafe void HideCursor() => _glfw.SetInputMode((WindowHandle*)_windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorDisabled);
        public override unsafe void ShowCursor() => _glfw.SetInputMode((WindowHandle*)_windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);

        public Vector2 GetCursorPosition()
        {
            unsafe
            {
                _glfw.GetCursorPos((WindowHandle*)_windowHandle, out var xpos, out var ypos);
                return new Vector2((float)xpos, (float)ypos);
            }
        }

        public override float GetTime() => (float)_glfw.GetTime();

        public override int GetWidth() => (int)GetSize().X;

        public override int GetHeight() => (int)GetSize().Y;

        private Vector2 GetSize()
        {
            unsafe
            {
                _glfw.GetWindowSize((WindowHandle*)_windowHandle, out int width, out int height);
                return new Vector2(width, height);
            }
        }

        private unsafe void KeyInputCallback(WindowHandle* window, Keys key, int scancode, InputAction action, KeyModifiers mods) => OnInputChange((Key)key, action != InputAction.Release);
        private unsafe void CursorPosInputCallback(WindowHandle* window, double xpos, double ypos) => OnCursorPositionChange(new Vector2((float)xpos, (float)ypos));
        private unsafe void MouseButtonCallback(WindowHandle* window, Silk.NET.GLFW.MouseButton button, InputAction action, KeyModifiers mods) => OnMouseButtonChange((MouseButton)button, action != InputAction.Release);
        private unsafe void ScrollCallback(WindowHandle* window, double xoffset, double yoffset) => OnMouseScrollChange(new Vector2((float)xoffset, (float)yoffset));
    }
}
