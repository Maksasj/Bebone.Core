using System.Numerics;

namespace Bebone.Windowing;

public class CursorPositionChangedEventArgs(Vector2 cursorPosition) : EventArgs
{
    public Vector2 CursorPosition { get; init; } = cursorPosition;
}
