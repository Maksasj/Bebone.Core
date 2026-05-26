namespace Bebone.Windowing;

public class InputChangedEventArgs(Key key, bool isPressed) : EventArgs
{
    public Key Key { get; init; } = key;
    public bool IsPressed { get; init; } = isPressed;
}