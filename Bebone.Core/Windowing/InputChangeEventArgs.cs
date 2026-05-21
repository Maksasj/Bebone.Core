namespace Bebone.Core.Windowing
{
    public class InputChangeEventArgs(Key key, bool isPressed) : EventArgs
    {
        public Key Key { get; } = key;
        public bool IsPressed { get; } = isPressed;
    }
}
