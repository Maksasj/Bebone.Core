using System.Numerics;

namespace Bebone.Windowing;

public interface IWindow : IDisposable
{
    int Width { get; }
    int Height { get; }
    Vector2 CursorPosition { get; }

    event EventHandler<CursorPositionChangedEventArgs>? CursorPositionChanged;
    event EventHandler<InputChangedEventArgs>? InputChanged;
    event EventHandler<MouseButtonStatusChangedEventArgs>? MouseButtonStatusChanged;
    event EventHandler<MouseScrollChangedEventArgs>? MouseScrollChanged;
    event EventHandler<WindowSizeChangedEventArgs>? WindowSizeChanged;

    void HideCursor();
    void ShowCursor();
    void SetCursorPosition(Vector2 position);

    float GetTime();

    void SwapBuffers();
    void PollEvents();
    bool WindowShouldClose();

    Func<string, IntPtr> GetProcAddressLoader();
}
