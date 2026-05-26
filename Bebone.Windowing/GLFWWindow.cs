using Silk.NET.GLFW;
using System.Numerics;

namespace Bebone.Windowing;

public sealed class GlfwWindow : IWindow
{
    private readonly Glfw _glfw;
    private readonly unsafe WindowHandle* _windowHandle;

    private bool _disposed;

    private int _currentWidth;
    private int _currentHeight;
    private Vector2 _currentCursorPosition;

    public event EventHandler<CursorPositionChangedEventArgs>? CursorPositionChanged;
    public event EventHandler<InputChangedEventArgs>? InputChanged;
    public event EventHandler<MouseButtonStatusChangedEventArgs>? MouseButtonStatusChanged;
    public event EventHandler<MouseScrollChangedEventArgs>? MouseScrollChanged;
    public event EventHandler<WindowSizeChangedEventArgs>? WindowSizeChanged;

    public int Width
    {
        get
        {
            ThrowIfDisposed();
            return _currentWidth;
        }
    }

    public int Height
    {
        get
        {
            ThrowIfDisposed();
            return _currentHeight;
        }
    }

    public Vector2 CursorPosition
    {
        get
        {
            ThrowIfDisposed();
            return _currentCursorPosition;
        }
    }

    public GlfwWindow(string title, int width, int height, bool isResizable)
    {
        _glfw = Glfw.GetApi();

        if (!_glfw.Init())
            throw new InvalidOperationException("GLFW initialization failed.");

        _glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGL);
        _glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
        _glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
        _glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
        _glfw.WindowHint(WindowHintBool.Resizable, isResizable);

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
            _glfw.SetWindowSizeCallback(_windowHandle, WindowSizeCallback);

            _glfw.SwapInterval(0);
        }

        _currentWidth = width;
        _currentHeight = height;

        unsafe
        {
            _glfw.GetCursorPos(_windowHandle, out var x, out var y);
            _currentCursorPosition = new Vector2((float)x, (float)y);
        }
    }

    public unsafe void SwapBuffers()
    {
        ThrowIfDisposed();

        _glfw.SwapBuffers(_windowHandle);
    }

    public void PollEvents()
    {
        ThrowIfDisposed();

        _glfw.PollEvents();
    }

    public unsafe bool WindowShouldClose()
    {
        ThrowIfDisposed();

        return _glfw.WindowShouldClose(_windowHandle);
    }

    public unsafe void HideCursor()
    {
        ThrowIfDisposed();

        _glfw.SetInputMode(_windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorDisabled);
    }

    public unsafe void ShowCursor()
    {
        ThrowIfDisposed();

        _glfw.SetInputMode(_windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);
    }

    public Func<string, IntPtr> GetProcAddressLoader()
    {
        ThrowIfDisposed();

        return _glfw.GetProcAddress;
    }

    public float GetTime()
    {
        ThrowIfDisposed();

        return (float)_glfw.GetTime();
    }

    private unsafe void KeyCallback(WindowHandle* window, Keys key, int scancode, InputAction action, KeyModifiers mods)
    {
        if (_disposed)
            return;

        var args = new InputChangedEventArgs((Key)key, action != InputAction.Release);
        InputChanged?.Invoke(this, args);
    }

    private unsafe void CursorPositionCallback(WindowHandle* window, double xPosition, double yPosition)
    {
        if (_disposed)
            return;

        var cursorPosition = new Vector2((float)xPosition, (float)yPosition);
        _currentCursorPosition = cursorPosition;

        var args = new CursorPositionChangedEventArgs(cursorPosition);
        CursorPositionChanged?.Invoke(this, args);
    }

    private unsafe void MouseButtonCallback(WindowHandle* window, Silk.NET.GLFW.MouseButton button, InputAction action, KeyModifiers mods)
    {
        if (_disposed)
            return;

        var args = new MouseButtonStatusChangedEventArgs((MouseButton)button, action != InputAction.Release);
        MouseButtonStatusChanged?.Invoke(this, args);
    }

    private unsafe void ScrollCallback(WindowHandle* window, double xOffset, double yOffset)
    {
        if (_disposed)
            return;

        var offset = new Vector2((float)xOffset, (float)yOffset);
        var args = new MouseScrollChangedEventArgs(offset);
        MouseScrollChanged?.Invoke(this, args);
    }

    private unsafe void WindowSizeCallback(WindowHandle* window, int width, int height)
    {
        if (_disposed)
            return;

        _currentWidth = width;
        _currentHeight = height;

        var args = new WindowSizeChangedEventArgs(width, height);
        WindowSizeChanged?.Invoke(this, args);
    }

    public unsafe void Dispose()
    {
        if (_disposed)
            return;

        _glfw.SetKeyCallback(_windowHandle, null);
        _glfw.SetCursorPosCallback(_windowHandle, null);
        _glfw.SetMouseButtonCallback(_windowHandle, null);
        _glfw.SetScrollCallback(_windowHandle, null);
        _glfw.SetWindowSizeCallback(_windowHandle, null);

        _glfw.DestroyWindow(_windowHandle);
        _glfw.Terminate();

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(GlfwWindow));
    }
}