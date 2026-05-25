namespace Bebone.Graphics.RenderGraph;

public readonly record struct FrameData
{
    public uint Width { get; init; }
    public uint Height { get; init; }
    public readonly float AspectRatio() => (float)Width / Height;

    public float Time { get; init; }
}