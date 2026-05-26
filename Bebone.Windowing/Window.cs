namespace Bebone.Windowing;

public abstract class Window
{
    public abstract int GetWidth();
    public abstract int GetHeight();

    public abstract void HideCursor();
    public abstract void ShowCursor();

    public abstract float GetTime();

    public abstract void SwapBuffers();
    public abstract void PollEvents();
    public abstract bool WindowShouldClose();
    public abstract void Destroy();

    public abstract Func<string, IntPtr> GetProcAddressLoader();

    public event EventHandler<CursorPositionChangedEventArgs>? CursorPositionChanged;
    public event EventHandler<InputChangedEventArgs>? InputChanged;
    public event EventHandler<MouseButtonStatusChangedEventArgs>? MouseButtonStatusChanged;
    public event EventHandler<MouseScrollChangedEventArgs>? MouseScrollChanged;

    protected virtual void OnCursorPositionChanged(CursorPositionChangedEventArgs args) => CursorPositionChanged?.Invoke(this, args);
    protected virtual void OnInputChanged(InputChangedEventArgs args) => InputChanged?.Invoke(this, args);
    protected virtual void OnMouseButtonStatusChanged(MouseButtonStatusChangedEventArgs args) => MouseButtonStatusChanged?.Invoke(this, args);
    protected virtual void OnMouseScrollChanged(MouseScrollChangedEventArgs args) => MouseScrollChanged?.Invoke(this, args);
}