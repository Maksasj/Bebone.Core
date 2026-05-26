using Silk.NET.GLFW;

namespace Bebone.Windowing;

public class MouseButtonStatusChangedEventArgs(MouseButton mouseButton, bool isPressed) : EventArgs
{
    public MouseButton MouseButton { get; init; } = mouseButton;
    public bool IsPressed { get; init; } = isPressed;
}
