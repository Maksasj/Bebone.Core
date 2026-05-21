namespace Bebone.Core.Windowing
{
    public class MouseButtonChangeEventArgs(MouseButton button, bool isPressed) : EventArgs
    {
        public MouseButton Button { get; } = button;
        public bool IsPressed { get; } = isPressed;
    }
}
