namespace Bebone.Windowing;

public class WindowSizeChangedEventArgs(int width, int height) : EventArgs
{
    public int Width { get; init; } = width;
    public int Height { get; init; } = height;
}
