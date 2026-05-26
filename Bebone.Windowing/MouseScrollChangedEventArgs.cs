using System.Numerics;

namespace Bebone.Windowing;

public class MouseScrollChangedEventArgs(Vector2 offset) : EventArgs
{
    public Vector2 Offset { get; init; } = offset;
}
