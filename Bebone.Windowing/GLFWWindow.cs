using Silk.NET.GLFW;
using System.Numerics;

namespace Bebone.Windowing;

public sealed class GLFWWindow : Window, IDisposable
{
    private readonly Glfw _glfw;
    private readonly unsafe WindowHandle* _windowHandle;

    private bool _disposed;

    public GLFWWindow(string title, int width, int height)
    {
        _glfw = Glfw.GetApi();

        if (!_glfw.Init())
            throw new InvalidOperationException("GLFW initialization failed.");

        _glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGL);
        _glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
        _glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
        _glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
        _glfw.WindowHint(WindowHintBool.Resizable, false);

        unsafe
        {
            _windowHandle = _glfw.CreateWindow(width, height, title, null, null);

            if (_windowHandle is null)
            {
                _glfw.Terminate();
                throw new InvalidOperationException("Failed to create GLFW window.");
            }

            _glfw.MakeContextCurrent(_windowHandle);
        }

        unsafe
        {
            _glfw.SetKeyCallback(_windowHandle, KeyCallback);
            _glfw.SetCursorPosCallback(_windowHandle, CursorPositionCallback);
            _glfw.SetMouseButtonCallback(_windowHandle, MouseButtonCallback);
            _glfw.SetScrollCallback(_windowHandle, ScrollCallback);

            _glfw.SwapInterval(0);
        }
    }

    public unsafe override void SwapBuffers()
    {
        ThrowIfDisposed();

        _glfw.SwapBuffers(_windowHandle);
    }

    public override void PollEvents()
    {
        ThrowIfDisposed();

        _glfw.PollEvents();
    }

    public unsafe override bool WindowShouldClose()
    {
        ThrowIfDisposed();

        return _glfw.WindowShouldClose(_windowHandle);
    }

    public override unsafe void HideCursor()
    {
        ThrowIfDisposed();

        _glfw.SetInputMode(_windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorDisabled);
    }

    public override unsafe void ShowCursor()
    {
        ThrowIfDisposed();

        _glfw.SetInputMode(_windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);
    }

    public unsafe Vector2 GetCursorPosition()
    {
        ThrowIfDisposed();

        _glfw.GetCursorPos(_windowHandle, out var xPos, out var yPos);
        return new Vector2((float)xPos, (float)yPos);
    }

    public override Func<string, IntPtr> GetProcAddressLoader()
    {
        ThrowIfDisposed();

        return _glfw.GetProcAddress;
    }

    public override float GetTime()
    {
        ThrowIfDisposed();

        return (float)_glfw.GetTime();
    }

    public override int GetWidth()
    {
        return (int)GetSize().X;
    }

    public override int GetHeight()
    {
        return (int)GetSize().Y;
    }

    private unsafe Vector2 GetSize()
    {
        ThrowIfDisposed();

        _glfw.GetWindowSize(_windowHandle, out var width, out var height);
        return new Vector2(width, height);
    }

    private unsafe void KeyCallback(WindowHandle* window, Keys key, int scancode, InputAction action, KeyModifiers mods)
    {
        if (_disposed)
            return;

        var args = new InputChangedEventArgs((Key)key, action != InputAction.Release);
        OnInputChanged(args);
    }

    private unsafe void CursorPositionCallback(WindowHandle* window, double xPosition, double yPosition)
    {
        if (_disposed)
            return;

        var cursorPosition = new Vector2((float)xPosition, (float)yPosition);
        var args = new CursorPositionChangedEventArgs(cursorPosition);
        OnCursorPositionChanged(args);
    }

    private unsafe void MouseButtonCallback(WindowHandle* window, Silk.NET.GLFW.MouseButton button, InputAction action, KeyModifiers mods)
    {
        if (_disposed)
            return;

        var args = new MouseButtonStatusChangedEventArgs((MouseButton)button, action != InputAction.Release);
        OnMouseButtonStatusChanged(args);
    }

    private unsafe void ScrollCallback(WindowHandle* window, double xOffset, double yOffset)
    {
        if (_disposed)
            return;

        var offset = new Vector2((float)xOffset, (float)yOffset);
        var args = new MouseScrollChangedEventArgs(offset);
        OnMouseScrollChanged(args);
    }

    public unsafe override void Dispose()
    {
        if (_disposed)
            return;

        _glfw.DestroyWindow(_windowHandle);
        _glfw.Terminate();

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(GLFWWindow));
    }
}