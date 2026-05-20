using Bebone.Core.Graphics;
using Bebone.Core.Graphics.Renderer.Agnostic;
using Bebone.Core.Graphics.Renderer.OpenGL;
using Silk.NET.GLFW;
using System.Numerics;

namespace Bebone.Core.Window
{
    public class GlfwWindow : Window
    {
        private readonly Glfw glfw;
        private readonly IntPtr windowHandle;

        private readonly string baseTitle;
        private double lastTime;
        private int frameCount;
        private double timer;

        public GlfwWindow(string title, int width, int height)
        {
            baseTitle = title;
            glfw = Glfw.GetApi();

            if (!glfw.Init())
                throw new Exception("GLFW initialization failed.");

            glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGL);
            glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
            glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
            glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
            glfw.WindowHint(WindowHintBool.Resizable, false);

            unsafe
            {
                var windowPtr = glfw.CreateWindow(width, height, title, null, null);

                if (windowPtr is null)
                {
                    glfw.Terminate();
                    throw new NotImplementedException();
                    return;
                }

                windowHandle = (IntPtr)windowPtr;

                glfw.MakeContextCurrent((WindowHandle*)windowHandle.ToPointer());
                lastTime = glfw.GetTime();
                timer = glfw.GetTime();
            }

            unsafe
            {
                glfw.SetKeyCallback((WindowHandle*)windowHandle, KeyInputCallback);
                glfw.SetCursorPosCallback((WindowHandle*)windowHandle, CursorPosInputCallback);
                glfw.SetMouseButtonCallback((WindowHandle*)windowHandle, MouseButtonCallback);
                glfw.SetScrollCallback((WindowHandle*)windowHandle, ScrollCallback);

                bool enabled = false;
                glfw.SwapInterval(enabled ? 1 : 0);
            }

            frameCount = 0;
        }

        public override void SwapBuffers()
        {
            UpdatePerformanceTitle();

            unsafe
            {
                glfw.SwapBuffers((WindowHandle*)windowHandle);
            }
        }

        private void UpdatePerformanceTitle()
        {
            double currentTime = glfw.GetTime();
            float deltaTime = (float)(currentTime - lastTime);
            lastTime = currentTime;

            frameCount++;
            timer += deltaTime;

            if (timer >= 1.0)
            {
                double msPerFrame = (timer / frameCount) * 1000.0;
                double fps = frameCount / timer;

                unsafe
                {
                    string newTitle = $"{baseTitle} - {msPerFrame:F2} ms ({fps:F0} FPS)";
                    glfw.SetWindowTitle((WindowHandle*)windowHandle, newTitle);
                }

                frameCount = 0;
                timer = 0;
            }
        }

        public override void PollEvents() => glfw.PollEvents();

        public override bool WindowShouldClose()
        {
            unsafe
            {
                return glfw.WindowShouldClose((WindowHandle*)windowHandle);
            }
        }

        public override void Destroy()
        {
            unsafe
            {
                glfw.DestroyWindow((WindowHandle*)windowHandle);
            }
        }

        public override IGraphicsApiContext CreateApiContext() => new OpenGLContext(glfw);

        public override unsafe void HideCursor() => glfw.SetInputMode((WindowHandle*)windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorDisabled);
        public override unsafe void ShowCursor() => glfw.SetInputMode((WindowHandle*)windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);

        public Vector2 GetCursorPosition()
        {
            unsafe
            {
                glfw.GetCursorPos((WindowHandle*)windowHandle, out var xpos, out var ypos);
                return new Vector2((float)xpos, (float)ypos);
            }
        }

        public override float GetTime() => (float)glfw.GetTime();

        public override int GetWidth() => (int)GetSize().X;

        public override int GetHeight() => (int)GetSize().Y;

        private Vector2 GetSize()
        {
            unsafe
            {
                glfw.GetWindowSize((WindowHandle*)windowHandle, out int width, out int height);
                return new Vector2(width, height);
            }
        }

        private unsafe void KeyInputCallback(WindowHandle* window, Keys key, int scancode, InputAction action, KeyModifiers mods) => OnInputChange((Key)key, action != InputAction.Release);
        private unsafe void CursorPosInputCallback(WindowHandle* window, double xpos, double ypos) => OnCursorPositionChange(new Vector2((float)xpos, (float)ypos));
        private unsafe void MouseButtonCallback(WindowHandle* window, Silk.NET.GLFW.MouseButton button, InputAction action, KeyModifiers mods) => OnMouseButtonChange((Graphics.MouseButton)button, action != InputAction.Release);
        private unsafe void ScrollCallback(WindowHandle* window, double xoffset, double yoffset) => OnMouseScrollChange(new Vector2((float)xoffset, (float)yoffset));
    }
}
